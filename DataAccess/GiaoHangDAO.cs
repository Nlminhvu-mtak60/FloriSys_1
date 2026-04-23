using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FloriSys.Models;

namespace FloriSys.DataAccess
{
    public class GiaoHangDAO
    {
        public static List<GiaoHang> LayDanhSach(string trangThai = "")
        {
            string sql = @"SELECT gh.MaGiaoHang, gh.MaDon, kh.HoTen AS TenKH, kh.DiaChi, kh.SoDienThoai,
                          gh.NgayGiao, gh.TrangThai, gh.GhiChuGiaoHang,
                          nv.HoTen AS TenShipper, dh.TongTien, dh.GhiChu AS GhiChuDon
                          FROM GIAO_HANG gh
                          INNER JOIN DON_HANG dh ON gh.MaDon = dh.MaDon
                          INNER JOIN KHACH_HANG kh ON dh.MaKH = kh.MaKH
                          LEFT JOIN NHAN_VIEN nv ON gh.MaNV_Shipper = nv.MaNV
                          WHERE 1=1";
            var parms = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(trangThai))
            {
                sql += " AND gh.TrangThai = @TrangThai";
                parms.Add(new SqlParameter("@TrangThai", trangThai));
            }
            sql += " ORDER BY gh.NgayGiao DESC";
            return DatabaseHelper.ExecuteRawList<GiaoHang>(sql, parms.ToArray());
        }

        public static List<GiaoHang> LayDonChoGiao()
        {
            string sql = @"SELECT gh.MaGiaoHang, gh.MaDon, kh.HoTen AS TenKH, kh.DiaChi, kh.SoDienThoai,
                          dh.TongTien, dh.GhiChu AS GhiChuDon
                          FROM GIAO_HANG gh
                          INNER JOIN DON_HANG dh ON gh.MaDon = dh.MaDon
                          INNER JOIN KHACH_HANG kh ON dh.MaKH = kh.MaKH
                          WHERE gh.TrangThai = N'ChoPhanCong'
                          ORDER BY dh.NgayTao";
            return DatabaseHelper.ExecuteRawList<GiaoHang>(sql);
        }

        public static List<GiaoHang> LayDonCuaShipper(string maNV)
        {
            string sql = @"SELECT gh.MaGiaoHang, gh.MaDon, kh.HoTen AS TenKH, kh.DiaChi, kh.SoDienThoai,
                          gh.TrangThai, dh.TongTien, dh.GhiChu AS GhiChuDon, gh.GhiChuGiaoHang
                          FROM GIAO_HANG gh
                          INNER JOIN DON_HANG dh ON gh.MaDon = dh.MaDon
                          INNER JOIN KHACH_HANG kh ON dh.MaKH = kh.MaKH
                          WHERE gh.MaNV_Shipper = @MaNV
                          ORDER BY gh.TrangThai, dh.NgayTao";
            return DatabaseHelper.ExecuteRawList<GiaoHang>(sql, new SqlParameter[] { new SqlParameter("@MaNV", maNV) });
        }

        public static string TaoGiaoHang(string maDon, string ghiChu = null)
        {
            string maGH = DatabaseHelper.GenerateCode("GH", "GIAO_HANG", "MaGiaoHang");
            DatabaseHelper.ExecuteNonQuery("sp_TaoGiaoHang", new SqlParameter[]
            {
                new SqlParameter("@MaGiaoHang", maGH),
                new SqlParameter("@MaDon", maDon),
                new SqlParameter("@GhiChu", (object)ghiChu ?? DBNull.Value)
            });
            return maGH;
        }

        public static void PhanCongShipper(string maGH, string maNVShipper)
        {
            DatabaseHelper.ExecuteNonQuery("sp_PhanCongShipper", new SqlParameter[]
            {
                new SqlParameter("@MaGiaoHang", maGH),
                new SqlParameter("@MaNV_Shipper", maNVShipper)
            });
        }

        public static void CapNhatTrangThai(string maGH, string trangThai, string ghiChu = null)
        {
            DatabaseHelper.ExecuteNonQuery("sp_CapNhatTrangThaiGiao", new SqlParameter[]
            {
                new SqlParameter("@MaGiaoHang", maGH),
                new SqlParameter("@TrangThai", trangThai),
                new SqlParameter("@GhiChu", (object)ghiChu ?? DBNull.Value)
            });
        }

        public static ThongKeShipper ThongKeShipper(string maNV)
        {
            string sql = @"SELECT 
                (SELECT COUNT(*) FROM GIAO_HANG WHERE MaNV_Shipper=@MaNV AND CAST(NgayGiao AS DATE)=CAST(GETDATE() AS DATE)) AS TongDonHnay,
                (SELECT COUNT(*) FROM GIAO_HANG WHERE MaNV_Shipper=@MaNV AND TrangThai=N'GiaoThanhCong' AND CAST(NgayGiao AS DATE)=CAST(GETDATE() AS DATE)) AS DaGiaoHnay,
                (SELECT COUNT(*) FROM GIAO_HANG WHERE MaNV_Shipper=@MaNV AND TrangThai=N'DangGiao') AS DangDiGiao,
                (SELECT COUNT(*) FROM GIAO_HANG WHERE MaNV_Shipper=@MaNV AND TrangThai=N'ChoPhanCong') AS ChuaGiao";
            return DatabaseHelper.ExecuteRawSingle<ThongKeShipper>(sql, new SqlParameter[] { new SqlParameter("@MaNV", maNV) });
        }
    }
}
