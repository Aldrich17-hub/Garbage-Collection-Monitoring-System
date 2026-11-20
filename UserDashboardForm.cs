#nullable disable
namespace GarbageCollectionMonitoringSystem
{
    /// <summary>Resident view: Schedule + History for the resident's barangay (read-only).</summary>
    public partial class UserDashboardForm : DashboardBaseForm
    {
        public UserDashboardForm()
        {
            InitializeComponent();
        }

        protected override bool FilterByBarangay { get { return true; } }
    }
}
