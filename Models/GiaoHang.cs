using System;

namespace FloriSys.Models
{
    public class GiaoHang
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

    public class ThongKeShipper
    {
        public int TongDonHnay { get; set; }
        public int DaGiaoHnay { get; set; }
        public int DangDiGiao { get; set; }
        public int ChuaGiao { get; set; }
    }
}
