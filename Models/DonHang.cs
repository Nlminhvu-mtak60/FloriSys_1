using System;
using System.Collections.Generic;

namespace FloriSys.Models
{
    public class DonHang
    {
        public string MaDon { get; set; }
        public DateTime NgayTao { get; set; }
        public string MaKH { get; set; }
        public string MaNV_TaoDon { get; set; }
        public string HinhThucNhanHang { get; set; }
        public string TrangThai { get; set; }
        public decimal TongTien { get; set; }
        public string GhiChu { get; set; }

        // JOIN properties
        public string TenKH { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string TenNV { get; set; }

        // Navigation property
        public List<ChiTietDonHang> ChiTiet { get; set; } = new List<ChiTietDonHang>();

        public string TrangThaiDisplay
        {
            get
            {
                switch (TrangThai)
                {
                    case "Moi": return "Mới";
                    case "DangXuLy": return "Đang xử lý";
                    case "DaGiao": return "Đã giao";
                    case "HoanThanh": return "Hoàn thành";
                    case "Huy": return "Hủy";
                    case "HoanHang": return "Hoàn hàng";
                    default: return TrangThai;
                }
            }
        }

        public string HinhThucDisplay
        {
            get
            {
                return HinhThucNhanHang == "TaiQuay" ? "Tại quầy" : "Giao tận nơi";
            }
        }
    }

    public class ChiTietDonHang
    {
        public string MaDon { get; set; }
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
    }
}
