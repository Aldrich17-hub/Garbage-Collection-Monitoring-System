#nullable disable
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GarbageCollectionMonitoringSystem
{
    /// <summary>Municipal Official view: Schedule, + Add Schedule, History.</summary>
    public partial class OfficialDashboardForm : DashboardBaseForm
    {
        public OfficialDashboardForm()
        {
            InitializeComponent();
            navAdd.Click += delegate { ShowAdd(); };
            btnPublish.Click += btnPublish_Click;
            pnlAddView.Resize += delegate { LayoutAdd(); };
            rdoBio.Checked = true;
            dtpDate.Value = DateTime.Today.AddDays(1);
            dtpTime.Value = DateTime.Today.AddHours(7);
            LayoutAdd();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (Session.Current == null) return;
            LoadFormLists();
        }

        private void LayoutAdd()
        {
            int left = Math.Max(24, (pnlAddView.Width - cardAdd.Width) / 2);
            lblAddEyebrow.Location = new Point(left, 26);
            lblAddTitle.Location = new Point(left - 2, 42);
            lblAddSub.Location = new Point(left, 82);
            cardAdd.Location = new Point(left, 118);
        }

        private void LoadFormLists()
        {
            try
            {
                cmbBarangay.Items.Clear();
                foreach (string b in ScheduleRepository.GetBarangays()) cmbBarangay.Items.Add(b);
                cmbBarangay.Text = Session.Current.Barangay;

                cmbDriver.Items.Clear();
                foreach (DriverItem d in ScheduleRepository.GetDrivers()) cmbDriver.Items.Add(d);
                if (cmbDriver.Items.Count > 0) cmbDriver.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                SetMessage("Could not load lists: " + ex.Message, false);
            }
        }

        private void ShowAdd()
        {
            ShowView(pnlAddView, navAdd, "Add Schedule");
            RefreshNextBatch();
            SetMessage("", true);
        }

        private void RefreshNextBatch()
        {
            try { lblNextBatch.Text = ScheduleRepository.NextBatchId(); }
            catch { lblNextBatch.Text = "BCH-????"; }
        }

        private void SetMessage(string text, bool ok)
        {
            lblAddMessage.ForeColor = ok ? Theme.Accent : Theme.Danger;
            lblAddMessage.Text = text;
        }

        private void btnPublish_Click(object sender, EventArgs e)
        {
            string barangay = cmbBarangay.Text.Trim();
            string area = txtArea.Text.Trim();
            DriverItem driver = cmbDriver.SelectedItem as DriverItem;
            string category = rdoBio.Checked ? "Biodegradable"
                            : rdoNon.Checked ? "Non-Biodegradable"
                            : rdoRec.Checked ? "Recyclable"
                            : rdoHaz.Checked ? "Hazardous" : null;

            if (barangay.Length == 0) { SetMessage("Please enter the barangay.", false); return; }
            if (area.Length == 0) { SetMessage("Please enter the collection area / location.", false); txtArea.Focus(); return; }
            if (driver == null) { SetMessage("Please choose an assigned driver.", false); return; }
            if (category == null) { SetMessage("Please select a waste category.", false); return; }
            if (dtpDate.Value.Date < DateTime.Today) { SetMessage("The collection date cannot be in the past.", false); return; }

            try
            {
                string batchId = ScheduleRepository.NextBatchId();
                ScheduleRepository.CreateSchedule(batchId, barangay, area, driver, category,
                    dtpDate.Value.Date, dtpTime.Value.TimeOfDay);

                MessageBox.Show("Batch " + batchId + " has been published to the schedule.", "Schedule published",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtArea.Clear();
                ReloadData();
                ShowSchedule();
            }
            catch (Exception ex)
            {
                SetMessage("Could not publish the schedule: " + ex.Message, false);
            }
        }
    }
}
