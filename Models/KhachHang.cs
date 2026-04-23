using System;

namespace FloriSys.Models
{
    public class KhachHang
    {
        public string MaKH { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public DateTime NgayTao { get; set; }

        // Computed property from JOIN queries
        public int TongDon { get; set; }
    }
}
