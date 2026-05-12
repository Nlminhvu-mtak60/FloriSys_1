using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FloriSys.Models;

namespace FloriSys.DataAccess
{
    /// <summary>
    /// BaoCaoRepository - reporting/statistics repository.
    /// Instance methods (OOP) instead of static.
    /// Does not inherit BaseRepository because it returns multiple DTO types, not a single entity.
    /// </summary>
    public class BaoCaoRepository
    {
        public BaoCaoDoanhThu DoanhThuNgay(DateTime ngay)
        {
            return DatabaseHelper.ExecuteSingle<BaoCaoDoanhThu>("sp_BaoCaoDoanhThuNgay", new SqlParameter[]
            {
                new SqlParameter("@Ngay", ngay.Date)
            });
        }

        public BaoCaoDoanhThu DoanhThuThang(int thang, int nam)
        {
            return DatabaseHelper.ExecuteSingle<BaoCaoDoanhThu>("sp_BaoCaoDoanhThuThang", new SqlParameter[]
            {
                new SqlParameter("@Thang", thang),
                new SqlParameter("@Nam", nam)
            });
        }

        public List<SanPhamBanChay> SanPhamBanChay(int? thang = null, int? nam = null)
        {
            return DatabaseHelper.ExecuteList<SanPhamBanChay>("sp_SanPhamBanChay", new SqlParameter[]
            {
                new SqlParameter("@Thang", (object)thang ?? DBNull.Value),
                new SqlParameter("@Nam", (object)nam ?? DBNull.Value)
            });
        }

        public List<HieuSuatNhanVien> HieuSuatNhanVien(int? thang = null, int? nam = null)
        {
            return DatabaseHelper.ExecuteList<HieuSuatNhanVien>("sp_HieuSuatNhanVien", new SqlParameter[]
            {
                new SqlParameter("@Thang", (object)thang ?? DBNull.Value),
                new SqlParameter("@Nam", (object)nam ?? DBNull.Value)
            });
        }

        public List<SanPham> BaoCaoTonKho()
        {
            return DatabaseHelper.ExecuteList<SanPham>("sp_CanhBaoTonKho");
        }

        public List<TopSanPhamNgay> TopSanPhamNgay(DateTime ngay)
        {
            string sql = @"SELECT TOP 10 sp.TenSP, SUM(ct.SoLuong) AS SLBan, SUM(ct.ThanhTien) AS DoanhThu
                          FROM CHI_TIET_DON_HANG ct
                          INNER JOIN SAN_PHAM sp ON ct.MaSP = sp.MaSP
                          INNER JOIN DON_HANG dh ON ct.MaDon = dh.MaDon
                          WHERE CAST(dh.NgayTao AS DATE) = @Ngay AND dh.TrangThai NOT IN (N'Huy', N'HoanHang')
                          GROUP BY sp.TenSP ORDER BY SLBan DESC";
            return DatabaseHelper.ExecuteRawList<TopSanPhamNgay>(sql, new SqlParameter[] { new SqlParameter("@Ngay", ngay.Date) });
        }

        public int SoLuongSanPhamBanNgay(DateTime ngay)
        {
            string sql = @"SELECT ISNULL(SUM(ct.SoLuong),0) AS TongSP
                          FROM CHI_TIET_DON_HANG ct
                          INNER JOIN DON_HANG dh ON ct.MaDon = dh.MaDon
                          WHERE CAST(dh.NgayTao AS DATE) = @Ngay AND dh.TrangThai NOT IN (N'Huy', N'HoanHang')";
            DataTable dt = DatabaseHelper.ExecuteRawQuery(sql, new SqlParameter[] { new SqlParameter("@Ngay", ngay.Date) });
            return dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["TongSP"]) : 0;
        }

        public ThongKeDashboard ThongKeDashboard()
        {
            string sql = @"SELECT 
                (SELECT COUNT(*) FROM DON_HANG WHERE CAST(NgayTao AS DATE)=CAST(GETDATE() AS DATE) AND TrangThai != N'Huy') AS DonHomNay,
                (SELECT ISNULL(SUM(TongTien),0) FROM DON_HANG WHERE CAST(NgayTao AS DATE)=CAST(GETDATE() AS DATE) AND TrangThai NOT IN (N'Huy', N'HoanHang')) AS DoanhThuHomNay,
                (SELECT COUNT(*) FROM GIAO_HANG WHERE TrangThai=N'DangGiao') AS DonDangGiao,
                (SELECT COUNT(*) FROM SAN_PHAM WHERE TrangThai=N'DangBan' AND SoLuongTon <= MucTonToiThieu) AS SPSapHet,
                (SELECT COUNT(*) FROM DON_HANG WHERE CAST(NgayTao AS DATE)=DATEADD(day,-1,CAST(GETDATE() AS DATE)) AND TrangThai != N'Huy') AS DonHomQua,
                (SELECT ISNULL(SUM(TongTien),0) FROM DON_HANG WHERE CAST(NgayTao AS DATE)=DATEADD(day,-1,CAST(GETDATE() AS DATE)) AND TrangThai NOT IN (N'Huy', N'HoanHang')) AS DoanhThuHomQua,
                (SELECT COUNT(DISTINCT MaNV_Shipper) FROM GIAO_HANG WHERE TrangThai=N'DangGiao') AS ShipperDangGiao";
            return DatabaseHelper.ExecuteRawSingle<ThongKeDashboard>(sql);
        }

