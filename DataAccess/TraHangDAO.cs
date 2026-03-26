using System;
using System.Data;
using System.Data.SqlClient;

namespace FloriSys.DataAccess
{
    public class TraHangDAO
    {
        public static string ThemPhieuTra(string maDon, string lyDo, string hinhThuc, string ghiChu)
        {
            string maPhieu = DatabaseHelper.GenerateCode("PTR", "TRA_HANG", "MaPhieuTra");
            string sql = @"INSERT INTO TRA_HANG (MaPhieuTra, MaDon, LyDo, HinhThucHoanTien, GhiChu) 
                          VALUES (@MaPT, @MaDon, @LyDo, @HinhThuc, @GhiChu)";
            DatabaseHelper.ExecuteRawNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@MaPT", maPhieu),
                new SqlParameter("@MaDon", maDon),
                new SqlParameter("@LyDo", lyDo),
                new SqlParameter("@HinhThuc", hinhThuc),
                new SqlParameter("@GhiChu", (object)ghiChu ?? DBNull.Value)
            });
            // Update order status
            DonHangDAO.CapNhatTrangThai(maDon, "HoanHang");
            return maPhieu;
        }

        public static void ThemChiTietTra(string maPhieu, string maSP, int soLuong, bool coNhapKho)
        {
            string sql = @"INSERT INTO CT_TRA_HANG (MaPhieuTra, MaSP, SoLuong, CoNhapKho) 
                          VALUES (@MaPT, @MaSP, @SoLuong, @CoNhapKho)";
            DatabaseHelper.ExecuteRawNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@MaPT", maPhieu),
                new SqlParameter("@MaSP", maSP),
                new SqlParameter("@SoLuong", soLuong),
                new SqlParameter("@CoNhapKho", coNhapKho)
            });
            if (coNhapKho)
            {
                // Increase stock
                string sqlStock = "UPDATE SAN_PHAM SET SoLuongTon = SoLuongTon + @SoLuong WHERE MaSP = @MaSP";
                DatabaseHelper.ExecuteRawNonQuery(sqlStock, new SqlParameter[]
                {
                    new SqlParameter("@MaSP", maSP),
                    new SqlParameter("@SoLuong", soLuong)
                });
            }
        }
    }
}
