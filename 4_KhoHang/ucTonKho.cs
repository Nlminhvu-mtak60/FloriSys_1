using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Shared;

namespace FloriSys._4_KhoHang
{
    public partial class ucTonKho : BaseUserControl
    {
        private readonly SanPhamRepository _spRepo = new SanPhamRepository();
        
        // --- BƯỚC 1: BIẾN LƯU TRỮ DỮ LIỆU GỐC TRÊN RAM (IN-MEMORY CACHE) ---
        // Tại sao lại cần biến này? 
        // Thay vì mỗi lần đổi kiểu sắp xếp lại phải chui xuống Database (SQL Server) gọi lệnh SELECT làm chậm máy,
        // Ta dùng biến _dsSP để lưu sẵn danh sách tải lên lần đầu. Mọi thao tác sắp xếp sau đó chỉ chạy trên RAM cực kỳ nhanh.
        private List<SanPham> _dsSP = new List<SanPham>();

        public ucTonKho()
        {
            InitializeComponent();
            if (btnConfig != null)
            {
                btnConfig.Click += (s, ev) => 
                {
                    var parentForm = this.FindForm() as FloriSys._2_QuanLy.frmMain;
                    if (parentForm != null)
                    {
                        var found = parentForm.Controls.Find("panel1", true);
                        if (found.Length > 0)
                        {
                            var pnl = found[0] as Panel;
                            if (pnl != null)
                            {
                                while (pnl.Controls.Count > 0) { var old = pnl.Controls[0]; pnl.Controls.RemoveAt(0); old.Dispose(); }
                                var uc = new _4_KhoHang.ucCauHinhTonKho();
                                uc.Dock = DockStyle.Fill;
                                pnl.Controls.Add(uc);
                            }
                        }
                    }
                };
            }
        }

        private void ucTonKho_Load(object sender, EventArgs e) 
        { 
            // BỎ COMMENT DÒNG DƯỚI NÀY KHI BỊ GIÁO VIÊN HỎI:
            // InitComboBoxSapXep();
            
            LoadData(); 
        }

        public override void LoadData()
        {
            try
            {
                string key = txtTimKiem.Text.Trim();
                if (key == "🔍 Tìm tên sản phẩm...") key = "";
                
                // Lấy dữ liệu gốc từ DB và lưu vào biến _dsSP
                _dsSP = _spRepo.LayDanhSach(key, "", "DangBan");
                
                // [BƯỚC 3: MỞ COMMENT DÒNG NÀY ĐỂ TỰ ĐỘNG SẮP XẾP (VÀ NÓ SẼ BỎ QUA GÁN DỮ LIỆU GỐC BÊN DƯỚI)]
                // if (cboSapXep != null) { CboSapXep_SelectedIndexChanged(null, null); return; }

                dgvTonKho.DataSource = _dsSP;
                FormatGrid();
            }
            catch (Exception ex) { ShowError(ex.Message); }
        }

        // =========================================================================
        // BÍ KÍP TRẢ LỜI VẤN ĐÁP: CHỌN TẤT CẢ ĐOẠN CODE BÊN DƯỚI RỒI ẤN (Ctrl + K, U) ĐỂ BỎ COMMENT 
        // =========================================================================

