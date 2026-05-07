using System;

namespace FloriSys.Models
{
    /// <summary>
    /// HangHu model - inherits BaseEntity.
    /// Demonstrates: INHERITANCE, ENCAPSULATION (business methods).
    /// </summary>
    public class HangHu : BaseEntity
    {
        public string MaPhieuHuy { get; set; }
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaNhap { get; set; }
        public string LyDo { get; set; }
        public DateTime NgayHuy { get; set; }
        public string GhiChu { get; set; }

        // POLYMORPHISM: Override abstract members from BaseEntity
        public override string DisplayText => MaPhieuHuy ?? "";
        public override string Id => MaPhieuHuy;

        public override bool IsValid => !string.IsNullOrEmpty(MaPhieuHuy) && !string.IsNullOrEmpty(MaSP) && SoLuong > 0;
    }
}
