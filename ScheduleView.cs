#nullable disable
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GarbageCollectionMonitoringSystem
{
    /// <summary>Heading + search/filter bar + styled grid. Used for both "Schedule" and "History" views.</summary>
    public class ScheduleView : UserControl
    {
        private readonly Label lblEyebrow;
        private readonly Label lblTitle;
        private readonly Label lblSub;
        private readonly Label lblShowing;
        private readonly Label lblUpdated;
        private readonly RoundedButton btnAction;
        private readonly RoundedButton btnAll;
        private readonly RoundedPanel card;
        private readonly TextBox txtSearch;
        private readonly ComboBox cmbStatus;
        private readonly DataGridView grid;
        private List<ScheduleRow> rows = new List<ScheduleRow>();

        public event EventHandler ActionClicked;

        public ScheduleView(string eyebrow, string title, string subtitle, string actionText, string[] statuses)
        {
            SuspendLayout();
            DoubleBuffered = true;
            BackColor = Theme.Background;
            Dock = DockStyle.Fill;

            lblEyebrow = Theme.MakeLabel("\u25CF " + eyebrow.ToUpperInvariant(), 7.5f, FontStyle.Bold, Theme.Accent, Theme.Background, 0, 0);
            lblTitle = Theme.MakeLabel(title, 18f, FontStyle.Bold, Theme.TextDark, Theme.Background, 0, 0);
            lblSub = Theme.MakeLabel(subtitle, 9f, FontStyle.Regular, Theme.TextMuted, Theme.Background, 0, 0);

            btnAction = Theme.MakeButton(actionText, 0, 0, 150, 36, Color.White, Theme.TextDark, Theme.Border);
            btnAction.HoverColor = Theme.MintSoft;
            btnAction.Click += delegate { if (ActionClicked != null) ActionClicked(this, EventArgs.Empty); };

            card = new RoundedPanel();
            card.BackColor = Color.White;

            txtSearch = Theme.MakeTextBox(16, 15, 300, "Search batch ID, date, category, driver, or location");
            txtSearch.TextChanged += delegate { ApplyFilter(); };

            cmbStatus = Theme.MakeCombo(0, 15, 160);
            cmbStatus.Items.AddRange(statuses);
            cmbStatus.SelectedIndex = 0;
            cmbStatus.SelectedIndexChanged += delegate { ApplyFilter(); };

            btnAll = Theme.MakeButton("All", 0, 14, 48, 34, Color.FromArgb(0x0F, 0x3D, 0x2E), Color.White, Color.Empty);
            btnAll.Click += delegate { txtSearch.Clear(); cmbStatus.SelectedIndex = 0; };

            grid = ScheduleGrid.Create();

            lblShowing = Theme.MakeLabel("Showing 0 of 0 batches", 8f, FontStyle.Regular, Theme.TextMuted, Color.White, 16, 0);
            lblUpdated = Theme.MakeLabel("Updated just now", 8f, FontStyle.Italic, Theme.TextMuted, Color.White, 0, 0);

            card.Controls.AddRange(new Control[] { txtSearch, cmbStatus, btnAll, grid, lblShowing, lblUpdated });
            Controls.AddRange(new Control[] { lblEyebrow, lblTitle, lblSub, btnAction, card });
            ResumeLayout(false);
            DoLayout();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            DoLayout();
        }

        private void DoLayout()
        {
            if (card == null) return;
            int cw = Math.Min(Width - 80, 1000);
            if (cw < 420) cw = 420;
            int left = Math.Max(20, (Width - cw) / 2);

            lblEyebrow.Location = new Point(left, 26);
            lblTitle.Location = new Point(left, 42);
            lblSub.Location = new Point(left, 82);
            btnAction.SetBounds(left + cw - 150, 40, 150, 36);
            card.SetBounds(left, 118, cw, Math.Max(220, Height - 118 - 32));

            btnAll.SetBounds(card.Width - 16 - 48, 14, 48, 34);
            cmbStatus.SetBounds(btnAll.Left - 8 - 160, 15, 160, 30);
            txtSearch.SetBounds(16, 15, Math.Max(80, cmbStatus.Left - 8 - 16), 30);
            grid.SetBounds(0, 62, card.Width, Math.Max(60, card.Height - 62 - 40));
            lblShowing.Location = new Point(16, card.Height - 28);
            lblUpdated.Location = new Point(card.Width - lblUpdated.Width - 16, card.Height - 28);
        }

        public void SetData(List<ScheduleRow> data)
        {
            rows = data ?? new List<ScheduleRow>();
            lblUpdated.Text = "Updated " + DateTime.Now.ToString("h:mm:ss tt");
            ApplyFilter();
            DoLayout();
        }

        private static string Haystack(ScheduleRow r)
        {
            return string.Join(" ", r.BatchId, r.Date.ToString("MMM dd yyyy"), r.Date.ToString("yyyy-MM-dd"),
                DateTime.Today.Add(r.Time).ToString("h:mm tt"), r.Category, r.Area, r.Driver, r.Status).ToLowerInvariant();
        }

        private void ApplyFilter()
        {
            string q = txtSearch.Text.Trim().ToLowerInvariant();
            string status = cmbStatus.SelectedIndex > 0 ? Convert.ToString(cmbStatus.SelectedItem) : null;

            List<ScheduleRow> shown = rows.Where(r =>
                (status == null || r.Status == status) &&
                (q.Length == 0 || Haystack(r).Contains(q))).ToList();

            ScheduleGrid.Populate(grid, shown);
            lblShowing.Text = "Showing " + shown.Count + " of " + rows.Count + " batches";
        }
    }
}
