using System;
using System.Data;
using System.Data.SqlClient;

namespace FloriSys.DataAccess
{
    public class HangHuDAO
    {
        public static void GhiNhan(string maSP, int soLuong, string lyDo, string ghiChu)
        {
            string maPhieu = DatabaseHelper.GenerateCode("PHH", "HANG_HU", "MaPhieuHuy");
            string sql = @"INSERT INTO HANG_HU (MaPhieuHuy, MaSP, SoLuong, LyDo, GhiChu) 
                          VALUES (@MaPH, @MaSP, @SoLuong, @LyDo, @GhiChu)";
            DatabaseHelper.ExecuteRawNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@MaPH", maPhieu),
                new SqlParameter("@MaSP", maSP),
                new SqlParameter("@SoLuong", soLuong),
                new SqlParameter("@LyDo", lyDo),
                new SqlParameter("@GhiChu", (object)ghiChu ?? DBNull.Value)
            });
            // Decrease stock
            string sqlStock = "UPDATE SAN_PHAM SET SoLuongTon = SoLuongTon - @SoLuong WHERE MaSP = @MaSP AND SoLuongTon >= @SoLuong";
            DatabaseHelper.ExecuteRawNonQuery(sqlStock, new SqlParameter[]
            {
                new SqlParameter("@MaSP", maSP),
                new SqlParameter("@SoLuong", soLuong)
            });
        }

        public static DataTable LayLichSu(int thang = 0, int nam = 0)
        {
            string sql = @"SELECT h.MaPhieuHuy, s.TenSP, h.SoLuong, h.LyDo, h.NgayHuy, h.GhiChu 
                          FROM HANG_HU h JOIN SAN_PHAM s ON h.MaSP = s.MaSP";
            if (thang > 0 && nam > 0)
            {
                sql += " WHERE MONTH(NgayHuy) = @Thang AND YEAR(NgayHuy) = @Nam";
            }
            sql += " ORDER BY NgayHuy DESC";
            return DatabaseHelper.ExecuteRawQuery(sql, new SqlParameter[] {
                new SqlParameter("@Thang", thang),
                new SqlParameter("@Nam", nam)
            });
        }
    }
}
