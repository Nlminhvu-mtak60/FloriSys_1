using System.Data;
using System.Data.SqlClient;

namespace FloriSys.DataAccess
{
    public class KhachHangDAO
    {
        public static DataTable LayDanhSach(string keyword = "")
        {
            string sql = @"SELECT kh.MaKH, kh.HoTen, kh.SoDienThoai, kh.DiaChi, kh.Email, kh.NgayTao,
                           (SELECT COUNT(*) FROM DON_HANG WHERE MaKH=kh.MaKH) AS TongDon
                           FROM KHACH_HANG kh WHERE 1=1";
            var parms = new System.Collections.Generic.List<SqlParameter>();
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " AND (kh.HoTen LIKE @Key OR kh.SoDienThoai LIKE @Key OR kh.Email LIKE @Key)";
                parms.Add(new SqlParameter("@Key", "%" + keyword + "%"));
            }
            sql += " ORDER BY kh.NgayTao DESC";
            return DatabaseHelper.ExecuteRawQuery(sql, parms.ToArray());
        }

        public static DataTable TimTheoSDT(string sdt)
        {
            string sql = "SELECT MaKH, HoTen, SoDienThoai, DiaChi, Email FROM KHACH_HANG WHERE SoDienThoai=@SDT";
            return DatabaseHelper.ExecuteRawQuery(sql, new SqlParameter[] { new SqlParameter("@SDT", sdt) });
        }

        public static string ThemKhachHang(string hoTen, string sdt, string diaChi = null, string email = null)
        {
            string maKH = DatabaseHelper.GenerateCode("KH", "KHACH_HANG", "MaKH");
            string sql = @"INSERT INTO KHACH_HANG (MaKH, HoTen, SoDienThoai, DiaChi, Email) 
                          VALUES (@MaKH, @HoTen, @SDT, @DiaChi, @Email)";
            DatabaseHelper.ExecuteRawNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@MaKH", maKH),
                new SqlParameter("@HoTen", hoTen),
                new SqlParameter("@SDT", sdt),
                new SqlParameter("@DiaChi", (object)diaChi ?? System.DBNull.Value),
                new SqlParameter("@Email", (object)email ?? System.DBNull.Value)
            });
            return maKH;
        }
    }
}
