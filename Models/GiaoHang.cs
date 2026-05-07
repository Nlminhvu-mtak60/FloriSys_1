using System;

namespace FloriSys.Models
{
    /// <summary>
    /// GiaoHang model - inherits BaseEntity.
    /// Demonstrates: ENCAPSULATION (ChoPhanCong, DangGiao status checks),
    /// INHERITANCE (from BaseEntity), POLYMORPHISM (overrides).
    /// </summary>
    public class GiaoHang : BaseEntity
    {
        public string MaGiaoHang { get; set; }
        public string MaDon { get; set; }
        public string MaNV_Shipper { get; set; }
        public DateTime? NgayGiao { get; set; }
        public string TrangThai { get; set; }
        public string GhiChuGiaoHang { get; set; }

        // JOIN properties
        public string TenKH { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string TenShipper { get; set; }
        public decimal TongTien { get; set; }
        public string GhiChuDon { get; set; }

        // POLYMORPHISM: Override abstract members from BaseEntity
        public override string DisplayText => MaGiaoHang ?? MaDon ?? "";
        public override string Id => MaGiaoHang;

        public override bool IsValid => !string.IsNullOrEmpty(MaGiaoHang) && !string.IsNullOrEmpty(MaDon);

        // ENCAPSULATION: Business logic inside the model
        public bool ChoPhanCong => TrangThai == "ChoPhanCong";
        public bool DangGiao => TrangThai == "DangGiao";
        public bool GiaoThanhCong => TrangThai == "GiaoThanhCong";
        public bool DaPhanCong => !string.IsNullOrEmpty(MaNV_Shipper);

        // Computed display properties
        public string TrangThaiDisplay
        {
            get
            {
                switch (TrangThai)
                {
                    case "ChoPhanCong": return "Chờ phân công";
                    case "DangGiao": return "Đang giao";
                    case "GiaoThanhCong": return "Giao thành công";
                    case "HoanHang": return "Hoàn hàng";
                    case "GiaoLai": return "Giao lại";
                    default: return TrangThai;
                }
            }
        }
    }

    /// <summary>
    /// ThongKeShipper - DTO for shipper statistics (no BaseEntity).
    /// </summary>
    public class ThongKeShipper
    {
        public int TongDonHnay { get; set; }
        public int DaGiaoHnay { get; set; }
        public int DangDiGiao { get; set; }
        public int ChuaGiao { get; set; }
    }
}
