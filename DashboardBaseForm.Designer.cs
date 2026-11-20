#nullable disable
using System.Drawing;
using System.Windows.Forms;

namespace GarbageCollectionMonitoringSystem
{
    partial class DashboardBaseForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SuspendLayout();
            Color logoBox = ColorTranslator.FromHtml("#17372B");
            Color profileBack = ColorTranslator.FromHtml("#12261D");
            Color avatar = ColorTranslator.FromHtml("#22C58B");
            Color softText = ColorTranslator.FromHtml("#8FB3A3");

            // ================= TOP BAR =================
            this.lblBreadcrumb = Theme.MakeLabel("GARBAGE COLLECTION MONITORING SYSTEM", 6.5f, FontStyle.Bold, Theme.TextMuted, Color.White, 28, 9);
            this.lblPageTitle = Theme.MakeLabel("Schedule", 11f, FontStyle.Bold, Theme.TextDark, Color.White, 26, 22);
            this.pnlBarangayBadge = new RoundedPanel();
            this.pnlBarangayBadge.Radius = 8;
            this.pnlBarangayBadge.BackColor = Theme.MintSoft;
            this.pnlBarangayBadge.BorderColor = ColorTranslator.FromHtml("#CBEADD");
            this.pnlBarangayBadge.Size = new Size(160, 30);
            this.pnlBarangayBadge.Location = new Point(900, 16);
            this.lblBarangay = Theme.MakeLabel("Barangay", 8f, FontStyle.Bold, Theme.Accent, Theme.MintSoft, 14, 6);
            this.pnlBarangayBadge.Controls.Add(this.lblBarangay);

            this.pnlTop = new Panel();
            this.pnlTop.Dock = DockStyle.Top;
            this.pnlTop.Height = 62;
            this.pnlTop.BackColor = Color.White;
            this.pnlTop.Controls.AddRange(new Control[] { lblBreadcrumb, lblPageTitle, pnlBarangayBadge });

            // ================= CONTENT VIEWS =================
            this.viewSchedule = new ScheduleView("Live service plan", "Upcoming collections",
                "View and monitor scheduled waste collections in your area.", "\u27F2  View history",
                new string[] { "All statuses", "Scheduled", "Ongoing" });
            this.viewHistory = new ScheduleView("Collection archive", "Collection history",
                "Review completed and missed collection activity.", "\u2039  Back to schedule",
                new string[] { "All statuses", "Completed", "Missed" });
            this.viewHistory.Visible = false;

            this.pnlContent = new Panel();
            this.pnlContent.Dock = DockStyle.Fill;
            this.pnlContent.BackColor = Theme.Background;
            this.pnlContent.Controls.Add(this.viewSchedule);
            this.pnlContent.Controls.Add(this.viewHistory);

            this.pnlMain = new Panel();
            this.pnlMain.Dock = DockStyle.Fill;
            this.pnlMain.BackColor = Theme.Background;
            this.pnlMain.Controls.Add(this.pnlContent);   // Fill first,
            this.pnlMain.Controls.Add(this.pnlTop);       // then Top

            // ================= SIDEBAR =================
            this.pnlLogo = new RoundedPanel();
            this.pnlLogo.Radius = 10;
            this.pnlLogo.BackColor = logoBox;
            this.pnlLogo.BorderColor = ColorTranslator.FromHtml("#245240");
            this.pnlLogo.Bounds = new Rectangle(16, 22, 38, 38);
            this.lblLogo = Theme.MakeLabel("\u267B", 13f, FontStyle.Bold, Theme.Mint, logoBox, 0, 0);
            this.lblLogo.AutoSize = false;
            this.lblLogo.Dock = DockStyle.Fill;
            this.lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            this.pnlLogo.Controls.Add(this.lblLogo);
            this.lblBrandTitle = Theme.MakeLabel("Garbage Collection", 9.5f, FontStyle.Bold, Color.White, Theme.DarkGreen, 62, 22);
            this.lblBrandSub = Theme.MakeLabel("MONITORING SYSTEM", 6.25f, FontStyle.Bold, Theme.Mint, Theme.DarkGreen, 63, 42);
            this.pnlBrand = new Panel();
            this.pnlBrand.Dock = DockStyle.Top;
            this.pnlBrand.Height = 84;
            this.pnlBrand.BackColor = Theme.DarkGreen;
            this.pnlBrand.Controls.AddRange(new Control[] { pnlLogo, lblBrandTitle, lblBrandSub });

            this.lblWorkspace = Theme.MakeLabel("WORKSPACE", 6.75f, FontStyle.Bold, ColorTranslator.FromHtml("#6E8C7F"), Theme.DarkGreen, 0, 0);
            this.lblWorkspace.Margin = new Padding(6, 4, 0, 8);
            this.navSchedule = new NavButton();
            this.navSchedule.Text = "\u25A4   Schedule";
            this.navHistory = new NavButton();
            this.navHistory.Text = "\u27F2   History";

