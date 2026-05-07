using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FloriSys.Models;

namespace FloriSys.DataAccess
{
    /// <summary>
    /// Generic base repository implementing common CRUD operations.
    /// Demonstrates: INHERITANCE (all repositories inherit this),
    /// ABSTRACTION (abstract properties TableName, IdColumn, IdPrefix),
    /// POLYMORPHISM (virtual methods LayDanhSach, TaoMoi that subclasses override),
    /// ENCAPSULATION (protected helper methods hide DB details from subclasses).
    /// </summary>
    public abstract class BaseRepository<T> where T : BaseEntity, new()
    {
        // ============================================================
        // ABSTRACTION: Each subclass MUST define its own table metadata
        // ============================================================

        /// <summary>Database table name (e.g. "SAN_PHAM")</summary>
        public abstract string TableName { get; }

        /// <summary>Primary key column name (e.g. "MaSP")</summary>
        public abstract string IdColumn { get; }

        /// <summary>Auto-code prefix (e.g. "SP")</summary>
        public abstract string IdPrefix { get; }

        // ============================================================
        // ENCAPSULATION: Protected helper methods hide DB complexity
        // ============================================================

        /// <summary>Execute raw SQL and map to list of T</summary>
        protected static List<T> GetList(string sql, List<SqlParameter> parms = null)
        {
            return DatabaseHelper.ExecuteRawList<T>(sql, parms?.ToArray());
        }

        /// <summary>Execute raw SQL and map first row to T</summary>
        protected static T GetSingle(string sql, SqlParameter[] parms)
        {
            return DatabaseHelper.ExecuteRawSingle<T>(sql, parms);
        }

        /// <summary>Execute stored procedure and map to list of T</summary>
        protected static List<T> GetListFromSP(string spName, SqlParameter[] parms = null)
        {
            return DatabaseHelper.ExecuteList<T>(spName, parms);
        }

        /// <summary>Execute stored procedure and map first row to T</summary>
        protected static T GetSingleFromSP(string spName, SqlParameter[] parms)
        {
            return DatabaseHelper.ExecuteSingle<T>(spName, parms);
        }

        /// <summary>Execute SP non-query (INSERT/UPDATE/DELETE)</summary>
        protected static int ExecuteSP(string spName, SqlParameter[] parms)
        {
            return DatabaseHelper.ExecuteNonQuery(spName, parms);
        }

        /// <summary>Execute raw SQL non-query</summary>
        protected static int ExecuteSql(string sql, SqlParameter[] parms)
        {
            return DatabaseHelper.ExecuteRawNonQuery(sql, parms);
        }

        /// <summary>Generate auto-code using sp_SinhMa</summary>
        protected static string GenerateCode(string prefix, string table, string column)
        {
            return DatabaseHelper.GenerateCode(prefix, table, column);
        }

        /// <summary>Build null-safe SQL parameter</summary>
        protected static SqlParameter NullableParam(string name, object value)
        {
            return new SqlParameter(name, value ?? DBNull.Value);
        }

        /// <summary>Execute raw SQL and return DataTable</summary>
        protected static DataTable GetDataTable(string sql, SqlParameter[] parms = null)
        {
            return DatabaseHelper.ExecuteRawQuery(sql, parms);
        }

        // ============================================================
        // POLYMORPHISM: Virtual methods that subclasses can override
        // ============================================================

        /// <summary>
        /// Get list with optional keyword filter.
        /// Default implementation uses WHERE 1=1 + LIKE on IdColumn.
        /// Subclasses SHOULD override this for custom JOIN queries.
        /// </summary>
        public virtual List<T> LayDanhSach(string keyword = "")
        {
            string sql = $"SELECT * FROM {TableName} WHERE 1=1";
            var parms = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += $" AND {IdColumn} LIKE @Key";
                parms.Add(new SqlParameter("@Key", "%" + keyword + "%"));
            }
            sql += $" ORDER BY {IdColumn}";
            return GetList(sql, parms);
        }

        /// <summary>
        /// Generate a new auto-code for this entity.
        /// Uses the abstract IdPrefix, TableName, IdColumn properties.
        /// </summary>
        public virtual string TaoMoi()
        {
            return GenerateCode(IdPrefix, TableName, IdColumn);
        }

        /// <summary>
        /// Get a single entity by its primary key.
        /// </summary>
        public virtual T LayTheoMa(string ma)
        {
            string sql = $"SELECT * FROM {TableName} WHERE {IdColumn} = @Id";
            return GetSingle(sql, new SqlParameter[] { new SqlParameter("@Id", ma) });
        }
    }
}