        // private ComboBox cboSapXep;
        // 
        // private void InitComboBoxSapXep()
        // {
        //     cboSapXep = new ComboBox();
        //     cboSapXep.Items.AddRange(new string[] {
        //         "Mặc định",
        //         "Tồn kho: Tăng dần",
        //         "Tồn kho: Giảm dần",
        //         "Cảnh báo: Sắp hết hàng"
        //     });
        //     cboSapXep.SelectedIndex = 0;
        //     cboSapXep.DropDownStyle = ComboBoxStyle.DropDownList;
        //     cboSapXep.Width = 200;
        //     cboSapXep.Location = new System.Drawing.Point(btnConfig.Left - 210, btnConfig.Top);
        //     cboSapXep.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        //     cboSapXep.SelectedIndexChanged += CboSapXep_SelectedIndexChanged;
        //     this.Controls.Add(cboSapXep);
        //     cboSapXep.BringToFront();
        // }
        // 
        // // --- BƯỚC 3: THUẬT TOÁN SẮP XẾP SỬ DỤNG LINQ (LANGUAGE INTEGRATED QUERY) ---
        // // Giáo viên hỏi: "Em sắp xếp bằng thuật toán gì? Dùng vòng lặp For à?"
        // // Trả lời: "Dạ không, em sử dụng thư viện LINQ của C#. LINQ sử dụng thuật toán tối ưu của .NET, 
        // // giúp sắp xếp danh sách Object cực kỳ nhanh chóng bằng biểu thức Lambda."
        // private void CboSapXep_SelectedIndexChanged(object sender, EventArgs e)
        // {
        //     if (_dsSP == null || _dsSP.Count == 0) return;
        //     List<SanPham> dsDaSapXep = _dsSP;
        // 
        //     switch (cboSapXep.SelectedIndex)
        //     {
        //         case 1: // 1. Tồn kho: Tăng dần
        //             dsDaSapXep = _dsSP.OrderBy(sp => sp.SoLuongTon).ToList();
        //             break;
        //         case 2: // 2. Tồn kho: Giảm dần
        //             dsDaSapXep = _dsSP.OrderByDescending(sp => sp.SoLuongTon).ToList();
        //             break;
        //         case 3: // 3. Cảnh báo hết hàng (Trọng tâm ăn điểm)
        //             // Giáo viên hỏi: "Làm sao em tìm được hàng sắp hết mà không cần cột Mới trong Database?"
        //             // Trả lời: "Em tính ẢO trên RAM: Lấy (Số lượng Tồn) trừ đi (Mức Tối Thiểu). OrderBy tăng dần sẽ đẩy số âm lên đầu bảng."
        //             dsDaSapXep = _dsSP.OrderBy(sp => sp.SoLuongTon - sp.MucTonToiThieu).ToList();
        //             break;
        //     }
        // 
        //     dgvTonKho.DataSource = dsDaSapXep;
        //     FormatGrid(); 
        // }

        public override void FormatGrid()
        {
            if (dgvTonKho.Columns.Count > 0)
            {
                var visibleCols = new List<string> { "MaSP", "TenSP", "LoaiHoa", "SoLuongTon", "MucTonToiThieu", "GiaBan", "GiaNhap" };
                foreach (DataGridViewColumn col in dgvTonKho.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

                if (dgvTonKho.Columns.Contains("MaSP")) dgvTonKho.Columns["MaSP"].HeaderText = "Mã SP";
                if (dgvTonKho.Columns.Contains("TenSP")) dgvTonKho.Columns["TenSP"].HeaderText = "Tên sản phẩm";
                if (dgvTonKho.Columns.Contains("LoaiHoa")) dgvTonKho.Columns["LoaiHoa"].HeaderText = "Loại";
                if (dgvTonKho.Columns.Contains("SoLuongTon")) dgvTonKho.Columns["SoLuongTon"].HeaderText = "Tồn kho";
                if (dgvTonKho.Columns.Contains("MucTonToiThieu")) dgvTonKho.Columns["MucTonToiThieu"].HeaderText = "Tối thiểu";
                if (dgvTonKho.Columns.Contains("GiaBan")) dgvTonKho.Columns["GiaBan"].HeaderText = "Giá bán";
                if (dgvTonKho.Columns.Contains("GiaNhap")) dgvTonKho.Columns["GiaNhap"].HeaderText = "Giá nhập";
                if (dgvTonKho.Columns.Contains("TrangThai")) dgvTonKho.Columns["TrangThai"].Visible = false;
                dgvTonKho.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "🔍 Tìm tên sản phẩm...")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                txtTimKiem.Text = "🔍 Tìm tên sản phẩm...";
                txtTimKiem.ForeColor = System.Drawing.Color.Gray;
            }
        }
    }
}