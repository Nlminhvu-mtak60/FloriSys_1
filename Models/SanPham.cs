using System;

namespace FloriSys.Models
{
    /// <summary>
    /// SanPham model - inherits BaseEntity.
    /// Demonstrates: ENCAPSULATION (GiaBan/SoLuongTon validation),
    /// INHERITANCE (from BaseEntity),
    /// POLYMORPHISM (overrides DisplayText, IsValid),
    /// ABSTRACTION (ConHang, SapHetHang, HetHang hide status logic).
    /// </summary>
    public class SanPham : BaseEntity
    {
        private decimal _giaBan;
        private decimal _giaNhap;
        private int _soLuongTon;

        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string LoaiHoa { get; set; }

        public decimal GiaBan
        {
            get => _giaBan;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Giá bán không được âm");
                _giaBan = value;
            }
        }

        public decimal GiaNhap
        {
            get => _giaNhap;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Giá nhập không được âm");
                _giaNhap = value;
            }
        }

        public int SoLuongTon
        {
            get => _soLuongTon;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Số lượng tồn không được âm");
                _soLuongTon = value;
            }
        }

        public int MucTonToiThieu { get; set; }
        public string TrangThai { get; set; }

        // POLYMORPHISM: Override abstract members from BaseEntity
        public override string DisplayText => TenSP ?? MaSP ?? "";
        public override string Id => MaSP;

        public override bool IsValid =>
            !string.IsNullOrEmpty(MaSP) &&
            !string.IsNullOrEmpty(TenSP) &&
            GiaBan >= 0;

        // ENCAPSULATION: Business logic inside the model
        public bool ConHang => SoLuongTon > 0;
        public bool SapHetHang => SoLuongTon > 0 && SoLuongTon <= MucTonToiThieu;
        public bool HetHang => SoLuongTon == 0;
        public bool DangBan => TrangThai == "DangBan";

        // Computed display properties
        public string TrangThaiDisplay => TrangThai == "DangBan" ? "Đang bán" : "Ngưng bán";

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
