using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FloriSys.Models;

namespace FloriSys.DataAccess
{
    /// <summary>
    /// GiaoHang repository - inherits BaseRepository&lt;GiaoHang&gt;.
    /// Demonstrates: INHERITANCE, POLYMORPHISM (multiple LayDanhSach overloads).
    /// </summary>
    public class GiaoHangRepository : BaseRepository<GiaoHang>
    {
        public override string TableName => "GIAO_HANG";
        public override string IdColumn => "MaGiaoHang";
        public override string IdPrefix => "GH";

        public List<GiaoHang> LayDanhSach(string trangThai = "", string maShipper = "")
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
            if (!string.IsNullOrEmpty(maShipper))
            {
                sql += " AND gh.MaNV_Shipper = @MaShipper";
                parms.Add(new SqlParameter("@MaShipper", maShipper));
            }
            sql += " ORDER BY gh.NgayGiao DESC";
            return GetList(sql, parms);
        }

        public List<GiaoHang> LayDonChoGiao()
        {
            string sql = @"SELECT gh.MaGiaoHang, gh.MaDon, kh.HoTen AS TenKH, kh.DiaChi, kh.SoDienThoai,
                          dh.TongTien, dh.GhiChu AS GhiChuDon
                          FROM GIAO_HANG gh
                          INNER JOIN DON_HANG dh ON gh.MaDon = dh.MaDon
                          INNER JOIN KHACH_HANG kh ON dh.MaKH = kh.MaKH
                          WHERE gh.TrangThai = N'ChoPhanCong' AND dh.TrangThai = N'DangXuLy'
                          ORDER BY dh.NgayTao";
            return GetList(sql);
        }

        public List<GiaoHang> LayDonCuaShipper(string maNV)
        {
            string sql = @"SELECT gh.MaGiaoHang, gh.MaDon, kh.HoTen AS TenKH, kh.DiaChi, kh.SoDienThoai,
                          gh.TrangThai, dh.TongTien, dh.GhiChu AS GhiChuDon, gh.GhiChuGiaoHang
                          FROM GIAO_HANG gh
                          INNER JOIN DON_HANG dh ON gh.MaDon = dh.MaDon
                          INNER JOIN KHACH_HANG kh ON dh.MaKH = kh.MaKH
                          WHERE gh.MaNV_Shipper = @MaNV 
                          AND (CAST(gh.NgayGiao AS DATE) = CAST(GETDATE() AS DATE) 
                               OR gh.TrangThai IN (N'DangGiao', N'ChoPhanCong', N'GiaoLai'))
                          ORDER BY CASE 
                            WHEN gh.TrangThai = N'DangGiao' THEN 1 
                            WHEN gh.TrangThai = N'ChoPhanCong' THEN 2 
                            WHEN gh.TrangThai = N'GiaoLai' THEN 3 
                            ELSE 4 END, dh.NgayTao DESC";
            return GetList(sql, new List<SqlParameter> { new SqlParameter("@MaNV", maNV) });
        }

        public string TaoGiaoHang(string maDon, string ghiChu = null)
        {
            string maGH = TaoMoi();  // INHERITANCE: Uses base class method
            ExecuteSP("sp_TaoGiaoHang", new SqlParameter[]
            {
                new SqlParameter("@MaGiaoHang", maGH),
                new SqlParameter("@MaDon", maDon),
                NullableParam("@GhiChu", ghiChu)
            });
            return maGH;
        }

        public void PhanCongShipper(string maGH, string maNVShipper)
        {
            ExecuteSP("sp_PhanCongShipper", new SqlParameter[]
            {
                new SqlParameter("@MaGiaoHang", maGH),
                new SqlParameter("@MaNV_Shipper", maNVShipper)
            });
        }

        public void CapNhatTrangThai(string maGH, string trangThai, string ghiChu = null)
        {
            ExecuteSP("sp_CapNhatTrangThaiGiao", new SqlParameter[]
            {
                new SqlParameter("@MaGiaoHang", maGH),
                new SqlParameter("@TrangThai", trangThai),
                NullableParam("@GhiChu", ghiChu)
            });
        }

        public ThongKeShipper ThongKe(string maNV)
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