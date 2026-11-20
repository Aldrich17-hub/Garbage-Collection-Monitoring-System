#nullable disable
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GarbageCollectionMonitoringSystem
{
    /// <summary>Collection Driver view: Schedule, Assigned Batches (+ Update status card), History.</summary>
    public partial class DriverDashboardForm : DashboardBaseForm
    {
        public DriverDashboardForm()
        {
            InitializeComponent();
            navAssigned.Click += delegate { ShowView(pnlAssignedView, navAssigned, "Assigned Batches"); };
            pnlAssignedView.Resize += delegate { LayoutAssigned(); };
            btnSubmit.Click += btnSubmit_Click;
            chipCompleted.Checked = true;
            LayoutAssigned();
        }

        private void LayoutAssigned()
        {
            int cw = Math.Min(pnlAssignedView.Width - 80, 1100);
            if (cw < 640) cw = 640;
            int left = Math.Max(20, (pnlAssignedView.Width - cw) / 2);
            int gap = 20;
            int routeW = cw - cardUpdate.Width - gap;

            lblAsgEyebrow.Location = new Point(left, 26);
            lblAsgTitle.Location = new Point(left - 2, 42);
            lblAsgSub.Location = new Point(left, 82);

            int top = 118;
            cardRoute.SetBounds(left, top, routeW, Math.Max(260, pnlAssignedView.Height - top - 32));
            cardUpdate.Location = new Point(left + routeW + gap, top);

            pnlRouteBadge.Location = new Point(cardRoute.Width - pnlRouteBadge.Width - 16, 16);
            gridRoute.SetBounds(0, 66, cardRoute.Width, Math.Max(60, cardRoute.Height - 66 - 8));
        }

        /// <summary>Also refreshes the driver's own assigned batches and the batch drop-down.</summary>
        protected override void ReloadData()
        {
            base.ReloadData();
            if (Session.Current == null) return;
            try
            {
                List<ScheduleRow> mine = ScheduleRepository.GetSchedules(ScheduleMode.All, null, Session.Current.FullName);
                ScheduleGrid.Populate(gridRoute, mine);
                lblRouteCount.Text = mine.Count + (mine.Count == 1 ? " assigned batch" : " assigned batches");

                string keep = cmbBatch.SelectedItem as string;
                cmbBatch.Items.Clear();
                foreach (ScheduleRow r in mine) cmbBatch.Items.Add(r.BatchId);
                if (keep != null && cmbBatch.Items.Contains(keep)) cmbBatch.SelectedItem = keep;
                else if (cmbBatch.Items.Count > 0) cmbBatch.SelectedIndex = 0;
            }
            catch (Exception)
            {
                // base.ReloadData already reports database problems once.
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string batch = cmbBatch.SelectedItem as string;
            string status = chipCompleted.Checked ? "Completed"
                          : chipOngoing.Checked ? "Ongoing"
                          : chipMissed.Checked ? "Missed"
                          : chipScheduled.Checked ? "Scheduled" : null;

            if (batch == null) { Feedback("Select a batch first.", false); return; }
            if (status == null) { Feedback("Choose a new status.", false); return; }

            try
            {
                int rows = ScheduleRepository.UpdateStatus(batch, status);
                if (rows > 0)
                {
                    Feedback(batch + " marked as " + status + ".", true);
                    ReloadData();
                }
                else Feedback("No schedule was updated for " + batch + ".", false);
            }
            catch (Exception ex)
            {
                Feedback("Update failed: " + ex.Message, false);
            }
        }

        private void Feedback(string text, bool ok)
        {
            lblFeedback.ForeColor = ok ? Theme.Accent : Theme.Danger;
            lblFeedback.Text = text;
        }
    }
}
