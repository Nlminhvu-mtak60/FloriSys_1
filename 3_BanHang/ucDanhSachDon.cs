using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Shared;

namespace FloriSys._3_BanHang
{
    public partial class ucDanhSachDon : BaseUserControl
    {
        private readonly DonHangRepository _dhRepo = new DonHangRepository();
        public event Action<string> XemChiTiet;
        public event Action TaoDonMoi;

        private int CurrentPage = 1;
        private int PageSize = 50; // Tăng từ 15 lên 50 dòng/trang cho WinForms
        private int TotalPages = 1;

        public ucDanhSachDon()
        {
            InitializeComponent();
            InitializePaging(); // Khởi tạo thanh điều hướng
            LoadStatusList();
        }

        private void ucDanhSachDon_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.Rows.Count == 0)
            {
                ShowWarning("Không có dữ liệu để xuất!");
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel File|*.xls", FileName = "DanhSachDonHang.xls" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var sb = new System.Text.StringBuilder();
                        
                        // Sử dụng HTML Table format để Excel tự động chia cột và có định dạng đẹp (Màu sắc, in đậm)
                        sb.AppendLine("<html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
                        sb.AppendLine("<head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
                        sb.AppendLine("<!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet>");
                        sb.AppendLine("<x:Name>Danh Sach Don Hang</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions>");
                        sb.AppendLine("</x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]-->");
                        sb.AppendLine("<style> table, td, th { border: 1px solid gray; border-collapse: collapse; font-family: Arial; } th { background-color: #4CAF50; color: white; padding: 5px; } td { padding: 5px; } </style>");
                        sb.AppendLine("</head><body><table>");

                        // Headers
                        sb.AppendLine("<tr>");
                        foreach (DataGridViewColumn col in dgvDonHang.Columns)
                        {
                            if (col.Visible) sb.AppendLine($"<th>{col.HeaderText}</th>");
                        }
                        sb.AppendLine("</tr>");

                        // Data Rows
                        foreach (DataGridViewRow row in dgvDonHang.Rows)
                        {
                            sb.AppendLine("<tr>");
                            foreach (DataGridViewColumn col in dgvDonHang.Columns)
                            {
                                if (col.Visible)
                                {
                                    object val = row.Cells[col.Index].Value;
                                    string cellText = val != null ? val.ToString() : "";
                                    // Thêm style mso-number-format cho các cột có thể là số điện thoại để Excel không bị mất số 0 ở đầu
                                    if (col.Name == "SoDienThoai" || col.Name == "MaDon")
                                        sb.AppendLine($"<td style=\"mso-number-format:'\\@';\">{cellText}</td>");
                                    else
                                        sb.AppendLine($"<td>{cellText}</td>");
                                }
                            }
                            sb.AppendLine("</tr>");
                        }

                        sb.AppendLine("</table></body></html>");

                        // Lưu file
                        System.IO.File.WriteAllText(sfd.FileName, sb.ToString(), new System.Text.UTF8Encoding(false));
                        ShowSuccess("Xuất Excel thành công!\nFile đã được lưu tại:\n" + sfd.FileName);
                    }
                    catch (Exception ex)
                    {
                        ShowError("Lỗi khi lưu file: " + ex.Message);
                    }
                }
            }
        }

        private void LoadStatusList()
        {
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("Tất cả trạng thái");
            cboTrangThai.Items.AddRange(new object[] { "Moi", "DangXuLy", "DaGiao", "HoanThanh", "Huy", "HoanHang" });
            cboTrangThai.SelectedIndex = 0;
        }

        public override void LoadData()
        {
            try
            {
                string keyword = txtTimKiem.Text == "🔍 Tìm mã đơn, tên khách, SĐT..." ? "" : txtTimKiem.Text.Trim();
                string tt = cboTrangThai.SelectedIndex > 0 ? cboTrangThai.SelectedItem.ToString() : "";
                
                DateTime? ngayLoc = null;
                if (chkLocNgay.Checked)
                {
                    ngayLoc = dtpNgay.Value;
                }

                // Gọi hàm phân trang từ Repository
                var result = _dhRepo.LayDanhSachPhanTrang(CurrentPage, PageSize, keyword, tt, "", ngayLoc);
                dgvDonHang.DataSource = result.Data;
            
                TotalPages = (int)Math.Ceiling((double)result.TotalCount / PageSize);
                lblPageInfo.Text = $"Trang {CurrentPage} / {Math.Max(1, TotalPages)}";
                lblTongDon.Text = $"Hiển thị {result.Data.Count} / {result.TotalCount} đơn hàng";
            
                btnPrev.Enabled = CurrentPage > 1;
                btnNext.Enabled = CurrentPage < TotalPages;

                FormatGrid();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi tải danh sách: " + ex.Message);
            }
        }

        public override void FormatGrid()
        {
            if (dgvDonHang.Columns.Count == 0) return;

            // Define columns we want to show
            var visibleCols = new List<string> { 
                "MaDon", "NgayTao", "TenKH", "SoDienThoai", "DiaChi", 
                "TongTien", "HinhThucDisplay", "TrangThaiDisplay", "TenNV" 
            };

            // Hide all other auto-generated columns
            foreach (DataGridViewColumn col in dgvDonHang.Columns)
            {
                if (!visibleCols.Contains(col.Name))
                {
                    col.Visible = false;
                }
            }

            // Set headers for visible columns
            if (dgvDonHang.Columns.Contains("MaDon")) dgvDonHang.Columns["MaDon"].HeaderText = "Mã đơn";
            if (dgvDonHang.Columns.Contains("NgayTao")) 
            {
                dgvDonHang.Columns["NgayTao"].HeaderText = "Ngày tạo";
                dgvDonHang.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvDonHang.Columns["NgayTao"].SortMode = DataGridViewColumnSortMode.Programmatic;
            }
            if (dgvDonHang.Columns.Contains("TenKH")) dgvDonHang.Columns["TenKH"].HeaderText = "Khách hàng";
            if (dgvDonHang.Columns.Contains("SoDienThoai")) dgvDonHang.Columns["SoDienThoai"].HeaderText = "SĐT";
            if (dgvDonHang.Columns.Contains("DiaChi")) dgvDonHang.Columns["DiaChi"].HeaderText = "Địa chỉ";
            if (dgvDonHang.Columns.Contains("TongTien")) 
            {
                dgvDonHang.Columns["TongTien"].HeaderText = "Tổng tiền";
                dgvDonHang.Columns["TongTien"].DefaultCellStyle.Format = "#,##0";
            }
            if (dgvDonHang.Columns.Contains("HinhThucDisplay")) dgvDonHang.Columns["HinhThucDisplay"].HeaderText = "Hình thức";
            if (dgvDonHang.Columns.Contains("TrangThaiDisplay")) dgvDonHang.Columns["TrangThaiDisplay"].HeaderText = "Trạng thái";
            if (dgvDonHang.Columns.Contains("TenNV")) dgvDonHang.Columns["TenNV"].HeaderText = "NV tạo";
            
            dgvDonHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDonHang.ReadOnly = true;
            dgvDonHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDonHang.MultiSelect = false;
        }

        private void btnTaoDon_Click(object sender, EventArgs e) { TaoDonMoi?.Invoke(); }
        private void btnLoc_Click(object sender, EventArgs e) { LoadData(); }

        private void chkLocNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpNgay.Enabled = chkLocNgay.Checked;
            LoadData(); // Tự động lọc khi đổi trạng thái checkbox
        }

        private void dgvDonHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DonHang dh = dgvDonHang.Rows[e.RowIndex].DataBoundItem as DonHang;
                if (dh != null) XemChiTiet?.Invoke(dh.MaDon);
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.CurrentRow != null)
            {
                DonHang dh = dgvDonHang.CurrentRow.DataBoundItem as DonHang;
                if (dh != null) XemChiTiet?.Invoke(dh.MaDon);
            }
        }
        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "🔍 Tìm mã đơn, tên khách, SĐT...")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                txtTimKiem.Text = "🔍 Tìm mã đơn, tên khách, SĐT...";
                txtTimKiem.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Ngăn tiếng bip
                CurrentPage = 1; // Reset về trang 1 khi tìm kiếm mới
                LoadData();
            }
        }

        private void phảnHồiKhiếuNạiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.CurrentRow != null)
            {
                DonHang dh = dgvDonHang.CurrentRow.DataBoundItem as DonHang;
                if (dh != null)
                {
                    var frmMain = this.FindForm() as FloriSys._2_QuanLy.frmMain;
                    if (frmMain != null)
                    {
                        frmMain.OnMenuClicked("PhanHoi", dh.MaDon);
                    }
                }
            }
        }
    }
}
