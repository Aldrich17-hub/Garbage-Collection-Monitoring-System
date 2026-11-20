#nullable disable
using System.Drawing;
using System.Windows.Forms;

namespace GarbageCollectionMonitoringSystem
{
    partial class DriverDashboardForm
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();
            Color white = Color.White;
            Color blueSoft = ColorTranslator.FromHtml("#DCEAFB");
            Color blue = ColorTranslator.FromHtml("#1F5FBF");

            this.navAssigned = new NavButton();
            this.navAssigned.Text = "\u25A3   Assigned Batches";
            this.flowNav.Controls.Add(this.navAssigned);
            this.flowNav.Controls.SetChildIndex(this.navAssigned, 2);   // Schedule, Assigned Batches, History

            // ---- heading ----
            this.lblAsgEyebrow = Theme.MakeLabel("COLLECTION TEAM", 7.5f, FontStyle.Bold, Theme.Accent, Theme.Background, 0, 0);
            this.lblAsgTitle = Theme.MakeLabel("Assigned batches", 18f, FontStyle.Bold, Theme.TextDark, Theme.Background, 0, 0);
            this.lblAsgSub = Theme.MakeLabel("Review your route and report collection progress in real time.", 9f, FontStyle.Regular, Theme.TextMuted, Theme.Background, 0, 0);

            // ---- route card (left) ----
            this.cardRoute = new RoundedPanel();
            this.cardRoute.Radius = 12;
            this.cardRoute.BackColor = white;
            this.lblRouteTitle = Theme.MakeLabel("Today's route", 10f, FontStyle.Bold, Theme.TextDark, white, 20, 14);
            this.lblRouteCount = Theme.MakeLabel("0 assigned batches", 8f, FontStyle.Regular, Theme.TextMuted, white, 21, 36);
            this.pnlRouteBadge = new RoundedPanel();
            this.pnlRouteBadge.Radius = 10;
            this.pnlRouteBadge.DrawBorder = false;
            this.pnlRouteBadge.BackColor = blueSoft;
            this.pnlRouteBadge.Size = new Size(96, 22);
            this.lblRouteBadge = Theme.MakeLabel("Route active", 7.75f, FontStyle.Bold, blue, blueSoft, 0, 0);
            this.lblRouteBadge.AutoSize = false;
            this.lblRouteBadge.Dock = DockStyle.Fill;
            this.lblRouteBadge.TextAlign = ContentAlignment.MiddleCenter;
            this.pnlRouteBadge.Controls.Add(this.lblRouteBadge);
            this.gridRoute = ScheduleGrid.Create();
            this.cardRoute.Controls.AddRange(new Control[] { lblRouteTitle, lblRouteCount, pnlRouteBadge, gridRoute });

            // ---- update status card (right) ----
            this.cardUpdate = new RoundedPanel();
            this.cardUpdate.Radius = 12;
            this.cardUpdate.BackColor = white;
            this.cardUpdate.Size = new Size(300, 410);

            this.pnlUpdIcon = new RoundedPanel();
            this.pnlUpdIcon.Radius = 10;
            this.pnlUpdIcon.DrawBorder = false;
            this.pnlUpdIcon.BackColor = Theme.MintSoft;
            this.pnlUpdIcon.Bounds = new Rectangle(20, 20, 40, 40);
            this.lblUpdIcon = Theme.MakeLabel("\U0001F69A", 12f, FontStyle.Regular, Theme.Accent, Theme.MintSoft, 0, 0);
            this.lblUpdIcon.AutoSize = false;
            this.lblUpdIcon.Dock = DockStyle.Fill;
            this.lblUpdIcon.TextAlign = ContentAlignment.MiddleCenter;
            this.pnlUpdIcon.Controls.Add(this.lblUpdIcon);

            this.lblUpdTitle = Theme.MakeLabel("Update status", 12f, FontStyle.Bold, Theme.TextDark, white, 18, 72);
            this.lblUpdSub = Theme.MakeLabel("Select a batch and report its latest collection status.", 8.5f, FontStyle.Regular, Theme.TextMuted, white, 20, 100);
            this.lblUpdSub.MaximumSize = new Size(260, 0);
            this.lblBatchCaption = Theme.MakeLabel("Batch ID", 8.25f, FontStyle.Bold, Theme.TextDark, white, 20, 148);
            this.cmbBatch = Theme.MakeCombo(20, 168, 260);
            this.lblStatusCaption = Theme.MakeLabel("New status", 8.25f, FontStyle.Bold, Theme.TextDark, white, 20, 212);
            this.chipCompleted = Theme.MakeChip("Completed", 20, 234, 126, 34);
            this.chipOngoing = Theme.MakeChip("Ongoing", 154, 234, 126, 34);
            this.chipMissed = Theme.MakeChip("Missed", 20, 276, 126, 34);
            this.chipScheduled = Theme.MakeChip("Scheduled", 154, 276, 126, 34);
            this.btnSubmit = Theme.MakeButton("Submit status   \u2713", 20, 326, 260, 42, Theme.DarkGreen, Color.White, Color.Empty);
            this.lblFeedback = Theme.MakeLabel("", 8.25f, FontStyle.Regular, Theme.Accent, white, 20, 376);
            this.lblFeedback.AutoSize = false;
            this.lblFeedback.Size = new Size(260, 28);

            this.cardUpdate.Controls.AddRange(new Control[] { pnlUpdIcon, lblUpdTitle, lblUpdSub, lblBatchCaption, cmbBatch,
                lblStatusCaption, chipCompleted, chipOngoing, chipMissed, chipScheduled, btnSubmit, lblFeedback });

            this.pnlAssignedView = new Panel();
            this.pnlAssignedView.Dock = DockStyle.Fill;
            this.pnlAssignedView.BackColor = Theme.Background;
            this.pnlAssignedView.Visible = false;
            this.pnlAssignedView.Controls.AddRange(new Control[] { lblAsgEyebrow, lblAsgTitle, lblAsgSub, cardRoute, cardUpdate });
            this.pnlContent.Controls.Add(this.pnlAssignedView);

            this.Name = "DriverDashboardForm";
            this.Text = "Collection Driver Dashboard - Garbage Collection Monitoring System";
            this.ResumeLayout(false);
        }

        private NavButton navAssigned;
        private Panel pnlAssignedView;
        private RoundedPanel cardRoute, cardUpdate, pnlRouteBadge, pnlUpdIcon;
        private Label lblAsgEyebrow, lblAsgTitle, lblAsgSub, lblRouteTitle, lblRouteCount, lblRouteBadge;
        private Label lblUpdIcon, lblUpdTitle, lblUpdSub, lblBatchCaption, lblStatusCaption, lblFeedback;
        private DataGridView gridRoute;
        private ComboBox cmbBatch;
        private RadioButton chipCompleted, chipOngoing, chipMissed, chipScheduled;
        private RoundedButton btnSubmit;
    }
}
