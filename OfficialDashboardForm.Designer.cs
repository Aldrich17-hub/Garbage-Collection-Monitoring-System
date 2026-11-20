#nullable disable
using System.Drawing;
using System.Windows.Forms;

namespace GarbageCollectionMonitoringSystem
{
    partial class OfficialDashboardForm
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();
            Color white = Color.White;
            Color headerGreen = ColorTranslator.FromHtml("#0A1914");

            this.navAdd = new NavButton();
            this.navAdd.Text = "\uFF0B   Add Schedule";
            this.flowNav.Controls.Add(this.navAdd);
            this.flowNav.Controls.SetChildIndex(this.navAdd, 2);   // Schedule, Add Schedule, History

            // ---- page heading ----
            this.lblAddEyebrow = Theme.MakeLabel("OFFICIALS WORKSPACE", 7.5f, FontStyle.Bold, Theme.Accent, Theme.Background, 0, 0);
            this.lblAddTitle = Theme.MakeLabel("Create a collection schedule", 18f, FontStyle.Bold, Theme.TextDark, Theme.Background, 0, 0);
            this.lblAddSub = Theme.MakeLabel("Assign a collection team and set the service details for a new batch.", 9f, FontStyle.Regular, Theme.TextMuted, Theme.Background, 0, 0);

            // ---- card ----
            this.cardAdd = new RoundedPanel();
            this.cardAdd.Radius = 12;
            this.cardAdd.BackColor = white;
            this.cardAdd.Size = new Size(640, 380);

            this.pnlBatchHeader = new RoundedPanel();
            this.pnlBatchHeader.Radius = 12;
            this.pnlBatchHeader.DrawBorder = false;
            this.pnlBatchHeader.BackColor = headerGreen;
            this.pnlBatchHeader.Bounds = new Rectangle(0, 0, 640, 76);
            this.pnlBatchHeaderSquare = new Panel();
            this.pnlBatchHeaderSquare.BackColor = headerGreen;
            this.pnlBatchHeaderSquare.Bounds = new Rectangle(0, 50, 640, 26);
            this.lblNextBatchCaption = Theme.MakeLabel("Next batch number", 8f, FontStyle.Regular, Theme.Mint, headerGreen, 24, 12);
            this.lblNextBatch = Theme.MakeLabel("BCH-0000", 16f, FontStyle.Bold, Color.White, headerGreen, 22, 28);
            this.pnlAutoTag = new RoundedPanel();
            this.pnlAutoTag.Radius = 10;
            this.pnlAutoTag.DrawBorder = false;
            this.pnlAutoTag.BackColor = Theme.Mint;
            this.pnlAutoTag.Bounds = new Rectangle(500, 26, 116, 22);
            this.lblAutoTag = Theme.MakeLabel("Auto-incremented", 7.5f, FontStyle.Bold, headerGreen, Theme.Mint, 0, 0);
            this.lblAutoTag.AutoSize = false;
            this.lblAutoTag.Dock = DockStyle.Fill;
            this.lblAutoTag.TextAlign = ContentAlignment.MiddleCenter;
            this.pnlAutoTag.Controls.Add(this.lblAutoTag);
            this.pnlBatchHeader.Controls.Add(this.lblNextBatchCaption);
            this.pnlBatchHeader.Controls.Add(this.lblNextBatch);
            this.pnlBatchHeader.Controls.Add(this.pnlAutoTag);

            this.lblBarangayCap = Theme.MakeLabel("Barangay", 8.25f, FontStyle.Bold, Theme.TextDark, white, 24, 94);
            this.cmbBarangay = Theme.MakeCombo(24, 114, 280);
            this.cmbBarangay.DropDownStyle = ComboBoxStyle.DropDown;
            this.lblAreaCap = Theme.MakeLabel("Collection area / location", 8.25f, FontStyle.Bold, Theme.TextDark, white, 328, 94);
            this.txtArea = Theme.MakeTextBox(328, 114, 288, "e.g. Purok 7 - East Road");

