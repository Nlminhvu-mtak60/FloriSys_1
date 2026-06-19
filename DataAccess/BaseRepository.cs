using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FloriSys.Models;

namespace FloriSys.DataAccess
{
    public abstract class BaseRepository<T> where T : BaseEntity, new()
    {
        // Tên bảng trong CSDL
        public abstract string TableName { get; }

        // Tên cột khóa chính
        public abstract string IdColumn { get; }

        // Tiền tố mã tự động
        public abstract string IdPrefix { get; }

        // Thực thi câu lệnh SQL thô và map sang danh sách đối tượng T
        protected static List<T> GetList(string sql, List<SqlParameter> parms = null)
        {
            return DatabaseHelper.ExecuteRawList<T>(sql, parms?.ToArray());
        }

        // Thực thi SQL thô và map dòng đầu tiên sang đối tượng T
        protected static T GetSingle(string sql, SqlParameter[] parms)
        {
            return DatabaseHelper.ExecuteRawSingle<T>(sql, parms);
        }

        // Thực thi Stored Procedure và map sang danh sách đối tượng T
        protected static List<T> GetListFromSP(string spName, SqlParameter[] parms = null)
        {
            return DatabaseHelper.ExecuteList<T>(spName, parms);
        }

        // Thực thi Stored Procedure và map dòng đầu tiên sang đối tượng T
        protected static T GetSingleFromSP(string spName, SqlParameter[] parms)
        {
            return DatabaseHelper.ExecuteSingle<T>(spName, parms);
        }

        // Thực thi SP không trả về kết quả (INSERT/UPDATE/DELETE)
        protected static int ExecuteSP(string spName, SqlParameter[] parms)
        {
            return DatabaseHelper.ExecuteNonQuery(spName, parms);
        }

        // Thực thi lệnh SQL thô không trả về kết quả
        protected static int ExecuteSql(string sql, SqlParameter[] parms)
        {
            return DatabaseHelper.ExecuteRawNonQuery(sql, parms);
        }

        // Tạo mã tự động sử dụng thủ tục sp_SinhMa
        protected static string GenerateCode(string prefix, string table, string column)
        {
            return DatabaseHelper.GenerateCode(prefix, table, column);
        }

        // Tạo tham số SQL an toàn với giá trị null
        protected static SqlParameter NullableParam(string name, object value)
        {
            return new SqlParameter(name, value ?? DBNull.Value);
        }

        // Thực thi SQL thô và trả về DataTable
        protected static DataTable GetDataTable(string sql, SqlParameter[] parms = null)
        {
            return DatabaseHelper.ExecuteRawQuery(sql, parms);
        }

        // Lấy danh sách kèm bộ lọc từ khóa tùy chọn
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

        // Tạo một mã tự động mới cho đối tượng này
        public virtual string TaoMoi()
        {
            return GenerateCode(IdPrefix, TableName, IdColumn);
        }

        // Lấy một đối tượng duy nhất dựa vào khóa chính
        public virtual T LayTheoMa(string ma)
        {
            string sql = $"SELECT * FROM {TableName} WHERE {IdColumn} = @Id";
            return GetSingle(sql, new SqlParameter[] { new SqlParameter("@Id", ma) });
        }

        // Xóa đối tượng dựa vào khóa chính
        public virtual void Xoa(string ma)
        {
            string sql = $"DELETE FROM {TableName} WHERE {IdColumn} = @Id";
            ExecuteSql(sql, new SqlParameter[] { new SqlParameter("@Id", ma) });
        }
    }
}
