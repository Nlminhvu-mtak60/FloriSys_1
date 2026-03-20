using System.Data;
using System.Data.SqlClient;

namespace FloriSys.DataAccess
{
    public class NhanVienDAO
    {
        public static DataTable DangNhap(string taiKhoan, string matKhauHash)
        {
            return DatabaseHelper.ExecuteQuery("sp_DangNhap", new SqlParameter[]
            {
                new SqlParameter("@TaiKhoan", taiKhoan),
                new SqlParameter("@MatKhau", matKhauHash)
            });
        }

        public static bool DoiMatKhau(string maNV, string matKhauCuHash, string matKhauMoiHash)
        {
            DataTable dt = DatabaseHelper.ExecuteQuery("sp_DoiMatKhau", new SqlParameter[]
            {
                new SqlParameter("@MaNV", maNV),
                new SqlParameter("@MatKhauCu", matKhauCuHash),
                new SqlParameter("@MatKhauMoi", matKhauMoiHash)
            });
            return dt.Rows.Count > 0 && dt.Rows[0]["KetQua"].ToString() == "1";
        }

        public static DataTable LayDanhSach(string keyword = "", string chucVu = "", string trangThai = "")
        {
            string sql = @"SELECT MaNV, HoTen, ChucVu, SoDienThoai, TaiKhoan, TrangThai 
                          FROM NHAN_VIEN WHERE 1=1";
            var parms = new System.Collections.Generic.List<SqlParameter>();
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " AND (HoTen LIKE @Key OR SoDienThoai LIKE @Key OR TaiKhoan LIKE @Key)";
                parms.Add(new SqlParameter("@Key", "%" + keyword + "%"));
            }
            if (!string.IsNullOrEmpty(chucVu))
            {
                sql += " AND ChucVu = @ChucVu";
                parms.Add(new SqlParameter("@ChucVu", chucVu));
            }
            if (!string.IsNullOrEmpty(trangThai))
            {
                sql += " AND TrangThai = @TrangThai";
                parms.Add(new SqlParameter("@TrangThai", trangThai));
            }
            sql += " ORDER BY MaNV";
            return DatabaseHelper.ExecuteRawQuery(sql, parms.ToArray());
        }

        public static void ThemNhanVien(string maNV, string hoTen, string chucVu, string sdt, string taiKhoan, string matKhauHash)
        {
            string sql = @"INSERT INTO NHAN_VIEN (MaNV, HoTen, ChucVu, SoDienThoai, TaiKhoan, MatKhau) 
                          VALUES (@MaNV, @HoTen, @ChucVu, @SoDienThoai, @TaiKhoan, @MatKhau)";
            DatabaseHelper.ExecuteRawNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@MaNV", maNV),
                new SqlParameter("@HoTen", hoTen),
                new SqlParameter("@ChucVu", chucVu),
                new SqlParameter("@SoDienThoai", sdt),
                new SqlParameter("@TaiKhoan", taiKhoan),
                new SqlParameter("@MatKhau", matKhauHash)
            });
        }

        public static void CapNhatNhanVien(string maNV, string hoTen, string chucVu, string sdt)
        {
            string sql = @"UPDATE NHAN_VIEN SET HoTen=@HoTen, ChucVu=@ChucVu, SoDienThoai=@SoDienThoai WHERE MaNV=@MaNV";
            DatabaseHelper.ExecuteRawNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@MaNV", maNV),
                new SqlParameter("@HoTen", hoTen),
                new SqlParameter("@ChucVu", chucVu),
                new SqlParameter("@SoDienThoai", sdt)
            });
        }

        public static void CapNhatTrangThai(string maNV, string trangThai)
        {
            string sql = "UPDATE NHAN_VIEN SET TrangThai=@TrangThai WHERE MaNV=@MaNV";
            DatabaseHelper.ExecuteRawNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@MaNV", maNV),
                new SqlParameter("@TrangThai", trangThai)
            });
        }

        public static DataTable LayShippers()
        {
            string sql = "SELECT MaNV, HoTen FROM NHAN_VIEN WHERE ChucVu=N'Shipper' AND TrangThai=N'DangLam'";
            return DatabaseHelper.ExecuteRawQuery(sql);
        }
    }
}
