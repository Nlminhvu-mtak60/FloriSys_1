using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;

namespace FloriSys._5_GiaoHang
{
    public partial class ucGiaoHang : UserControl
    {
        public ucGiaoHang()
        {
            InitializeComponent();
            this.Load += ucGiaoHang_Load;
            btnPhanCong.Click += btnPhanCong_Click;
        }

        private void ucGiaoHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                // Load KPI stats
                LoadStats();

                // Load DataGridView
                List<GiaoHang> dsGH = GiaoHangDAO.LayDanhSach();
                dgvGiaoHang.AutoGenerateColumns = false;

                // Map default columns
                colMaDon.DataPropertyName = "MaDon";
                colKhach.DataPropertyName = "TenKH";
                colDiaChi.DataPropertyName = "DiaChi";
                colGio.DataPropertyName = "NgayGiao";
                colShipper.DataPropertyName = "TenShipper";
                colTT.DataPropertyName = "TrangThai";

                colAction.DataPropertyName = "TongTien";
                colAction.HeaderText = "TỔNG TIỀN";
                colAction.DefaultCellStyle.Format = "N0";
                colAction.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvGiaoHang.Columns["colGio"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvGiaoHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvGiaoHang.ReadOnly = true;
                dgvGiaoHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvGiaoHang.MultiSelect = false;

                dgvGiaoHang.DataSource = dsGH;

                // Color-code rows by status
                dgvGiaoHang.CellFormatting -= DgvGiaoHang_CellFormatting;
                dgvGiaoHang.CellFormatting += DgvGiaoHang_CellFormatting;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách giao hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStats()
        {
            try
            {
                List<GiaoHang> dsGH = GiaoHangDAO.LayDanhSach();
                int choPhanCong = 0, dangGiao = 0, thanhCong = 0, hoanHang = 0;

                foreach (GiaoHang gh in dsGH)
                {
                    switch (gh.TrangThai)
                    {
                        case "ChoPhanCong": choPhanCong++; break;
                        case "DangGiao": dangGiao++; break;
                        case "GiaoThanhCong": thanhCong++; break;
                        case "HoanHang": hoanHang++; break;
                        case "GiaoLai": choPhanCong++; break;
                    }
                }

                lblS1Val.Text = choPhanCong.ToString();
                lblS2Val.Text = dangGiao.ToString();
                label1.Text = thanhCong.ToString();
                label2.Text = hoanHang.ToString();
            }
            catch { }
        }

        private void DgvGiaoHang_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvGiaoHang.Columns[e.ColumnIndex].DataPropertyName == "TrangThai" && e.Value != null)
            {
                string val = e.Value.ToString();
                switch (val)
                {
                    case "ChoPhanCong":
                        e.Value = "Chờ phân công";
                        e.CellStyle.ForeColor = Color.FromArgb(30, 64, 175);
                        break;
                    case "DangGiao":
                        e.Value = "Đang giao";
                        e.CellStyle.ForeColor = Color.FromArgb(146, 64, 14);
                        break;
                    case "GiaoThanhCong":
                        e.Value = "Giao thành công";
                        e.CellStyle.ForeColor = Color.FromArgb(22, 101, 52);
                        break;
                    case "HoanHang":
                        e.Value = "Hoàn hàng";
                        e.CellStyle.ForeColor = Color.FromArgb(124, 58, 237);
                        break;
                    case "GiaoLai":
                        e.Value = "Giao lại";
                        e.CellStyle.ForeColor = Color.FromArgb(146, 64, 14);
                        break;
                }
                e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }

            if (dgvGiaoHang.Columns[e.ColumnIndex].DataPropertyName == "TenShipper" && (e.Value == null || e.Value == DBNull.Value || string.IsNullOrEmpty(e.Value.ToString())))
            {
                e.Value = "—";
                e.CellStyle.ForeColor = Color.Gray;
            }
        }

        private void btnPhanCong_Click(object sender, EventArgs e)
        {
            // Trigger navigation to PhanCong screen via frmMain
            MessageBox.Show("Vui lòng chọn menu 'Phân công' ở thanh điều hướng để phân công shipper.", "Thông báo");
        }
    }
}
