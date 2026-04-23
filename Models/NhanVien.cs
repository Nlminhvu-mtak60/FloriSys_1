using System;

namespace FloriSys.Models
{
    public class NhanVien
    {
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public string ChucVu { get; set; }
        public string SoDienThoai { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string TrangThai { get; set; }

        // Computed display properties
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

        public string TrangThaiDisplay
        {
            get
            {
                return TrangThai == "DangLam" ? "Đang làm" : "Đã nghỉ";
            }
        }
    }
}