            this.flowNav = new FlowLayoutPanel();
            this.flowNav.Dock = DockStyle.Fill;
            this.flowNav.FlowDirection = FlowDirection.TopDown;
            this.flowNav.WrapContents = false;
            this.flowNav.BackColor = Theme.DarkGreen;
            this.flowNav.Padding = new Padding(12, 8, 0, 0);
            this.flowNav.Controls.Add(this.lblWorkspace);
            this.flowNav.Controls.Add(this.navSchedule);
            this.flowNav.Controls.Add(this.navHistory);

            this.pnlProfile = new RoundedPanel();
            this.pnlProfile.Radius = 10;
            this.pnlProfile.BackColor = profileBack;
            this.pnlProfile.BorderColor = ColorTranslator.FromHtml("#1C3A2D");
            this.pnlProfile.Bounds = new Rectangle(16, 8, 200, 56);
            this.pnlAvatar = new RoundedPanel();
            this.pnlAvatar.Radius = 17;
            this.pnlAvatar.DrawBorder = false;
            this.pnlAvatar.BackColor = avatar;
            this.pnlAvatar.Bounds = new Rectangle(11, 11, 34, 34);
            this.lblInitials = Theme.MakeLabel("", 8f, FontStyle.Bold, Theme.DarkGreen, avatar, 0, 0);
            this.lblInitials.AutoSize = false;
            this.lblInitials.Dock = DockStyle.Fill;
            this.lblInitials.TextAlign = ContentAlignment.MiddleCenter;
            this.pnlAvatar.Controls.Add(this.lblInitials);
            this.lblProfileName = Theme.MakeLabel("", 9f, FontStyle.Bold, Color.White, profileBack, 54, 10);
            this.lblProfileRole = Theme.MakeLabel("", 7.5f, FontStyle.Regular, softText, profileBack, 55, 30);
            this.pnlProfile.Controls.AddRange(new Control[] { pnlAvatar, lblProfileName, lblProfileRole });

            this.btnLogout = Theme.MakeButton("\u21E6   Log out", 16, 74, 200, 38, Theme.DarkGreen, ColorTranslator.FromHtml("#CFE5DB"), Color.Empty);
            this.btnLogout.LeftAligned = true;
            this.btnLogout.Font = Theme.F(9.5f);
            this.btnLogout.HoverColor = ColorTranslator.FromHtml("#14291F");

            this.pnlBottom = new Panel();
            this.pnlBottom.Dock = DockStyle.Bottom;
            this.pnlBottom.Height = 124;
            this.pnlBottom.BackColor = Theme.DarkGreen;
            this.pnlBottom.Controls.AddRange(new Control[] { pnlProfile, btnLogout });

            this.pnlSidebar = new Panel();
            this.pnlSidebar.Dock = DockStyle.Left;
            this.pnlSidebar.Width = 232;
            this.pnlSidebar.BackColor = Theme.DarkGreen;
            this.pnlSidebar.Controls.Add(this.flowNav);   // Fill first,
            this.pnlSidebar.Controls.Add(this.pnlBottom); // then Bottom,
            this.pnlSidebar.Controls.Add(this.pnlBrand);  // then Top

            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.tmrRefresh.Interval = 15000;

            // ================= FORM =================
            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.BackColor = Theme.Background;
            this.ClientSize = new Size(1280, 780);
            this.Controls.Add(this.pnlMain);      // Fill first,
            this.Controls.Add(this.pnlSidebar);   // then Left
            this.MinimumSize = new Size(1040, 660);
            this.Name = "DashboardBaseForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Garbage Collection Monitoring System";
            this.ResumeLayout(false);
        }

        protected Panel pnlMain;
        protected Panel pnlContent;
        protected Panel pnlTop;
        protected Panel pnlSidebar;
        protected Panel pnlBrand;
        protected Panel pnlBottom;
        protected FlowLayoutPanel flowNav;
        protected RoundedPanel pnlLogo;
        protected RoundedPanel pnlProfile;
        protected RoundedPanel pnlAvatar;
        protected RoundedPanel pnlBarangayBadge;
        protected Label lblLogo;
        protected Label lblBrandTitle;
        protected Label lblBrandSub;
        protected Label lblWorkspace;
        protected Label lblBreadcrumb;
        protected Label lblPageTitle;
        protected Label lblBarangay;
        protected Label lblInitials;
        protected Label lblProfileName;
        protected Label lblProfileRole;
        protected NavButton navSchedule;
        protected NavButton navHistory;
        protected RoundedButton btnLogout;
        protected ScheduleView viewSchedule;
        protected ScheduleView viewHistory;
        protected System.Windows.Forms.Timer tmrRefresh;
    }
}
