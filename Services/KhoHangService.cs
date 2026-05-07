using System;
using System.Collections.Generic;
using System.Data;
using FloriSys.DataAccess;
using FloriSys.Models;

namespace FloriSys.Services
{
    /// <summary>
    /// Warehouse service - encapsulates inventory operations.
    /// Demonstrates: ENCAPSULATION (multi-step warehouse operations hidden from UI).
    /// </summary>
    public class KhoHangService
    {
        private readonly PhieuNhapKhoRepository _pnRepo;
        private readonly SanPhamRepository _spRepo;
        private readonly HangHuRepository _hhRepo;

        public KhoHangService()
        {
            _pnRepo = new PhieuNhapKhoRepository();
            _spRepo = new SanPhamRepository();
            _hhRepo = new HangHuRepository();
        }

        /// <summary>
        /// Create a complete goods receipt: header + detail items.
        /// ENCAPSULATION: UI passes a DataTable, service handles the rest.
        /// </summary>
        public string TaoPhieuNhap(string maNV, string ghiChu, DataTable danhSachNhap, out string error)
        {
            error = "";

            if (danhSachNhap == null || danhSachNhap.Rows.Count == 0)
            {
                error = "Danh sách nhập kho trống.";
                return null;
            }

            // Delegate to the transactional method in Repository
            return _pnRepo.TaoPhieuNhapHoanChinh(maNV, ghiChu, danhSachNhap);
        }

        /// <summary>
        /// Record damaged goods.
        /// </summary>
        public bool GhiNhanHangHu(string maSP, int soLuong, string lyDo, string ghiChu, out string error)
        {
            error = "";

            if (string.IsNullOrEmpty(maSP))
            {
                error = "Chưa chọn sản phẩm.";
                return false;
            }
            if (soLuong <= 0)
            {
                error = "Số lượng phải lớn hơn 0.";
                return false;
            }

            var hh = new HangHu
            {
                MaSP = maSP,
                SoLuong = soLuong,
                LyDo = lyDo,
                GhiChu = ghiChu
            };
            _hhRepo.GhiNhan(hh);
            return true;
        }

        /// <summary>
        /// Get products with stock alerts.
        /// </summary>
        public List<SanPham> LaySanPhamCanhBao()
        {
            return _spRepo.LayCanhBaoTonKho();
        }
    }
}
