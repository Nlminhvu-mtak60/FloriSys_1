using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FloriSys.Models;

namespace FloriSys.DataAccess
{
    /// <summary>
    /// KhachHang repository - inherits BaseRepository&lt;KhachHang&gt;.
    /// Demonstrates: INHERITANCE, POLYMORPHISM (custom LayDanhSach with JOIN).
    /// </summary>
    public class KhachHangRepository : BaseRepository<KhachHang>
    {
        public override string TableName => "KHACH_HANG";
        public override string IdColumn => "MaKH";
        public override string IdPrefix => "KH";

        // POLYMORPHISM: Override with custom JOIN query (hides base)
        public new List<KhachHang> LayDanhSach(string keyword = "")
        {
            string sql = @"SELECT kh.MaKH, kh.HoTen, kh.SoDienThoai, kh.DiaChi, kh.Email, kh.NgayTao,
                           COUNT(dh.MaDon) AS TongDon
                           FROM KHACH_HANG kh
                           LEFT JOIN DON_HANG dh ON kh.MaKH = dh.MaKH
                           WHERE 1=1";
            var parms = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " AND (kh.HoTen LIKE @Key OR kh.SoDienThoai LIKE @Key OR kh.Email LIKE @Key)";
                parms.Add(new SqlParameter("@Key", "%" + keyword + "%"));
            }
            sql += " GROUP BY kh.MaKH, kh.HoTen, kh.SoDienThoai, kh.DiaChi, kh.Email, kh.NgayTao";
            sql += " ORDER BY kh.NgayTao DESC";
            return GetList(sql, parms);
        }

        public KhachHang TimTheoSDT(string sdt)
        {
            string sql = "SELECT MaKH, HoTen, SoDienThoai, DiaChi, Email FROM KHACH_HANG WHERE SoDienThoai=@SDT";
            return GetSingle(sql, new SqlParameter[] { new SqlParameter("@SDT", sdt) });
        }

        /// <summary>
        /// Find existing customer by phone, or create new one.
        /// ENCAPSULATION: hides the find-or-create logic.
        /// </summary>
        public string TimHoacTao(string hoTen, string sdt, string diaChi)
        {
            var kh = TimTheoSDT(sdt);
            if (kh != null) return kh.MaKH;
            return ThemKhachHang(new KhachHang
            {
                HoTen = hoTen,
                SoDienThoai = sdt,
                DiaChi = diaChi
            });
        }

        public string ThemKhachHang(KhachHang kh)
        {
            string maKH = TaoMoi();  // INHERITANCE: Uses base class method
            string sql = @"INSERT INTO KHACH_HANG (MaKH, HoTen, SoDienThoai, DiaChi, Email) 
                          VALUES (@MaKH, @HoTen, @SDT, @DiaChi, @Email)";
            ExecuteSql(sql, new SqlParameter[]
            {
                new SqlParameter("@MaKH", maKH),
                new SqlParameter("@HoTen", kh.HoTen),
                new SqlParameter("@SDT", kh.SoDienThoai),
                NullableParam("@DiaChi", kh.DiaChi),
                NullableParam("@Email", kh.Email)
            });
            return maKH;
        }

        public void CapNhatKhachHang(KhachHang kh)
        {
            string sql = @"UPDATE KHACH_HANG SET HoTen=@HoTen, SoDienThoai=@SDT, DiaChi=@DiaChi, Email=@Email 
                          WHERE MaKH=@MaKH";
            ExecuteSql(sql, new SqlParameter[]
            {
                new SqlParameter("@MaKH", kh.MaKH),
                new SqlParameter("@HoTen", kh.HoTen),
                new SqlParameter("@SDT", kh.SoDienThoai),
                NullableParam("@DiaChi", kh.DiaChi),
                NullableParam("@Email", kh.Email)
            });
        }

        public void XoaKhachHang(string maKH)
        {
            // Check if customer has orders
            string sqlCheck = "SELECT COUNT(*) FROM DON_HANG WHERE MaKH=@MaKH";
            DataTable dt = GetDataTable(sqlCheck, new SqlParameter[] { new SqlParameter("@MaKH", maKH) });
            if (dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > 0)
                throw new Exception("Không thể xóa khách hàng đã có đơn hàng.");

            string sql = "DELETE FROM KHACH_HANG WHERE MaKH=@MaKH";
            ExecuteSql(sql, new SqlParameter[] { new SqlParameter("@MaKH", maKH) });
        }
    }
}