        public List<SanPhamSapHet> LaySanPhamSapHet()
        {
            string sql = "SELECT TenSP, SoLuongTon FROM SAN_PHAM WHERE TrangThai=N'DangBan' AND SoLuongTon <= MucTonToiThieu";
            return DatabaseHelper.ExecuteRawList<SanPhamSapHet>(sql);
        }

        public List<DonHangGanDay> DonHangGanDay(int top = 5)
        {
            string sql = @"SELECT TOP (@Top) dh.MaDon, kh.HoTen AS TenKH, dh.TongTien, dh.NgayTao, dh.TrangThai
                          FROM DON_HANG dh
                          INNER JOIN KHACH_HANG kh ON dh.MaKH = kh.MaKH
                          ORDER BY dh.NgayTao DESC";
            return DatabaseHelper.ExecuteRawList<DonHangGanDay>(sql, new SqlParameter[] { new SqlParameter("@Top", top) });
        }

        public ThongKeKho ThongKeKho()
        {
            string sql = @"SELECT 
                (SELECT COUNT(*) FROM DON_HANG WHERE TrangThai=N'Moi') AS DonChoXuat,
                (SELECT COUNT(*) FROM SAN_PHAM WHERE SoLuongTon <= MucTonToiThieu) AS SPSapHet,
                (SELECT COUNT(*) FROM DON_HANG WHERE TrangThai=N'DangXuLy' AND CAST(NgayTao AS DATE)=CAST(GETDATE() AS DATE)) AS DaXuatHomNay,
                (SELECT COUNT(*) FROM PHIEU_NHAP_KHO WHERE MONTH(NgayNhap)=MONTH(GETDATE()) AND YEAR(NgayNhap)=YEAR(GETDATE())) AS PhieuNhapThang";
            return DatabaseHelper.ExecuteRawSingle<ThongKeKho>(sql);
        }

        public ThongKeBanHang ThongKeBanHang(string maNV)
        {
            string sql = @"SELECT 
                (SELECT COUNT(*) FROM DON_HANG WHERE MaNV_TaoDon=@MaNV AND CAST(NgayTao AS DATE)=CAST(GETDATE() AS DATE)) AS DonHomNay,
                (SELECT ISNULL(SUM(TongTien),0) FROM DON_HANG WHERE MaNV_TaoDon=@MaNV AND CAST(NgayTao AS DATE)=CAST(GETDATE() AS DATE) AND TrangThai NOT IN (N'Huy', N'HoanHang')) AS DoanhThuHomNay,
                (SELECT COUNT(*) FROM DON_HANG WHERE MaNV_TaoDon=@MaNV AND TrangThai IN (N'Moi', N'DangXuLy', N'DaGiao')) AS DonDangXuLy,
                (SELECT COUNT(*) FROM DON_HANG WHERE MaNV_TaoDon=@MaNV AND TrangThai=N'HoanThanh' AND CAST(NgayTao AS DATE)=CAST(GETDATE() AS DATE)) AS DonHoanThanh";
            return DatabaseHelper.ExecuteRawSingle<ThongKeBanHang>(sql, new SqlParameter[] { new SqlParameter("@MaNV", maNV) });
        }

        public List<DonHangGanDay> DonHangCuaNV(string maNV, int top = 10)
        {
            string sql = @"SELECT TOP (@Top) dh.MaDon, kh.HoTen AS TenKH, dh.TongTien, dh.NgayTao, dh.TrangThai
                          FROM DON_HANG dh
                          INNER JOIN KHACH_HANG kh ON dh.MaKH = kh.MaKH
                          WHERE dh.MaNV_TaoDon = @MaNV
                          ORDER BY dh.NgayTao DESC";
            return DatabaseHelper.ExecuteRawList<DonHangGanDay>(sql, new SqlParameter[] { new SqlParameter("@MaNV", maNV), new SqlParameter("@Top", top) });
        }

        public List<DonHangGanDay> DonHangChoXuat()
        {
            string sql = @"SELECT dh.MaDon, kh.HoTen AS TenKH, dh.NgayTao, dh.TrangThai
                          FROM DON_HANG dh
                          INNER JOIN KHACH_HANG kh ON dh.MaKH = kh.MaKH
                          WHERE dh.TrangThai = N'Moi'
                          ORDER BY dh.NgayTao ASC";
            return DatabaseHelper.ExecuteRawList<DonHangGanDay>(sql);
        }

        public List<DoanhThuNgay> DoanhThu7Ngay()
        {
            string sql = @"WITH Last7Days AS (
                                SELECT CAST(GETDATE() AS DATE) AS Ngay
                                UNION ALL
                                SELECT DATEADD(day, -1, Ngay)
                                FROM Last7Days
                                WHERE Ngay > DATEADD(day, -6, CAST(GETDATE() AS DATE))
                            )
                            SELECT d.Ngay, ISNULL(SUM(dh.TongTien), 0) AS DoanhThu
                            FROM Last7Days d
                            LEFT JOIN DON_HANG dh ON CAST(dh.NgayTao AS DATE) = d.Ngay AND dh.TrangThai NOT IN (N'Huy', N'HoanHang')
                            GROUP BY d.Ngay
                            ORDER BY d.Ngay ASC";
            return DatabaseHelper.ExecuteRawList<DoanhThuNgay>(sql);
        }

        public List<DoanhThuNgay> DoanhThuTheoNgayTrongThang(int thang, int nam)
        {
            return DatabaseHelper.ExecuteList<DoanhThuNgay>("sp_DoanhThuTheoNgayTrongThang", new SqlParameter[]
            {
                new SqlParameter("@Thang", thang),
                new SqlParameter("@Nam", nam)
            });
        }
    }
}
