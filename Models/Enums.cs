using System;

namespace FloriSys.Models
{
    /// <summary>
    /// Replaces magic strings throughout the codebase with type-safe enums.
    /// Demonstrates: ENCAPSULATION (valid values are constrained).
    /// </summary>

    public enum ChucVu
    {
        Admin,
        Cashier,
        Warehouse,
        Shipper
    }

    public enum TrangThaiNV
    {
        DangLam,
        DaNghi
    }

    public enum TrangThaiDon
    {
        Moi,
        DangXuLy,
        DaGiao,
        HoanThanh,
        Huy,
        HoanHang
    }

    public enum TrangThaiGiao
    {
        ChoPhanCong,
        DangGiao,
        GiaoThanhCong,
        HoanHang,
        GiaoLai
    }

    public enum TrangThaiSP
    {
        DangBan,
        NgungBan
    }

    public enum HinhThucNhan
    {
        TaiQuay,
        GiaoTanNoi
    }

    public enum HinhThucHoanTien
    {
        TienMat,
        ChuyenKhoan,
        DoiHang
    }

    public enum TrangThaiXuLy
    {
        ChuaXuLy,
        DangXuLy,
        DaXuLy
    }

    /// <summary>
    /// Helper extension methods to convert enum to/from database string values.
    /// </summary>
    public static class EnumExtensions
    {
        public static string ToDbString(this ChucVu val) => val.ToString();
        public static string ToDbString(this TrangThaiNV val) => val.ToString();
        public static string ToDbString(this TrangThaiDon val) => val.ToString();
        public static string ToDbString(this TrangThaiGiao val) => val.ToString();
        public static string ToDbString(this TrangThaiSP val) => val.ToString();
        public static string ToDbString(this HinhThucNhan val) => val.ToString();
        public static string ToDbString(this HinhThucHoanTien val) => val.ToString();
        public static string ToDbString(this TrangThaiXuLy val) => val.ToString();

        public static ChucVu ToChucVu(this string val)
        {
            if (Enum.TryParse<ChucVu>(val, out var result)) return result;
            return ChucVu.Cashier;
        }

        public static TrangThaiNV ToTrangThaiNV(this string val)
        {
            if (Enum.TryParse<TrangThaiNV>(val, out var result)) return result;
            return TrangThaiNV.DangLam;
        }

        public static TrangThaiDon ToTrangThaiDon(this string val)
        {
            if (Enum.TryParse<TrangThaiDon>(val, out var result)) return result;
            return TrangThaiDon.Moi;
        }

        public static TrangThaiGiao ToTrangThaiGiao(this string val)
        {
            if (Enum.TryParse<TrangThaiGiao>(val, out var result)) return result;
            return TrangThaiGiao.ChoPhanCong;
        }

        public static TrangThaiSP ToTrangThaiSP(this string val)
        {
            if (Enum.TryParse<TrangThaiSP>(val, out var result)) return result;
            return TrangThaiSP.DangBan;
        }

        public static HinhThucNhan ToHinhThucNhan(this string val)
        {
            if (Enum.TryParse<HinhThucNhan>(val, out var result)) return result;
            return HinhThucNhan.TaiQuay;
        }

        public static HinhThucHoanTien ToHinhThucHoanTien(this string val)
        {
            if (Enum.TryParse<HinhThucHoanTien>(val, out var result)) return result;
            return HinhThucHoanTien.TienMat;
        }
    }
}
