#nullable disable
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace GarbageCollectionMonitoringSystem
{
    public class UserSession
    {
        public int UserId { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Barangay { get; set; }

        public string FullName { get { return ((FirstName ?? "") + " " + (LastName ?? "")).Trim(); } }

        public string Initials
        {
            get
            {
                string a = string.IsNullOrEmpty(FirstName) ? "" : FirstName.Substring(0, 1);
                string b = string.IsNullOrEmpty(LastName) ? "" : LastName.Substring(0, 1);
                return (a + b).ToUpperInvariant();
            }
        }

        public string RoleLabel
        {
            get
            {
                if (Role == "Resident") return "User";
                if (Role == "Municipal Official") return "Official";
                return "Collection Driver";
            }
        }
    }

    public static class Session
    {
        public static UserSession Current { get; set; }
    }

    public enum ScheduleMode { Upcoming, History, All }

    public class ScheduleRow
    {
        public string BatchId;
        public DateTime Date;
        public TimeSpan Time;
        public string Category;
        public string Area;
        public string Driver;
        public string Status;

        public string WhenText
        {
            get { return Date.ToString("MMM dd, yyyy") + "\n" + DateTime.Today.Add(Time).ToString("h:mm tt"); }
        }
    }

    public class DriverItem
    {
        public string First;
        public string Middle;
        public string Last;
        public override string ToString() { return ((First ?? "") + " " + (Last ?? "")).Trim(); }
    }

    public static class ScheduleRepository
    {
        public static List<ScheduleRow> GetSchedules(ScheduleMode mode, string barangay, string driverName)
        {
            string sql =
                "SELECT s.Batch_id AS batch, s.`Date` AS d, s.`Time` AS t, s.Garbage_category AS cat, " +
                "p.Street AS area, TRIM(CONCAT_WS(' ', gc.First_name, gc.Last_name)) AS driver, s.Status AS st " +
                "FROM Schedule s " +
                "JOIN Place p ON p.Location_id = s.Location_id " +
                "JOIN Garbage_Collectors gc ON gc.Batch_id = s.Batch_id WHERE 1=1";

            List<MySqlParameter> ps = new List<MySqlParameter>();
            if (mode == ScheduleMode.Upcoming) sql += " AND s.Status IN ('Scheduled','Ongoing')";
            else if (mode == ScheduleMode.History) sql += " AND s.Status IN ('Completed','Missed')";

            if (!string.IsNullOrEmpty(barangay))
            {
                sql += " AND p.Barangay = @b";
                ps.Add(DatabaseConnection.P("@b", barangay));
            }
            if (!string.IsNullOrEmpty(driverName))
            {
                sql += " AND TRIM(CONCAT_WS(' ', gc.First_name, gc.Last_name)) = @drv";
                ps.Add(DatabaseConnection.P("@drv", driverName));
            }

            if (mode == ScheduleMode.History) sql += " ORDER BY s.`Date` DESC, s.`Time` DESC";
            else if (mode == ScheduleMode.All) sql += " ORDER BY (s.Status IN ('Completed','Missed')), s.`Date`, s.`Time`";
            else sql += " ORDER BY s.`Date`, s.`Time`";

            DataTable dt = DatabaseConnection.ExecuteQuery(sql, ps.ToArray());
            List<ScheduleRow> list = new List<ScheduleRow>();
            foreach (DataRow r in dt.Rows)
            {
                TimeSpan time;
                if (r["t"] is TimeSpan) time = (TimeSpan)r["t"];
                else TimeSpan.TryParse(Convert.ToString(r["t"]), out time);

                list.Add(new ScheduleRow
                {
                    BatchId = Convert.ToString(r["batch"]),
                    Date = Convert.ToDateTime(r["d"]),
                    Time = time,
                    Category = Convert.ToString(r["cat"]),
                    Area = Convert.ToString(r["area"]),
                    Driver = Convert.ToString(r["driver"]),
                    Status = Convert.ToString(r["st"])
                });
            }
            return list;
        }

        public static string NextBatchId()
        {
            object o = DatabaseConnection.ExecuteScalar(
                "SELECT MAX(CAST(SUBSTRING(Batch_id, 5) AS UNSIGNED)) FROM Garbage_Collectors WHERE Batch_id LIKE 'BCH-%'");
            int n = (o == null || o == DBNull.Value) ? 0 : Convert.ToInt32(o);
            return "BCH-" + (n + 1).ToString("D4");
        }

        public static List<DriverItem> GetDrivers()
        {
            DataTable dt = DatabaseConnection.ExecuteQuery(
                "SELECT DISTINCT First_name AS f, IFNULL(Middle_name,'') AS m, Last_name AS l FROM Garbage_Collectors " +
                "UNION SELECT First_name, IFNULL(Middle_name,''), Last_name FROM `User` WHERE User_Role = 'Collection Driver' " +
                "ORDER BY f, l");
            List<DriverItem> list = new List<DriverItem>();
            foreach (DataRow r in dt.Rows)
                list.Add(new DriverItem { First = Convert.ToString(r["f"]), Middle = Convert.ToString(r["m"]), Last = Convert.ToString(r["l"]) });
            return list;
        }

        public static List<string> GetBarangays()
        {
            DataTable dt = DatabaseConnection.ExecuteQuery("SELECT DISTINCT Barangay AS b FROM Place ORDER BY b");
            List<string> list = new List<string>();
            foreach (DataRow r in dt.Rows) list.Add(Convert.ToString(r["b"]));
            return list;
        }

        public static int UpdateStatus(string batchId, string status)
        {
            string history = status + " on " + DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            return DatabaseConnection.ExecuteNonQuery(
                "UPDATE Schedule SET Status = @s, History = @h WHERE Batch_id = @b",
                DatabaseConnection.P("@s", status),
                DatabaseConnection.P("@h", history),
                DatabaseConnection.P("@b", batchId));
        }

        private static MySqlCommand Cmd(MySqlConnection c, MySqlTransaction t, string sql, params object[] kv)
        {
            MySqlCommand cmd = new MySqlCommand(sql, c, t);
            for (int i = 0; i + 1 < kv.Length; i += 2)
                cmd.Parameters.AddWithValue((string)kv[i], kv[i + 1] ?? DBNull.Value);
            return cmd;
        }

        private static long FindOrCreate(MySqlConnection c, MySqlTransaction t, string selectSql, string insertSql, params object[] kv)
        {
            using (MySqlCommand find = Cmd(c, t, selectSql, kv))
            {
                object o = find.ExecuteScalar();
                if (o != null && o != DBNull.Value) return Convert.ToInt64(o);
            }
            using (MySqlCommand ins = Cmd(c, t, insertSql, kv))
            {
                ins.ExecuteNonQuery();
                return ins.LastInsertedId;
            }
        }

        /// <summary>Creates Place / Garbage / Garbage_Collectors / Schedule rows for a new batch in one transaction.</summary>
        public static void CreateSchedule(string batchId, string barangay, string area, DriverItem driver,
                                          string category, DateTime date, TimeSpan time)
        {
            using (MySqlConnection conn = DatabaseConnection.OpenConnection())
            using (MySqlTransaction tx = conn.BeginTransaction())
            {
                try
                {
                    long locationId = FindOrCreate(conn, tx,
                        "SELECT Location_id FROM Place WHERE Street = @a AND Barangay = @b LIMIT 1",
                        "INSERT INTO Place (Street, Barangay) VALUES (@a, @b)",
                        "@a", area, "@b", barangay);

                    long garbageId = FindOrCreate(conn, tx,
                        "SELECT Garbage_id FROM Garbage WHERE Location_id = @l AND Garbage_category = @c LIMIT 1",
                        "INSERT INTO Garbage (Location_id, Garbage_category) VALUES (@l, @c)",
                        "@l", locationId, "@c", category);

                    using (MySqlCommand c1 = Cmd(conn, tx,
                        "INSERT INTO Garbage_Collectors (Batch_id, Garbage_id, Location_id, First_name, Middle_name, Last_name) " +
                        "VALUES (@bid, @g, @l, @f, @m, @ln)",
                        "@bid", batchId, "@g", garbageId, "@l", locationId,
                        "@f", driver.First, "@m", driver.Middle, "@ln", driver.Last))
                    {
                        c1.ExecuteNonQuery();
                    }

                    using (MySqlCommand c2 = Cmd(conn, tx,
                        "INSERT INTO Schedule (Batch_id, Location_id, Garbage_id, `Date`, `Time`, Status, History, Garbage_category) " +
                        "VALUES (@bid, @l, @g, @d, @t, 'Scheduled', '', @c)",
                        "@bid", batchId, "@l", locationId, "@g", garbageId,
                        "@d", date.Date, "@t", time, "@c", category))
                    {
                        c2.ExecuteNonQuery();
                    }

                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }
    }
}
