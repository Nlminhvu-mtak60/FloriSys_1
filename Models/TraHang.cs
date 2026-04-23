using System;
using System.Collections.Generic;

namespace FloriSys.Models
{
    public class TraHang
    {
        public string MaPhieuTra { get; set; }
        public string MaDon { get; set; }
        public string LyDo { get; set; }
        public string HinhThucHoanTien { get; set; }
        public string GhiChu { get; set; }
        public DateTime NgayTra { get; set; }

        // Navigation property
        public List<ChiTietTraHang> ChiTiet { get; set; } = new List<ChiTietTraHang>();

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

    public class ChiTietTraHang
    {
        public string MaPhieuTra { get; set; }
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public bool CoNhapKho { get; set; }
    }
}
