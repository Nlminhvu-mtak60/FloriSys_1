# BÁO CÁO CHI TIẾT CƠ SỞ DỮ LIỆU (DATABASE SYSTEM PLAN)
**Dự án: FloriSys — Quản lý Cửa hàng Hoa tươi**
**Tài liệu dành cho Giảng viên phản biện và Hội đồng bảo vệ đồ án**

Tài liệu này trình bày chi tiết về cấu trúc cơ sở dữ liệu của dự án FloriSys, bao gồm định nghĩa 14 bảng, cơ chế tự động hóa bằng Trigger, các ràng buộc dữ liệu toàn vẹn, và hệ thống Stored Procedure tối ưu hóa hiệu năng và bảo mật.

---

## 📑 MỤC LỤC
1. [CHI TIẾT CẤU TRÚC 14 BẢNG DỮ LIỆU](#1-cau-truc-14-bang)
2. [HỆ THỐNG TRIGGER (TỰ ĐỘNG HÓA DỮ LIỆU)](#2-he-thong-trigger)
3. [RÀNG BUỘC TOÀN VẸN (CONSTRAINTS) & BẢO MẬT](#3-rang-buoc-toan-ven)
4. [DANH SÁCH & CHỨC NĂNG CÁC STORED PROCEDURE](#4-stored-procedures)

---

<a name="1-cau-truc-14-bang"></a>
## 📂 1. CHI TIẾT CẤU TRÚC 14 BẢNG DỮ LIỆU

### 1.1 NHAN_VIEN (Quản lý thông tin và tài khoản nhân sự)
* **Khóa chính:** `MaNV` (NVARCHAR(20))
* **Các trường dữ liệu:**
  * `HoTen` (NVARCHAR(100)) - Họ tên nhân viên.
  * `ChucVu` (NVARCHAR(20)) - Quyền hạn: `Admin`, `Cashier`, `Warehouse`, `Shipper`.
  * `SoDienThoai` (NVARCHAR(15)) - Số điện thoại liên hệ.
  * `TaiKhoan` (NVARCHAR(50)) - Tài khoản đăng nhập (Duy nhất - UNIQUE).
  * `MatKhau` (NVARCHAR(256)) - Mật khẩu băm một chiều SHA-256.
  * `TrangThai` (NVARCHAR(20)) - Tình trạng làm việc: `DangLam`, `DaNghi`.

### 1.2 KHACH_HANG (Quản lý tệp khách hàng của cửa hàng)
* **Khóa chính:** `MaKH` (NVARCHAR(20))
* **Các trường dữ liệu:**
  * `HoTen` (NVARCHAR(100)) - Tên khách hàng.
  * `SoDienThoai` (NVARCHAR(15)) - Số điện thoại (Duy nhất - UNIQUE).
  * `DiaChi` (NVARCHAR(200)) - Địa chỉ khách hàng.
  * `Email` (NVARCHAR(100)) - Thư điện tử khách hàng.
  * `NgayTao` (DATETIME) - Ngày khởi tạo tài khoản trên hệ thống.

### 1.3 SAN_PHAM (Quản lý thông tin hoa, phụ kiện và tồn kho)
* **Khóa chính:** `MaSP` (NVARCHAR(20))
* **Các trường dữ liệu:**
  * `TenSP` (NVARCHAR(100)) - Tên bó hoa/sản phẩm.
  * `LoaiHoa` (NVARCHAR(50)) - Phân loại: Hoa tươi, Hoa bó, Giỏ hoa, Phụ kiện, v.v.
  * `GiaBan` (DECIMAL) - Đơn giá bán ra cho khách hàng.
  * `GiaNhap` (DECIMAL) - Giá nhập kho từ vựa hoa.
  * `SoLuongTon` (INT) - Số lượng tồn kho thực tế hiện tại.
  * `MucTonToiThieu` (INT) - Ngưỡng cảnh báo cần nhập thêm hàng.
  * `TrangThai` (NVARCHAR(20)) - Tình trạng kinh doanh: `DangBan`, `NgungBan`.

### 1.4 DON_HANG (Quản lý thông tin chung của hóa đơn bán hàng)
* **Khóa chính:** `MaDon` (NVARCHAR(20))
* **Các trường dữ liệu:**
  * `NgayTao` (DATETIME) - Ngày và giờ lập đơn.
  * `MaKH` (NVARCHAR(20)) - Liên kết với `KHACH_HANG(MaKH)`.
  * `MaNV_TaoDon` (NVARCHAR(20)) - Liên kết với `NHAN_VIEN(MaNV)` (Thu ngân lập đơn).
  * `HinhThucNhanHang` (NVARCHAR(30)) - Phân loại: `TaiQuay` (Nhận tại quầy), `GiaoTanNoi` (Giao hàng).
  * `TrangThai` (NVARCHAR(20)) - Trạng thái xử lý: `Moi`, `DangXuLy`, `DaGiao`, `HoanThanh`, `Huy`, `HoanHang`.
  * `TongTien` (DECIMAL) - Tổng trị giá của đơn hàng (Trigger tự cập nhật).
  * `GhiChu` (NVARCHAR(500)) - Ghi chú đơn hàng.

### 1.5 CHI_TIET_DON_HANG (Chi tiết các sản phẩm trong mỗi đơn hàng)
* **Khóa chính phức hợp:** `(MaDon, MaSP)`
* **Khóa ngoại:** 
  * `MaDon` tham chiếu `DON_HANG(MaDon)`
  * `MaSP` tham chiếu `SAN_PHAM(MaSP)`
* **Các trường dữ liệu:**
  * `SoLuong` (INT) - Số lượng mua của sản phẩm đó.
  * `DonGia` (DECIMAL) - Giá bán thực tế tại thời điểm mua.
  * `ThanhTien` (DECIMAL) - Giá trị của mặt hàng (`SoLuong * DonGia`).

### 1.6 GIAO_HANG (Quản lý các đơn hàng được vận chuyển)
* **Khóa chính:** `MaGiaoHang` (NVARCHAR(20))
* **Khóa ngoại:** 
  * `MaDon` tham chiếu `DON_HANG(MaDon)`
  * `MaNV_Shipper` tham chiếu `NHAN_VIEN(MaNV)`
* **Các trường dữ liệu:**
  * `NgayGiao` (DATETIME) - Ngày giờ giao hàng.
  * `TrangThai` (NVARCHAR(20)) - Trạng thái: `ChoPhanCong`, `DangGiao`, `GiaoThanhCong`, `HoanHang`, `GiaoLai`.
  * `GhiChuGiaoHang` (NVARCHAR(500)) - Ghi chú từ Shipper.

### 1.7 PHIEU_NHAP_KHO (Thông tin phiếu nhập hoa từ nhà cung cấp)
* **Khóa chính:** `MaPhieu` (NVARCHAR(20))
* **Khóa ngoại:** `MaNV` tham chiếu `NHAN_VIEN(MaNV)` (Thủ kho làm phiếu).
* **Các trường dữ liệu:**
  * `NgayNhap` (DATETIME) - Ngày giờ nhập kho.
  * `GhiChu` (NVARCHAR(500)) - Ghi chú đợt nhập.

### 1.8 CT_NHAP_KHO (Danh sách chi tiết các mặt hàng trong phiếu nhập)
* **Khóa chính phức hợp:** `(MaPhieu, MaSP)`
* **Khóa ngoại:**
  * `MaPhieu` tham chiếu `PHIEU_NHAP_KHO(MaPhieu)`
  * `MaSP` tham chiếu `SAN_PHAM(MaSP)`
* **Các trường dữ liệu:**
  * `SoLuong` (INT) - Số lượng hoa nhập thêm.
  * `GiaNhap` (DECIMAL) - Đơn giá nhập thực tế.

### 1.9 TRA_HANG (Thông tin phiếu trả lại hàng của khách hàng)
* **Khóa chính:** `MaPhieuTra` (NVARCHAR(20))
* **Khóa ngoại:** `MaDon` tham chiếu `DON_HANG(MaDon)`
* **Các trường dữ liệu:**
  * `LyDo` (NVARCHAR(500)) - Nguyên nhân trả hàng.
  * `HinhThucHoanTien` (NVARCHAR(50)) - `TienMat`, `ChuyenKhoan`, `DoiHang`.
  * `NgayTra` (DATETIME) - Ngày giờ hoàn trả hàng.
  * `GhiChu` (NVARCHAR(500)) - Ghi chú thêm.

### 1.10 CT_TRA_HANG (Chi tiết các loại hoa khách hàng trả lại)
* **Khóa chính phức hợp:** `(MaPhieuTra, MaSP)`
* **Khóa ngoại:**
  * `MaPhieuTra` tham chiếu `TRA_HANG(MaPhieuTra)`
  * `MaSP` tham chiếu `SAN_PHAM(MaSP)`
* **Các trường dữ liệu:**
  * `SoLuong` (INT) - Số lượng sản phẩm trả lại.
  * `CoNhapKho` (BIT) - Cờ xác nhận (`1`: Hoa còn tốt, nhập lại kho; `0`: Hoa hỏng, tiêu hủy).

### 1.11 HANG_HU (Ghi nhận tổn thất hoa bị héo dập trong kho)
* **Khóa chính:** `MaPhieuHuy` (NVARCHAR(20))
* **Khóa ngoại:** `MaSP` tham chiếu `SAN_PHAM(MaSP)`
* **Các trường dữ liệu:**
  * `SoLuong` (INT) - Số lượng hoa bị hư hại.
  * `LyDo` (NVARCHAR(200)) - Lý do (Ví dụ: Hoa héo, gãy cành).
  * `NgayHuy` (DATETIME) - Ngày ghi nhận hủy hàng.
  * `GhiChu` (NVARCHAR(500)) - Ghi chú thêm.

### 1.12 PHAN_HOI (Đánh giá và phản hồi của khách hàng)
* **Khóa chính:** `MaPH` (NVARCHAR(20))
* **Khóa ngoại:** `MaDon` tham chiếu `DON_HANG(MaDon)`
* **Các trường dữ liệu:**
  * `NoiDung` (NVARCHAR(1000)) - Nội dung phản hồi.
  * `NgayGhi` (DATETIME) - Ngày nhận phản hồi.
  * `TrangThaiXuLy` (NVARCHAR(30)) - Trạng thái: `ChuaXuLy`, `DangXuLy`, `DaXuLy`.
  * `KetQuaXuLy` (NVARCHAR(500)) - Biện pháp đền bù/giải quyết (nếu có).

### 1.13 PHAN_QUYEN (Quản lý phân quyền chức năng cho các vai trò)
* **Khóa chính phức hợp:** `(ChucVu, Module)`
* **Các trường dữ liệu:**
  * `ChucVu` (NVARCHAR(20)) - Vai trò nhân viên.
  * `Module` (NVARCHAR(50)) - Tên chức năng hệ thống (Ví dụ: `DonHang`, `KhoHang`, v.v.).
  * `Xem`, `Them`, `Sua`, `Xoa`, `Export` (BIT) - Lưu quyền dưới dạng Đúng (`1`) / Sai (`0`).

### 1.14 LICH_SU_DON_HANG (Lưu vết thay đổi trạng thái đơn hàng)
* **Khóa chính:** `Id` (INT IDENTITY - Tự tăng)
* **Khóa ngoại:** `MaDon` tham chiếu `DON_HANG(MaDon)`
* **Các trường dữ liệu:**
  * `TrangThai` (NVARCHAR(20)) - Trạng thái mới được cập nhật.
  * `ThoiGian` (DATETIME) - Mốc thời gian thực hiện cập nhật.
  * `GhiChu` (NVARCHAR(500)) - Mô tả hành động (Ví dụ: "Tạo mới đơn hàng", "Giao hàng thành công").

---

<a name="2-he-thong-trigger"></a>
## ⚡ 2. HỆ THỐNG TRIGGER (TỰ ĐỘNG HÓA DỮ LIỆU THỜI GIAN THỰC)

Hệ thống FloriSys thiết lập **5 Triggers** cực kỳ quan trọng giúp đảm bảo tính tự động hóa và toàn vẹn dữ liệu:

### 2.1 Trigger `trg_TinhThanhTien` (Trên bảng `CHI_TIET_DON_HANG`)
* **Chức năng:** Tự động tính giá trị của cột `ThanhTien` mỗi khi có hành động thêm mới (INSERT) hoặc cập nhật (UPDATE) chi tiết hóa đơn.
* **Đoạn mã SQL:**
```sql
CREATE OR ALTER TRIGGER trg_TinhThanhTien
ON CHI_TIET_DON_HANG
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE ct SET ct.ThanhTien = ct.SoLuong * ct.DonGia
    FROM CHI_TIET_DON_HANG ct
    INNER JOIN inserted i ON ct.MaDon = i.MaDon AND ct.MaSP = i.MaSP;
END;
```

### 2.2 Trigger `trg_CapNhatTongTien` (Trên bảng `CHI_TIET_DON_HANG`)
* **Chức năng:** Tự động tính toán tổng số tiền của tất cả các dòng chi tiết và cập nhật trực tiếp vào trường `TongTien` trong bảng `DON_HANG` mỗi khi có hành động Thêm, Sửa hoặc Xóa sản phẩm.
* **Đoạn mã SQL:**
```sql
CREATE OR ALTER TRIGGER trg_CapNhatTongTien
ON CHI_TIET_DON_HANG
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dh SET dh.TongTien = ISNULL((SELECT SUM(ThanhTien) FROM CHI_TIET_DON_HANG WHERE MaDon = dh.MaDon), 0)
    FROM DON_HANG dh
    WHERE dh.MaDon IN (SELECT MaDon FROM inserted UNION SELECT MaDon FROM deleted);
END;
```

### 2.3 Trigger `trg_NhapKho_TangTon` (Trên bảng `CT_NHAP_KHO`)
* **Chức năng:** Tự động cộng dồn số lượng sản phẩm nhập kho vào cột tồn kho của sản phẩm trong bảng `SAN_PHAM` ngay khi xác nhận nhập kho thành công.
* **Đoạn mã SQL:**
```sql
CREATE OR ALTER TRIGGER trg_NhapKho_TangTon
ON CT_NHAP_KHO
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE sp SET sp.SoLuongTon = sp.SoLuongTon + i.SoLuong
    FROM SAN_PHAM sp
    INNER JOIN inserted i ON sp.MaSP = i.MaSP;
END;
```

### 2.4 Trigger `trg_DonHang_Insert_Log` (Trên bảng `DON_HANG`)
* **Chức năng:** Tự động chèn một dòng ghi nhận lịch sử trạng thái đầu tiên là "Tạo đơn hàng mới" vào bảng `LICH_SU_DON_HANG` khi có một đơn hàng mới được thêm vào hệ thống.

### 2.5 Trigger `trg_DonHang_Update_Log` (Trên bảng `DON_HANG`)
* **Chức năng:** Kiểm tra xem trạng thái của đơn hàng có bị thay đổi không. Nếu có, tự động ghi nhận lại trạng thái mới và mô tả hành động (Ví dụ: Hủy đơn, hoàn thành đơn, giao lại) vào bảng `LICH_SU_DON_HANG`.

---

<a name="3-rang-buoc-toan-ven"></a>
## 🔒 3. RÀNG BUỘC TOÀN VẸN (CONSTRAINTS) & BẢO MẬT

Hệ thống của chúng ta áp dụng các ràng buộc chặt chẽ sau để tránh lỗi dữ liệu bất hợp lý:
* **Ràng buộc CHECK giá trị dương:**
  * Bảng `SAN_PHAM`: `GiaBan >= 0`, `GiaNhap >= 0`, `SoLuongTon >= 0`.
  * Bảng `CHI_TIET_DON_HANG`: `SoLuong > 0`, `DonGia >= 0`.
* **Ràng buộc CHECK trạng thái giới hạn (Domain constraints):**
  * `NHAN_VIEN.ChucVu`: Bắt buộc chỉ nằm trong 4 vai trò: `Admin`, `Cashier`, `Warehouse`, `Shipper`.
  * `DON_HANG.HinhThucNhanHang`: Chỉ được chọn `TaiQuay` hoặc `GiaoTanNoi`.
  * `DON_HANG.TrangThai`: Chỉ chấp nhận các trạng thái hợp lệ của vòng đời đơn hàng (`Moi`, `DangXuLy`, `DaGiao`, `HoanThanh`, `Huy`, `HoanHang`).
* **Mã hóa một chiều mật khẩu:**
  * Mật khẩu của nhân viên được băm bằng thuật toán **SHA-256** (mã hóa một chiều dài 64 ký tự hex) ngay từ tầng C# trước khi gửi xuống database, ngăn chặn việc rò rỉ mật khẩu gốc của người dùng.

---

<a name="4-stored-procedures"></a>
## ⚙️ 4. DANH SÁCH & CHỨC NĂNG CÁC STORED PROCEDURE

Dưới đây là các Stored Procedure chính được tối ưu hóa sẵn trong hệ thống:

### 4.1 sp_ThemChiTietDon (Thêm sản phẩm & Kiểm tra tồn kho an toàn)
* **Nghiệm vụ:** Thực hiện kiểm tra lượng hoa còn tồn kho trước khi bán. Nếu không đủ, lệnh `RAISERROR` sẽ được kích hoạt để rollback toàn bộ giao dịch bán hàng, trả về thông báo lỗi chi tiết cho tầng C#. Nếu đủ hàng, hệ thống tự động trừ kho của sản phẩm đó.
* **Đoạn mã SQL:**
```sql
CREATE PROCEDURE sp_ThemChiTietDon
    @MaDon  NVARCHAR(20),
    @MaSP   NVARCHAR(20),
    @SoLuong INT,
    @DonGia  DECIMAL(18,0)
AS
BEGIN
    DECLARE @TonKho INT;
    SELECT @TonKho = SoLuongTon FROM SAN_PHAM WHERE MaSP = @MaSP;
    
    IF @TonKho < @SoLuong
    BEGIN
        RAISERROR(N'Tồn kho không đủ! Còn %d, yêu cầu %d.', 16, 1, @TonKho, @SoLuong);
        RETURN;
    END

    INSERT INTO CHI_TIET_DON_HANG (MaDon, MaSP, SoLuong, DonGia, ThanhTien)
    VALUES (@MaDon, @MaSP, @SoLuong, @DonGia, @SoLuong * @DonGia);

    -- Tự động trừ tồn kho ngay lập tức
    UPDATE SAN_PHAM 
    SET SoLuongTon = SoLuongTon - @SoLuong 
    WHERE MaSP = @MaSP;
END;
```

### 4.2 sp_CapNhatTrangThaiDon (Cập nhật trạng thái đơn & tự động hoàn kho khi hủy)
* **Nhiệm vụ:** Thay đổi trạng thái đơn hàng. Đặc biệt, nếu đơn hàng bị chuyển trạng thái thành **`Huy` (Hủy đơn)**, thủ tục sẽ tự động tính toán số lượng của các mặt hàng trong đơn và **cộng trả lại vào tồn kho** của sản phẩm đó.
```sql
CREATE PROCEDURE sp_CapNhatTrangThaiDon
    @MaDon     NVARCHAR(20),
    @TrangThai NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    -- Nếu hủy đơn, tự động cộng lại tồn kho cho các sản phẩm đã mua
    IF @TrangThai = N'Huy'
    BEGIN
        DECLARE @TrangThaiHienTai NVARCHAR(20);
        SELECT @TrangThaiHienTai = TrangThai FROM DON_HANG WHERE MaDon = @MaDon;

        IF @TrangThaiHienTai IN (N'Moi', N'DangXuLy')
        BEGIN
            UPDATE sp 
            SET sp.SoLuongTon = sp.SoLuongTon + ct.SoLuong
            FROM SAN_PHAM sp
            INNER JOIN CHI_TIET_DON_HANG ct ON sp.MaSP = ct.MaSP
            WHERE ct.MaDon = @MaDon;
        END
    END
    UPDATE DON_HANG SET TrangThai = @TrangThai WHERE MaDon = @MaDon;
END;
```

### 4.3 sp_GhiNhanHangHu (Hủy sản phẩm hỏng hóc & Bảo vệ tồn kho)
* **Nhiệm vụ:** Kiểm tra xem số lượng hủy có vượt quá lượng tồn kho thực tế hay không. Nếu hợp lệ, tiến hành trừ số lượng tồn của sản phẩm đó và ghi chép nhật ký vào bảng `HANG_HU`.

### 4.4 sp_SanPhamBanChay (Thống kê top sản phẩm bán chạy theo tháng/năm)
* **Nhiệm vụ:** Trích xuất danh sách 10 sản phẩm có tổng doanh thu hoặc số lượng bán nhiều nhất trong một tháng hoặc một năm cụ thể để phục vụ vẽ biểu đồ và xuất báo cáo.
```sql
CREATE PROCEDURE sp_SanPhamBanChay
    @Thang INT = NULL,
    @Nam   INT = NULL
AS
BEGIN
    SELECT TOP 10
        sp.MaSP, sp.TenSP, sp.LoaiHoa,
        SUM(ct.SoLuong) AS TongSoLuong,
        SUM(ct.ThanhTien) AS TongDoanhThu
    FROM CHI_TIET_DON_HANG ct
    INNER JOIN SAN_PHAM sp ON ct.MaSP = sp.MaSP
    INNER JOIN DON_HANG dh ON ct.MaDon = dh.MaDon
    WHERE dh.TrangThai NOT IN (N'Huy', N'HoanHang')
        AND (@Thang IS NULL OR MONTH(dh.NgayTao) = @Thang)
        AND (@Nam IS NULL OR YEAR(dh.NgayTao) = @Nam)
    GROUP BY sp.MaSP, sp.TenSP, sp.LoaiHoa
    ORDER BY TongDoanhThu DESC; -- Hoặc TongSoLuong DESC tùy tiêu chí lựa chọn
END;
```

### 4.5 sp_SinhMa (Cơ chế sinh mã tự động thông minh cho các bảng)
* **Nhiệm vụ:** Tự động đếm số lượng dòng bản ghi hiện tại trong bảng, trích xuất số lớn nhất để cộng thêm 1, sau đó sinh ra một mã mới có độ dài cố định kèm theo tiền tố (Ví dụ: `SP` + `000001` = `SP000001` cho sản phẩm).
* **Đoạn mã SQL:**
```sql
CREATE PROCEDURE sp_SinhMa
    @Prefix NVARCHAR(10),
    @Table  NVARCHAR(50),
    @Column NVARCHAR(50),
    @NewCode NVARCHAR(20) OUTPUT
AS
BEGIN
    DECLARE @SQL NVARCHAR(500);
    DECLARE @MaxNum INT;
    SET @SQL = N'SELECT @Num = ISNULL(MAX(CAST(SUBSTRING(' + QUOTENAME(@Column) + N', LEN(''' + @Prefix + N''')+1, 10) AS INT)), 0) FROM ' + QUOTENAME(@Table);
    EXEC sp_executesql @SQL, N'@Num INT OUTPUT', @Num = @MaxNum OUTPUT;
    SET @NewCode = @Prefix + RIGHT('000000' + CAST(@MaxNum + 1 AS NVARCHAR), 6);
END;
```
*(C chế này giúp toàn bộ hệ thống tự động sinh mã khóa chính mà không xảy ra hiện tượng trùng lặp hoặc xung đột khóa).*

---

<a name="5-kich-ban-nang-cao"></a>
## 🚀 5. CÁC KỊCH BẢN NÂNG CAO THẦY CÓ THỂ YÊU CẦU THÊM

Dưới đây là mã SQL chi tiết của 4 kịch bản nâng cao đề xuất (2 Trigger và 2 Stored Procedure) để bạn có thể chạy trực tiếp trong SQL Server Management Studio (SSMS) khi giảng viên yêu cầu viết thêm.

### 5.1 TRIGGER: Chặn bán lỗ (Không cho phép bán giá thấp hơn giá nhập)
* **Ý nghĩa nghiệp vụ:** Ngăn ngừa việc nhân viên tự ý giảm giá bán của sản phẩm thấp hơn giá nhập kho ban đầu của sản phẩm đó để đảm bảo lợi nhuận tối thiểu cho cửa hàng.
```sql
CREATE OR ALTER TRIGGER trg_ChanBanLo
ON CHI_TIET_DON_HANG
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra nếu có dòng chi tiết đơn hàng nào có giá bán thấp hơn giá nhập của sản phẩm đó
    IF EXISTS (
        SELECT 1 FROM inserted i
        INNER JOIN SAN_PHAM sp ON i.MaSP = sp.MaSP
        WHERE i.DonGia < sp.GiaNhap
    )
    BEGIN
        -- Báo lỗi ra màn hình
        RAISERROR (N'Lỗi: Giá bán sản phẩm không được phép thấp hơn giá nhập vốn trong kho!', 16, 1);
        -- Hủy bỏ giao dịch chèn/cập nhật dữ liệu
        ROLLBACK TRANSACTION;
    END
END;
GO
```

### 5.2 TRIGGER: Tự động trừ tồn kho khi phát sinh phiếu hủy hàng hỏng (`HANG_HU`)
* **Ý nghĩa nghiệp vụ:** Tự động trừ số lượng sản phẩm bị hao hụt/hỏng hóc khỏi kho hàng ngay khi thủ kho ghi nhận phiếu hàng hủy mà không cần chạy code trừ kho thủ công ở ứng dụng C#.
```sql
CREATE OR ALTER TRIGGER trg_HangHu_GiamTon
ON HANG_HU
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Cập nhật trừ trực tiếp số lượng tồn kho của sản phẩm bị hủy
    UPDATE sp SET sp.SoLuongTon = sp.SoLuongTon - i.SoLuong
    FROM SAN_PHAM sp
    INNER JOIN inserted i ON sp.MaSP = i.MaSP;
END;
GO
```

### 5.3 STORED PROCEDURE: Thống kê doanh thu, chi phí vốn và lợi nhuận theo Tháng
* **Ý nghĩa nghiệp vụ:** Giúp quản lý xem báo cáo tài chính chi tiết của một tháng bất kỳ gồm: Tổng doanh thu bán ra, Tổng số tiền vốn bỏ ra để nhập số hoa đã bán, và Lợi nhuận ròng thu về.
```sql
CREATE OR ALTER PROCEDURE sp_TinhLoiNhuanThang
    @Thang INT,
    @Nam INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        ISNULL(SUM(ct.SoLuong * ct.DonGia), 0) AS TongDoanhThu,
        ISNULL(SUM(ct.SoLuong * sp.GiaNhap), 0) AS TongTienVon,
        ISNULL(SUM(ct.SoLuong * (ct.DonGia - sp.GiaNhap)), 0) AS TongLoiNhuan
    FROM CHI_TIET_DON_HANG ct
    INNER JOIN DON_HANG dh ON ct.MaDon = dh.MaDon
    INNER JOIN SAN_PHAM sp ON ct.MaSP = sp.MaSP
    WHERE MONTH(dh.NgayTao) = @Thang 
      AND YEAR(dh.NgayTao) = @Nam 
      AND dh.TrangThai NOT IN (N'Huy', N'HoanHang');
END;
GO
```

### 5.4 STORED PROCEDURE: Tự động tìm kiếm và phân công Shipper rảnh nhất
* **Ý nghĩa nghiệp vụ:** Tự động tìm kiếm trong danh sách nhân viên giao hàng đang làm việc xem ai đang có ít đơn hàng cần giao (`DangGiao`) nhất để tự động gán đơn hàng mới, giúp phân bổ công việc công bằng và nhanh chóng.
```sql
CREATE OR ALTER PROCEDURE sp_TuDongPhanCongShipper
    @MaDon NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ShipperRanh NVARCHAR(20);

    -- Tìm kiếm shipper đang làm việc có số lượng đơn 'DangGiao' ít nhất hiện tại
    SELECT TOP 1 @ShipperRanh = nv.MaNV
    FROM NHAN_VIEN nv
    LEFT JOIN GIAO_HANG gh ON nv.MaNV = gh.MaNV_Shipper AND gh.TrangThai = N'DangGiao'
    WHERE nv.ChucVu = N'Shipper' AND nv.TrangThai = N'DangLam'
    GROUP BY nv.MaNV
    ORDER BY COUNT(gh.MaDon) ASC;

    -- Nếu tìm thấy shipper phù hợp, tiến hành phân công cho đơn hàng
    IF @ShipperRanh IS NOT NULL
    BEGIN
        UPDATE GIAO_HANG 
        SET MaNV_Shipper = @ShipperRanh, 
            TrangThai = N'DangGiao', 
            NgayGiao = GETDATE()
        WHERE MaDon = @MaDon;
        
        PRINT N'Đã tự động phân công đơn hàng cho Shipper: ' + @ShipperRanh;
    END
    ELSE
    BEGIN
        PRINT N'Không tìm thấy shipper nào đang sẵn sàng!';
    END
END;
GO
