using System;
using System.Collections.Generic;

namespace FloriSys.Models
{
    public class PhieuNhapKho
    {
        public string MaPhieu { get; set; }
        public DateTime NgayNhap { get; set; }
        public string MaNV { get; set; }
        public string GhiChu { get; set; }

        // JOIN properties
        public string TenNV { get; set; }
        public int SoLoaiSP { get; set; }
        public int TongSL { get; set; }
        public decimal TongTien { get; set; }

        // Navigation property
        public List<ChiTietNhapKho> ChiTiet { get; set; } = new List<ChiTietNhapKho>();
    }

    public class ChiTietNhapKho
    {
        public string MaPhieu { get; set; }
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaNhap { get; set; }
        public decimal ThanhTien { get; set; }
    }
}
