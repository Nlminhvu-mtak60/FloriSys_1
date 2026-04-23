using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FloriSys.Models;

namespace FloriSys.DataAccess
{
    public class KhachHangDAO
    {
        public static List<KhachHang> LayDanhSach(string keyword = "")
        {
            string sql = @"SELECT kh.MaKH, kh.HoTen, kh.SoDienThoai, kh.DiaChi, kh.Email, kh.NgayTao,
                           (SELECT COUNT(*) FROM DON_HANG WHERE MaKH=kh.MaKH) AS TongDon
                           FROM KHACH_HANG kh WHERE 1=1";
            var parms = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " AND (kh.HoTen LIKE @Key OR kh.SoDienThoai LIKE @Key OR kh.Email LIKE @Key)";
                parms.Add(new SqlParameter("@Key", "%" + keyword + "%"));
            }
            sql += " ORDER BY kh.NgayTao DESC";
            return DatabaseHelper.ExecuteRawList<KhachHang>(sql, parms.ToArray());
        }

        public static KhachHang TimTheoSDT(string sdt)
        {
            string sql = "SELECT MaKH, HoTen, SoDienThoai, DiaChi, Email FROM KHACH_HANG WHERE SoDienThoai=@SDT";
            return DatabaseHelper.ExecuteRawSingle<KhachHang>(sql, new SqlParameter[] { new SqlParameter("@SDT", sdt) });
        }

        public static string ThemKhachHang(KhachHang kh)
        {
            string maKH = DatabaseHelper.GenerateCode("KH", "KHACH_HANG", "MaKH");
            string sql = @"INSERT INTO KHACH_HANG (MaKH, HoTen, SoDienThoai, DiaChi, Email) 
                          VALUES (@MaKH, @HoTen, @SDT, @DiaChi, @Email)";
            DatabaseHelper.ExecuteRawNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@MaKH", maKH),
                new SqlParameter("@HoTen", kh.HoTen),
                new SqlParameter("@SDT", kh.SoDienThoai),
                new SqlParameter("@DiaChi", (object)kh.DiaChi ?? DBNull.Value),
                new SqlParameter("@Email", (object)kh.Email ?? DBNull.Value)
            });
            return maKH;
        }

        public static void CapNhatKhachHang(KhachHang kh)
        {
            string sql = @"UPDATE KHACH_HANG SET HoTen=@HoTen, SoDienThoai=@SDT, DiaChi=@DiaChi, Email=@Email 
                          WHERE MaKH=@MaKH";
            DatabaseHelper.ExecuteRawNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@MaKH", kh.MaKH),
                new SqlParameter("@HoTen", kh.HoTen),
                new SqlParameter("@SDT", kh.SoDienThoai),
                new SqlParameter("@DiaChi", (object)kh.DiaChi ?? DBNull.Value),
                new SqlParameter("@Email", (object)kh.Email ?? DBNull.Value)
            });
        }

        public static void XoaKhachHang(string maKH)
        {
            // Check if customer has orders
            string sqlCheck = "SELECT COUNT(*) FROM DON_HANG WHERE MaKH=@MaKH";
            DataTable dt = DatabaseHelper.ExecuteRawQuery(sqlCheck, new SqlParameter[] { new SqlParameter("@MaKH", maKH) });
            if (dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > 0)
                throw new Exception("Không thể xóa khách hàng đã có đơn hàng.");

            string sql = "DELETE FROM KHACH_HANG WHERE MaKH=@MaKH";
            DatabaseHelper.ExecuteRawNonQuery(sql, new SqlParameter[] { new SqlParameter("@MaKH", maKH) });
        }
    }
}
