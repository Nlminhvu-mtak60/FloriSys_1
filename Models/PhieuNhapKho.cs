using System;
using System.Collections.Generic;

namespace FloriSys.Models
{
    /// <summary>
    /// PhieuNhapKho model - inherits BaseEntity.
    /// Demonstrates: INHERITANCE, ENCAPSULATION (TongTienFormatted).
    /// </summary>
    public class PhieuNhapKho : BaseEntity
    {
        public string MaPhieu { get; set; }
        public DateTime NgayNhap { get; set; }
        public string MaNV { get; set; }
        public string GhiChu { get; set; }

        // JOIN properties
        public string TenNV { get; set; }
        public int SoLoaiSP { get; set; }
        public int TongSL { get; set; }
        public decimal TongTien { get; set; }

        // Navigation property
        public List<ChiTietNhapKho> ChiTiet { get; set; } = new List<ChiTietNhapKho>();

        // POLYMORPHISM: Override abstract members from BaseEntity
        public override string DisplayText => MaPhieu ?? "";
        public override string Id => MaPhieu;

        public override bool IsValid => !string.IsNullOrEmpty(MaPhieu);

        // ENCAPSULATION: Business logic
        public string TongTienFormatted => TongTien.ToString("N0") + " VNĐ";
        public bool HasChiTiet => ChiTiet.Count > 0;
    }

    /// <summary>
    /// ChiTietNhapKho - detail model (child entity).
    /// </summary>
    public class ChiTietNhapKho
    {
        public string MaPhieu { get; set; }
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaNhap { get; set; }
        public decimal ThanhTien { get; set; }
    }
}
