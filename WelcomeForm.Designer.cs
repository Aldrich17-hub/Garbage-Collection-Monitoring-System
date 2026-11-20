#nullable disable
using System.Drawing;
using System.Windows.Forms;

namespace GarbageCollectionMonitoringSystem
{
    partial class WelcomeForm
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
            Color logoBox = ColorTranslator.FromHtml("#17372B");
            Color logoBorder = ColorTranslator.FromHtml("#245240");
            Color soft = ColorTranslator.FromHtml("#8FB3A3");

            // ----- left (brand) panel -----
            this.pnlLeft = new Panel();
            this.pnlLeft.Dock = DockStyle.Left;
            this.pnlLeft.Width = 520;
            this.pnlLeft.BackColor = Theme.DarkGreen;

            this.pnlLogo = new RoundedPanel();
            this.pnlLogo.Radius = 9;
            this.pnlLogo.BackColor = logoBox;
            this.pnlLogo.BorderColor = logoBorder;
            this.pnlLogo.Bounds = new Rectangle(48, 44, 36, 36);
            this.lblLogo = Theme.MakeLabel("\u267B", 13f, FontStyle.Bold, Theme.Mint, logoBox, 0, 0);
            this.lblLogo.AutoSize = false;
            this.lblLogo.Dock = DockStyle.Fill;
            this.lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            this.pnlLogo.Controls.Add(this.lblLogo);

            this.lblBrand = Theme.MakeLabel("Garbage Collection", 10.5f, FontStyle.Bold, Color.White, Theme.DarkGreen, 94, 44);
            this.lblBrandSub = Theme.MakeLabel("MONITORING SYSTEM", 6.75f, FontStyle.Bold, Theme.Mint, Theme.DarkGreen, 95, 65);

            this.pnlTag = new RoundedPanel();
            this.pnlTag.Radius = 12;
            this.pnlTag.BackColor = logoBox;
            this.pnlTag.BorderColor = logoBorder;
            this.pnlTag.Bounds = new Rectangle(48, 150, 282, 26);
            this.lblTag = Theme.MakeLabel("\u25CF  Cleaner communities, better coordinated", 8f, FontStyle.Regular, Theme.Mint, logoBox, 12, 4);
            this.pnlTag.Controls.Add(this.lblTag);

            this.lblHeadline = Theme.MakeLabel("Smarter waste\ncollection starts\nhere.", 32f, FontStyle.Bold, Color.White, Theme.DarkGreen, 44, 196);
            this.lblBlurb = Theme.MakeLabel("A reliable, transparent platform connecting residents, municipal officials, and collection teams.",
                10.5f, FontStyle.Regular, Theme.Mint, Theme.DarkGreen, 48, 380);
            this.lblBlurb.MaximumSize = new Size(390, 0);
            this.lblFeatures = Theme.MakeLabel("Live schedules        |        Real-time status        |        Local impact",
                8.25f, FontStyle.Regular, soft, Theme.DarkGreen, 48, 570);

            this.pnlLeft.Controls.AddRange(new Control[] { pnlLogo, lblBrand, lblBrandSub, pnlTag, lblHeadline, lblBlurb, lblFeatures });

            // ----- right (actions) panel -----
            this.pnlRight = new Panel();
            this.pnlRight.Dock = DockStyle.Fill;
            this.pnlRight.BackColor = Color.White;

            this.pnlIcon = new RoundedPanel();
            this.pnlIcon.Radius = 12;
            this.pnlIcon.DrawBorder = false;
            this.pnlIcon.BackColor = Theme.MintSoft;
            this.pnlIcon.Bounds = new Rectangle(60, 150, 46, 46);
            this.lblIcon = Theme.MakeLabel("\U0001F69A", 14f, FontStyle.Regular, Theme.Accent, Theme.MintSoft, 0, 0);
            this.lblIcon.AutoSize = false;
            this.lblIcon.Dock = DockStyle.Fill;
            this.lblIcon.TextAlign = ContentAlignment.MiddleCenter;
            this.pnlIcon.Controls.Add(this.lblIcon);

            this.lblEyebrow = Theme.MakeLabel("WELCOME", 7.5f, FontStyle.Bold, Theme.Accent, Color.White, 60, 216);
            this.lblTitle = Theme.MakeLabel("Your community dashboard", 17f, FontStyle.Bold, Theme.TextDark, Color.White, 58, 232);
            this.lblSub = Theme.MakeLabel("Track collection schedules, receive service updates, and help keep your barangay clean.",
                9f, FontStyle.Regular, Theme.TextMuted, Color.White, 60, 276);
            this.lblSub.MaximumSize = new Size(360, 0);

            this.btnSignIn = Theme.MakeButton("Sign in to your account   \u203A", 60, 346, 360, 44, Theme.DarkGreen, Color.White, Color.Empty);
            this.btnCreate = Theme.MakeButton("Create an account", 60, 400, 360, 44, Color.White, Theme.TextDark, Theme.Border);
            this.btnCreate.HoverColor = Theme.MintSoft;
            this.lblOffice = Theme.MakeLabel("Municipal Environment & Natural Resources Office", 8f, FontStyle.Regular, Theme.TextMuted, Color.White, 62, 470);

            this.pnlRight.Controls.AddRange(new Control[] { pnlIcon, lblEyebrow, lblTitle, lblSub, btnSignIn, btnCreate, lblOffice });

            // ----- form -----
            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.BackColor = Color.White;
            this.ClientSize = new Size(1000, 640);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "WelcomeForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Garbage Collection Monitoring System";
            this.ResumeLayout(false);
        }

        private Panel pnlLeft;
        private Panel pnlRight;
        private RoundedPanel pnlLogo;
        private RoundedPanel pnlTag;
        private RoundedPanel pnlIcon;
        private Label lblLogo;
        private Label lblBrand;
        private Label lblBrandSub;
        private Label lblTag;
        private Label lblHeadline;
        private Label lblBlurb;
        private Label lblFeatures;
        private Label lblIcon;
        private Label lblEyebrow;
        private Label lblTitle;
        private Label lblSub;
        private Label lblOffice;
        private RoundedButton btnSignIn;
        private RoundedButton btnCreate;
    }
}
