using System;
using System.Data;
using FloriSys.DataAccess;
using FloriSys.Models;

namespace FloriSys.Services
{
    /// <summary>
    /// Return service - encapsulates return processing.
    /// Demonstrates: ENCAPSULATION (return + stock update + order status all handled).
    /// </summary>
    public class TraHangService
    {
        private readonly TraHangRepository _thRepo;

        public TraHangService()
        {
            _thRepo = new TraHangRepository();
        }

        /// <summary>
        /// Process a complete return: create return slip + details + stock update + order status.
        /// ENCAPSULATION: all steps handled internally via transaction.
        /// </summary>
        public string XuLyTraHang(string maDon, string lyDo, string hinhThuc,
                                   string ghiChu, DataTable chiTietTra, out string error)
        {
            error = "";

            if (string.IsNullOrEmpty(maDon))
            {
                error = "Thiếu mã đơn hàng.";
                return null;
            }
            if (chiTietTra == null || chiTietTra.Rows.Count == 0)
            {
                error = "Chưa chọn sản phẩm trả.";
                return null;
            }

            // Delegate to transactional repository method
            string maPhieu = _thRepo.ThemPhieuTraHoanChinh(maDon, lyDo, hinhThuc, ghiChu, chiTietTra);

            return maPhieu;
        }
    }
}

