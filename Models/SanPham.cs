using System;

namespace FloriSys.Models
{
    public class SanPham
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string LoaiHoa { get; set; }
        public decimal GiaBan { get; set; }
        public decimal GiaNhap { get; set; }
        public int SoLuongTon { get; set; }
        public int MucTonToiThieu { get; set; }
        public string TrangThai { get; set; }

        public string TrangThaiDisplay
        {
            get
            {
                return TrangThai == "DangBan" ? "Đang bán" : "Ngưng bán";
            }
        }

        // Used by CanhBaoTonKho query
        public string TinhTrang { get; set; }

        public string TinhTrangDisplay
        {
            get
            {
                switch (TinhTrang)
                {
                    case "HetHang": return "Hết hàng";
                    case "SapHet": return "Sắp hết";
                    case "DuHang": return "Đủ hàng";
                    default: return TinhTrang;
                }
            }
        }
    }
}
