# FloriSys - Flower Shop Management System

A Windows Forms desktop application for managing a flower shop, built with **C# (.NET Framework 4.7.2)** and **SQL Server**. The system covers the complete retail workflow: from point-of-sale and order management, through warehouse operations and delivery tracking, to business reporting.

---

## Table of Contents

- [Features](#features)
- [Technology Stack](#technology-stack)
- [System Architecture](#system-architecture)
- [Database Schema](#database-schema)
- [Role-Based Access Control](#role-based-access-control)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [Key Business Flows](#key-business-flows)
- [Stored Procedures & Triggers](#stored-procedures--triggers)
- [Auto Code Generation](#auto-code-generation)
- [License](#license)

---

## Features

```mermaid
graph TD
    A[FloriSys] --> B[User Authentication]
    A --> C[Staff Management]
    A --> D[Sales Operations]
    A --> E[Inventory Management]
    A --> F[Delivery Management]
    A --> G[Reporting & Analytics]
    A --> H[Master Data Management]
    B --> B1[Login with SHA-256]
    B --> B2[Password Change]
    C --> C1[Staff Dashboard]
    C --> C2[Staff Profile Management]
    D --> D1[Sales Dashboard]
    D --> D2[Order Creation]
    D --> D3[Order List]
    D --> D4[Order Details]
    D --> D5[Customer Feedback]
    D --> D6[Returns Processing]
    E --> E1[Inventory Dashboard]
    E --> E2[Stock Level Configuration]
    E --> E3[Stock On Hand]
    E --> E4[Inventory Receipts]
    E --> E5[Inventory Dispatches]
    E --> E6[Damaged Goods Management]
    E --> E7[Inventory History]
    F --> F1[Delivery Dashboard]
    F --> F2[Delivery Assignment]
    F --> F3[Delivery Status Update]
    G --> G1[Daily Revenue Report]
    G --> G2[Monthly Revenue Report]
    G --> G3[Staff Performance Report]
    G --> G4[Product Sales Report]
    G --> E3[Inventory Report]
    H --> H1[Customer Management]
    H --> H2[Product Management]
```

### Feature Details

| Module | Feature | Description |
|--------|---------|-------------|
| **Authentication** | Login | SHA-256 hashed password verification via stored procedure |
| | Change Password | Validates old password before updating |
| **Sales** | Create Order | Cart-based order creation with real-time stock validation |
| | Order List | Filter by keyword, status, employee, date |
| | Order Details | View full order info including items, customer, and delivery |
| | Customer Feedback | Record and track customer complaints/feedback |
| | Returns Processing | Auto-fill from shipper returns list, process returns with optional stock re-entry |
| **Inventory** | Stock On Hand | View current inventory levels with stock alerts |
| | Inventory Receipts | Create purchase receipts with auto stock increment (trigger) |
| | Inventory Dispatches | Process orders for dispatch with auto stock decrement |
| | Damaged Goods | Record damaged/destroyed items with stock adjustment |
| | Stock Level Config | Set minimum stock thresholds per product |
| | Receipt History | View historical inventory receipt records |
| **Delivery** | Delivery Assignment | Assign shippers to delivery orders |
| | Delivery Status Update | Track delivery progress (waiting, delivering, delivered, returned) |
| **Reports** | Daily Revenue | Revenue summary by day with order counts |
| | Monthly Revenue | Revenue summary by month with daily chart breakdown |
| | Product Sales | Top 10 best-selling products by quantity and revenue |
| | Staff Performance | Employee order counts, revenue, and cancellation rates |
| | Inventory Report | Stock levels with shortage alerts |
| **Master Data** | Product Management | CRUD for products (flower types, accessories) |
| | Customer Management | CRUD for customers with phone-based lookup |
| **Admin** | Staff Management | CRUD for employees with role assignment |
| | Role Permissions | Configure CRUD + Export permissions per role per module |

---

## Technology Stack

| Component | Technology |
|-----------|-----------|
| **Language** | C# (.NET Framework 4.7.2) |
| **UI Framework** | Windows Forms (WinForms) |
| **Database** | SQL Server 2022 (Developer Edition) |
| **Data Access** | ADO.NET (SqlClient) + Custom Reflection ORM + Repository Pattern |
| **Charts** | System.Windows.Forms.DataVisualization |
| **Password Hashing** | SHA-256 (System.Security.Cryptography) |
| **Architecture** | 3-Layer (UI → Repository Layer → Database) |

---

## System Architecture

### 3-Layer Flow

```mermaid
graph LR
    A[UI Layer<br>WinForms] -->|calls| B[Repository Layer<br>OOP BaseRepository]
    B -->|calls| C[Database<br>SQL Server]
    C -->|triggers| D[Auto Calculations]
```

### How frmMain Works (SPA Pattern)

```mermaid
graph TB
    subgraph frmMain["frmMain (Single Container)"]
        P[panel1<br>Dock=Fill]
    end

    UC1[ucDashboard] -->|LoadUC| P
    UC2[ucTaoDon] -->|LoadUC| P
    UC3[ucNhapKho] -->|LoadUC| P

    P -.->|Controls.Clear| OLD["❌ Remove old UC"]
    P -->|Controls.Add| NEW["✅ New UC fills panel"]
```

**The 3-line navigation engine:**
```csharp
panel1.Controls.Clear();
uc.Dock = DockStyle.Fill;
panel1.Controls.Add(uc);
```

### Reflection ORM Mapping

```mermaid
graph LR
    SQL["SQL Result<br>DataTable"] -->|MapDataTable&lt;T&gt;| REF["Reflection<br>PropertyInfo[]"]
    REF -->|SetValue| LIST["List&lt;T&gt;<br>C# Objects"]
```

**Rule**: `Property Name` in C# **must equal** `Column Name` in SQL.

### Event Communication (Menu → frmMain → UC)

```mermaid
sequenceDiagram
    participant USER
    participant MENU as ucThanhMenu
    participant MAIN as frmMain
    participant UC as UserControl

    USER->>MENU: Click "TaoDon"
    MENU->>MENU: SetActive(btn)
    MENU->>MAIN: MenuClicked?.Invoke("TaoDon")
    MAIN->>MAIN: OnMenuClicked("TaoDon")
    MAIN->>MAIN: uc = new ucTaoDon()
    MAIN->>MAIN: LoadUC(uc)
    MAIN->>UC: panel.Controls.Add(uc)
```

### Design Patterns Used

| Pattern | Where | Why |
|---------|-------|-----|
| **3-Layer** | UI → Repository → DB | Separation of concerns |
| **Repository (OOP)**| `BaseRepository<T>` | Code reuse, Inheritance, Polymorphism |
| **Transaction**| `*Repository.cs` | Atomic operations using `SqlTransaction` for data integrity |
| **Generic ORM** | `DatabaseHelper` | One generic method maps all Database results to C# Objects |
| **Singleton** | `SessionManager` | Global user state across the application |
| **Event-Driven** | `MenuClicked` | Decoupled navigation |
| **SPA WinForms** | `frmMain.Panel` | Swap pages smoothly without popping up multiple forms |

---

## Database Schema

### Entity Relationship Diagram

```mermaid
erDiagram
    NHAN_VIEN ||--o{ DON_HANG : "tao don"
    NHAN_VIEN ||--o{ PHIEU_NHAP_KHO : "nhap kho"
    NHAN_VIEN ||--o{ GIAO_HANG : "giao hang"
    KHACH_HANG ||--o{ DON_HANG : "dat hang"
    SAN_PHAM ||--o{ CHI_TIET_DON_HANG : "chi tiet"
    SAN_PHAM ||--o{ CT_NHAP_KHO : "nhap kho"
    SAN_PHAM ||--o{ HANG_HU : "hang hu"
    SAN_PHAM ||--o{ CT_TRA_HANG : "tra hang"
    DON_HANG ||--o{ CHI_TIET_DON_HANG : "chi tiet"
    DON_HANG ||--o| GIAO_HANG : "giao hang"
    DON_HANG ||--o{ PHAN_HOI : "phan hoi"
    DON_HANG ||--o{ TRA_HANG : "tra hang"
    PHIEU_NHAP_KHO ||--o{ CT_NHAP_KHO : "chi tiet"
    TRA_HANG ||--o{ CT_TRA_HANG : "chi tiet"
    DON_HANG ||--o{ LICH_SU_DON_HANG : "lich su"
    PHAN_QUYEN }o--|| NHAN_VIEN : "phan quyen theo ChucVu"

    NHAN_VIEN {
        nvarchar MaNV PK
        nvarchar HoTen
        nvarchar ChucVu
        nvarchar SoDienThoai
        nvarchar TaiKhoan UK
        nvarchar MatKhau
        nvarchar TrangThai
    }
    KHACH_HANG {
        nvarchar MaKH PK
        nvarchar HoTen
        nvarchar SoDienThoai UK
        nvarchar DiaChi
        nvarchar Email
        datetime NgayTao
    }
    SAN_PHAM {
        nvarchar MaSP PK
        nvarchar TenSP
        nvarchar LoaiHoa
        decimal GiaBan
        decimal GiaNhap
        int SoLuongTon
        int MucTonToiThieu
        nvarchar TrangThai
    }
    DON_HANG {
        nvarchar MaDon PK
        datetime NgayTao
        nvarchar MaKH FK
        nvarchar MaNV_TaoDon FK
        nvarchar HinhThucNhanHang
        nvarchar TrangThai
        decimal TongTien
        nvarchar GhiChu
    }
    CHI_TIET_DON_HANG {
        nvarchar MaDon PK_FK
        nvarchar MaSP PK_FK
        int SoLuong
        decimal DonGia
        decimal ThanhTien
    }
    GIAO_HANG {
        nvarchar MaGiaoHang PK
        nvarchar MaDon FK
        nvarchar MaNV_Shipper FK
        datetime NgayGiao
        nvarchar TrangThai
        nvarchar GhiChuGiaoHang
    }
    PHIEU_NHAP_KHO {
        nvarchar MaPhieu PK
        datetime NgayNhap
        nvarchar MaNV FK
        nvarchar GhiChu
    }
    CT_NHAP_KHO {
        nvarchar MaPhieu PK_FK
        nvarchar MaSP PK_FK
        int SoLuong
        decimal GiaNhap
    }
    HANG_HU {
        nvarchar MaPhieuHuy PK
        nvarchar MaSP FK
        int SoLuong
        nvarchar LyDo
        datetime NgayHuy
        nvarchar GhiChu
    }
    PHAN_HOI {
        nvarchar MaPH PK
        nvarchar MaDon FK
        nvarchar NoiDung
        datetime NgayGhi
        nvarchar TrangThaiXuLy
        nvarchar KetQuaXuLy
    }
    TRA_HANG {
        nvarchar MaPhieuTra PK
        nvarchar MaDon FK
        nvarchar LyDo
        nvarchar HinhThucHoanTien
        nvarchar GhiChu
        datetime NgayTra
    }
    CT_TRA_HANG {
        nvarchar MaPhieuTra PK_FK
        nvarchar MaSP PK_FK
        int SoLuong
        bit CoNhapKho
    }
    LICH_SU_DON_HANG {
        int Id PK
        nvarchar MaDon FK
        nvarchar TrangThai
        datetime ThoiGian
        nvarchar GhiChu
    }
    PHAN_QUYEN {
        nvarchar ChucVu PK
        nvarchar Module PK
        bit Xem
        bit Them
        bit Sua
        bit Xoa
        bit Export
    }
```

### Table Summary (14 tables)

| # | Table | Primary Key | Foreign Keys | Purpose |
|---|-------|------------|-------------|---------|
| 1 | NHAN_VIEN | MaNV | — | Employees (staff) |
| 2 | KHACH_HANG | MaKH | — | Customers |
| 3 | SAN_PHAM | MaSP | — | Products (flowers, accessories) |
| 4 | DON_HANG | MaDon | MaKH → KHACH_HANG, MaNV_TaoDon → NHAN_VIEN | Orders |
| 5 | CHI_TIET_DON_HANG | (MaDon, MaSP) | MaDon → DON_HANG, MaSP → SAN_PHAM | Order line items |
| 6 | GIAO_HANG | MaGiaoHang | MaDon → DON_HANG, MaNV_Shipper → NHAN_VIEN | Delivery tracking |
| 7 | PHIEU_NHAP_KHO | MaPhieu | MaNV → NHAN_VIEN | Inventory receipt headers |
| 8 | CT_NHAP_KHO | (MaPhieu, MaSP) | MaPhieu → PHIEU_NHAP_KHO, MaSP → SAN_PHAM | Inventory receipt line items |
| 9 | PHAN_HOI | MaPH | MaDon → DON_HANG | Customer feedback |
| 10 | LICH_SU_DON_HANG | Id | MaDon → DON_HANG | Order status history log |
| 11 | HANG_HU | MaPhieuHuy | MaSP → SAN_PHAM | Damaged/destroyed goods log |
| 12 | PHAN_QUYEN | (ChucVu, Module) | — | Role-based permissions |
| 13 | TRA_HANG | MaPhieuTra | MaDon → DON_HANG | Return headers |
| 14 | CT_TRA_HANG | (MaPhieuTra, MaSP) | MaPhieuTra → TRA_HANG, MaSP → SAN_PHAM | Return line items |

---

## Role-Based Access Control

The system supports 4 roles with granular permissions per module:

| Module | Admin | Cashier | Warehouse | Shipper |
|--------|-------|---------|-----------|---------|
| Dashboard | Full | View | View | View |
| DonHang (Orders) | Full | View/Add/Edit | — | — |
| KhoHang (Inventory) | Full | — | View/Add/Edit/Export | — |
| GiaoHang (Delivery) | Full | — | — | View/Edit |
| NhanVien (Staff) | Full | — | — | — |
| KhachHang (Customers) | Full | View/Add/Edit | — | — |
| SanPham (Products) | Full | View | View/Add/Edit | — |
| PhanQuyen (Permissions) | View/Add/Edit | — | — | — |
| BaoCao (Reports) | View/Export | — | — | — |
| TraHang (Returns) | View/Add/Edit | View/Add | — | — |
| PhanHoi (Feedback) | View/Add/Edit | View/Add | — | — |

Permissions are stored in the `PHAN_QUYEN` table with columns: `Xem` (View), `Them` (Add), `Sua` (Edit), `Xoa` (Delete), `Export`.

---

## Project Structure & Module Dependencies

```mermaid
graph TB
    subgraph UI["Presentation Layer"]
        F1[frmDangNhap]
        F2[frmMain]
        F2 --> UC[20+ UserControls]
    end

    subgraph SV["Services"]
        SM[SessionManager]
    end

    subgraph DA["Repository Layer (OOP)"]
        DH[DatabaseHelper]
        BR[BaseRepository&lt;T&gt टैंक]
        REP1[SanPhamRepository]
        REP2[DonHangRepository]
        REP3[NhanVienRepository]
        REP4[...10 more Repositories]
        
        BR --> DH
        REP1 -- inherits --> BR
        REP2 -- inherits --> BR
        REP3 -- inherits --> BR
        REP4 -- inherits --> BR
    end

    subgraph DB["SQL Server"]
        T[(14 Tables)]
        SP[(16 Stored Procs)]
        TR[(3 Triggers)]
    end

    F1 --> SM
    F2 --> SM
    UC --> REP1
    UC --> REP2
    DH --> SP
    DH --> T
    SP --> TR
    TR --> T
```

### File Organization by Namespace

| Folder | Contains | Count |
|--------|----------|-------|
| `1_DangNhap/` | Login Form + Change Password UC | 2 files |
| `2_QuanLy/` | Main Form + Dashboard + Staff UC | 3 files |
| `3_BanHang/` | Sales: Cart, Orders, Feedback, Returns | 6 files |
| `4_KhoHang/` | Inventory: Stock, Receipts, Damaged | 7 files |
| `5_GiaoHang/` | Delivery: Assignment, Tracking | 4 files |
| `6_BaoCao/` | Reports: Day, Month, Product, Staff | 6 files |
| `7_DanhMuc/` | Master Data: Products, Customers | 2 files |
| `DataAccess/` | BaseRepository, DatabaseHelper + 11 Repositories | 13 files |
| `Models/` | 11 Entity classes, BaseEntity, Enums, DTOs | 13 files |
| `Services/` | SessionManager | 1 file |
| `Shared/` | Navigation Menu + Permission UC | 2 files |

---

## Getting Started

### Prerequisites

- **Visual Studio 2019+** with .NET desktop development workload
- **SQL Server 2019+** (Developer or Express edition)
- **.NET Framework 4.7.2** (included in Windows 10+)

### Step 1: Create the Database

Open **SQL Server Management Studio (SSMS)** and execute the database script:

```sql
-- Run the full script
-- This will: CREATE DATABASE, CREATE TABLES, TRIGGERS, SPs, and sample data
```

Or from command line:
```bash
sqlcmd -S . -i FloriSys_Database.sql
```

### Step 2: Configure Connection String

Edit `App.config` if your SQL Server instance differs from the default:

```xml
<connectionStrings>
  <add name="FloriSys"
       connectionString="Server=.;Database=FloriSys;Integrated Security=True;TrustServerCertificate=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

Common configurations:
| Server | ConnectionString Server value |
|--------|------------------------------|
| Local default instance | `Server=.` |
| Named instance | `Server=.\SQLEXPRESS` |
| Remote server | `Server=192.168.1.100` |
| SQL Authentication | Add `User ID=sa;Password=yourpassword;` |

### Step 3: Build & Run

1. Open `FloriSys.slnx` in Visual Studio
2. Restore NuGet packages (if any)
3. Build the solution (Ctrl+Shift+B)
4. Run (F5)

### Default Login Accounts

All accounts use the password: **`123456**`

| Username | Role | Name |
|----------|------|------|
| `admin` | Admin | Nguyen Le Minh Vu |
| `thuhuong` | Cashier | Tran Thu Huong |
| `minhkho` | Warehouse | Le Minh Khoa |
| `shipper1` | Shipper | Nguyen Van Son |
| `xuanxuan` | Cashier (inactive) | Hoang Thi Xuan |

> Password is hashed with SHA-256 before being sent to the database. The hash of "123456" is `8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92`.

---

## Key Business Flows

### Order Creation Flow (Transactional)

```mermaid
sequenceDiagram
    participant U as Cashier
    participant UI as ucTaoDon
    participant KH as KhachHangRepository
    participant DH as DonHangRepository
    participant DB as SQL Server
    participant TR as Trigger

    U->>UI: Select products → Add to cart
    UI->>UI: Validate stock (SoLuongTon > 0)
    UI->>UI: Calculate total
    U->>UI: Click Confirm
    UI->>KH: TimTheoSDT(phone)
    alt Customer exists
        KH-->>UI: Return MaKH
    else New customer
        KH->>DB: INSERT KHACH_HANG
        DB-->>KH: Return new MaKH
        KH-->>UI: Return MaKH
    end
    UI->>DH: TaoDonHangHoanChinh(maKH, maNV, hinhThuc, gioHang)
    note right of DH: Begin SqlTransaction
    DH->>DB: sp_SinhMa (DH)
    DH->>DB: sp_TaoDonHang
    loop For each cart item
        DH->>DB: sp_ThemChiTietDon
        DB->>DB: Check stock (RAISERROR if insufficient)
        DB->>DB: INSERT CHI_TIET_DON_HANG
        DB->>TR: trg_TinhThanhTien
        TR->>DB: UPDATE ThanhTien = SoLuong * DonGia
        DB->>TR: trg_CapNhatTongTien
        TR->>DB: UPDATE DON_HANG.TongTien = SUM(ThanhTien)
    end
    alt Delivery order
        DH->>DB: sp_SinhMa (GH)
        DH->>DB: sp_TaoGiaoHang
    end
    note right of DH: Commit Transaction (Rollback on error)
    DH-->>UI: Order ID
    UI-->>U: Order created successfully
```

### Order Status State Machine

```mermaid
stateDiagram-v2
    [*] --> Moi: Order created
    Moi --> DangXuLy: Process order (stock deducted)
    Moi --> Huy: Cancel before processing
    DangXuLy --> DaGiao: Shipper delivers
    DangXuLy --> HoanHang: Customer returns
    DaGiao --> HoanThanh: Customer confirms receipt
    DaGiao --> HoanHang: Customer returns after delivery
    HoanHang --> [*]: Return processed (stock restored)
    HoanThanh --> [*]: Order complete
    Huy --> [*]: Order cancelled

    note right of DangXuLy: SAN_PHAM.SoLuongTon -= SoLuong
    note right of HoanHang: SAN_PHAM.SoLuongTon += SoLuong
    note right of Huy: No stock change (was never deducted)
```

### Inventory Stock Flow

```mermaid
flowchart LR
    subgraph Inflows
        A[Inventory Receipt] -->|Trigger| TON[SoLuongTon]
        B[Return with re-entry] -->|CoNhapKho=1| TON
    end
    subgraph Outflows
        TON -->|Order → DangXuLy| C[Order Dispatch]
        TON -->|sp_GhiNhanHangHu| D[Damaged Goods]
    end
```

### The OOP Repository Architecture

```mermaid
classDiagram
    class BaseRepository~T~ {
        <<abstract>>
        +String TableName
        +String IdColumn
        +String IdPrefix
        +LayDanhSach(keyword) List~T~
        +TaoMoi() String
        +LayTheoMa(ma) T
        #GetList(sql, parms) List~T~
    }
    class DonHangRepository {
        +TableName: "DON_HANG"
        +LayDanhSach(keyword, status, date)
        +TaoDonHangHoanChinh(...)
    }
    class SanPhamRepository {
        +TableName: "SAN_PHAM"
        +CapNhatSoLuong(...)
    }
    BaseRepository <|-- DonHangRepository
    BaseRepository <|-- SanPhamRepository
```

---

## Stored Procedures & Triggers

### Trigger Execution Flow

```mermaid
graph LR
    INSERT["INSERT CHI_TIET_DON_HANG"] --> T1["trg_TinhThanhTien<br>ThanhTien = SL * DonGia"]
    T1 --> T2["trg_CapNhatTongTien<br>TongTien = SUM(ThanhTien)"]
    T2 --> DONE["DON_HANG updated"]

    INSERT2["INSERT CT_NHAP_KHO"] --> T3["trg_NhapKho_TangTon<br>SoLuongTon += SL"]
    T3 --> DONE2["SAN_PHAM updated"]
```

### Stored Procedures (19 total)

| Procedure | Purpose |
|-----------|---------|
| `sp_DangNhap` | Authenticate user |
| `sp_DoiMatKhau` | Change password |
| `sp_TaoDonHang` | Create order |
| `sp_ThemChiTietDon` | Add order line item |
| `sp_CapNhatTrangThaiDon` | Update order status + stock |
| `sp_TaoPhieuNhap` / `sp_ThemChiTietNhap` | Inventory receipt |
| `sp_GhiNhanHangHu` | Record damaged goods |
| `sp_TaoGiaoHang` / `sp_PhanCongShipper` / `sp_CapNhatTrangThaiGiao` | Delivery |
| `sp_GhiNhanPhanHoi` | Customer feedback |
| `sp_BaoCaoDoanhThuNgay` / `sp_BaoCaoDoanhThuThang` | Revenue reports |
| `sp_SanPhamBanChay` | Top 10 products |
| `sp_HieuSuatNhanVien` | Staff performance |
| `sp_CanhBaoTonKho` | Stock alerts |
| `sp_DoanhThuTheoNgayTrongThang` | Monthly chart data |
| `sp_SinhMa` | Auto-generate codes |

---

## Auto Code Generation

The system uses `sp_SinhMa` to generate unique codes for all entities:

| Entity | Prefix | Table | Column | Example |
|--------|--------|-------|--------|---------|
| Employee | `NV` | NHAN_VIEN | MaNV | NV000006 |
| Customer | `KH` | KHACH_HANG | MaKH | KH000005 |
| Product | `SP` | SAN_PHAM | MaSP | SP000009 |
| Order | `DH` | DON_HANG | MaDon | DH000006 |
| Receipt | `PN` | PHIEU_NHAP_KHO | MaPhieu | PN000002 |
| Delivery | `GH` | GIAO_HANG | MaGiaoHang | GH000004 |
| Feedback | `PH` | PHAN_HOI | MaPH | PH000002 |
| Return | `PT` | TRA_HANG | MaPhieuTra | PT000001 |
| Damaged | `HH` | HANG_HU | MaPhieuHuy | HH000001 |

**Algorithm**: `SELECT MAX(CAST(SUBSTRING(column, LEN(prefix)+1, 10) AS INT))` → increment by 1 → pad to 6 digits.

---

## License

This project is developed for educational purposes as part of a coursework assignment.
