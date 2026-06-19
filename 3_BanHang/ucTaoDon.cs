using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Services;
using FloriSys.Shared;
using FloriSys._2_QuanLy;

namespace FloriSys._3_BanHang
{
    public partial class ucTaoDon : BaseUserControl
    {
        private readonly SanPhamRepository _spRepo = new SanPhamRepository();
        private readonly KhachHangRepository _khRepo = new KhachHangRepository();
        private readonly DonHangService _dhService = new DonHangService();
        private DataTable _gioHang;
        private List<KhachHang> _dsKhachHang;
        public event Action DonDaTao;

        /// <summary>
        /// Khởi tạo UserControl, tạo cấu trúc cho giỏ hàng (DataTable) lưu trong bộ nhớ tạm.
        /// </summary>
        public ucTaoDon()
        {
            InitializeComponent();
            _gioHang = new DataTable();
            _gioHang.Columns.Add("MaSP", typeof(string));
            _gioHang.Columns.Add("TenSP", typeof(string));
            _gioHang.Columns.Add("SoLuong", typeof(int));
            _gioHang.Columns.Add("DonGia", typeof(decimal));
            _gioHang.Columns.Add("ThanhTien", typeof(decimal));
        }

       
        public override void LoadData() { LoadSanPham(); }

        
        // Sự kiện khởi chạy đầu tiên khi mở màn hình: Cài đặt ComboBox, nạp danh sách hoa và thiết lập Autocomplete.
       
        private void ucTaoDon_Load(object sender, EventArgs e)
        {
            cboHinhThuc.Items.Clear();
            cboHinhThuc.Items.Add("Nhận tại quầy");
            cboHinhThuc.Items.Add("Giao tận nơi");
            cboHinhThuc.SelectedIndex = 0;
            LoadSanPham();
            dgvGioHang.DataSource = _gioHang;
            TinhTong();
            LoadAutocompleteKhachHang();
        }

        private ListBox lstGoiY;

        
        /// Khởi tạo cơ chế tự động gợi ý tên/SĐT khách hàng 
       
        private void LoadAutocompleteKhachHang()
        {
            _dsKhachHang = _khRepo.LayDanhSach();
            
            lstGoiY = new ListBox();
            lstGoiY.Visible = false;
            lstGoiY.Font = new System.Drawing.Font("Segoe UI", 10F);
            lstGoiY.Height = 120;
            lstGoiY.Width = txtTenKH.Width;
            pnlKhachHang.Controls.Add(lstGoiY);
            lstGoiY.BringToFront();

            lstGoiY.Click += LstGoiY_Click;
            
            // Allow hiding when clicking away
            this.Click += (s, e) => lstGoiY.Visible = false;
            pnlKhachHang.Click += (s, e) => lstGoiY.Visible = false;

            txtTenKH.AutoCompleteMode = AutoCompleteMode.None;
            txtSDT.AutoCompleteMode = AutoCompleteMode.None;

            txtTenKH.TextChanged += TxtSearch_TextChanged;
            txtSDT.TextChanged += TxtSearch_TextChanged;
            
            txtTenKH.Leave += TxtSearch_Leave;
            txtSDT.Leave += TxtSearch_Leave;
        }

        /// Sự kiện gõ phím vào ô Tên hoặc SĐT: Lọc danh sách khách hàng và hiển thị danh sách gợi ý
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (!txt.Focused) return;

            string keyword = txt.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(keyword) || _dsKhachHang == null)
            {
                lstGoiY.Visible = false;
                return;
            }

            var results = _dsKhachHang.Where(k => 
                (k.HoTen != null && k.HoTen.ToLower().Contains(keyword)) || 
                (k.SoDienThoai != null && k.SoDienThoai.Contains(keyword))
            ).Take(8).ToList();

