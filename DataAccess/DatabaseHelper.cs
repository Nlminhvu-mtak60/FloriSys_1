using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace FloriSys.DataAccess
{
    public class DatabaseHelper
    {
        // =====================================================
        // Generic Mapping Helpers (OOP)
        // =====================================================

        /// <summary>
        /// Thực thi Stored Procedure và map kết quả thành danh sách đối tượng.
        /// </summary>
        public static List<T> ExecuteList<T>(string spName, SqlParameter[] parameters = null) where T : new()
        {
            DataTable dt = ExecuteQuery(spName, parameters);
            return MapDataTable<T>(dt);
        }

        /// <summary>
        /// Thực thi Raw SQL và map kết quả thành danh sách đối tượng.
        /// </summary>
        public static List<T> ExecuteRawList<T>(string sql, SqlParameter[] parameters = null) where T : new()
        {
            DataTable dt = ExecuteRawQuery(sql, parameters);
            return MapDataTable<T>(dt);
        }

        /// <summary>
        /// Thực thi Stored Procedure và map dòng đầu tiên thành đối tượng (hoặc null).
        /// </summary>
        public static T ExecuteSingle<T>(string spName, SqlParameter[] parameters = null) where T : class, new()
        {
            DataTable dt = ExecuteQuery(spName, parameters);
            if (dt.Rows.Count == 0) return null;
            return MapDataRow<T>(dt.Rows[0], dt.Columns);
        }

        /// <summary>
        /// Thực thi Raw SQL và map dòng đầu tiên thành đối tượng (hoặc null).
        /// </summary>
        public static T ExecuteRawSingle<T>(string sql, SqlParameter[] parameters = null) where T : class, new()
        {
            DataTable dt = ExecuteRawQuery(sql, parameters);
            if (dt.Rows.Count == 0) return null;
            return MapDataRow<T>(dt.Rows[0], dt.Columns);
        }

        /// <summary>
        /// Map toàn bộ DataTable thành List of T bằng Reflection.
        /// </summary>
        private static List<T> MapDataTable<T>(DataTable dt) where T : new()
        {
            List<T> list = new List<T>();
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(MapDataRow<T>(row, dt.Columns, props));
            }
            return list;
        }

        /// <summary>
        /// Map 1 DataRow thành 1 object T bằng Reflection.
        /// </summary>
        private static T MapDataRow<T>(DataRow row, DataColumnCollection columns, PropertyInfo[] props = null) where T : new()
        {
            T obj = new T();
            if (props == null)
                props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                if (!columns.Contains(prop.Name)) continue;
                object value = row[prop.Name];
                if (value == DBNull.Value) continue;

                // Handle type conversion
                Type targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                prop.SetValue(obj, Convert.ChangeType(value, targetType));
            }
            return obj;
        }

        private static readonly string _connectionString;

        static DatabaseHelper()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["FloriSys"]?.ConnectionString
                ?? @"Server=.;Database=FloriSys;Integrated Security=True;TrustServerCertificate=True";
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public static DataTable ExecuteQuery(string spName, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public static DataTable ExecuteRawQuery(string sql, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public static int ExecuteNonQuery(string spName, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static int ExecuteRawNonQuery(string sql, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static object ExecuteScalar(string spName, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        public static string GenerateCode(string prefix, string table, string column)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("sp_SinhMa", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Prefix", prefix);
                    cmd.Parameters.AddWithValue("@Table", table);
                    cmd.Parameters.AddWithValue("@Column", column);
                    SqlParameter outParam = new SqlParameter("@NewCode", SqlDbType.NVarChar, 20)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outParam);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return outParam.Value.ToString();
                }
            }
        }
    }
}
