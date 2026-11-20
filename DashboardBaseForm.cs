#nullable disable
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GarbageCollectionMonitoringSystem
{
    /// <summary>Shared layout + Schedule/History logic for the Resident, Official and Driver dashboards.</summary>
    public partial class DashboardBaseForm : Form
    {
        private bool errorShown;

        public DashboardBaseForm()
        {
            InitializeComponent();

            navSchedule.Click += delegate { ShowSchedule(); };
            navHistory.Click += delegate { ShowHistory(); };
            viewSchedule.ActionClicked += delegate { ShowHistory(); };
            viewHistory.ActionClicked += delegate { ShowSchedule(); };
            btnLogout.Click += btnLogout_Click;
            tmrRefresh.Tick += delegate { ReloadData(); };

            pnlTop.Paint += delegate(object s, PaintEventArgs e)
            {
                using (Pen p = new Pen(Theme.Border)) e.Graphics.DrawLine(p, 0, pnlTop.Height - 1, pnlTop.Width, pnlTop.Height - 1);
            };
            pnlTop.Resize += delegate { pnlBarangayBadge.Left = pnlTop.Width - pnlBarangayBadge.Width - 28; };

            if (Session.Current != null) ApplySession();
        }

        /// <summary>Residents only see collections in their own barangay.</summary>
        protected virtual bool FilterByBarangay { get { return false; } }

        private void ApplySession()
        {
            UserSession s = Session.Current;
            lblProfileName.Text = s.FullName;
            lblProfileRole.Text = s.RoleLabel;
            lblInitials.Text = s.Initials;
            lblBarangay.Text = "\u25A3  Barangay  " + s.Barangay;
            pnlBarangayBadge.Width = lblBarangay.PreferredWidth + 28;
            pnlBarangayBadge.Left = pnlTop.Width - pnlBarangayBadge.Width - 28;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (Session.Current == null) return;
            ShowSchedule();
            ReloadData();
            tmrRefresh.Start();
        }

        protected void ShowView(Control view, NavButton nav, string title)
        {
            foreach (Control c in pnlContent.Controls) c.Visible = (c == view);
            foreach (Control c in flowNav.Controls)
            {
                NavButton nb = c as NavButton;
                if (nb != null) nb.Active = (nb == nav);
            }
            lblPageTitle.Text = title;
            view.BringToFront();
        }

        protected void ShowSchedule() { ShowView(viewSchedule, navSchedule, "Schedule"); }
        protected void ShowHistory() { ShowView(viewHistory, navHistory, "History"); }

        protected virtual void ReloadData()
        {
            if (Session.Current == null) return;
            try
            {
                string barangay = FilterByBarangay ? Session.Current.Barangay : null;
                viewSchedule.SetData(ScheduleRepository.GetSchedules(ScheduleMode.Upcoming, barangay, null));
                viewHistory.SetData(ScheduleRepository.GetSchedules(ScheduleMode.History, barangay, null));
            }
            catch (Exception ex)
            {
                if (!errorShown)
                {
                    errorShown = true;
                    MessageBox.Show("Could not load schedules: " + ex.Message, "Database error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            tmrRefresh.Stop();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
