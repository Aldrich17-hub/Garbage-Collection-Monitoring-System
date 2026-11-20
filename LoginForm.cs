#nullable disable
using System;
using System.Windows.Forms;

namespace GarbageCollectionMonitoringSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            AcceptButton = btnEnter;
            lnkBack.LinkClicked += delegate { DialogResult = DialogResult.Cancel; Close(); };
            btnEnter.Click += btnEnter_Click;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (username.Length == 0 || password.Length == 0)
            {
                lblError.Text = "Please enter your username and password.";
                return;
            }

            try
            {
                var dt = DatabaseConnection.ExecuteQuery(
                    "SELECT User_id, User_Role, First_name, Middle_name, Last_name, Barangay " +
                    "FROM `User` WHERE Username = @u AND Password = @p LIMIT 1",
                    DatabaseConnection.P("@u", username),
                    DatabaseConnection.P("@p", DatabaseConnection.HashPassword(password)));

                if (dt.Rows.Count == 0)
                {
                    lblError.Text = "Incorrect username or password.";
                    txtPassword.Clear();
                    txtPassword.Focus();
                    return;
                }

                var r = dt.Rows[0];
                Session.Current = new UserSession
                {
                    UserId = Convert.ToInt32(r["User_id"]),
                    Role = Convert.ToString(r["User_Role"]),
                    FirstName = Convert.ToString(r["First_name"]),
                    MiddleName = r["Middle_name"] as string,
                    LastName = Convert.ToString(r["Last_name"]),
                    Barangay = Convert.ToString(r["Barangay"])
                };

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                lblError.Text = "Cannot reach the database. Start MySQL in XAMPP and import database.sql. (" + ex.Message + ")";
            }
        }
    }
}
