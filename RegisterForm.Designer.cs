#nullable disable
using System.Drawing;
using System.Windows.Forms;

namespace GarbageCollectionMonitoringSystem
{
    partial class RegisterForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private Label Caption(string text, int x, int y)
        {
            return Theme.MakeLabel(text, 8.25f, FontStyle.Bold, Theme.TextDark, Color.White, x, y);
        }

        private RoundedPanel NumberBadge(string number, int x, int y)
        {
            RoundedPanel p = new RoundedPanel();
            p.Radius = 6;
            p.DrawBorder = false;
            p.BackColor = Theme.MintSoft;
            p.Bounds = new Rectangle(x, y, 26, 26);
            Label l = Theme.MakeLabel(number, 7.5f, FontStyle.Bold, Theme.Accent, Theme.MintSoft, 0, 0);
            l.AutoSize = false;
            l.Dock = DockStyle.Fill;
            l.TextAlign = ContentAlignment.MiddleCenter;
            p.Controls.Add(l);
            return p;
        }

        private RoundedPanel RoleCard(int y, string title, string description, out RadioButton radio, out Label desc)
        {
            RoundedPanel card = new RoundedPanel();
            card.Radius = 10;
            card.BackColor = Color.White;
            card.Bounds = new Rectangle(476, y, 412, 58);
            card.Cursor = Cursors.Hand;
            radio = new RadioButton();
            radio.Text = title;
            radio.AutoSize = true;
            radio.Font = Theme.F(9f, FontStyle.Bold);
            radio.ForeColor = Theme.TextDark;
            radio.BackColor = Color.White;
            radio.Location = new Point(14, 8);
            desc = Theme.MakeLabel(description, 8f, FontStyle.Regular, Theme.TextMuted, Color.White, 36, 31);
            card.Controls.Add(radio);
            card.Controls.Add(desc);
            return card;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            Color logoBox = Theme.MintSoft;

            // header
            this.pnlLogo = new RoundedPanel();
            this.pnlLogo.Radius = 8;
            this.pnlLogo.DrawBorder = false;
            this.pnlLogo.BackColor = logoBox;
            this.pnlLogo.Bounds = new Rectangle(50, 20, 32, 32);
            this.lblLogo = Theme.MakeLabel("\u267B", 11f, FontStyle.Bold, Theme.Accent, logoBox, 0, 0);
            this.lblLogo.AutoSize = false;
            this.lblLogo.Dock = DockStyle.Fill;
            this.lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            this.pnlLogo.Controls.Add(this.lblLogo);
            this.lblBrand = Theme.MakeLabel("Garbage Collection", 9.5f, FontStyle.Bold, Theme.TextDark, Theme.Background, 90, 19);
            this.lblBrandSub = Theme.MakeLabel("MONITORING SYSTEM", 6.25f, FontStyle.Bold, Theme.Accent, Theme.Background, 91, 38);
            this.lnkBack = new LinkLabel();
            this.lnkBack.Text = "\u2039  Back";
            this.lnkBack.AutoSize = true;
            this.lnkBack.Font = Theme.F(9f);
            this.lnkBack.BackColor = Theme.Background;
            this.lnkBack.LinkColor = Theme.TextMuted;
            this.lnkBack.ActiveLinkColor = Theme.Accent;
            this.lnkBack.LinkBehavior = LinkBehavior.NeverUnderline;
            this.lnkBack.Location = new Point(920, 28);
            this.lnkBack.Cursor = Cursors.Hand;

            // card
            this.pnlCard = new RoundedPanel();
            this.pnlCard.Radius = 16;
            this.pnlCard.BackColor = Color.White;
            this.pnlCard.Bounds = new Rectangle(50, 64, 920, 704);

            this.lblEyebrow = Theme.MakeLabel("NEW RESIDENT PROFILE", 7.5f, FontStyle.Bold, Theme.Accent, Color.White, 32, 22);
            this.lblTitle = Theme.MakeLabel("Create your account", 18f, FontStyle.Bold, Theme.TextDark, Color.White, 30, 38);
            this.lblSub = Theme.MakeLabel("Complete the information below to join your barangay collection network.", 9f, FontStyle.Regular, Theme.TextMuted, Color.White, 32, 76);
            this.pnlDivider = new Panel();
            this.pnlDivider.BackColor = Theme.Border;
            this.pnlDivider.Bounds = new Rectangle(0, 108, 920, 1);

            // 01 personal
            this.badge01 = NumberBadge("01", 32, 126);
            this.lblSec1 = Theme.MakeLabel("Personal information", 10f, FontStyle.Bold, Theme.TextDark, Color.White, 66, 128);
            this.lblFirst = Caption("First name", 32, 168);
            this.txtFirst = Theme.MakeTextBox(32, 188, 190, "Juan");
            this.lblMiddle = Caption("Middle name", 242, 168);
            this.txtMiddle = Theme.MakeTextBox(242, 188, 190, "Santos");
            this.lblLast = Caption("Last name", 32, 232);
            this.txtLast = Theme.MakeTextBox(32, 252, 400, "Dela Cruz");

            // 02 address
            this.badge02 = NumberBadge("02", 32, 306);
            this.lblSec2 = Theme.MakeLabel("Home address", 10f, FontStyle.Bold, Theme.TextDark, Color.White, 66, 308);
            this.lblHouse = Caption("House no.", 32, 348);
            this.txtHouse = Theme.MakeTextBox(32, 368, 190, "24");
            this.lblStreet = Caption("Street", 242, 348);
            this.txtStreet = Theme.MakeTextBox(242, 368, 190, "Matini Street");
            this.lblBarangay = Caption("Barangay", 32, 412);
            this.txtBarangay = Theme.MakeTextBox(32, 432, 190, "San Isidro");
            this.lblMunicipality = Caption("Municipality", 242, 412);
            this.txtMunicipality = Theme.MakeTextBox(242, 432, 190, "Santa Maria");
            this.lblRegion = Caption("Region", 32, 476);
            this.txtRegion = Theme.MakeTextBox(32, 496, 400, "Region IV-A");

            // 03 account
            this.badge03 = NumberBadge("03", 476, 126);
            this.lblSec3 = Theme.MakeLabel("Account details", 10f, FontStyle.Bold, Theme.TextDark, Color.White, 510, 128);
            this.lblContact = Caption("Contact number", 476, 168);
            this.txtContact = Theme.MakeTextBox(476, 188, 196, "+63 912 345 6789");
            this.lblEmail = Caption("Email address", 688, 168);
            this.txtEmail = Theme.MakeTextBox(688, 188, 200, "juan@email.com");
            this.lblUsername = Caption("Username", 476, 232);
            this.txtUsername = Theme.MakeTextBox(476, 252, 196, "juandelacruz");
            this.lblPassword = Caption("Password", 688, 232);
            this.txtPassword = Theme.MakeTextBox(688, 252, 200, "At least 8 characters", true);
            this.lblUserId = Caption("User ID", 476, 296);
            this.txtUserId = Theme.MakeTextBox(476, 316, 412, "USR-2026-00001");
            this.txtUserId.ReadOnly = true;
            this.txtUserId.BackColor = Theme.Background;
            this.lblUserIdHint = Theme.MakeLabel("Auto-generated. You cannot change this in the future.", 7.75f, FontStyle.Regular, Theme.TextMuted, Color.White, 476, 348);

            // 04 role
            this.badge04 = NumberBadge("04", 476, 378);
            this.lblSec4 = Theme.MakeLabel("Select your role", 10f, FontStyle.Bold, Theme.TextDark, Color.White, 510, 380);
            this.pnlRoleResident = RoleCard(420, "Resident", "View upcoming collection schedules in your barangay.", out this.rdoResident, out this.lblRoleResidentDesc);
            this.pnlRoleOfficial = RoleCard(488, "Municipal Official", "Create schedules and manage collection activity.", out this.rdoOfficial, out this.lblRoleOfficialDesc);
            this.pnlRoleDriver = RoleCard(556, "Driver / Garbage Collector", "View assigned batches and update their status.", out this.rdoDriver, out this.lblRoleDriverDesc);

            // footer
            this.pnlFooterLine = new Panel();
            this.pnlFooterLine.BackColor = Theme.Border;
            this.pnlFooterLine.Bounds = new Rectangle(0, 636, 920, 1);
            this.lblError = Theme.MakeLabel("", 8.5f, FontStyle.Regular, Theme.Danger, Color.White, 32, 652);
            this.lblError.AutoSize = false;
            this.lblError.Size = new Size(560, 36);
            this.btnCancel = Theme.MakeButton("Cancel", 668, 650, 100, 40, Color.White, Theme.TextDark, Theme.Border);
            this.btnCancel.HoverColor = Theme.MintSoft;
            this.btnCreate = Theme.MakeButton("Create account   \u203A", 778, 650, 130, 40, Theme.DarkGreen, Color.White, Color.Empty);
            this.btnCreate.Width = 120;
            this.btnCreate.Left = 788;
            this.btnCancel.Left = 678;

            this.pnlCard.Controls.AddRange(new Control[] {
                lblEyebrow, lblTitle, lblSub, pnlDivider,
                badge01, lblSec1, lblFirst, txtFirst, lblMiddle, txtMiddle, lblLast, txtLast,
                badge02, lblSec2, lblHouse, txtHouse, lblStreet, txtStreet, lblBarangay, txtBarangay,
                lblMunicipality, txtMunicipality, lblRegion, txtRegion,
                badge03, lblSec3, lblContact, txtContact, lblEmail, txtEmail, lblUsername, txtUsername,
                lblPassword, txtPassword, lblUserId, txtUserId, lblUserIdHint,
                badge04, lblSec4, pnlRoleResident, pnlRoleOfficial, pnlRoleDriver,
                pnlFooterLine, lblError, btnCancel, btnCreate });

            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.BackColor = Theme.Background;
            this.ClientSize = new Size(1020, 800);
            this.Controls.AddRange(new Control[] { pnlLogo, lblBrand, lblBrandSub, lnkBack, pnlCard });
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "RegisterForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Create account - Garbage Collection Monitoring System";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private RoundedPanel pnlLogo;
        private RoundedPanel pnlCard;
        private RoundedPanel badge01, badge02, badge03, badge04;
        private RoundedPanel pnlRoleResident, pnlRoleOfficial, pnlRoleDriver;
        private Panel pnlDivider, pnlFooterLine;
        private LinkLabel lnkBack;
        private Label lblLogo, lblBrand, lblBrandSub, lblEyebrow, lblTitle, lblSub;
        private Label lblSec1, lblSec2, lblSec3, lblSec4;
        private Label lblFirst, lblMiddle, lblLast, lblHouse, lblStreet, lblBarangay, lblMunicipality, lblRegion;
        private Label lblContact, lblEmail, lblUsername, lblPassword, lblUserId, lblUserIdHint, lblError;
        private Label lblRoleResidentDesc, lblRoleOfficialDesc, lblRoleDriverDesc;
        private TextBox txtFirst, txtMiddle, txtLast, txtHouse, txtStreet, txtBarangay, txtMunicipality, txtRegion;
        private TextBox txtContact, txtEmail, txtUsername, txtPassword, txtUserId;
        private RadioButton rdoResident, rdoOfficial, rdoDriver;
        private RoundedButton btnCancel, btnCreate;
    }
}
