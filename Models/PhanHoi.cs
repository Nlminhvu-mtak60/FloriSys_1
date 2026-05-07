using System;

namespace FloriSys.Models
{
    /// <summary>
    /// PhanHoi model - inherits BaseEntity.
    /// Demonstrates: INHERITANCE, ENCAPSULATION (status checks).
    /// </summary>
    public class PhanHoi : BaseEntity
    {
        public string MaPH { get; set; }
        public string MaDon { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayGhi { get; set; }
        public string TrangThaiXuLy { get; set; }
        public string KetQuaXuLy { get; set; }

        // JOIN property
        public string TenKH { get; set; }

        // POLYMORPHISM: Override abstract members from BaseEntity
        public override string DisplayText => MaPH ?? "";
        public override string Id => MaPH;

        public override bool IsValid => !string.IsNullOrEmpty(MaPH) && !string.IsNullOrEmpty(MaDon);

        // ENCAPSULATION: Business logic inside the model
        public bool ChuaXuLy => TrangThaiXuLy == "ChuaXuLy";
        public bool DangXuLy => TrangThaiXuLy == "DangXuLy";
        public bool DaXuLy => TrangThaiXuLy == "DaXuLy";

        // Computed display properties
        public string TrangThaiDisplay
        {
            get
            {
                switch (TrangThaiXuLy)
                {
                    case "ChuaXuLy": return "Chưa xử lý";
                    case "DangXuLy": return "Đang xử lý";
                    case "DaXuLy": return "Đã xử lý";
                    default: return TrangThaiXuLy;
                }
            }
        }
    }
}
