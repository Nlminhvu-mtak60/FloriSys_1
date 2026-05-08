# 🌸 FloriSys — Tài Liệu Tổng Quan & Hướng Dẫn Bảo Vệ Project

> **Dự án:** Hệ thống Quản lý Cửa hàng Hoa — FloriSys  
> **Công nghệ:** C# WinForms · .NET Framework 4.7.2 · SQL Server 2022  
> **Tác giả:** Nlminhvu-mtak60  
> **Phiên bản tài liệu:** 08/05/2026

---

## MỤC LỤC

1. [Tổng Quan Dự Án](#1-tổng-quan-dự-án)
2. [Kiến Trúc Hệ Thống](#2-kiến-trúc-hệ-thống)
3. [Các Module Chức Năng](#3-các-module-chức-năng)
4. [Phân Tích Code Chi Tiết](#4-phân-tích-code-chi-tiết)
5. [Cơ Sở Dữ Liệu — Phân Tích Đầy Đủ](#5-cơ-sở-dữ-liệu--phân-tích-đầy-đủ)
6. [Hướng Dẫn Trình Bày & Bảo Vệ Project](#6-hướng-dẫn-trình-bày--bảo-vệ-project)
7. [Câu Hỏi Giảng Viên Có Thể Hỏi & Gợi Ý Trả Lời](#7-câu-hỏi-giảng-viên-có-thể-hỏi--gợi-ý-trả-lời)
8. [Câu Hỏi Về CSDL & Hướng Dẫn Trả Lời](#8-câu-hỏi-về-csdl--hướng-dẫn-trả-lời)
9. [Điểm Mạnh Cần Nhấn Mạnh & Điểm Yếu Cần Chuẩn Bị](#9-điểm-mạnh-cần-nhấn-mạnh--điểm-yếu-cần-chuẩn-bị)

---

## 1. TỔNG QUAN DỰ ÁN

### 1.1 Giới thiệu

**FloriSys** là hệ thống phần mềm quản lý toàn diện cho cửa hàng hoa, được xây dựng bằng **C# WinForms** trên nền tảng **.NET Framework 4.7.2**, kết nối với **SQL Server 2022**. Hệ thống hướng đến việc số hóa và tự động hóa toàn bộ quy trình vận hành của một cửa hàng hoa: từ bán hàng, quản lý kho, giao hàng, trả hàng đến báo cáo thống kê.

### 1.2 Mục tiêu

- Quản lý đơn hàng, sản phẩm, khách hàng và nhân viên tập trung
- Kiểm soát tồn kho theo thời gian thực, cảnh báo khi hàng sắp hết
- Theo dõi vòng đời giao hàng từ phân công đến hoàn thành
- Xử lý trả hàng và hoàn tiền theo nhiều hình thức
- Cung cấp báo cáo doanh thu ngày/tháng và hiệu suất nhân viên
- Phân quyền theo vai trò: Admin, Thu ngân, Thủ kho, Shipper

### 1.3 Các vai trò người dùng

| Vai trò | Quyền hạn chính |
|---------|----------------|
| **Admin** | Toàn quyền: quản lý nhân viên, phân quyền, xử lý trả hàng, xem báo cáo |
| **Thu ngân (Cashier)** | Tạo đơn hàng, quản lý khách hàng, tiếp nhận phản hồi |
| **Thủ kho (Warehouse)** | Nhập kho, quản lý tồn kho, ghi nhận hàng hư |
| **Shipper** | Nhận phân công, cập nhật trạng thái giao hàng |

### 1.4 Công nghệ sử dụng

| Thành phần | Công nghệ |
|-----------|-----------|
| Ngôn ngữ | C# |
| Framework UI | Windows Forms (.NET Framework 4.7.2) |
| Database | SQL Server 2022 |
| Kết nối DB | ADO.NET (System.Data.SqlClient) |
| Kiến trúc | Layered Architecture (3 tầng) |
| Bảo mật | SHA-256 password hashing |
| Quản lý phiên | SessionManager (Singleton pattern) |

---

## 2. KIẾN TRÚC HỆ THỐNG

### 2.1 Mô hình 3 tầng (3-Layer Architecture)

```
┌─────────────────────────────────────────┐
│         PRESENTATION LAYER              │
│  WinForms: frmDangNhap, frmMain,        │
│  ucTaoDon, ucDanhSachDon, ucTraHang,    │
│  ucNhapKho, ucGiaoHang, ucBaoCao...     │
├─────────────────────────────────────────┤
│         BUSINESS LOGIC LAYER            │
│  Services: AuthService, DonHangService, │
│  GiaoHangService, KhoHangService,       │
│  TraHangService, SessionManager         │
├─────────────────────────────────────────┤
│         DATA ACCESS LAYER               │
│  DatabaseHelper (Generic)               │
│  DAOs: NhanVienDAO, SanPhamDAO,         │
│  DonHangDAO, GiaoHangDAO,              │
│  PhieuNhapKhoDAO, TraHangDAO, BaoCaoDAO│
│  Repositories: *Repository.cs           │
├─────────────────────────────────────────┤
│         DATABASE LAYER                  │
│  SQL Server: Tables, Triggers,          │
│  Stored Procedures, Constraints         │
└─────────────────────────────────────────┘
```

### 2.2 Luồng xử lý đăng nhập

```
User nhập TK/MK
    → frmDangNhap.cs: validate input
    → Hash mật khẩu bằng SHA-256
    → NhanVienDAO.DangNhap(taiKhoan, matKhauHash)
    → DatabaseHelper.ExecuteSingle<NhanVien>("sp_DangNhap", params)
    → SQL Server: sp_DangNhap kiểm tra TrangThai = 'DangLamViec'
    → SessionManager.CurrentUser = nhanVien (nếu thành công)
    → Mở frmMain với quyền tương ứng
```

### 2.3 Cấu trúc thư mục project

```
FloriSys_1/
├── 1_DangNhap/          # Đăng nhập, đổi mật khẩu
├── 2_QuanLy/            # Dashboard chính, quản lý nhân viên
├── 3_BanHang/           # Bán hàng, đơn hàng, trả hàng, phản hồi
├── 4_KhoHang/           # Kho hàng, nhập/xuất, tồn kho, hàng hư
├── 5_GiaoHang/          # Phân công, cập nhật giao hàng
├── 6_BaoCao/            # Báo cáo ngày, tháng, nhân viên, sản phẩm
├── 7_DanhMuc/           # Danh mục sản phẩm, khách hàng
├── DataAccess/          # DAO + Repository classes
├── Models/              # Các entity classes
├── Services/            # Business logic services
├── Shared/              # User controls dùng chung
├── FloriSys_Database.sql # Script tạo CSDL
└── FloriSys.csproj      # Project file
```

---

## 3. CÁC MODULE CHỨC NĂNG

### 3.1 Module Đăng Nhập (1_DangNhap)

**Màn hình:** `frmDangNhap`, `ucDoiMatKhau`

**Chức năng:**
- Xác thực tài khoản/mật khẩu qua Stored Procedure `sp_DangNhap`
- Mật khẩu được hash SHA-256 trước khi gửi lên DB
- Chặn đăng nhập nếu nhân viên có trạng thái `NghiViec`
- Đổi mật khẩu qua `sp_DoiMatKhau` (yêu cầu nhập mật khẩu cũ)

**Tài khoản mặc định:**
- Admin: `admin` / `123456`
- Các vai trò khác: mật khẩu mặc định `123456`

---

### 3.2 Module Quản Lý Chính (2_QuanLy)

**Màn hình:** `frmMain`, `ucDashboard`, `ucNhanVien`

**Chức năng:**
- Dashboard tổng quan: doanh thu hôm nay, số đơn, tồn kho thấp, cảnh báo
- Quản lý nhân viên: thêm/sửa/khóa tài khoản
- Phân quyền theo module (bảng `PHAN_QUYEN`)
- Menu động: hiển thị/ẩn module theo quyền của người dùng đang đăng nhập

---

### 3.3 Module Bán Hàng (3_BanHang)

**Màn hình:** `ucTaoDon`, `ucDanhSachDon`, `ucChiTietDonHang`, `ucPhanHoi`, `ucTraHang`

**Chức năng:**

**Tạo đơn hàng:**
1. Chọn khách hàng (tìm theo SĐT) hoặc tạo mới
2. Thêm sản phẩm vào giỏ — kiểm tra tồn kho real-time
3. Chọn hình thức nhận hàng: `TaiQuay` hoặc `GiaoHang`
4. Xác nhận → `sp_TaoDonHang` tạo đơn, `sp_ThemChiTietDon` thêm từng sản phẩm
5. Trigger tự động tính `ThanhTien` và `TongTien`

**Vòng đời đơn hàng:**
```
ChoDuyet → DangXuLy → ChoGiao → DangGiao → HoanThanh
                                          → DaHuy
                                          → DaTraHang
```

**Trả hàng (ucTraHang — DH-06):**
- Chỉ Admin mới có quyền thực hiện
- Nhập mã đơn gốc
- Chọn lý do (dropdown)
- Liệt kê sản phẩm trả lại
- Chọn hình thức hoàn tiền: `TienMat`, `ChuyenKhoan`, `TheNganHang`
- Tùy chọn nhập lại kho (`CoNhapKho = true/false`)
- Ghi chú nội bộ

---

### 3.4 Module Kho Hàng (4_KhoHang)

**Màn hình:** `ucNhapKho`, `ucXuatKho`, `ucTonKho`, `ucHangHu`, `ucLichSuNhapKho`, `ucCauHinhTonKho`, `ucDashboardKho`

**Chức năng:**
- Tạo phiếu nhập kho → trigger tự động tăng `SoLuongTon`
- Xuất kho khi đơn hàng được xử lý → trigger giảm `SoLuongTon`
- Ghi nhận hàng hư qua `sp_GhiNhanHangHu` — giảm tồn kho
- Cảnh báo tồn kho thấp qua `sp_CanhBaoTonKho`
- Cấu hình mức tồn kho tối thiểu từng sản phẩm

---

### 3.5 Module Giao Hàng (5_GiaoHang)

**Màn hình:** `ucGiaoHang`, `ucPhanCong`, `ucCapNhatGH`, `ucDashboardShipper`

**Chức năng:**
- Tạo phiếu giao hàng từ đơn có hình thức `GiaoHang`
- Phân công shipper qua `sp_PhanCongShipper`
- Cập nhật trạng thái giao: `ChoGiao → DangGiao → DaGiao / ThatBai`
- Khi giao thành công → `DON_HANG.TrangThai` chuyển sang `HoanThanh`
- Dashboard shipper: thống kê đơn hôm nay, đã giao, đang giao, chưa giao

---

### 3.6 Module Báo Cáo (6_BaoCao)

**Màn hình:** `ucBaoCaoNgay`, `ucBaoCaoThang`, `ucBaoCaoNhanVien`, `ucBaoCaoSanPham`, `ucBaoCaoTonKho`

**Chức năng:**
- Báo cáo doanh thu theo ngày (`sp_BaoCaoDoanhThuNgay`)
- Báo cáo doanh thu theo tháng (`sp_BaoCaoDoanhThuThang`)
- Sản phẩm bán chạy (`sp_SanPhamBanChay`)
- Hiệu suất nhân viên (`sp_HieuSuatNhanVien`)
- Biểu đồ doanh thu từng ngày trong tháng (`sp_DoanhThuTheoNgayTrongThang`)
- Báo cáo tồn kho hiện tại

---

### 3.7 Module Danh Mục (7_DanhMuc)

**Màn hình:** `ucSanPham`, `ucKhachHang`, `frmThemSanPham`

**Chức năng:**
- CRUD sản phẩm: tên, loại hoa, giá bán, giá nhập, tồn kho, trạng thái
- CRUD khách hàng: họ tên, SĐT (unique), địa chỉ, email
- Tìm kiếm và lọc theo nhiều tiêu chí

---

## 4. PHÂN TÍCH CODE CHI TIẾT

### 4.1 DatabaseHelper.cs — Lớp kết nối trung tâm

`DatabaseHelper` là lớp generic dùng chung cho toàn bộ tầng Data Access. Nó cung cấp các phương thức:

- **`ExecuteList<T>(spName, params)`** — Thực thi stored procedure, trả về `List<T>`. Dùng Reflection để map từng cột DB vào property của model tương ứng (theo tên).
- **`ExecuteSingle<T>(spName, params)`** — Như trên nhưng chỉ lấy 1 bản ghi.
- **`ExecuteNonQuery(spName, params)`** — Thực thi các lệnh INSERT/UPDATE/DELETE, trả về số row bị ảnh hưởng.
- **`ExecuteScalar<T>(spName, params)`** — Trả về một giá trị đơn (dùng cho count, sum...).
- **`GenerateCode(prefix, tableName, columnName)`** — Gọi `sp_SinhMa` để sinh mã tự động (VD: `DH001`, `NV003`).

**Chuỗi kết nối** đọc từ `App.config`:
```xml
<connectionStrings>
  <add name="FloriSys" 
       connectionString="Server=.;Database=FloriSys;Integrated Security=True;TrustServerCertificate=True"/>
</connectionStrings>
```

### 4.2 SessionManager.cs — Quản lý phiên đăng nhập

Triển khai theo **Singleton Pattern**:
```csharp
// Lưu thông tin nhân viên đang đăng nhập
SessionManager.CurrentUser = nhanVien;
// Kiểm tra quyền
SessionManager.HasPermission(module, action);
// Đăng xuất
SessionManager.Logout();
```

Sau khi đăng nhập, `frmMain` load menu động dựa trên `PHAN_QUYEN` của `CurrentUser.ChucVu`.

### 4.3 BaseRepository.cs — Generic CRUD

`BaseRepository<T>` cung cấp các phương thức CRUD chung mà các Repository cụ thể kế thừa và override khi cần. Giảm trùng lặp code giữa các DAO.

### 4.4 AuthService.cs

Xử lý logic xác thực:
- Hash mật khẩu SHA-256 trước khi gọi DB
- Kiểm tra trạng thái tài khoản
- Đóng gói NhanVienDAO, không để UI gọi trực tiếp DAO

### 4.5 TraHangService.cs

Điều phối quy trình trả hàng:
1. Kiểm tra đơn hàng tồn tại và hợp lệ
2. Tạo phiếu trả hàng (`TRA_HANG`)
3. Thêm từng sản phẩm trả (`CT_TRA_HANG`)
4. Nếu `CoNhapKho = true` → cập nhật lại tồn kho
5. Cập nhật trạng thái đơn hàng gốc → `DaTraHang`

### 4.6 Models — Các Entity Class

Mỗi model là POCO (Plain Old C# Object) ánh xạ trực tiếp từ bảng DB, bổ sung thêm computed properties cho UI:

```csharp
// Ví dụ: SanPham.cs
public class SanPham {
    public string MaSP { get; set; }
    public string TenSP { get; set; }
    public decimal GiaBan { get; set; }
    public int SoLuongTon { get; set; }
    public int MucTonToiThieu { get; set; }
    public string TrangThai { get; set; }
    
    // Computed property cho UI
    public string TrangThaiDisplay => TrangThai == "DangBan" ? "Đang bán" : "Ngừng bán";
    public string TinhTrangDisplay => SoLuongTon <= MucTonToiThieu ? "⚠ Sắp hết" : "Bình thường";
}
```

---

## 5. CƠ SỞ DỮ LIỆU — PHÂN TÍCH ĐẦY ĐỦ

### 5.1 Tổng Quan CSDL

- **Tên database:** `FloriSys`
- **DBMS:** SQL Server 2022
- **Script khởi tạo:** `FloriSys_Database.sql`
- **Số bảng:** 14 bảng chính
- **Stored Procedures:** 16+ stored procedures
- **Triggers:** 3 triggers chính
- **Chuẩn hóa:** Đạt 3NF (Third Normal Form)

---

### 5.2 Sơ Đồ ERD — Quan Hệ Giữa Các Bảng

```
NHAN_VIEN ──────────────┐
  │ MaNV (PK)           │ tạo đơn
  └──────► DON_HANG ◄───┘
             │ MaDon (PK)
             │
    ┌────────┼────────────────────┐
    ▼        ▼                   ▼
CHI_TIET  GIAO_HANG           TRA_HANG
_DON_HANG   │                    │
    │       │ phân công          │
    ▼       ▼                   ▼
 SAN_PHAM NHAN_VIEN          CT_TRA_HANG
           (Shipper)            │
                                ▼
                             SAN_PHAM

PHIEU_NHAP_KHO ──► CT_NHAP_KHO ──► SAN_PHAM
HANG_HU ──► SAN_PHAM
PHAN_HOI ──► DON_HANG
CANH_BAO_TON_KHO ──► SAN_PHAM
PHAN_QUYEN: (ChucVu, Module) — không FK
```

---

### 5.3 Chi Tiết Từng Bảng

#### Bảng NHAN_VIEN
```sql
MaNV        VARCHAR(10)   PK
HoTen       NVARCHAR(100) NOT NULL
ChucVu      VARCHAR(20)   CHECK IN ('Admin','ThuNgan','ThuKho','Shipper')
SoDienThoai VARCHAR(15)   NOT NULL
TaiKhoan    VARCHAR(50)   UNIQUE NOT NULL
MatKhau     VARCHAR(255)  NOT NULL  -- SHA-256 hash
TrangThai   VARCHAR(20)   CHECK IN ('DangLamViec','NghiViec')
```
**Ý nghĩa:** Quản lý toàn bộ nhân viên. `ChucVu` xác định phân quyền hệ thống. `MatKhau` lưu hash SHA-256 (không lưu plain text).

#### Bảng KHACH_HANG
```sql
MaKH        VARCHAR(10)   PK
HoTen       NVARCHAR(100) NOT NULL
SoDienThoai VARCHAR(15)   UNIQUE NOT NULL
DiaChi      NVARCHAR(200)
Email       VARCHAR(100)
NgayTao     DATETIME      DEFAULT GETDATE()
```
**Ý nghĩa:** Thông tin khách hàng. `SoDienThoai` là UNIQUE — dùng để tìm kiếm nhanh và tránh trùng lặp.

#### Bảng SAN_PHAM
```sql
MaSP           VARCHAR(10)    PK
TenSP          NVARCHAR(100)  NOT NULL
LoaiHoa        NVARCHAR(50)
GiaBan         DECIMAL(18,2)  CHECK >= 0
GiaNhap        DECIMAL(18,2)  CHECK >= 0
SoLuongTon     INT            CHECK >= 0, DEFAULT 0
MucTonToiThieu INT            DEFAULT 10
TrangThai      VARCHAR(20)    CHECK IN ('DangBan','NgungBan')
```
**Ý nghĩa:** Danh mục sản phẩm. `MucTonToiThieu` là ngưỡng cảnh báo. Khi `SoLuongTon <= MucTonToiThieu`, hệ thống cảnh báo "sắp hết hàng".

#### Bảng DON_HANG
```sql
MaDon          VARCHAR(10)   PK
NgayTao        DATETIME      DEFAULT GETDATE()
MaKH           VARCHAR(10)   FK → KHACH_HANG
MaNV_TaoDon    VARCHAR(10)   FK → NHAN_VIEN
HinhThucNhanHang VARCHAR(20) CHECK IN ('TaiQuay','GiaoHang')
TrangThai      VARCHAR(20)   CHECK IN ('ChoDuyet','DangXuLy','ChoGiao',
                                       'DangGiao','HoanThanh','DaHuy','DaTraHang')
TongTien       DECIMAL(18,2) DEFAULT 0  -- tự động tính bởi trigger
GhiChu         NVARCHAR(500)
```
**Ý nghĩa:** Header của đơn hàng. `TongTien` không nhập tay — được trigger tính từ `CHI_TIET_DON_HANG`.

#### Bảng CHI_TIET_DON_HANG
```sql
MaDon      VARCHAR(10)   PK, FK → DON_HANG
MaSP       VARCHAR(10)   PK, FK → SAN_PHAM
SoLuong    INT           CHECK > 0
DonGia     DECIMAL(18,2) CHECK > 0
ThanhTien  DECIMAL(18,2) DEFAULT 0  -- tự động = SoLuong * DonGia
```
**Ý nghĩa:** Chi tiết từng sản phẩm trong đơn. Khóa chính composite (MaDon, MaSP). `ThanhTien` do trigger tính.

#### Bảng GIAO_HANG
```sql
MaGiaoHang    VARCHAR(10)  PK
MaDon         VARCHAR(10)  FK → DON_HANG
MaNV_Shipper  VARCHAR(10)  FK → NHAN_VIEN (nullable — chưa phân công)
NgayGiao      DATETIME
TrangThai     VARCHAR(20)  CHECK IN ('ChoGiao','DangGiao','DaGiao','ThatBai')
GhiChuGiaoHang NVARCHAR(500)
```
**Ý nghĩa:** Quản lý vận chuyển. `MaNV_Shipper` nullable (có thể chưa phân công). Khi `TrangThai = 'DaGiao'` → trigger/SP cập nhật `DON_HANG.TrangThai = 'HoanThanh'`.

#### Bảng PHIEU_NHAP_KHO
```sql
MaPhieu  VARCHAR(10)  PK
NgayNhap DATETIME     DEFAULT GETDATE()
MaNV     VARCHAR(10)  FK → NHAN_VIEN
GhiChu   NVARCHAR(500)
```

#### Bảng CT_NHAP_KHO
```sql
MaPhieu  VARCHAR(10)   PK, FK → PHIEU_NHAP_KHO
MaSP     VARCHAR(10)   PK, FK → SAN_PHAM
SoLuong  INT           CHECK > 0
GiaNhap  DECIMAL(18,2) CHECK > 0
```
**Ý nghĩa:** Khi INSERT vào `CT_NHAP_KHO` → trigger tự động tăng `SAN_PHAM.SoLuongTon`.

#### Bảng PHAN_HOI
```sql
MaPH          VARCHAR(10)  PK
MaDon         VARCHAR(10)  FK → DON_HANG
NoiDung       NVARCHAR(MAX)
NgayGhi       DATETIME     DEFAULT GETDATE()
TrangThaiXuLy VARCHAR(20)  CHECK IN ('ChuaXuLy','DangXuLy','DaXuLy')
KetQuaXuLy    NVARCHAR(500)
```

#### Bảng CANH_BAO_TON_KHO
```sql
MaSP        VARCHAR(10)  PK, FK → SAN_PHAM
MucToiThieu INT          DEFAULT 10
NgayCapNhat DATETIME     DEFAULT GETDATE()
```
**Ý nghĩa:** Bảng phụ theo dõi ngưỡng tồn kho từng sản phẩm riêng biệt.

#### Bảng HANG_HU
```sql
MaPhieuHuy VARCHAR(10)  PK
MaSP       VARCHAR(10)  FK → SAN_PHAM
SoLuong    INT          CHECK > 0
LyDo       NVARCHAR(500)
NgayHuy    DATETIME     DEFAULT GETDATE()
GhiChu     NVARCHAR(500)
```
**Ý nghĩa:** Ghi nhận khi hàng bị hỏng/hư. `sp_GhiNhanHangHu` giảm `SoLuongTon` tương ứng và kiểm tra không để tồn kho < 0.

#### Bảng PHAN_QUYEN
```sql
ChucVu  VARCHAR(20)  PK (composite)
Module  VARCHAR(50)  PK (composite)
Xem     BIT
Them    BIT
Sua     BIT
Xoa     BIT
Export  BIT
```
**Ý nghĩa:** Ma trận phân quyền. Không có FK — chỉ dùng `ChucVu` dạng chuỗi khớp với `NHAN_VIEN.ChucVu`. Seed data mặc định được INSERT khi khởi tạo DB.

#### Bảng TRA_HANG
```sql
MaPhieuTra      VARCHAR(10)  PK
MaDon           VARCHAR(10)  FK → DON_HANG
LyDo            NVARCHAR(500)
HinhThucHoanTien VARCHAR(20) CHECK IN ('TienMat','ChuyenKhoan','TheNganHang')
GhiChu          NVARCHAR(500)
NgayTra         DATETIME     DEFAULT GETDATE()
```

#### Bảng CT_TRA_HANG
```sql
MaPhieuTra VARCHAR(10)  PK, FK → TRA_HANG
MaSP       VARCHAR(10)  PK, FK → SAN_PHAM
SoLuong    INT          CHECK > 0
CoNhapKho  BIT          DEFAULT 0
```
**Ý nghĩa:** `CoNhapKho = 1` → sản phẩm trả lại được nhập vào kho (tăng tồn kho). `CoNhapKho = 0` → hàng bị hủy.

---

### 5.4 Các Trigger

#### Trigger 1: Tự động tính ThanhTien khi thêm/sửa chi tiết đơn
```sql
-- Sau INSERT/UPDATE trên CHI_TIET_DON_HANG
-- ThanhTien = SoLuong * DonGia
UPDATE CHI_TIET_DON_HANG
SET ThanhTien = SoLuong * DonGia
WHERE MaDon = @MaDon AND MaSP = @MaSP
```

#### Trigger 2: Tự động cập nhật TongTien đơn hàng
```sql
-- Sau INSERT/UPDATE/DELETE trên CHI_TIET_DON_HANG
-- TongTien = SUM(ThanhTien) của tất cả sản phẩm trong đơn
UPDATE DON_HANG
SET TongTien = (SELECT SUM(ThanhTien) FROM CHI_TIET_DON_HANG WHERE MaDon = @MaDon)
WHERE MaDon = @MaDon
```

#### Trigger 3: Tự động tăng tồn kho khi nhập hàng
```sql
-- Sau INSERT trên CT_NHAP_KHO
-- SoLuongTon += SoLuong vừa nhập
UPDATE SAN_PHAM
SET SoLuongTon = SoLuongTon + inserted.SoLuong
FROM SAN_PHAM JOIN inserted ON SAN_PHAM.MaSP = inserted.MaSP
```

---

### 5.5 Danh Sách Stored Procedures

| Nhóm | Tên SP | Mô tả |
|------|--------|-------|
| **Auth** | `sp_DangNhap` | Xác thực đăng nhập theo tài khoản + hash mật khẩu |
| **Auth** | `sp_DoiMatKhau` | Đổi mật khẩu (cần xác nhận mật khẩu cũ) |
| **Đơn hàng** | `sp_TaoDonHang` | Tạo header đơn hàng mới |
| **Đơn hàng** | `sp_ThemChiTietDon` | Thêm sản phẩm vào đơn (kiểm tra tồn kho) |
| **Đơn hàng** | `sp_CapNhatTrangThaiDon` | Chuyển trạng thái đơn hàng; giảm kho khi `DangXuLy`, hoàn kho khi hủy |
| **Kho** | `sp_TaoPhieuNhap` | Tạo phiếu nhập kho |
| **Kho** | `sp_ThemChiTietNhap` | Thêm sản phẩm vào phiếu nhập (trigger tự tăng kho) |
| **Kho** | `sp_GhiNhanHangHu` | Ghi nhận hàng hư, giảm tồn kho với validation |
| **Giao hàng** | `sp_TaoGiaoHang` | Tạo phiếu giao hàng từ đơn đã duyệt |
| **Giao hàng** | `sp_PhanCongShipper` | Gán shipper cho phiếu giao |
| **Giao hàng** | `sp_CapNhatTrangThaiGiao` | Cập nhật trạng thái giao hàng + đồng bộ DON_HANG |
| **Báo cáo** | `sp_BaoCaoDoanhThuNgay` | Tổng doanh thu theo ngày |
| **Báo cáo** | `sp_BaoCaoDoanhThuThang` | Tổng doanh thu theo tháng |
| **Báo cáo** | `sp_SanPhamBanChay` | Top sản phẩm bán chạy |
| **Báo cáo** | `sp_HieuSuatNhanVien` | Hiệu suất thu ngân theo đơn hàng |
| **Báo cáo** | `sp_CanhBaoTonKho` | Danh sách sản phẩm cần nhập thêm |
| **Báo cáo** | `sp_DoanhThuTheoNgayTrongThang` | Doanh thu từng ngày trong tháng (cho biểu đồ) |
| **Tiện ích** | `sp_SinhMa` | Sinh mã tự động tăng (DH001, NV003...) |

---

### 5.6 Chuẩn Hóa CSDL

**1NF (First Normal Form):**
- Tất cả các thuộc tính đều nguyên tử (atomic)
- Mỗi ô trong bảng chứa đúng 1 giá trị
- Có khóa chính rõ ràng cho mọi bảng

**2NF (Second Normal Form):**
- Đạt 1NF
- Mọi thuộc tính không khóa đều phụ thuộc hàm đầy đủ vào toàn bộ khóa chính
- Bảng `CHI_TIET_DON_HANG` có PK composite (MaDon, MaSP) — `SoLuong` và `DonGia` phụ thuộc vào cả hai

**3NF (Third Normal Form):**
- Đạt 2NF
- Không có phụ thuộc bắc cầu (transitive dependency)
- Ví dụ: Thông tin khách hàng tách ra bảng `KHACH_HANG` riêng, không lặp lại trong `DON_HANG`

---

## 6. HƯỚNG DẪN TRÌNH BÀY & BẢO VỆ PROJECT

### 6.1 Cấu Trúc Bài Trình Bày (Gợi Ý 15-20 Phút)

#### Phần 1: Giới Thiệu (2-3 phút)
> *"Chào thầy/cô, nhóm em xin trình bày dự án FloriSys — Hệ thống Quản lý Cửa hàng Hoa."*

- Nêu bài toán thực tế: cửa hàng hoa quản lý thủ công gặp nhiều vấn đề (nhầm đơn, thiếu hàng, khó theo dõi)
- Mục tiêu: xây dựng hệ thống desktop quản lý toàn diện
- Công nghệ: C# WinForms + SQL Server 2022

**Tip:** Dùng slide có ảnh chụp màn hình chính của phần mềm để tạo ấn tượng ngay từ đầu.

#### Phần 2: Demo Live (5-7 phút)

Thứ tự demo được khuyên:
1. **Đăng nhập** với tài khoản Admin → giới thiệu Dashboard
2. **Tạo đơn hàng mới** — từ bước chọn khách hàng đến xác nhận đơn
3. **Xem danh sách đơn** — lọc theo trạng thái
4. **Phân công giao hàng** — gán shipper
5. **Cập nhật trạng thái giao** — mô phỏng hoàn thành
6. **Xem báo cáo** — doanh thu ngày
7. **Xử lý trả hàng** — form DH-06 (đây là chức năng độc đáo)

**Tip:** Chuẩn bị sẵn dữ liệu mẫu, không để màn hình trắng khi demo.

#### Phần 3: Kiến Trúc Kỹ Thuật (3-4 phút)

- Vẽ sơ đồ 3 tầng lên slide
- Giải thích vai trò của `DatabaseHelper` (generic, dùng Reflection để map)
- Nhấn mạnh: tất cả thao tác DB đều qua Stored Procedure (không SQL inline)
- Giới thiệu hệ thống phân quyền theo `PHAN_QUYEN`

#### Phần 4: Cơ Sở Dữ Liệu (3-4 phút)

- Trình bày sơ đồ ERD (đã có sẵn trong tài liệu này)
- Giải thích cơ chế trigger (3 trigger chính)
- Nhấn mạnh: `TongTien` và `ThanhTien` không nhập tay — trigger tự tính
- Giới thiệu hệ thống 16+ Stored Procedures

#### Phần 5: Kết Luận (1-2 phút)

- Điểm mạnh đã đạt được
- Hướng phát triển nếu có thêm thời gian

---

### 6.2 Kỹ Năng Trình Bày Khi Bảo Vệ

**Về thái độ:**
- Tự tin, nhìn thẳng vào giảng viên khi trả lời
- Khi không biết → nói thẳng: *"Em chưa tìm hiểu phần đó, em sẽ nghiên cứu thêm"* (không đoán bừa)
- Nếu giảng viên ngắt lời để hỏi → dừng demo, trả lời trước, rồi tiếp tục

**Về demo:**
- Chuẩn bị môi trường trước: SQL Server đang chạy, ứng dụng đã build
- Có file backup DB phòng khi lỗi
- Biết cách tắt/mở lại app nhanh nếu bị crash

**Về câu hỏi:**
- Lắng nghe kỹ trước khi trả lời
- Nếu cần thời gian suy nghĩ → nói: *"Dạ, cho em suy nghĩ một chút ạ"*
- Chỉ vào code thực tế khi trả lời câu hỏi kỹ thuật

---

### 6.3 Phân Chia Vai Trò Trình Bày (Gợi Ý)

| Thành viên | Phụ trách |
|-----------|-----------|
| Thành viên 1 | Giới thiệu tổng quan + Demo đăng nhập, bán hàng |
| Thành viên 2 | Demo kho hàng, giao hàng + Giải thích kiến trúc |
| Thành viên 3 | Trình bày CSDL + Trả lời câu hỏi kỹ thuật |

---

## 7. CÂU HỎI GIẢNG VIÊN CÓ THỂ HỎI & GỢI Ý TRẢ LỜI

### 7.1 Câu Hỏi Về Kiến Trúc & Thiết Kế

**Q: Tại sao chọn WinForms thay vì Web App?**
> "Dạ, chúng em chọn WinForms vì phù hợp với môi trường sử dụng của cửa hàng hoa — máy tính bàn tại quầy, không cần kết nối internet liên tục. WinForms cho phép tương tác nhanh, dữ liệu local, và phù hợp với khả năng công nghệ C# mà nhóm đã học."

**Q: Kiến trúc 3 tầng của em là gì? Phân biệt các tầng như thế nào?**
> "Dạ, project chia 3 tầng rõ ràng:
> - **Tầng Presentation**: Các form WinForms (frmMain, ucTaoDon...) — chỉ hiển thị dữ liệu và nhận input từ user
> - **Tầng Business Logic**: Các Service classes (DonHangService, TraHangService...) — xử lý nghiệp vụ, validate, điều phối
> - **Tầng Data Access**: DatabaseHelper + DAO classes — kết nối DB, gọi stored procedure, map kết quả
> 
> Tầng Presentation không biết gì về SQL, tầng Data Access không biết gì về giao diện."

**Q: SessionManager dùng để làm gì? Pattern gì?**
> "Dạ, SessionManager là Singleton — chỉ có một instance duy nhất trong suốt vòng đời ứng dụng. Nó lưu thông tin nhân viên đang đăng nhập (CurrentUser) và cung cấp phương thức kiểm tra quyền. Mọi màn hình đều truy vấn SessionManager để biết cần hiển thị/ẩn chức năng gì."

**Q: Tại sao dùng Reflection để map dữ liệu từ DB?**
> "Dạ, DatabaseHelper dùng Reflection để tự động map các cột trong DataReader vào properties của Model theo tên. Thay vì viết `obj.MaNV = reader["MaNV"]` lặp đi lặp lại cho mọi class, chúng em dùng Reflection để làm generic — một đoạn code dùng được cho tất cả 13 entity classes. Nhược điểm là chậm hơn một chút so với map tường minh."

---

### 7.2 Câu Hỏi Về Chức Năng

**Q: Luồng tạo đơn hàng hoạt động thế nào?**
> "Dạ, quy trình gồm 5 bước:
> 1. Thu ngân chọn hoặc tạo khách hàng (tìm theo SĐT)
> 2. Thêm sản phẩm vào giỏ — mỗi lần thêm gọi `sp_ThemChiTietDon`, kiểm tra tồn kho thực
> 3. Chọn hình thức nhận: Tại quầy hoặc Giao hàng
> 4. Xác nhận → `sp_TaoDonHang` tạo header đơn, trigger tự tính TongTien
> 5. Nếu giao hàng → tạo phiếu giao hàng tương ứng"

**Q: Hệ thống phân quyền hoạt động ra sao?**
> "Dạ, bảng PHAN_QUYEN lưu ma trận (ChucVu, Module) với 5 quyền: Xem, Thêm, Sửa, Xóa, Export. Khi đăng nhập, hệ thống load toàn bộ quyền của ChucVu hiện tại vào SessionManager. Khi mở màn hình, code kiểm tra quyền và ẩn/disable các nút không được phép."

**Q: Cơ chế cảnh báo tồn kho thấp hoạt động thế nào?**
> "Dạ, mỗi sản phẩm có trường `MucTonToiThieu`. Stored procedure `sp_CanhBaoTonKho` truy vấn tất cả sản phẩm có `SoLuongTon <= MucTonToiThieu`. Dashboard gọi SP này khi khởi động và hiển thị danh sách cảnh báo. Thủ kho cũng có thể cấu hình mức tối thiểu từng sản phẩm."

**Q: Trả hàng xử lý thế nào? CoNhapKho là gì?**
> "Dạ, khi trả hàng, Admin tạo phiếu TRA_HANG và chọn từng sản phẩm trong CT_TRA_HANG. Trường `CoNhapKho` (BIT) cho phép quyết định sản phẩm trả về có được nhập lại kho không: ví dụ hoa héo thì không nhập lại (`CoNhapKho = 0`), hoa còn tốt thì nhập lại (`CoNhapKho = 1`). Hệ thống tự động cập nhật tồn kho theo lựa chọn."

---

### 7.3 Câu Hỏi Về Bảo Mật

**Q: Hệ thống bảo mật mật khẩu như thế nào?**
> "Dạ, mật khẩu không bao giờ lưu dạng plain text. Trước khi gọi DB, ứng dụng hash mật khẩu bằng SHA-256. DB chỉ lưu chuỗi hash 64 ký tự. Khi đăng nhập, mật khẩu nhập vào cũng được hash rồi so sánh với hash trong DB."

**Q: Có thể SQL Injection không?**
> "Dạ, hệ thống tránh SQL Injection bằng cách toàn bộ thao tác DB đều qua Stored Procedure với parameterized parameters — không có SQL string concatenation. DatabaseHelper dùng `SqlParameter` để truyền giá trị, không nhúng trực tiếp vào chuỗi SQL."

---

### 7.4 Câu Hỏi Giảng Viên Có Thể Yêu Cầu Thay Đổi

**"Hãy thêm chức năng export Excel cho báo cáo"**
> Trả lời: *"Dạ thưa thầy/cô, trong code hiện tại có trường `Export` trong bảng PHAN_QUYEN đã được chuẩn bị sẵn. Để thực hiện, em cần thêm thư viện EPPlus hoặc ClosedXML, tạo phương thức export trong BaoCaoDAO, và kết nối button Export với phương thức đó."*

**"Hệ thống có thể mở rộng thêm module không?"**
> Trả lời: *"Dạ có. Vì hệ thống dùng kiến trúc 3 tầng và phân quyền qua bảng PHAN_QUYEN, để thêm module mới chỉ cần: (1) Tạo màn hình WinForms mới, (2) Thêm DAO/Service tương ứng, (3) INSERT dòng phân quyền cho module mới vào bảng PHAN_QUYEN."*

**"Tại sao không dùng ORM như Entity Framework?"**
> Trả lời: *"Dạ, chúng em chọn ADO.NET trực tiếp với Stored Procedure vì: (1) Kiểm soát hoàn toàn câu query để tối ưu, (2) Logic nghiệp vụ phức tạp (triggers, transactions) phù hợp hơn với SP, (3) Đây là môn học để hiểu nền tảng. Tuy nhiên nếu phát triển lên production, ORM như EF Core là lựa chọn hợp lý."*

---

## 8. CÂU HỎI VỀ CSDL & HƯỚNG DẪN TRẢ LỜI

### 8.1 Câu Hỏi Về Thiết Kế Schema

**Q: Tại sao PHAN_QUYEN không có Foreign Key đến NHAN_VIEN?**
> "Dạ, `PHAN_QUYEN` dùng `ChucVu` làm khóa, không phải `MaNV` cụ thể. Lý do là phân quyền áp dụng cho vai trò, không cho cá nhân. Nếu thêm FK đến NHAN_VIEN, mỗi lần thêm nhân viên mới cùng vai trò lại phải insert phân quyền. Thiết kế hiện tại linh hoạt hơn: thay đổi quyền của Admin → tất cả Admin đều được cập nhật."

**Q: Tại sao ThanhTien và TongTien không để ứng dụng tính mà dùng trigger?**
> "Dạ, để đảm bảo tính nhất quán dữ liệu. Nếu ứng dụng tính, có thể xảy ra race condition hoặc lỗi khi nhiều người dùng cùng sửa đơn hàng. Trigger chạy ngay trong transaction của câu INSERT/UPDATE, đảm bảo ThanhTien luôn chính xác bất kể ai thay đổi dữ liệu."

**Q: CSDL đạt chuẩn hóa đến mức nào?**
> "Dạ, CSDL đạt 3NF:
> - 1NF: Tất cả thuộc tính nguyên tử, có PK rõ ràng
> - 2NF: Mọi thuộc tính không khóa phụ thuộc đầy đủ vào PK (ví dụ CHI_TIET_DON_HANG có PK composite, SoLuong phụ thuộc cả MaDon và MaSP)
> - 3NF: Không có phụ thuộc bắc cầu (thông tin KH tách ra bảng riêng, không lặp trong DON_HANG)"

**Q: Khóa chính (MaDon, MaSP, MaNV...) được sinh ra như thế nào?**
> "Dạ, hệ thống dùng Stored Procedure `sp_SinhMa` để sinh mã tự động theo định dạng: prefix + số thứ tự tăng dần (VD: `DH001`, `DH002`). SP này đọc mã cuối cùng trong bảng tương ứng, tăng lên 1, format lại theo template. Điều này đảm bảo mã thân thiện với người dùng hơn GUID."

**Q: Nếu hai người cùng tạo đơn hàng cùng lúc, có bị trùng mã không?**
> "Dạ, đây là vấn đề concurrency. Hiện tại `sp_SinhMa` chưa dùng transaction với isolation level cao, nên về lý thuyết có thể bị trùng trong môi trường multi-user. Để fix, cần wrap `sp_SinhMa` trong SERIALIZABLE transaction hoặc dùng SEQUENCE object của SQL Server. Đây là điểm cần cải thiện trong phiên bản tới."

---

### 8.2 Câu Hỏi Về Stored Procedures

**Q: Tại sao dùng Stored Procedure thay vì viết SQL trong code?**
> "Dạ, 5 lý do:
> 1. **Bảo mật**: Tránh SQL Injection hoàn toàn
> 2. **Hiệu năng**: SP được compile và cache execution plan
> 3. **Tách biệt**: Logic nghiệp vụ DB tách khỏi code ứng dụng
> 4. **Maintainability**: Sửa SP không cần recompile ứng dụng
> 5. **Transaction an toàn**: SP có thể bao gồm nhiều bước trong 1 transaction"

**Q: sp_CapNhatTrangThaiDon làm gì khi trạng thái chuyển sang DangXuLy?**
> "Dạ, khi đơn hàng chuyển sang `DangXuLy`, SP này:
> 1. Kiểm tra tồn kho từng sản phẩm trong đơn
> 2. Nếu đủ hàng → giảm `SoLuongTon` cho từng sản phẩm
> 3. Cập nhật `DON_HANG.TrangThai = 'DangXuLy'`
> 4. Nếu thiếu hàng → ROLLBACK và báo lỗi
> Ngược lại khi hủy đơn → hoàn lại tồn kho."

**Q: sp_CapNhatTrangThaiGiao có 2 phiên bản (fix_sp và fix_sp2). Vì sao?**
> "Dạ, phiên bản đầu có lỗi khi đồng bộ trạng thái DON_HANG sau khi giao hàng — một số trạng thái DON_HANG không hợp lệ. `fix_sp.sql` sửa lỗi mapping trạng thái, `fix_sp2.sql` là phiên bản thay thế với cách xử lý khác. Nhóm em đã dùng phiên bản cuối nhất để đảm bảo tính đúng đắn."

---

### 8.3 Câu Hỏi Về Triggers

**Q: Trigger có ảnh hưởng hiệu năng không?**
> "Dạ có. Trigger chạy ngay sau mỗi INSERT/UPDATE/DELETE, nên tốc độ thêm chi tiết đơn hàng chậm hơn một chút. Tuy nhiên trong quy mô cửa hàng hoa (vài chục đơn/ngày), điều này không đáng kể. Nếu cần scale lớn hơn, có thể xem xét bỏ trigger tính TongTien và để ứng dụng tính, nhưng cần xử lý concurrency cẩn thận."

**Q: Trigger khác Stored Procedure ở điểm nào?**
> "Dạ:
> - **Trigger** tự động chạy khi có DML event (INSERT/UPDATE/DELETE) — không gọi trực tiếp
> - **Stored Procedure** phải được gọi tường minh từ ứng dụng hoặc SP khác
> - Trigger dùng để duy trì tính toàn vẹn dữ liệu tự động; SP dùng để thực hiện logic nghiệp vụ theo yêu cầu"

---

### 8.4 Câu Hỏi Về Index & Hiệu Năng

**Q: CSDL có Index không? Tại sao?**
> "Dạ, ngoài các PK index tự động, schema hiện tại chưa có explicit indexes. Để cải thiện hiệu năng, em đề xuất thêm:
> - `IX_DON_HANG_NgayTao` — báo cáo doanh thu theo ngày
> - `IX_DON_HANG_TrangThai` — lọc đơn theo trạng thái
> - `IX_SAN_PHAM_TrangThai` — lọc sản phẩm đang bán
> - `IX_GIAO_HANG_MaNV_Shipper` — tìm đơn theo shipper
> Đây là điểm cần bổ sung khi deploy production."

**Q: Làm thế nào để tối ưu query báo cáo tháng khi dữ liệu lớn?**
> "Dạ, một số hướng:
> 1. Thêm index trên `NgayTao` của `DON_HANG`
> 2. Dùng filtered index cho trạng thái `HoanThanh`
> 3. Xem xét tạo bảng summary/aggregate được cập nhật định kỳ
> 4. Partition bảng theo tháng nếu dữ liệu rất lớn"

---

### 8.5 Câu Hỏi Về Bảo Mật CSDL

**Q: Nếu ai đó lấy được file backup DB, họ có đọc được mật khẩu không?**
> "Dạ không. Trong bảng NHAN_VIEN, cột MatKhau lưu hash SHA-256 — một chiều, không thể reverse. Tuy nhiên SHA-256 thuần có thể bị tấn công bằng rainbow table. Nếu muốn bảo mật tốt hơn, nên thêm salt (chuỗi ngẫu nhiên) vào trước khi hash, hoặc dùng bcrypt/scrypt."

**Q: Quyền truy cập DB được kiểm soát như thế nào?**
> "Dạ, ứng dụng kết nối DB bằng Integrated Security (Windows Authentication), chỉ có máy/user được cấp quyền mới kết nối được. Trong môi trường production, nên tạo SQL Login riêng cho ứng dụng với quyền tối thiểu: chỉ EXECUTE trên các stored procedures, không có quyền SELECT/INSERT/UPDATE/DELETE trực tiếp trên bảng."

---

## 9. ĐIỂM MẠNH CẦN NHẤN MẠNH & ĐIỂM YẾU CẦN CHUẨN BỊ

### 9.1 Điểm Mạnh — Tự Hào Trình Bày

- ✅ **Kiến trúc rõ ràng 3 tầng** — tách biệt UI, logic, data
- ✅ **100% Stored Procedure** — không SQL inline, chống SQL Injection
- ✅ **Trigger thông minh** — đảm bảo nhất quán dữ liệu tự động
- ✅ **Phân quyền linh hoạt** — ma trận PHAN_QUYEN theo role
- ✅ **Bảo mật mật khẩu** — SHA-256 hashing
- ✅ **Generic DatabaseHelper** — Reflection-based mapping, DRY principle
- ✅ **Vòng đời đơn hàng đầy đủ** — từ tạo đến hoàn thành/trả hàng
- ✅ **Module trả hàng chi tiết** — CoNhapKho, nhiều hình thức hoàn tiền
- ✅ **Báo cáo đa dạng** — ngày, tháng, sản phẩm, nhân viên
- ✅ **CSDL chuẩn hóa 3NF** — thiết kế sạch, không dư thừa

### 9.2 Điểm Yếu — Cần Chuẩn Bị Câu Trả Lời

- ⚠️ **Chưa có Index tường minh** → "Sẽ thêm sau khi có dữ liệu thực để đo hiệu năng"
- ⚠️ **sp_SinhMa chưa an toàn concurrency** → "Sẽ dùng SEQUENCE hoặc SERIALIZABLE transaction"
- ⚠️ **SHA-256 không có salt** → "Sẽ thêm salt hoặc chuyển sang bcrypt"
- ⚠️ **Chưa có unit test** → "Việc test được thực hiện manual; framework nhỏ gọn phù hợp giai đoạn học"
- ⚠️ **Chưa export Excel** → "PHAN_QUYEN đã có cột Export sẵn sàng, cần thêm thư viện EPPlus"
- ⚠️ **Chỉ chạy trên Windows** → "WinForms là lựa chọn phù hợp bài toán desktop; web/mobile là bước phát triển tiếp theo"

---

*Tài liệu này được tạo tự động dựa trên phân tích toàn bộ source code tại repository `Nlminhvu-mtak60/FloriSys_1`, nhánh `vu`, commit `6c4d39d` (07/05/2026).*