            this.lblDriverCap = Theme.MakeLabel("Assigned driver", 8.25f, FontStyle.Bold, Theme.TextDark, white, 24, 160);
            this.cmbDriver = Theme.MakeCombo(24, 180, 280);
            this.lblDateCap = Theme.MakeLabel("Collection date", 8.25f, FontStyle.Bold, Theme.TextDark, white, 328, 160);
            this.dtpDate = new DateTimePicker();
            this.dtpDate.Format = DateTimePickerFormat.Short;
            this.dtpDate.Font = Theme.F(10f);
            this.dtpDate.Bounds = new Rectangle(328, 180, 150, 28);
            this.lblTimeCap = Theme.MakeLabel("Start time", 8.25f, FontStyle.Bold, Theme.TextDark, white, 494, 160);
            this.dtpTime = new DateTimePicker();
            this.dtpTime.Format = DateTimePickerFormat.Time;
            this.dtpTime.ShowUpDown = true;
            this.dtpTime.Font = Theme.F(10f);
            this.dtpTime.Bounds = new Rectangle(494, 180, 122, 28);

            this.lblCategoryCap = Theme.MakeLabel("Waste category", 8.25f, FontStyle.Bold, Theme.TextDark, white, 24, 226);
            this.rdoBio = Theme.MakeChip("Biodegradable", 24, 248, 140, 36);
            this.rdoNon = Theme.MakeChip("Non-Biodegradable", 176, 248, 140, 36);
            this.rdoRec = Theme.MakeChip("Recyclable", 328, 248, 140, 36);
            this.rdoHaz = Theme.MakeChip("Hazardous", 480, 248, 136, 36);

            this.pnlFooterLine = new Panel();
            this.pnlFooterLine.BackColor = Theme.Border;
            this.pnlFooterLine.Bounds = new Rectangle(0, 308, 640, 1);
            this.lblAddMessage = Theme.MakeLabel("", 8.5f, FontStyle.Regular, Theme.Danger, white, 24, 322);
            this.lblAddMessage.AutoSize = false;
            this.lblAddMessage.Size = new Size(400, 40);
            this.btnPublish = Theme.MakeButton("Publish schedule   \u203A", 436, 322, 180, 42, Theme.DarkGreen, Color.White, Color.Empty);

            this.cardAdd.Controls.AddRange(new Control[] {
                pnlBatchHeader, pnlBatchHeaderSquare,
                lblBarangayCap, cmbBarangay, lblAreaCap, txtArea,
                lblDriverCap, cmbDriver, lblDateCap, dtpDate, lblTimeCap, dtpTime,
                lblCategoryCap, rdoBio, rdoNon, rdoRec, rdoHaz,
                pnlFooterLine, lblAddMessage, btnPublish });
            this.pnlBatchHeaderSquare.BringToFront();
            this.lblNextBatch.Parent = this.pnlBatchHeader;

            this.pnlAddView = new Panel();
            this.pnlAddView.Dock = DockStyle.Fill;
            this.pnlAddView.BackColor = Theme.Background;
            this.pnlAddView.Visible = false;
            this.pnlAddView.Controls.AddRange(new Control[] { lblAddEyebrow, lblAddTitle, lblAddSub, cardAdd });
            this.pnlContent.Controls.Add(this.pnlAddView);

            this.Name = "OfficialDashboardForm";
            this.Text = "Municipal Official Dashboard - Garbage Collection Monitoring System";
            this.ResumeLayout(false);
        }

        private NavButton navAdd;
        private Panel pnlAddView;
        private Panel pnlBatchHeaderSquare;
        private Panel pnlFooterLine;
        private RoundedPanel cardAdd;
        private RoundedPanel pnlBatchHeader;
        private RoundedPanel pnlAutoTag;
        private Label lblAddEyebrow, lblAddTitle, lblAddSub, lblNextBatchCaption, lblNextBatch, lblAutoTag;
        private Label lblBarangayCap, lblAreaCap, lblDriverCap, lblDateCap, lblTimeCap, lblCategoryCap, lblAddMessage;
        private ComboBox cmbBarangay, cmbDriver;
        private TextBox txtArea;
        private DateTimePicker dtpDate, dtpTime;
        private RadioButton rdoBio, rdoNon, rdoRec, rdoHaz;
        private RoundedButton btnPublish;
    }
}
