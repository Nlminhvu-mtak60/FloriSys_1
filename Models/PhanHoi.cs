using System;

namespace FloriSys.Models
{
    public class PhanHoi
    {
        public string MaPH { get; set; }
        public string MaDon { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayGhi { get; set; }
        public string TrangThaiXuLy { get; set; }
        public string KetQuaXuLy { get; set; }

        // JOIN property
        public string TenKH { get; set; }

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
