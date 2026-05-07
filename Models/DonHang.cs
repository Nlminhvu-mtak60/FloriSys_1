using System;
using System.Collections.Generic;

namespace FloriSys.Models
{
    /// <summary>
    /// DonHang model - inherits BaseEntity.
    /// Demonstrates: ENCAPSULATION (TongTienFormatted, CanCancel, CanProcess),
    /// INHERITANCE (from BaseEntity), POLYMORPHISM (overrides).
    /// </summary>
    public class DonHang : BaseEntity
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

        // POLYMORPHISM: Override abstract members from BaseEntity
        public override string DisplayText => MaDon ?? "";
        public override string Id => MaDon;

        public override bool IsValid => !string.IsNullOrEmpty(MaDon) && !string.IsNullOrEmpty(MaKH);

        // ENCAPSULATION: Business logic inside the model
        public bool CanCancel => TrangThai == "Moi" || TrangThai == "DangXuLy";
        public bool CanProcess => TrangThai == "Moi";
        public bool IsComplete => TrangThai == "HoanThanh";
        public bool IsCancelled => TrangThai == "Huy";
        public bool IsGiaoTanNoi => HinhThucNhanHang == "GiaoTanNoi";
        public string TongTienFormatted => TongTien.ToString("N0") + " VNĐ";

        // Computed display properties
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

        public string HinhThucDisplay => HinhThucNhanHang == "TaiQuay" ? "Tại quầy" : "Giao tận nơi";
    }

    /// <summary>
    /// ChiTietDonHang - detail model (does not inherit BaseEntity, it's a child entity).
    /// </summary>
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