            if (results.Count > 0)
            {
                var displayList = results.Select(k => new { Text = k.HoTen + " - " + k.SoDienThoai, Value = k }).ToList();
                lstGoiY.DataSource = displayList;
                lstGoiY.DisplayMember = "Text";
                lstGoiY.ValueMember = "Value";

                lstGoiY.Location = new System.Drawing.Point(txt.Left, txt.Bottom);
                lstGoiY.Visible = true;
                lstGoiY.BringToFront();
            }
            else
            {
                lstGoiY.Visible = false;
            }
        }

      
        // Sự kiện click chọn khách hàng từ danh sách gợi ý: Tự động điền thông tin vào các ô nhập liệu.
    
        private void LstGoiY_Click(object sender, EventArgs e)
        {
            if (lstGoiY.SelectedItem == null) return;
            var item = lstGoiY.SelectedItem;
            var kh = (KhachHang)item.GetType().GetProperty("Value").GetValue(item, null);

            txtTenKH.Text = kh.HoTen;
            txtSDT.Text = kh.SoDienThoai;
            txtEmail.Text = kh.Email;
            
            // Mặc định tự động sao chép thông tin người đặt sang ô người nhận
            txtTenNhan.Text = kh.HoTen;
            txtSDTNhan.Text = kh.SoDienThoai;
            txtDiaChi.Text = kh.DiaChi;
            
            lstGoiY.Visible = false;
        }

       
        /// Sự kiện khi ô Tên/SĐT mất tiêu điểm (chuột bấm ra ngoài): Ẩn danh sách gợi ý đi (có độ trễ để không bị lỗi click).
       
        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            // Trì hoãn việc ẩn danh sách một chút để sự kiện Click có thời gian kích hoạt nếu người dùng bấm vào danh sách
            System.Threading.Tasks.Task.Delay(150).ContinueWith(t => 
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() => 
                    {
                        if (!lstGoiY.Focused) lstGoiY.Visible = false;
                    }));
                }
            });
        }

        
        /// Tải danh sách hoa đang bán từ Database lên bảng dữ liệu (có hỗ trợ tìm kiếm theo từ khóa).
       
        private void LoadSanPham(string key = "")
        {
            try
            {
                List<SanPham> dsSP = _spRepo.LaySanPhamDangBan(key);
                dgvSanPham.DataSource = dsSP;
                FormatGridSP();
            }
            catch (Exception ex) { ShowError(ex.Message); }
        }

       
        /// Làm đẹp bảng danh sách hoa: Ẩn cột thừa, đổi tên cột sang tiếng Việt, định dạng tiền tệ.
     
        private void FormatGridSP()
        {
            if (dgvSanPham.Columns.Count == 0) return;

            var visibleCols = new List<string> { "TenSP", "GiaBan", "SoLuongTon" };
            foreach (DataGridViewColumn col in dgvSanPham.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

            dgvSanPham.Columns["TenSP"].HeaderText = "Sản phẩm";
            dgvSanPham.Columns["GiaBan"].HeaderText = "Giá bán";
            dgvSanPham.Columns["GiaBan"].DefaultCellStyle.Format = "#,##0";
            dgvSanPham.Columns["SoLuongTon"].HeaderText = "Tồn kho";
            
            dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        
        /// Nút Tìm kiếm hoa.
        
        private void btnTimSP_Click(object sender, EventArgs e) { LoadSanPham(txtTimSP.Text.Trim()); }

        
        /// Nút Thêm vào giỏ hàng: Kiểm tra tồn kho, cộng dồn số lượng nếu trùng, và tính lại tổng tiền.
        
        private void btnThemSP_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow == null) return;
            SanPham sp = dgvSanPham.CurrentRow.DataBoundItem as SanPham;
            if (sp == null) return;

            int soLuongThem = (int)nudSoLuongThem.Value;

            if (sp.SoLuongTon < soLuongThem) { ShowWarning("Sản phẩm không đủ số lượng tồn kho!"); return; }

            foreach (DataRow row in _gioHang.Rows)
            {
                if (row["MaSP"].ToString() == sp.MaSP)
                {
                    int sl = Convert.ToInt32(row["SoLuong"]) + soLuongThem;
                    if (sl > sp.SoLuongTon) { ShowWarning("Vượt quá tồn kho!"); return; }
                    row["SoLuong"] = sl;
                    row["ThanhTien"] = sl * sp.GiaBan;
                    TinhTong();
                    nudSoLuongThem.Value = 1; // reset
                    return;
                }
            }
            _gioHang.Rows.Add(sp.MaSP, sp.TenSP, soLuongThem, sp.GiaBan, soLuongThem * sp.GiaBan);
            TinhTong();
            nudSoLuongThem.Value = 1; // reset
        }

        
        /// Nút Xóa khỏi giỏ hàng.
        
        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            if (dgvGioHang.CurrentRow == null) return;
            _gioHang.Rows.RemoveAt(dgvGioHang.CurrentRow.Index);
            TinhTong();
        }

        
        /// Duyệt qua giỏ hàng để cộng dồn Thành Tiền và hiển thị lên nhãn Tổng cộng.
        
        private void TinhTong()
        {
            decimal tong = 0;
            foreach (DataRow row in _gioHang.Rows)
                tong += Convert.ToDecimal(row["ThanhTien"]);
            lblTongTien.Text = string.Format("Tổng cộng: {0:#,##0}đ", tong);
        }

        
        /// Nút Tạo Đơn Hàng: Thu thập toàn bộ dữ liệu từ giao diện và gửi cho Service xử lý chốt đơn.
        
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (!CheckPermission("DonHang", "Them")) return;

            try
            {
                string hinhThuc = cboHinhThuc.SelectedIndex == 0 ? "TaiQuay" : "GiaoTanNoi";
                string error;
                
                // Gọi thẳng Service, đưa hết trách nhiệm kiểm tra và xử lý cho nó
                string maDon = _dhService.TaoDonHang(
                    txtTenKH.Text.Trim(), txtSDT.Text.Trim(), 
                    txtTenNhan.Text.Trim(), txtSDTNhan.Text.Trim(), 
                    txtDiaChi.Text.Trim(), txtEmail.Text.Trim(),
                    hinhThuc, txtGhiChu.Text.Trim(), _gioHang, 
                    SessionManager.MaNV, out error
                );

                if (maDon == null)
                {
                    ShowWarning(error);
                    return;
                }

                ShowSuccess("Tạo đơn hàng " + maDon + " thành công!");
                _gioHang.Clear();
                txtTenKH.Clear(); txtSDT.Clear(); txtEmail.Clear(); txtDiaChi.Clear(); txtGhiChu.Clear();
                txtTenNhan.Clear(); txtSDTNhan.Clear();
                TinhTong();
                var mainForm = this.FindForm() as frmMain;
                if (mainForm != null)
                {
                    mainForm.RefreshMenuBadges();
                }
                DonDaTao?.Invoke();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi: " + ex.Message);
            }
        }

      
        /// Nút Hủy: Xóa sạch giỏ hàng và các ô nhập liệu.
     
        private void btnHuy_Click(object sender, EventArgs e)
        {
            _gioHang.Clear();
            txtTenKH.Clear(); txtSDT.Clear(); txtEmail.Clear(); txtDiaChi.Clear(); txtGhiChu.Clear();
            txtTenNhan.Clear(); txtSDTNhan.Clear();
            TinhTong();
        }
    }
}
