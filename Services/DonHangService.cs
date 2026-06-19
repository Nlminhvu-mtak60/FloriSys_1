using System;
using System.Data;
using FloriSys.DataAccess;
using FloriSys.Models;

namespace FloriSys.Services
{
  
    public class DonHangService
    {
        private readonly KhachHangRepository _khRepo;
        private readonly DonHangRepository _dhRepo;
        private readonly GiaoHangRepository _ghRepo;

        public DonHangService()
        {
            _khRepo = new KhachHangRepository();
            _dhRepo = new DonHangRepository();
            _ghRepo = new GiaoHangRepository();
        }

       
        public string TaoDonHang(string tenKH, string sdt, string tenNhan, string sdtNhan, string diaChi, string email,
                                 string hinhThuc, string ghiChu, DataTable gioHang,
                                 string maNV, out string error)
        {
            error = "";

            // Bước 1: Kiểm tra tính hợp lệ
            if (string.IsNullOrEmpty(tenKH) || string.IsNullOrEmpty(sdt))
            {
                error = "Vui lòng nhập thông tin người đặt!";
                return null;
            }
            if (string.IsNullOrEmpty(tenNhan) || string.IsNullOrEmpty(sdtNhan))
            {
                error = "Vui lòng nhập thông tin người nhận!";
                return null;
            }
            if (gioHang == null || gioHang.Rows.Count == 0)
            {
                error = "Giỏ hàng trống!";
                return null;
            }

            // Xử lý logic gộp ghi chú nếu người nhận khác người đặt
            bool isDifferentReceiver = (tenKH != tenNhan) || (sdt != sdtNhan);
            if (isDifferentReceiver)
            {
                ghiChu = $"[Giao cho: {tenNhan} - {sdtNhan} - {diaChi}] {ghiChu}";
            }

            // Bước 2: Tìm hoặc tạo khách hàng mới
            string maKH = _khRepo.TimHoacTao(tenKH, sdt, diaChi, email);

            // Bước 3: Tạo đơn hàng với transaction đầy đủ (đảm bảo tính toàn vẹn)
            string maDon = _dhRepo.TaoDonHangHoanChinh(maKH, maNV, hinhThuc, ghiChu, gioHang);

            // Bước 4: Tự động xuất kho và hoàn thành cho đơn nhận tại quầy
            if (hinhThuc == "TaiQuay")
            {
                _dhRepo.CapNhatTrangThai(maDon, "DangXuLy"); // Trừ tồn kho
                _dhRepo.CapNhatTrangThai(maDon, "HoanThanh"); // Hoàn thành luôn
            }

            return maDon;
        }

      
        public bool CapNhatTrangThai(string maDon, string trangThaiMoi, out string error)
        {
            error = "";
            var don = _dhRepo.LayThongTinDon(maDon);
            if (don == null)
            {
                error = "Không tìm thấy đơn hàng.";
                return false;
            }

            // Luật kinh doanh: không thể thay đổi đơn hàng đã hoàn thành hoặc đã hủy
            if (don.IsComplete || don.IsCancelled)
            {
                error = "Không thể thay đổi trạng thái đơn hàng đã hoàn thành hoặc đã hủy.";
                return false;
            }

            _dhRepo.CapNhatTrangThai(maDon, trangThaiMoi);
            return true;
        }

        
        public DonHang LayChiTietDon(string maDon)
        {
            var don = _dhRepo.LayThongTinDon(maDon);
            if (don != null)
            {
                don.ChiTiet = _dhRepo.LayChiTiet(maDon);
            }
            return don;
        }
    }
}
