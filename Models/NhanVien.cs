using System;

namespace FloriSys.Models
{
    /// <summary>
    /// NhanVien model - inherits BaseEntity.
    /// Demonstrates: INHERITANCE (from BaseEntity),
    /// ENCAPSULATION (private fields with validation in setters),
    /// POLYMORPHISM (overrides DisplayText, IsValid, Id),
    /// ABSTRACTION (ConLamViec hides status check logic).
    /// </summary>
    public class NhanVien : BaseEntity
    {
        // ============================================================
        // ENCAPSULATION: Private fields with validation in setters
        // ============================================================

        private string _hoTen;
        private string _soDienThoai;
        private string _taiKhoan;

        public string MaNV { get; set; }

        /// <summary>
        /// ENCAPSULATION: Tự động trim khoảng trắng thừa khi gán tên.
        /// </summary>
        public string HoTen
        {
            get => _hoTen;
            set => _hoTen = string.IsNullOrWhiteSpace(value) ? value : value.Trim();
        }

        public string ChucVu { get; set; }

        /// <summary>
        /// ENCAPSULATION: Chỉ giữ lại ký tự số trong SĐT, loại bỏ dấu cách/gạch ngang.
        /// </summary>
        public string SoDienThoai
        {
            get => _soDienThoai;
            set
            {
                if (string.IsNullOrEmpty(value)) { _soDienThoai = value; return; }
                // Loại bỏ khoảng trắng và dấu gạch ngang, chỉ giữ số và dấu +
                string cleaned = "";
                foreach (char c in value)
                {
                    if (char.IsDigit(c) || c == '+') cleaned += c;
                }
                _soDienThoai = cleaned;
            }
        }

        /// <summary>
        /// ENCAPSULATION: Tự động lowercase và trim tài khoản khi gán.
        /// </summary>
        public string TaiKhoan
        {
            get => _taiKhoan;
            set => _taiKhoan = string.IsNullOrEmpty(value) ? value : value.Trim().ToLower();
        }

        public string MatKhau { get; set; }
        public string TrangThai { get; set; }

        // ============================================================
        // POLYMORPHISM: Override abstract members from BaseEntity
        // ============================================================

        public override string DisplayText => HoTen ?? MaNV ?? "";
        public override string Id => MaNV;

        public override bool IsValid =>
            !string.IsNullOrEmpty(MaNV) &&
            !string.IsNullOrEmpty(HoTen) &&
            !string.IsNullOrEmpty(TaiKhoan);

        // ============================================================
        // ENCAPSULATION: Business logic inside the model
        // ============================================================

        /// <summary>Is this employee currently working?</summary>
        public bool ConLamViec => TrangThai == "DangLam";

        /// <summary>Is this user an admin?</summary>
        public bool IsAdmin => ChucVu == "Admin";

        /// <summary>Is this user a cashier?</summary>
        public bool IsCashier => ChucVu == "Cashier";

        /// <summary>Is this user a warehouse worker?</summary>
        public bool IsWarehouse => ChucVu == "Warehouse";

        /// <summary>Is this user a shipper?</summary>
        public bool IsShipper => ChucVu == "Shipper";

        // ============================================================
        // Computed display properties
        // ============================================================

        public string ChucVuDisplay
        {
            get
            {
                switch (ChucVu)
                {
                    case "Admin": return "Quản lý";
                    case "Cashier": return "Nhân viên bán hàng";
                    case "Warehouse": return "Nhân viên kho";
                    case "Shipper": return "Nhân viên giao hàng";
                    default: return ChucVu;
                }
            }
        }

        public string TrangThaiDisplay => TrangThai == "DangLam" ? "Đang làm" : "Đã nghỉ";
    }
}
