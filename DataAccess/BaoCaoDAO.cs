using System;
using System.Data;
using System.Data.SqlClient;

namespace FloriSys.DataAccess
{
    public class BaoCaoDAO
    {
        public static DataTable DoanhThuNgay(DateTime ngay)
        {
            return DatabaseHelper.ExecuteQuery("sp_BaoCaoDoanhThuNgay", new SqlParameter[]
            {
                new SqlParameter("@Ngay", ngay.Date)
            });
        }

        public static DataTable DoanhThuThang(int thang, int nam)
        {
            return DatabaseHelper.ExecuteQuery("sp_BaoCaoDoanhThuThang", new SqlParameter[]
            {
                new SqlParameter("@Thang", thang),
                new SqlParameter("@Nam", nam)
            });
        }

        public static DataTable SanPhamBanChay(int? thang = null, int? nam = null)
        {
            return DatabaseHelper.ExecuteQuery("sp_SanPhamBanChay", new SqlParameter[]
            {
                new SqlParameter("@Thang", (object)thang ?? DBNull.Value),
                new SqlParameter("@Nam", (object)nam ?? DBNull.Value)
            });
        }

        public static DataTable HieuSuatNhanVien(int? thang = null, int? nam = null)
        {
            return DatabaseHelper.ExecuteQuery("sp_HieuSuatNhanVien", new SqlParameter[]
            {
                new SqlParameter("@Thang", (object)thang ?? DBNull.Value),
                new SqlParameter("@Nam", (object)nam ?? DBNull.Value)
            });
        }

        public static DataTable BaoCaoTonKho()
        {
            return DatabaseHelper.ExecuteQuery("sp_CanhBaoTonKho");
        }

        public static DataTable TopSanPhamNgay(DateTime ngay)
        {
            string sql = @"SELECT TOP 10 sp.TenSP, SUM(ct.SoLuong) AS SLBan, SUM(ct.ThanhTien) AS DoanhThu
                          FROM CHI_TIET_DON_HANG ct
                          INNER JOIN SAN_PHAM sp ON ct.MaSP = sp.MaSP
                          INNER JOIN DON_HANG dh ON ct.MaDon = dh.MaDon
                          WHERE CAST(dh.NgayTao AS DATE) = @Ngay AND dh.TrangThai NOT IN (N'Huy', N'HoanHang')
                          GROUP BY sp.TenSP ORDER BY SLBan DESC";
            return DatabaseHelper.ExecuteRawQuery(sql, new SqlParameter[] { new SqlParameter("@Ngay", ngay.Date) });
        }

        public static DataTable SoLuongSanPhamBanNgay(DateTime ngay)
        {
            string sql = @"SELECT ISNULL(SUM(ct.SoLuong),0) AS TongSP
                          FROM CHI_TIET_DON_HANG ct
                          INNER JOIN DON_HANG dh ON ct.MaDon = dh.MaDon
                          WHERE CAST(dh.NgayTao AS DATE) = @Ngay AND dh.TrangThai NOT IN (N'Huy', N'HoanHang')";
            return DatabaseHelper.ExecuteRawQuery(sql, new SqlParameter[] { new SqlParameter("@Ngay", ngay.Date) });
        }

        public static DataTable ThongKeDashboard()
        {
            string sql = @"SELECT 
                (SELECT COUNT(*) FROM DON_HANG WHERE CAST(NgayTao AS DATE)=CAST(GETDATE() AS DATE) AND TrangThai != N'Huy') AS DonHomNay,
                (SELECT ISNULL(SUM(TongTien),0) FROM DON_HANG WHERE CAST(NgayTao AS DATE)=CAST(GETDATE() AS DATE) AND TrangThai NOT IN (N'Huy', N'HoanHang')) AS DoanhThuHomNay,
                (SELECT COUNT(*) FROM GIAO_HANG WHERE TrangThai=N'DangGiao') AS DonDangGiao,
                (SELECT COUNT(*) FROM SAN_PHAM WHERE TrangThai=N'DangBan' AND SoLuongTon <= MucTonToiThieu) AS SPSapHet";
            return DatabaseHelper.ExecuteRawQuery(sql);
        }

