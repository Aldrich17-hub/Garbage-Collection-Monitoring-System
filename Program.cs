#nullable disable
using System;
using System.Windows.Forms;

namespace GarbageCollectionMonitoringSystem
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            ApplicationConfiguration.Initialize();

            string error;
            if (!DatabaseConnection.TestConnection(out error))
            {
                MessageBox.Show(
                    "Could not connect to MySQL.\n\nStart MySQL in the XAMPP Control Panel and import database.sql in phpMyAdmin.\n\n" + error,
                    "Garbage Collection Monitoring System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Application.Run(new WelcomeForm());
        }
    }
}
