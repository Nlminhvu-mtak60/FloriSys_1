using System.Data;
using System.Data.SqlClient;

namespace FloriSys.DataAccess
{
    public class PhanHoiDAO
    {
        public static DataTable LayDanhSach(string maDon = "")
        {
            string sql = @"SELECT ph.MaPH, ph.MaDon, ph.NoiDung, ph.NgayGhi, ph.TrangThaiXuLy, ph.KetQuaXuLy,
                          kh.HoTen AS TenKH
                          FROM PHAN_HOI ph
                          INNER JOIN DON_HANG dh ON ph.MaDon = dh.MaDon
                          INNER JOIN KHACH_HANG kh ON dh.MaKH = kh.MaKH
                          WHERE 1=1";
            var parms = new System.Collections.Generic.List<SqlParameter>();
            if (!string.IsNullOrEmpty(maDon))
            {
                sql += " AND ph.MaDon = @MaDon";
                parms.Add(new SqlParameter("@MaDon", maDon));
            }
            sql += " ORDER BY ph.NgayGhi DESC";
            return DatabaseHelper.ExecuteRawQuery(sql, parms.ToArray());
        }

        public static string GhiNhan(string maDon, string noiDung)
        {
            string maPH = DatabaseHelper.GenerateCode("PH", "PHAN_HOI", "MaPH");
            DatabaseHelper.ExecuteNonQuery("sp_GhiNhanPhanHoi", new SqlParameter[]
            {
                new SqlParameter("@MaPH", maPH),
                new SqlParameter("@MaDon", maDon),
                new SqlParameter("@NoiDung", noiDung)
            });
            return maPH;
        }

        public static void CapNhatXuLy(string maPH, string trangThai, string ketQua)
        {
            string sql = "UPDATE PHAN_HOI SET TrangThaiXuLy=@TrangThai, KetQuaXuLy=@KetQua WHERE MaPH=@MaPH";
            DatabaseHelper.ExecuteRawNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@MaPH", maPH),
                new SqlParameter("@TrangThai", trangThai),
                new SqlParameter("@KetQua", ketQua)
            });
        }
    }
}
