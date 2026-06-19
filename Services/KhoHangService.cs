using System;
using System.Collections.Generic;
using System.Data;
using FloriSys.DataAccess;
using FloriSys.Models;

namespace FloriSys.Services
{
    /// <summary>
    /// Service Kho Hàng - đóng gói các nghiệp vụ quản lý tồn kho.
    /// Thể hiện: TÍNH ĐÓNG GÓI (các thao tác kho nhiều bước được ẩn khỏi UI).
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
        /// Tạo một phiếu nhập kho hoàn chỉnh: thông tin chung + các mặt hàng chi tiết.
        /// TÍNH ĐÓNG GÓI: UI chỉ cần truyền DataTable, Service sẽ tự xử lý phần còn lại.
        /// </summary>
        public string TaoPhieuNhap(string maNV, string ghiChu, DataTable danhSachNhap, out string error)
        {
            error = "";

            if (danhSachNhap == null || danhSachNhap.Rows.Count == 0)
            {
                error = "Danh sách nhập kho trống.";
                return null;
            }

            // Ủy thác cho phương thức có hỗ trợ Transaction ở tầng Repository
            return _pnRepo.TaoPhieuNhapHoanChinh(maNV, ghiChu, danhSachNhap);
        }

        /// <summary>
        /// Ghi nhận hàng hư hỏng.
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
        /// Lấy danh sách sản phẩm đang ở mức cảnh báo tồn kho (sắp hết hàng).
        /// </summary>
        public List<SanPham> LaySanPhamCanhBao()
        {
            return _spRepo.LayCanhBaoTonKho();
        }
    }
}
