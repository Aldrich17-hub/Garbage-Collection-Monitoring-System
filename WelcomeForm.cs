#nullable disable
using System;
using System.Windows.Forms;

namespace GarbageCollectionMonitoringSystem
{
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
            btnSignIn.Click += btnSignIn_Click;
            btnCreate.Click += btnCreate_Click;
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            Hide();
            using (LoginForm login = new LoginForm())
            {
                if (login.ShowDialog() == DialogResult.OK) OpenDashboard();
            }
            Show();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Hide();
            using (RegisterForm register = new RegisterForm())
            {
                register.ShowDialog();
            }
            Show();
        }

        /// <summary>Routes the signed-in user to the dashboard that matches their role.</summary>
        private void OpenDashboard()
        {
            Form dashboard;
            switch (Session.Current.Role)
            {
                case "Municipal Official": dashboard = new OfficialDashboardForm(); break;
                case "Collection Driver": dashboard = new DriverDashboardForm(); break;
                default: dashboard = new UserDashboardForm(); break;
            }
            using (dashboard) dashboard.ShowDialog();
            Session.Current = null;
        }
    }
}
