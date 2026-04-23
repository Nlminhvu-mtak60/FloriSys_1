using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;

namespace FloriSys._5_GiaoHang
{
    public partial class ucPhanCong : UserControl
    {
        private List<GiaoHang> dsDonCho;
        private DataTable dtShippers;
        private string selectedMaGH = "";
        private string selectedMaDon = "";

        private ComboBox cboDonCho;

        public ucPhanCong()
        {
            InitializeComponent();
            
            cboDonCho = new ComboBox();
            cboDonCho.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDonCho.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            cboDonCho.ForeColor = Color.FromArgb(31, 41, 55);
            cboDonCho.Location = new Point(16, 12);
            cboDonCho.Size = new Size(400, 31);
            cboDonCho.SelectedIndexChanged += cboDonCho_SelectedIndexChanged;
            
            // Assume pnlLeft has lblCardTitle, we hide card title and substitute with combo
            lblCardTitle.Visible = false;
            pnlLeft.Controls.Add(cboDonCho);
            cboDonCho.BringToFront();

            this.Load += ucPhanCong_Load;
            btnXacNhanPC.Click += btnXacNhan_Click;
            dgvShipper.CellClick += dgvShipper_CellClick;
        }

        private void ucPhanCong_Load(object sender, EventArgs e)
        {
            btnXacNhanPC.Visible = true;
            LoadDonChoGiao();
            LoadShipperList();
        }

        private void LoadDonChoGiao()
        {
            try
            {
                dsDonCho = GiaoHangDAO.LayDonChoGiao();
                cboDonCho.Items.Clear();
                
                if (dsDonCho.Count > 0)
                {
                    foreach (GiaoHang gh in dsDonCho)
                    {
                        cboDonCho.Items.Add("📦 Đơn cần giao – " + gh.MaDon);
                    }
                    cboDonCho.SelectedIndex = 0;
                    btnXacNhanPC.Enabled = true;
                }
                else
                {
                    cboDonCho.Items.Add("📦 Không có đơn chờ phân công");
                    cboDonCho.SelectedIndex = 0;
                    selectedMaGH = "";
                    selectedMaDon = "";
                    lblKhachHangVal.Text = "—";
                    lblDiaChiVal.Text = "—";
                    lblThoiGianVal.Text = "—";
                    lblSanPhamVal.Text = "—";
                    lblGhiChuVal.Text = "—";
                    btnXacNhanPC.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải đơn chờ giao: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboDonCho_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDonCho.SelectedIndex >= 0 && dsDonCho != null && cboDonCho.SelectedIndex < dsDonCho.Count)
            {
                GiaoHang gh = dsDonCho[cboDonCho.SelectedIndex];
                selectedMaGH = gh.MaGiaoHang;
                selectedMaDon = gh.MaDon;
                lblKhachHangVal.Text = gh.TenKH;
                lblDiaChiVal.Text = !string.IsNullOrEmpty(gh.DiaChi) ? gh.DiaChi : "—";
                lblThoiGianVal.Text = "Giao trong ngày";
                lblSanPhamVal.Text = string.Format("Tổng {0:N0}đ", gh.TongTien);
                lblGhiChuVal.Text = !string.IsNullOrEmpty(gh.GhiChuDon) ? gh.GhiChuDon : "—";
            }
        }

        private void LoadShipperList()
        {
            try
            {
                string sql = @"SELECT nv.MaNV, nv.HoTen, 
                    (SELECT COUNT(*) FROM GIAO_HANG gh WHERE gh.MaNV_Shipper = nv.MaNV AND gh.TrangThai = N'DangGiao') AS DangGiao,
                    (SELECT COUNT(*) FROM GIAO_HANG gh WHERE gh.MaNV_Shipper = nv.MaNV AND gh.TrangThai = N'GiaoThanhCong' AND CAST(gh.NgayGiao AS DATE) = CAST(GETDATE() AS DATE)) AS DaGiaoHomNay,
                    CASE 
                        WHEN (SELECT COUNT(*) FROM GIAO_HANG gh WHERE gh.MaNV_Shipper = nv.MaNV AND gh.TrangThai = N'DangGiao') = 0 THEN N'Rảnh'
                        ELSE N'Đang giao'
                    END AS TrangThai
                    FROM NHAN_VIEN nv
                    WHERE nv.ChucVu = N'Shipper' AND nv.TrangThai = N'DangLam'
                    ORDER BY DangGiao ASC";

                dtShippers = DatabaseHelper.ExecuteRawQuery(sql);

                dgvShipper.AutoGenerateColumns = false;
                
                colTen.DataPropertyName = "HoTen";
                colDang.DataPropertyName = "DangGiao";
                colDaGiao.DataPropertyName = "DaGiaoHomNay";
                colStatus.DataPropertyName = "TrangThai";
                dgvShipper.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvShipper.ReadOnly = true;
                dgvShipper.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvShipper.MultiSelect = false;

                dgvShipper.DataSource = dtShippers;

                // Also populate the ComboBox
                cboShipper.Items.Clear();
                cboShipper.Items.Add("-- Chọn shipper --");
                foreach (DataRow dr in dtShippers.Rows)
                {
                    string display = string.Format("{0} ({1} đơn đang giao){2}",
                        dr["HoTen"],
                        dr["DangGiao"],
                        dr["TrangThai"].ToString() == "Rảnh" ? " ⭐" : "");
                    cboShipper.Items.Add(display);
                }
                cboShipper.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách shipper: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvShipper_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dtShippers != null && e.RowIndex < dtShippers.Rows.Count)
            {
                DataRow dr = dtShippers.Rows[e.RowIndex];
                lblTenShipperChon.Text = "👤  Tên Shipper: " + dr["HoTen"].ToString();
                lblSoDonDang.Text = "📦  Đang giao: " + dr["DangGiao"].ToString() + " đơn";
                lblTrangThaiShipper.Text = "🟢  Trạng thái: " + dr["TrangThai"].ToString();

                if (dr["TrangThai"].ToString() == "Rảnh")
                {
                    lblGhiY.Text = "⭐  Shipper đang rảnh, nên ưu tiên phân công!";
                    lblGhiY.ForeColor = Color.FromArgb(45, 106, 79);
                }
                else
                {
                    lblGhiY.Text = "⚠️  Shipper đang có đơn, cân nhắc trước khi phân công.";
                    lblGhiY.ForeColor = Color.FromArgb(146, 64, 14);
                }

                // Sync ComboBox
                cboShipper.SelectedIndex = e.RowIndex + 1;
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaGH))
            {
                MessageBox.Show("Không có đơn hàng nào cần phân công.", "Thông báo");
                return;
            }

            int shipperIndex = cboShipper.SelectedIndex - 1;
            if (shipperIndex < 0 || dtShippers == null || shipperIndex >= dtShippers.Rows.Count)
            {
                MessageBox.Show("Vui lòng chọn shipper để phân công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maNV = dtShippers.Rows[shipperIndex]["MaNV"].ToString();
            string tenShipper = dtShippers.Rows[shipperIndex]["HoTen"].ToString();

            if (MessageBox.Show(
                string.Format("Phân công đơn {0} cho {1}?\n\nGhi chú: {2}", selectedMaDon, tenShipper, txtGhiChu.Text),
                "Xác nhận phân công",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    GiaoHangDAO.PhanCongShipper(selectedMaGH, maNV);
                    MessageBox.Show("Đã phân công thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Reload data
                    txtGhiChu.Clear();
                    LoadDonChoGiao();
                    LoadShipperList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi phân công: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
