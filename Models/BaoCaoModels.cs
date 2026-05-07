using System;

namespace FloriSys.Models
{
    /// <summary>
    /// Kết quả báo cáo doanh thu ngày / tháng
    /// </summary>
    public class BaoCaoDoanhThu
    {
        public int TongDon { get; set; }
        public decimal TongDoanhThu { get; set; }
        public decimal DoanhThuHoanThanh { get; set; }
    }

    /// <summary>
    /// Sản phẩm bán chạy
    /// </summary>
    public class SanPhamBanChay
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string LoaiHoa { get; set; }
        public int TongSoLuong { get; set; }
        public decimal TongDoanhThu { get; set; }
    }

    /// <summary>
    /// Hiệu suất nhân viên
    /// </summary>
    public class HieuSuatNhanVien
    {
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public string ChucVu { get; set; }
        public int SoDonTao { get; set; }
        public decimal TongDoanhThu { get; set; }
        public int DonHuy { get; set; }
    }

    /// <summary>
    /// Thống kê Dashboard (Admin)
    /// </summary>
    public class ThongKeDashboard
    {
        public int DonHomNay { get; set; }
        public decimal DoanhThuHomNay { get; set; }
        public int DonDangGiao { get; set; }
        public int SPSapHet { get; set; }
        public int DonHomQua { get; set; }
        public decimal DoanhThuHomQua { get; set; }
        public int ShipperDangGiao { get; set; }
    }

    /// <summary>
    /// Thống kê Dashboard bán hàng (Cashier)
    /// </summary>
    public class ThongKeBanHang
    {
        public int DonHomNay { get; set; }
        public decimal DoanhThuHomNay { get; set; }
        public int DonDangXuLy { get; set; }
        public int DonHoanThanh { get; set; }
    }

    /// <summary>
    /// Thống kê Dashboard kho
    /// </summary>
    public class ThongKeKho
    {
        public int DonChoXuat { get; set; }
        public int SPSapHet { get; set; }
        public int DaXuatHomNay { get; set; }
        public int PhieuNhapThang { get; set; }
    }

    /// <summary>
    /// Top sản phẩm ngày
    /// </summary>
    public class TopSanPhamNgay
    {
        public string TenSP { get; set; }
        public int SLBan { get; set; }
        public decimal DoanhThu { get; set; }
    }

    /// <summary>
    /// Đơn hàng gần đây (dashboard)
    /// </summary>
    public class DonHangGanDay
    {
        public string MaDon { get; set; }
        public string TenKH { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayTao { get; set; }
    }

    /// <summary>
    /// Sản phẩm sắp hết (dashboard)
    /// </summary>
    public class SanPhamSapHet
    {
        public string TenSP { get; set; }
        public int SoLuongTon { get; set; }
    }

    /// <summary>
    /// Doanh thu theo ngày trong tháng (biểu đồ)
    /// </summary>
    public class DoanhThuNgay
    {
        public DateTime Ngay { get; set; }
        public int NgayTrongThang { get; set; }
        public decimal DoanhThu { get; set; }
        public int SoDon { get; set; }
    }

    /// <summary>
    /// Đơn chờ xuất kho
    /// </summary>
    public class DonChoXuatKho
    {
        public string MaDon { get; set; }
        public string TenKH { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public int SoLuongTon { get; set; }
        public string TinhTrangKho { get; set; }
        public string HinhThucNhanHang { get; set; }
    }

    /// <summary>
    /// Lịch sử trạng thái đơn hàng
    /// </summary>
    public class LichSuDonHang
    {
        public string MaDon { get; set; }
        public string TrangThai { get; set; }
        public DateTime ThoiGian { get; set; }
        public string GhiChu { get; set; }
    }
}
