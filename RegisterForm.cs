#nullable disable
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GarbageCollectionMonitoringSystem
{
    public partial class RegisterForm : Form
    {
        private string selectedRole = "Resident";

        public bool Registered { get; private set; }

        public RegisterForm()
        {
            InitializeComponent();
            AcceptButton = btnCreate;

            lnkBack.LinkClicked += delegate { DialogResult = DialogResult.Cancel; Close(); };
            btnCancel.Click += delegate { DialogResult = DialogResult.Cancel; Close(); };
            btnCreate.Click += btnCreate_Click;

            HookRoleCard(pnlRoleResident, rdoResident, lblRoleResidentDesc, "Resident");
            HookRoleCard(pnlRoleOfficial, rdoOfficial, lblRoleOfficialDesc, "Municipal Official");
            HookRoleCard(pnlRoleDriver, rdoDriver, lblRoleDriverDesc, "Collection Driver");
            SetRole("Resident");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GenerateUserId();
        }

        private void GenerateUserId()
        {
            try
            {
                object o = DatabaseConnection.ExecuteScalar("SELECT IFNULL(MAX(User_id), 0) + 1 FROM `User`");
                int next = Convert.ToInt32(o);
                txtUserId.Text = "USR-" + DateTime.Now.Year + "-" + next.ToString("D5");
            }
            catch
            {
                txtUserId.Text = "USR-" + DateTime.Now.Year + "-00001";
            }
        }

        private void HookRoleCard(RoundedPanel card, RadioButton radio, Label desc, string role)
        {
            EventHandler pick = delegate { SetRole(role); };
            card.Click += pick;
            desc.Click += pick;
            radio.Click += pick;
        }

        private void SetRole(string role)
        {
            selectedRole = role;
            rdoResident.Checked = role == "Resident";
            rdoOfficial.Checked = role == "Municipal Official";
            rdoDriver.Checked = role == "Collection Driver";
            ApplyRoleStyle(pnlRoleResident, rdoResident, lblRoleResidentDesc, rdoResident.Checked);
            ApplyRoleStyle(pnlRoleOfficial, rdoOfficial, lblRoleOfficialDesc, rdoOfficial.Checked);
            ApplyRoleStyle(pnlRoleDriver, rdoDriver, lblRoleDriverDesc, rdoDriver.Checked);

            lblEyebrow.Text = role == "Resident" ? "NEW RESIDENT PROFILE"
                            : role == "Municipal Official" ? "NEW OFFICIAL PROFILE" : "NEW COLLECTOR PROFILE";
        }

        private static void ApplyRoleStyle(RoundedPanel card, RadioButton radio, Label desc, bool selected)
        {
            Color back = selected ? ColorTranslator.FromHtml("#EDF9F3") : Color.White;
            card.BackColor = back;
            radio.BackColor = back;
            desc.BackColor = back;
            card.BorderColor = selected ? Theme.Accent : Theme.Border;
            card.Invalidate();
        }

        private static string NullIfEmpty(string s)
        {
            return string.IsNullOrWhiteSpace(s) ? null : s.Trim();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            lblError.Text = "";

            TextBox[] required = { txtFirst, txtLast, txtHouse, txtStreet, txtBarangay, txtMunicipality, txtRegion, txtContact, txtEmail, txtUsername, txtPassword };
            foreach (TextBox t in required)
            {
                if (string.IsNullOrWhiteSpace(t.Text))
                {
                    lblError.Text = "Please complete all required fields (middle name is optional).";
                    t.Focus();
                    return;
                }
            }

            if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                lblError.Text = "Please enter a valid email address.";
                txtEmail.Focus();
                return;
            }
            if (!Regex.IsMatch(txtContact.Text.Trim(), @"^[0-9+\-\s()]{7,20}$"))
            {
                lblError.Text = "Please enter a valid contact number.";
                txtContact.Focus();
                return;
            }
            if (txtPassword.Text.Length < 8)
            {
                lblError.Text = "Password must be at least 8 characters.";
                txtPassword.Focus();
                return;
            }

            try
            {
                object exists = DatabaseConnection.ExecuteScalar(
                    "SELECT COUNT(*) FROM `User` WHERE Username = @u", DatabaseConnection.P("@u", txtUsername.Text.Trim()));
                if (Convert.ToInt32(exists) > 0)
                {
                    lblError.Text = "That username is already taken. Please choose another.";
                    txtUsername.Focus();
                    return;
                }

                DatabaseConnection.ExecuteNonQuery(
                    "INSERT INTO `User` (User_Role, First_name, Middle_name, Last_name, House_Number, Street, Barangay, " +
                    "Municipality, Region, Contact_number, Email_Address, Username, Password) " +
                    "VALUES (@role, @fn, @mn, @ln, @house, @street, @brgy, @muni, @region, @contact, @email, @user, @pass)",
                    DatabaseConnection.P("@role", selectedRole),
                    DatabaseConnection.P("@fn", txtFirst.Text.Trim()),
                    DatabaseConnection.P("@mn", NullIfEmpty(txtMiddle.Text)),
                    DatabaseConnection.P("@ln", txtLast.Text.Trim()),
                    DatabaseConnection.P("@house", txtHouse.Text.Trim()),
                    DatabaseConnection.P("@street", txtStreet.Text.Trim()),
                    DatabaseConnection.P("@brgy", txtBarangay.Text.Trim()),
                    DatabaseConnection.P("@muni", txtMunicipality.Text.Trim()),
                    DatabaseConnection.P("@region", txtRegion.Text.Trim()),
                    DatabaseConnection.P("@contact", txtContact.Text.Trim()),
                    DatabaseConnection.P("@email", txtEmail.Text.Trim()),
                    DatabaseConnection.P("@user", txtUsername.Text.Trim()),
                    DatabaseConnection.P("@pass", DatabaseConnection.HashPassword(txtPassword.Text)));

                Registered = true;
                MessageBox.Show("Your account (" + txtUserId.Text + ") has been created. You can now sign in.",
                    "Account created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                lblError.Text = "Could not create the account: " + ex.Message;
            }
        }
    }
}
