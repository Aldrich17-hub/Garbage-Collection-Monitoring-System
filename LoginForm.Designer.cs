#nullable disable
using System.Drawing;
using System.Windows.Forms;

namespace GarbageCollectionMonitoringSystem
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            Color logoBox = Theme.MintSoft;

            this.lnkBack = new LinkLabel();
            this.lnkBack.Text = "\u2039  Back to welcome";
            this.lnkBack.AutoSize = true;
            this.lnkBack.Location = new Point(62, 60);
            this.lnkBack.Font = Theme.F(9f);
            this.lnkBack.BackColor = Theme.Background;
            this.lnkBack.LinkColor = Theme.TextMuted;
            this.lnkBack.ActiveLinkColor = Theme.Accent;
            this.lnkBack.LinkBehavior = LinkBehavior.NeverUnderline;
            this.lnkBack.Cursor = Cursors.Hand;

            this.pnlCard = new RoundedPanel();
            this.pnlCard.Radius = 16;
            this.pnlCard.BackColor = Color.White;
            this.pnlCard.Bounds = new Rectangle(60, 90, 400, 420);

            this.pnlLogo = new RoundedPanel();
            this.pnlLogo.Radius = 9;
            this.pnlLogo.DrawBorder = false;
            this.pnlLogo.BackColor = logoBox;
            this.pnlLogo.Bounds = new Rectangle(28, 26, 34, 34);
            this.lblLogo = Theme.MakeLabel("\u267B", 12f, FontStyle.Bold, Theme.Accent, logoBox, 0, 0);
            this.lblLogo.AutoSize = false;
            this.lblLogo.Dock = DockStyle.Fill;
            this.lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            this.pnlLogo.Controls.Add(this.lblLogo);

            this.lblBrand = Theme.MakeLabel("Garbage Collection", 10f, FontStyle.Bold, Theme.TextDark, Color.White, 70, 25);
            this.lblBrandSub = Theme.MakeLabel("MONITORING SYSTEM", 6.5f, FontStyle.Bold, Theme.Accent, Color.White, 71, 45);
            this.lblTitle = Theme.MakeLabel("Welcome back", 17f, FontStyle.Bold, Theme.TextDark, Color.White, 26, 88);
            this.lblSub = Theme.MakeLabel("Enter your details to access the monitoring system.", 9f, FontStyle.Regular, Theme.TextMuted, Color.White, 28, 128);

            this.lblUser = Theme.MakeLabel("Username", 8.5f, FontStyle.Bold, Theme.TextDark, Color.White, 28, 168);
            this.txtUsername = Theme.MakeTextBox(28, 190, 344, "Enter your username");
            this.lblPass = Theme.MakeLabel("Password", 8.5f, FontStyle.Bold, Theme.TextDark, Color.White, 28, 238);
            this.txtPassword = Theme.MakeTextBox(28, 260, 344, "Enter your password", true);

            this.lblError = Theme.MakeLabel("", 8.5f, FontStyle.Regular, Theme.Danger, Color.White, 28, 304);
            this.lblError.AutoSize = false;
            this.lblError.Size = new Size(344, 34);

            this.btnEnter = Theme.MakeButton("Enter dashboard   \u203A", 28, 350, 344, 44, Theme.DarkGreen, Color.White, Color.Empty);

            this.pnlCard.Controls.AddRange(new Control[] { pnlLogo, lblBrand, lblBrandSub, lblTitle, lblSub,
                lblUser, txtUsername, lblPass, txtPassword, lblError, btnEnter });

            this.lblFooter = Theme.MakeLabel("Authorized community members only", 8f, FontStyle.Regular, Theme.TextMuted, Theme.Background, 0, 530);
            this.lblFooter.AutoSize = false;
            this.lblFooter.Size = new Size(520, 20);
            this.lblFooter.TextAlign = ContentAlignment.MiddleCenter;

            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.BackColor = Theme.Background;
            this.ClientSize = new Size(520, 600);
            this.Controls.AddRange(new Control[] { lnkBack, pnlCard, lblFooter });
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Sign in - Garbage Collection Monitoring System";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private LinkLabel lnkBack;
        private RoundedPanel pnlCard;
        private RoundedPanel pnlLogo;
        private Label lblLogo;
        private Label lblBrand;
        private Label lblBrandSub;
        private Label lblTitle;
        private Label lblSub;
        private Label lblUser;
        private Label lblPass;
        private Label lblError;
        private Label lblFooter;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private RoundedButton btnEnter;
    }
}
