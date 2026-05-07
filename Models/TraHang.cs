using System;
using System.Collections.Generic;

namespace FloriSys.Models
{
    /// <summary>
    /// TraHang model - inherits BaseEntity.
    /// Demonstrates: INHERITANCE, ENCAPSULATION (HinhThucDisplay, status checks).
    /// </summary>
    public class TraHang : BaseEntity
    {
        public string MaPhieuTra { get; set; }
        public string MaDon { get; set; }
        public string LyDo { get; set; }
        public string HinhThucHoanTien { get; set; }
        public string GhiChu { get; set; }
        public DateTime NgayTra { get; set; }

        // Navigation property
        public List<ChiTietTraHang> ChiTiet { get; set; } = new List<ChiTietTraHang>();

        // POLYMORPHISM: Override abstract members from BaseEntity
        public override string DisplayText => MaPhieuTra ?? "";
        public override string Id => MaPhieuTra;

        public override bool IsValid => !string.IsNullOrEmpty(MaPhieuTra) && !string.IsNullOrEmpty(MaDon);

        // ENCAPSULATION: Business logic inside the model
        public bool HoanTienMat => HinhThucHoanTien == "TienMat";
        public bool HoanChuyenKhoan => HinhThucHoanTien == "ChuyenKhoan";
        public bool DoiHang => HinhThucHoanTien == "DoiHang";
        public bool HasChiTiet => ChiTiet.Count > 0;

        // Computed display properties
        public string HinhThucDisplay
        {
            get
            {
                switch (HinhThucHoanTien)
                {
                    case "TienMat": return "Tiền mặt";
                    case "ChuyenKhoan": return "Chuyển khoản";
                    case "DoiHang": return "Đổi hàng";
                    default: return HinhThucHoanTien;
                }
            }
        }
    }

    /// <summary>
    /// ChiTietTraHang - detail model (child entity).
    /// </summary>
    public class ChiTietTraHang
    {
        public string MaPhieuTra { get; set; }
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public bool CoNhapKho { get; set; }
    }
}
