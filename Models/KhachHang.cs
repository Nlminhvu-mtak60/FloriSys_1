using System;

namespace FloriSys.Models
{
    /// <summary>
    /// KhachHang model - inherits BaseEntity.
    /// Demonstrates: ENCAPSULATION (phone validation, HasOrders check),
    /// INHERITANCE (from BaseEntity), POLYMORPHISM (overrides).
    /// </summary>
    public class KhachHang : BaseEntity
    {
        private string _soDienThoai;

        public string MaKH { get; set; }
        public string HoTen { get; set; }

        public string SoDienThoai
        {
            get => _soDienThoai;
            set => _soDienThoai = value;
        }

        public string DiaChi { get; set; }
        public string Email { get; set; }
        public DateTime NgayTao { get; set; }

        // Computed property from JOIN queries
        public int TongDon { get; set; }

        // POLYMORPHISM: Override abstract members from BaseEntity
        public override string DisplayText => HoTen ?? MaKH ?? "";
        public override string Id => MaKH;

        public override bool IsValid =>
            !string.IsNullOrEmpty(MaKH) &&
            !string.IsNullOrEmpty(HoTen);

        // ENCAPSULATION: Business logic inside the model
        public bool HasOrders => TongDon > 0;
        public bool HasDiaChi => !string.IsNullOrEmpty(DiaChi);
    }
}
