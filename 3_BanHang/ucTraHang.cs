using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Shared;

namespace FloriSys._3_BanHang
{
    public partial class ucTraHang : BaseUserControl
    {
        private readonly DonHangRepository _dhRepo = new DonHangRepository();
        private readonly TraHangRepository _thRepo = new TraHangRepository();
        private string _maDon;

        public ucTraHang()
        {
            InitializeComponent();
            LoadLyDo();
            LoadHinhThuc();
            txtMaDon.ReadOnly = false;
            txtMaDon.BackColor = System.Drawing.Color.White;
            txtMaDon.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SetMaDon(txtMaDon.Text.Trim());
                }
            };
            dgvChoTra.CellClick += DgvChoTra_CellClick;
            this.Load += (s, e) => LoadPendingReturns();
        }

        private void DgvChoTra_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string maDon = dgvChoTra.Rows[e.RowIndex].Cells["MaDon"].Value.ToString();
                SetMaDon(maDon);
            }
        }

        private void LoadPendingReturns()
        {
            try
            {
                // Lấy các đơn ở trạng thái HoanHang (Shipper vừa mang về) nhưng CHƯA lập phiếu trả
                string sql = @"SELECT dh.MaDon, kh.HoTen AS TenKH, dh.NgayTao, dh.TongTien
                              FROM DON_HANG dh
                              JOIN KHACH_HANG kh ON dh.MaKH = kh.MaKH
                              WHERE dh.TrangThai = N'HoanHang'
                                AND dh.MaDon NOT IN (SELECT MaDon FROM TRA_HANG)
                              ORDER BY dh.NgayTao DESC";
                
                DataTable dt = DatabaseHelper.ExecuteRawQuery(sql);
                dgvChoTra.DataSource = dt;
                
                if (dgvChoTra.Columns.Count > 0)
                {
                    dgvChoTra.Columns["MaDon"].HeaderText = "Mã đơn";
                    dgvChoTra.Columns["TenKH"].HeaderText = "Khách hàng";
                    dgvChoTra.Columns["NgayTao"].HeaderText = "Ngày tạo";
                    dgvChoTra.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    dgvChoTra.Columns["TongTien"].HeaderText = "Tổng tiền";
                    dgvChoTra.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                }
            }
            catch { }
        }

        public override void LoadData() 
        {
            LoadPendingReturns();
        }

        private void LoadLyDo()
        {
            cboLyDo.Items.Clear();
            cboLyDo.Items.AddRange(new object[] { "Hoa héo/hỏng", "Giao sai sản phẩm", "Giao trễ dịp lễ", "Khách đổi ý", "Shipper báo hoàn hàng" });
            cboLyDo.SelectedIndex = 0;
        }
        
        private void LoadHinhThuc()
        {
            cboHoanTien.Items.Clear();
            cboHoanTien.Items.AddRange(new object[] { "Tiền mặt", "Chuyển khoản", "Đổi hàng" });
            cboHoanTien.SelectedIndex = 0;
        }

        private string LayHinhThucHoanTienDB()
        {
            switch (cboHoanTien.SelectedIndex)
            {
                case 0: return "TienMat";
                case 1: return "ChuyenKhoan";
                case 2: return "DoiHang";
                default: return "TienMat";
            }
        }

        public void SetMaDon(string maDon)
        {
            _maDon = maDon;
            txtMaDon.Text = _maDon;
            LoadOrderProducts();
        }

        private void LoadOrderProducts()
        {
            try
            {
                // Kiểm tra đơn hàng tồn tại và trạng thái hợp lệ
                DonHang dh = _dhRepo.LayThongTinDon(_maDon);
                if (dh == null)
                {
                    ShowWarning("Không tìm thấy đơn hàng: " + _maDon);
                    return;
                }
                
                // CẬP NHẬT: Cho phép trạng thái HoanHang (đã được shipper xác nhận)
                if (dh.TrangThai != "DaGiao" && dh.TrangThai != "HoanThanh" && dh.TrangThai != "HoanHang")
                {
                    ShowWarning("Chỉ có thể trả hàng cho đơn ở trạng thái 'Đã giao', 'Hoàn thành' hoặc 'Hoàn hàng'.\n"
                              + "Trạng thái hiện tại: " + dh.TrangThaiDisplay);
                    return;
                }

                // Nếu đơn là HoanHang, tự động chọn lý do Shipper báo
                if (dh.TrangThai == "HoanHang")
                {
                    cboLyDo.SelectedIndex = 4; // Shipper báo hoàn hàng
                }

                List<ChiTietDonHang> dsCT = _dhRepo.LayChiTiet(_maDon);
                
                DataTable dt = new DataTable();
                dt.Columns.Add("MaSP", typeof(string));
                dt.Columns.Add("TenSP", typeof(string));
                dt.Columns.Add("SoLuong", typeof(int));
                dt.Columns.Add("DonGia", typeof(decimal));
                dt.Columns.Add("ThanhTien", typeof(decimal));
                dt.Columns.Add("SLTra", typeof(int));
                dt.Columns["SLTra"].DefaultValue = 0;
                dt.Columns.Add("CoNhapKho", typeof(bool));
                dt.Columns["CoNhapKho"].DefaultValue = true;

                foreach (ChiTietDonHang ct in dsCT)
                {
                    // Nếu là đơn hoàn hàng (shipper mang về), mặc định trả toàn bộ số lượng
                    int slMacDinh = (dh.TrangThai == "HoanHang") ? ct.SoLuong : 0;
                    dt.Rows.Add(ct.MaSP, ct.TenSP, ct.SoLuong, ct.DonGia, ct.ThanhTien, slMacDinh, true);
                }

                dgvSanPhamTra.DataSource = dt;
                FormatGrid();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi tải sản phẩm đơn: " + ex.Message);
            }
        }

        public override void FormatGrid()
        {
            if (dgvSanPhamTra.Columns.Count == 0) return;

            var visibleCols = new List<string> { "MaSP", "TenSP", "SoLuong", "SLTra", "CoNhapKho" };
            foreach (DataGridViewColumn col in dgvSanPhamTra.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

            dgvSanPhamTra.Columns["MaSP"].HeaderText = "Mã SP";
            dgvSanPhamTra.Columns["TenSP"].HeaderText = "Tên SP";
            dgvSanPhamTra.Columns["SoLuong"].HeaderText = "SL Mua";
            dgvSanPhamTra.Columns["SoLuong"].ReadOnly = true;
            dgvSanPhamTra.Columns["DonGia"].Visible = false;
            dgvSanPhamTra.Columns["ThanhTien"].Visible = false;

            dgvSanPhamTra.Columns["SLTra"].HeaderText = "SL Trả";
            dgvSanPhamTra.Columns["CoNhapKho"].HeaderText = "Nhập lại kho?";
        }

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_maDon))
            {
                ShowWarning("Vui lòng nhập mã đơn hàng trước.");
                return;
            }

            // Kiểm tra trạng thái đơn hàng một lần nữa trước khi duyệt
            DonHang dh = _dhRepo.LayThongTinDon(_maDon);
            if (dh == null)
            {
                ShowWarning("Không tìm thấy đơn hàng: " + _maDon);
                return;
            }
            if (dh.TrangThai != "DaGiao" && dh.TrangThai != "HoanThanh" && dh.TrangThai != "HoanHang")
            {
                ShowWarning("Chỉ có thể trả hàng cho đơn ở trạng thái 'Đã giao', 'Hoàn thành' hoặc 'Hoàn hàng'.\n"
                          + "Trạng thái hiện tại: " + dh.TrangThaiDisplay);
                return;
            }

            try
            {
                // Validate: ít nhất 1 SP có SLTra > 0, và SLTra <= SoLuong
                bool coSPTra = false;
                foreach (DataGridViewRow row in dgvSanPhamTra.Rows)
                {
                    int slTra = Convert.ToInt32(row.Cells["SLTra"].Value);
                    int slMua = Convert.ToInt32(row.Cells["SoLuong"].Value);

                    if (slTra < 0)
                    {
                        ShowWarning("Số lượng trả không được âm!");
                        return;
                    }
                    if (slTra > slMua)
                    {
                        string tenSP = row.Cells["TenSP"].Value.ToString();
                        ShowWarning($"Số lượng trả ({slTra}) vượt quá số lượng mua ({slMua}) của '{tenSP}'!");
                        return;
                    }
                    if (slTra > 0) coSPTra = true;
                }

                if (!coSPTra)
                {
                    ShowWarning("Vui lòng nhập số lượng trả cho ít nhất 1 sản phẩm!");
                    return;
                }

                // Xây dựng DataTable chi tiết trả
                DataTable chiTietTra = new DataTable();
                chiTietTra.Columns.Add("MaSP", typeof(string));
                chiTietTra.Columns.Add("SoLuong", typeof(int));
                chiTietTra.Columns.Add("CoNhapKho", typeof(bool));

                foreach (DataGridViewRow row in dgvSanPhamTra.Rows)
                {
                    int slTra = Convert.ToInt32(row.Cells["SLTra"].Value);
                    if (slTra > 0)
                    {
                        chiTietTra.Rows.Add(
                            row.Cells["MaSP"].Value.ToString(),
                            slTra,
                            Convert.ToBoolean(row.Cells["CoNhapKho"].Value)
                        );
                    }
                }

                string lyDo = cboLyDo.SelectedItem.ToString();
                string hinhThuc = LayHinhThucHoanTienDB();
                string ghiChu = txtGhiChu.Text.Trim();

                // Gọi method transaction tập trung
                string maPhieu = _thRepo.ThemPhieuTraHoanChinh(_maDon, lyDo, hinhThuc, ghiChu, chiTietTra);

                ShowSuccess("Đã duyệt phiếu trả hàng " + maPhieu + " thành công!\nĐơn hàng " + _maDon + " đã chuyển sang trạng thái Hoàn hàng.");
                
                // Reset form
                _maDon = null;
                txtMaDon.Clear();
                txtGhiChu.Clear();
                dgvSanPhamTra.DataSource = null;
                LoadPendingReturns(); // Refresh danh sách đơn chờ
            }
            catch (Exception ex)
            {
                ShowError("Lỗi xử lý trả hàng: " + ex.Message);
            }
        }
    }
}

