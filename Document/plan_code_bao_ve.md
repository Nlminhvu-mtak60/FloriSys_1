# DANH SÁCH PLAN CODE MẪU COPY-PASTE (FLORISYS)

File này chứa danh sách các đoạn code ngắn gọn, độc lập và ăn khớp trực tiếp với dự án **FloriSys** để bạn có thể copy và paste trực tiếp vào các sự kiện khi giảng viên yêu cầu thay đổi tính năng.

---

## 📌 1. PLAN CODE: Đổi màu dòng dữ liệu theo điều kiện (CellFormatting)
*Sử dụng khi giảng viên yêu cầu tô màu cảnh báo sản phẩm hết hàng hoặc trạng thái đơn hàng.*

### File cần chỉnh sửa:
* [ucSanPham.cs](file:///D:/Learning/C%23/BAI_TAP_LON/FloriSys/7_DanhMuc/ucSanPham.cs) (hoặc bất kỳ UserControl nào chứa DataGridView hiển thị danh sách).

### Vị trí chèn code:
* Tìm hoặc đăng ký sự kiện `CellFormatting` của DataGridView (ví dụ `dgvSanPham`).

### Code Copy-Paste:
```csharp
private void dgvSanPham_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
{
    // Đảm bảo chỉ xử lý khi ở các hàng dữ liệu và đúng cột cần xét (ví dụ cột SoLuongTon)
    if (e.RowIndex >= 0 && dgvSanPham.Columns[e.ColumnIndex].Name == "SoLuongTon")
    {
        // Lấy đối tượng dữ liệu của hàng hiện tại
        var sp = dgvSanPham.Rows[e.RowIndex].DataBoundItem as FloriSys.Models.SanPham;
        
        if (sp != null)
        {
            if (sp.SoLuongTon == 0) // Hết hàng hoàn toàn
            {
                e.CellStyle.BackColor = System.Drawing.Color.LightCoral; // Màu nền đỏ nhạt
                e.CellStyle.ForeColor = System.Drawing.Color.DarkRed;     // Màu chữ đỏ đậm
            }
            else if (sp.SoLuongTon <= sp.MucTonToiThieu) // Sắp hết hàng
            {
                e.CellStyle.BackColor = System.Drawing.Color.Khaki;        // Màu nền vàng nhạt
                e.CellStyle.ForeColor = System.Drawing.Color.DarkGoldenrod; // Màu chữ vàng đậm
            }
        }
    }
}
```

---

## 📌 2. PLAN CODE: Lấy dữ liệu khi Click dòng DataGridView (CellClick)
*Sử dụng khi giảng viên yêu cầu bấm vào một hàng trên bảng thì thông tin hiện lên các textbox bên cạnh.*

### File cần chỉnh sửa:
* [ucKhachHang.cs](file:///D:/Learning/C%23/BAI_TAP_LON/FloriSys/7_DanhMuc/ucKhachHang.cs) (hoặc bất kỳ UserControl danh mục nào).

### Vị trí chèn code:
* Đăng ký sự kiện `CellClick` của DataGridView (ví dụ `dgvKhachHang`).

### Code Copy-Paste:
```csharp
private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
{
    // Kiểm tra RowIndex để không bị lỗi khi click vào dòng tiêu đề cột
    if (e.RowIndex >= 0)
    {
        DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];

        // Lấy dữ liệu từ các ô theo tên cột (Name) và hiển thị lên TextBox tương ứng
        txtMaKH.Text = row.Cells["MaKH"].Value?.ToString();
        txtHoTen.Text = row.Cells["HoTen"].Value?.ToString();
        txtSDT.Text = row.Cells["SoDienThoai"].Value?.ToString();
        txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString();
        
        // Nếu có thuộc tính phụ như Email
        if (row.Cells["Email"] != null)
        {
            txtEmail.Text = row.Cells["Email"].Value?.ToString();
        }
    }
}
```

---

## 📌 3. PLAN CODE: Tìm kiếm tức thì khi gõ phím (TextChanged)
*Sử dụng khi giảng viên yêu cầu nhập từ khóa vào ô tìm kiếm thì dữ liệu tự động lọc ngay mà không cần nhấn nút Tìm.*

### File cần chỉnh sửa:
* [ucSanPham.cs](file:///D:/Learning/C%23/BAI_TAP_LON/FloriSys/7_DanhMuc/ucSanPham.cs) (hoặc [ucKhachHang.cs](file:///D:/Learning/C%23/BAI_TAP_LON/FloriSys/7_DanhMuc/ucKhachHang.cs)).

### Vị trí chèn code:
* Double-click vào TextBox Tìm kiếm (`txtTimKiem`) trên thiết kế để mở sự kiện `TextChanged`.

### Code Copy-Paste:
```csharp
private void txtTimKiem_TextChanged(object sender, EventArgs e)
{
    // Lấy từ khóa và gọi hàm tải lại danh sách
    string keyword = txtTimKiem.Text.Trim();
    LoadData(keyword);
}

// Hàm LoadData gọi lại Repository để lọc
private void LoadData(string keyword = "")
{
    var repo = new SanPhamRepository();
    // Gán dữ liệu nguồn cho DataGridView
    dgvSanPham.DataSource = repo.LayDanhSach(keyword, "", "");
}
```

---

## 📌 4. PLAN CODE: Kiểm tra tính hợp lệ của dữ liệu (Validation) trước khi lưu
*Sử dụng để chặn dữ liệu rỗng, dữ liệu sai định dạng (số điện thoại, email) trước khi gửi xuống cơ sở dữ liệu.*

### File cần chỉnh sửa:
* Các Form thêm/sửa như [frmThemSuaKhachHang.cs](file:///d:/Learning/C%23/BAI_TAP_LON/FloriSys/7_DanhMuc/frmThemSuaKhachHang.cs).

### Code Copy-Paste:
```csharp
private bool KiemTraHopLe()
{
    // 1. Kiểm tra rỗng tên khách hàng
    if (string.IsNullOrWhiteSpace(txtHoTen.Text))
    {
        MessageBox.Show("Vui lòng nhập họ tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtHoTen.Focus();
        return false;
    }

    // 2. Kiểm tra số điện thoại (phải đúng 10 số)
    string sdt = txtSDT.Text.Trim();
    if (string.IsNullOrEmpty(sdt) || sdt.Length != 10 || !long.TryParse(sdt, out _))
    {
        MessageBox.Show("Số điện thoại không hợp lệ! Vui lòng nhập đúng 10 chữ số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtSDT.Focus();
        return false;
    }

    // 3. Kiểm tra định dạng Email (nếu có nhập thì phải chứa ký tự @ và .)
    string email = txtEmail.Text.Trim();
    if (!string.IsNullOrEmpty(email) && (!email.Contains("@") || !email.Contains(".")))
    {
        MessageBox.Show("Email không đúng định dạng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtEmail.Focus();
        return false;
    }

    return true;
}

// Áp dụng trong sự kiện click nút Lưu (btnLuu_Click):
private void btnLuu_Click(object sender, EventArgs e)
{
    if (!KiemTraHopLe()) return; // Dừng lại nếu dữ liệu không hợp lệ

    // Tiến hành lưu đối tượng...
    this.DialogResult = DialogResult.OK;
    this.Close();
}
```

---

## 📌 5. PLAN CODE: Sửa ngưỡng thời gian cảnh báo
*Sử dụng khi giảng viên yêu cầu đổi thời gian cảnh báo đơn hàng từ 30 phút thành 60 phút hoặc thời gian khác.*

### File cần chỉnh sửa:
* [ucXuatKho.cs](file:///D:/Learning/C%23/BAI_TAP_LON/FloriSys/4_KhoHang/ucXuatKho.cs).

### Vị trí chỉnh sửa:
* Tìm trong code sự kiện load hoặc sự kiện tô màu dòng của `ucXuatKho.cs`.

### Code sửa đổi trực tiếp (Ví dụ đổi thành 60 phút):
```csharp
// Tìm đoạn code tính khoảng thời gian chênh lệch:
var timeDiff = DateTime.Now - ngayTao;

// Thay đổi số 30 ban đầu thành 60:
if (timeDiff.TotalMinutes > 60)
{
    row.DefaultCellStyle.BackColor = Color.LightPink; // Tô màu đỏ nhạt cảnh báo trễ hạn
}
```

---

## 📌 6. PLAN CODE: Gọi Stored Procedure có tham số đầu ra (OUTPUT) từ C#
*Sử dụng khi giảng viên yêu cầu gọi một SP để tính toán giá trị và trả về ứng dụng.*

### Code SQL tạo Stored Procedure (Chạy trong SSMS):
```sql
CREATE PROCEDURE sp_LayTongTienKhachHang
    @SoDienThoai VARCHAR(15),
    @TongTien DECIMAL(18, 2) OUTPUT
AS
BEGIN
    SELECT @TongTien = ISNULL(SUM(TongTien), 0)
    FROM DON_HANG dh
    INNER JOIN KHACH_HANG kh ON dh.MaKH = kh.MaKH
    WHERE kh.SoDienThoai = @SoDienThoai AND dh.TrangThai != 'Huy';
END;
```

### Code C# gọi SP (Thêm vào Repository):
```csharp
public decimal LayTongTienMua(string sdt)
{
    // Tham số đầu vào
    SqlParameter paramSDT = new SqlParameter("@SoDienThoai", sdt);

    // Tham số đầu ra
    SqlParameter paramTong = new SqlParameter("@TongTien", SqlDbType.Decimal);
    paramTong.Direction = ParameterDirection.Output;
    paramTong.Precision = 18;
    paramTong.Scale = 2;

    // Thực thi stored procedure
    DatabaseHelper.ExecuteNonQuery("sp_LayTongTienKhachHang", new SqlParameter[] { paramSDT, paramTong });

    // Trả về kết quả đầu ra nhận được
    if (paramTong.Value != DBNull.Value)
    {
        return Convert.ToDecimal(paramTong.Value);
    }
    return 0;
}
```