        public static DataTable DonHangGanDay(int top = 5)
        {
            string sql = string.Format(@"SELECT TOP {0} dh.MaDon, kh.HoTen AS TenKH, dh.TongTien, dh.TrangThai
                          FROM DON_HANG dh
                          INNER JOIN KHACH_HANG kh ON dh.MaKH = kh.MaKH
                          ORDER BY dh.NgayTao DESC", top);
            return DatabaseHelper.ExecuteRawQuery(sql);
        }

        public static DataTable ThongKeKho()
        {
            string sql = @"SELECT 
                (SELECT COUNT(*) FROM DON_HANG WHERE TrangThai=N'Moi') AS DonChoXuat,
                (SELECT COUNT(*) FROM SAN_PHAM WHERE SoLuongTon <= MucTonToiThieu) AS SPSapHet,
                (SELECT COUNT(*) FROM DON_HANG WHERE CAST(NgayTao AS DATE)=CAST(GETDATE() AS DATE) AND TrangThai NOT IN (N'Moi', N'Huy')) AS DaXuatHomNay,
                (SELECT COUNT(*) FROM PHIEU_NHAP_KHO WHERE MONTH(NgayNhap)=MONTH(GETDATE()) AND YEAR(NgayNhap)=YEAR(GETDATE())) AS PhieuNhapThang";
            return DatabaseHelper.ExecuteRawQuery(sql);
        }

        public static DataTable ThongKeBanHang(string maNV)
        {
            string sql = @"SELECT 
                (SELECT COUNT(*) FROM DON_HANG WHERE MaNV_TaoDon=@MaNV AND CAST(NgayTao AS DATE)=CAST(GETDATE() AS DATE)) AS DonHomNay,
                (SELECT ISNULL(SUM(TongTien),0) FROM DON_HANG WHERE MaNV_TaoDon=@MaNV AND CAST(NgayTao AS DATE)=CAST(GETDATE() AS DATE) AND TrangThai NOT IN (N'Huy', N'HoanHang')) AS DoanhThuHomNay,
                (SELECT COUNT(*) FROM DON_HANG WHERE MaNV_TaoDon=@MaNV AND TrangThai IN (N'Moi', N'DangXuLy', N'DaGiao')) AS DonDangXuLy,
                (SELECT COUNT(*) FROM DON_HANG WHERE MaNV_TaoDon=@MaNV AND TrangThai=N'HoanThanh' AND CAST(NgayTao AS DATE)=CAST(GETDATE() AS DATE)) AS DonHoanThanh";
            return DatabaseHelper.ExecuteRawQuery(sql, new SqlParameter[] { new SqlParameter("@MaNV", maNV) });
        }

        public static DataTable DonHangCuaNV(string maNV, int top = 10)
        {
            string sql = string.Format(@"SELECT TOP {0} dh.MaDon, kh.HoTen AS TenKH, dh.TongTien, dh.NgayTao, dh.TrangThai
                          FROM DON_HANG dh
                          INNER JOIN KHACH_HANG kh ON dh.MaKH = kh.MaKH
                          WHERE dh.MaNV_TaoDon = @MaNV
                          ORDER BY dh.NgayTao DESC", top);
            return DatabaseHelper.ExecuteRawQuery(sql, new SqlParameter[] { new SqlParameter("@MaNV", maNV) });
        }

        public static DataTable DonHangChoXuat()
        {
            string sql = @"SELECT dh.MaDon, kh.HoTen AS TenKH, dh.NgayTao, dh.TrangThai
                          FROM DON_HANG dh
                          INNER JOIN KHACH_HANG kh ON dh.MaKH = kh.MaKH
                          WHERE dh.TrangThai = N'Moi'
                          ORDER BY dh.NgayTao ASC";
            return DatabaseHelper.ExecuteRawQuery(sql);
        }

        public static DataTable DoanhThu7Ngay()
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
            return DatabaseHelper.ExecuteRawQuery(sql);
        }

        public static DataTable DoanhThuTheoNgayTrongThang(int thang, int nam)
        {
            return DatabaseHelper.ExecuteQuery("sp_DoanhThuTheoNgayTrongThang", new SqlParameter[]
            {
                new SqlParameter("@Thang", thang),
                new SqlParameter("@Nam", nam)
            });
        }
    }
}
