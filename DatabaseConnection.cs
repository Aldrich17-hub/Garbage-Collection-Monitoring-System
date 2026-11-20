#nullable disable
using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;

namespace GarbageCollectionMonitoringSystem
{
    /// <summary>Reusable static helper for the local XAMPP / phpMyAdmin MySQL server.</summary>
    public static class DatabaseConnection
    {
        public const string ConnectionString =
            "Server=localhost;Database=garbage_collection_db;Uid=root;Pwd=;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public static MySqlConnection OpenConnection()
        {
            MySqlConnection conn = GetConnection();
            conn.Open();
            return conn;
        }

        public static bool TestConnection(out string error)
        {
            try
            {
                using (MySqlConnection conn = OpenConnection()) { }
                error = null;
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public static MySqlParameter P(string name, object value)
        {
            return new MySqlParameter(name, value ?? DBNull.Value);
        }

        /// <summary>Runs a parameterized SELECT and returns the rows.</summary>
        public static DataTable ExecuteQuery(string sql, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = OpenConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                if (parameters != null && parameters.Length > 0) cmd.Parameters.AddRange(parameters);
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }

        /// <summary>Runs a parameterized INSERT / UPDATE / DELETE and returns the affected row count.</summary>
        public static int ExecuteNonQuery(string sql, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = OpenConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                if (parameters != null && parameters.Length > 0) cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>Runs a parameterized query and returns the first column of the first row.</summary>
        public static object ExecuteScalar(string sql, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = OpenConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                if (parameters != null && parameters.Length > 0) cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteScalar();
            }
        }

        /// <summary>SHA-256 hex (lowercase) - matches MySQL SHA2(x,256) used in database.sql.</summary>
        public static string HashPassword(string password)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password ?? string.Empty));
            return Convert.ToHexString(bytes).ToLowerInvariant();
        }
    }
}
