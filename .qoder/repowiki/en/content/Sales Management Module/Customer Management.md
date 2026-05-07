# Customer Management

<cite>
**Referenced Files in This Document**
- [KhachHang.cs](file://Models/KhachHang.cs)
- [KhachHangDAO.cs](file://DataAccess/KhachHangDAO.cs)
- [ucKhachHang.cs](file://7_DanhMuc/ucKhachHang.cs)
- [ucKhachHang.Designer.cs](file://7_DanhMuc/ucKhachHang.Designer.cs)
- [DatabaseHelper.cs](file://DataAccess/DatabaseHelper.cs)
- [DonHang.cs](file://Models/DonHang.cs)
- [DonHangDAO.cs](file://DataAccess/DonHangDAO.cs)
- [PhanHoi.cs](file://Models/PhanHoi.cs)
- [PhanHoiDAO.cs](file://DataAccess/PhanHoiDAO.cs)
- [TraHang.cs](file://Models/TraHang.cs)
- [TraHangDAO.cs](file://DataAccess/TraHangDAO.cs)
- [BaoCaoDAO.cs](file://DataAccess/BaoCaoDAO.cs)
- [ucDashboard.cs](file://2_QuanLy/ucDashboard.cs)
- [FloriSys_Database.sql](file://FloriSys_Database.sql)
</cite>

## Table of Contents
1. [Introduction](#introduction)
2. [Project Structure](#project-structure)
3. [Core Components](#core-components)
4. [Architecture Overview](#architecture-overview)
5. [Detailed Component Analysis](#detailed-component-analysis)
6. [Dependency Analysis](#dependency-analysis)
7. [Performance Considerations](#performance-considerations)
8. [Troubleshooting Guide](#troubleshooting-guide)
9. [Conclusion](#conclusion)
10. [Appendices](#appendices)

## Introduction
This document describes the customer management capabilities within the FloriSys Sales Management Module. It covers customer registration, profile updates, search, and deletion safeguards, along with the customer data model, integration with sales order processing, customer service workflows (complaint handling and returns), and reporting/analytics for purchase behavior and retention. It also outlines procedures for data import/export, duplicate detection, and data privacy considerations.

## Project Structure
Customer management spans three layers:
- Presentation: A Windows Forms user control for managing customers.
- Data Access: DAO classes encapsulate CRUD operations against the database.
- Domain Model: Strongly typed models represent domain entities.

```mermaid
graph TB
subgraph "Presentation Layer"
UC["ucKhachHang<br/>Windows Forms User Control"]
end
subgraph "Data Access Layer"
DAO["KhachHangDAO"]
OrdersDAO["DonHangDAO"]
FeedbackDAO["PhanHoiDAO"]
ReturnsDAO["TraHangDAO"]
ReportsDAO["BaoCaoDAO"]
DBH["DatabaseHelper"]
end
subgraph "Domain Models"
KH["KhachHang"]
DH["DonHang"]
PH["PhanHoi"]
TH["TraHang"]
end
subgraph "Database"
DB["SQL Server Tables"]
end
UC --> DAO
UC --> OrdersDAO
UC --> FeedbackDAO
UC --> ReturnsDAO
UC --> ReportsDAO
DAO --> DBH
OrdersDAO --> DBH
FeedbackDAO --> DBH
ReturnsDAO --> DBH
ReportsDAO --> DBH
DBH --> DB
DAO --> KH
OrdersDAO --> DH
FeedbackDAO --> PH
ReturnsDAO --> TH
```

**Diagram sources**
- [ucKhachHang.cs:10-237](file://7_DanhMuc/ucKhachHang.cs#L10-L237)
- [KhachHangDAO.cs:9-75](file://DataAccess/KhachHangDAO.cs#L9-L75)
- [DonHangDAO.cs:9-114](file://DataAccess/DonHangDAO.cs#L9-L114)
- [PhanHoiDAO.cs:7-51](file://DataAccess/PhanHoiDAO.cs#L7-L51)
- [TraHangDAO.cs:7-51](file://DataAccess/TraHangDAO.cs#L7-L51)
- [BaoCaoDAO.cs:9-167](file://DataAccess/BaoCaoDAO.cs#L9-L167)
- [DatabaseHelper.cs:10-212](file://DataAccess/DatabaseHelper.cs#L10-L212)
- [FloriSys_Database.sql:34-44](file://FloriSys_Database.sql#L34-L44)

**Section sources**
- [ucKhachHang.cs:10-237](file://7_DanhMuc/ucKhachHang.cs#L10-L237)
- [ucKhachHang.Designer.cs:18-156](file://7_DanhMuc/ucKhachHang.Designer.cs#L18-L156)
- [KhachHangDAO.cs:9-75](file://DataAccess/KhachHangDAO.cs#L9-L75)
- [DonHangDAO.cs:9-114](file://DataAccess/DonHangDAO.cs#L9-L114)
- [PhanHoiDAO.cs:7-51](file://DataAccess/PhanHoiDAO.cs#L7-L51)
- [TraHangDAO.cs:7-51](file://DataAccess/TraHangDAO.cs#L7-L51)
- [BaoCaoDAO.cs:9-167](file://DataAccess/BaoCaoDAO.cs#L9-L167)
- [DatabaseHelper.cs:10-212](file://DataAccess/DatabaseHelper.cs#L10-L212)
- [FloriSys_Database.sql:34-44](file://FloriSys_Database.sql#L34-L44)

## Core Components
- Customer entity: identity, personal info, contact details, creation date, and computed total orders.
- Customer DAO: search, lookup by phone, create, update, delete with referential integrity checks.
- Presentation control: grid display, inline edit dialog, search, context actions (edit/delete).
- Order integration: customer-to-orders relationship and computed order totals.
- Customer service: feedback and returns workflows integrated with orders.
- Reporting: dashboard and analytics around orders and customer activity.

**Section sources**
- [KhachHang.cs:5-16](file://Models/KhachHang.cs#L5-L16)
- [KhachHangDAO.cs:11-72](file://DataAccess/KhachHangDAO.cs#L11-L72)
- [ucKhachHang.cs:24-234](file://7_DanhMuc/ucKhachHang.cs#L24-L234)
- [DonHang.cs:6-51](file://Models/DonHang.cs#L6-L51)
- [DonHangDAO.cs:11-98](file://DataAccess/DonHangDAO.cs#L11-L98)
- [PhanHoi.cs:5-30](file://Models/PhanHoi.cs#L5-L30)
- [PhanHoiDAO.cs:9-48](file://DataAccess/PhanHoiDAO.cs#L9-L48)
- [TraHang.cs:6-31](file://Models/TraHang.cs#L6-L31)
- [TraHangDAO.cs:9-48](file://DataAccess/TraHangDAO.cs#L9-L48)
- [BaoCaoDAO.cs:72-128](file://DataAccess/BaoCaoDAO.cs#L72-L128)
- [ucDashboard.cs:18-138](file://2_QuanLy/ucDashboard.cs#L18-L138)

## Architecture Overview
The customer management subsystem follows a layered architecture:
- UI layer (Windows Forms) interacts with business logic via DAOs.
- DAOs use a generic database helper to execute raw SQL and stored procedures.
- Database schema defines normalized tables with constraints and triggers to maintain data integrity.

```mermaid
classDiagram
class KhachHang {
+string MaKH
+string HoTen
+string SoDienThoai
+string DiaChi
+string Email
+DateTime NgayTao
+int TongDon
}
class DonHang {
+string MaDon
+DateTime NgayTao
+string MaKH
+string MaNV_TaoDon
+string HinhThucNhanHang
+string TrangThai
+decimal TongTien
+string GhiChu
+string TenKH
+string SoDienThoai
+string DiaChi
+string Email
+string TenNV
}
class PhanHoi {
+string MaPH
+string MaDon
+string NoiDung
+DateTime NgayGhi
+string TrangThaiXuLy
+string KetQuaXuLy
+string TenKH
}
class TraHang {
+string MaPhieuTra
+string MaDon
+string LyDo
+string HinhThucHoanTien
+string GhiChu
+DateTime NgayTra
}
class KhachHangDAO {
+LayDanhSach(keyword)
+TimTheoSDT(sdt)
+ThemKhachHang(kh)
+CapNhatKhachHang(kh)
+XoaKhachHang(maKH)
}
class DonHangDAO {
+LayDanhSach(...)
+LayChiTiet(maDon)
+LayThongTinDon(maDon)
+TaoDonHang(maKH, maNV, hinhThuc, ghiChu)
+ThemChiTiet(maDon, maSP, soLuong, donGia)
+CapNhatTrangThai(maDon, trangThai)
}
class PhanHoiDAO {
+LayDanhSach(maDon="")
+GhiNhan(maDon, noiDung)
+CapNhatXuLy(maPH, trangThai, ketQua)
}
class TraHangDAO {
+ThemPhieuTra(maDon, lyDo, hinhThuc, ghiChu)
+ThemChiTietTra(maPhieu, maSP, soLuong, coNhapKho)
}
class DatabaseHelper {
+ExecuteRawList<T>(sql, params)
+ExecuteRawSingle<T>(sql, params)
+ExecuteRawNonQuery(sql, params)
+GenerateCode(prefix, table, column)
}
class BaoCaoDAO {
+ThongKeDashboard()
+DonHangGanDay(top)
+DonHangCuaNV(maNV, top)
+DoanhThu7Ngay()
}
KhachHangDAO --> KhachHang : "maps"
DonHangDAO --> DonHang : "maps"
PhanHoiDAO --> PhanHoi : "maps"
TraHangDAO --> TraHang : "maps"
KhachHangDAO --> DatabaseHelper : "uses"
DonHangDAO --> DatabaseHelper : "uses"
PhanHoiDAO --> DatabaseHelper : "uses"
TraHangDAO --> DatabaseHelper : "uses"
BaoCaoDAO --> DatabaseHelper : "uses"
```

**Diagram sources**
- [KhachHang.cs:5-16](file://Models/KhachHang.cs#L5-L16)
- [DonHang.cs:6-51](file://Models/DonHang.cs#L6-L51)
- [PhanHoi.cs:5-30](file://Models/PhanHoi.cs#L5-L30)
- [TraHang.cs:6-31](file://Models/TraHang.cs#L6-L31)
- [KhachHangDAO.cs:9-75](file://DataAccess/KhachHangDAO.cs#L9-L75)
- [DonHangDAO.cs:9-114](file://DataAccess/DonHangDAO.cs#L9-L114)
- [PhanHoiDAO.cs:7-51](file://DataAccess/PhanHoiDAO.cs#L7-L51)
- [TraHangDAO.cs:7-51](file://DataAccess/TraHangDAO.cs#L7-L51)
- [DatabaseHelper.cs:10-212](file://DataAccess/DatabaseHelper.cs#L10-L212)
- [BaoCaoDAO.cs:9-167](file://DataAccess/BaoCaoDAO.cs#L9-L167)

## Detailed Component Analysis

### Customer Data Model
- Identity: unique customer ID.
- Personal info: full name.
- Contact: phone (unique), address, email.
- Audit: creation date.
- Computed: total orders (derived from joined orders count).

```mermaid
erDiagram
KHACH_HANG {
nvarchar MaKH PK
nvarchar HoTen
nvarchar SoDienThoai UK
nvarchar DiaChi
nvarchar Email
datetime NgayTao
}
DON_HANG {
nvarchar MaDon PK
datetime NgayTao
nvarchar MaKH FK
nvarchar MaNV_TaoDon
nvarchar HinhThucNhanHang
nvarchar TrangThai
decimal TongTien
nvarchar GhiChu
}
KHACH_HANG ||--o{ DON_HANG : "has"
```

**Diagram sources**
- [FloriSys_Database.sql:34-44](file://FloriSys_Database.sql#L34-L44)
- [FloriSys_Database.sql:64-74](file://FloriSys_Database.sql#L64-L74)

**Section sources**
- [KhachHang.cs:5-16](file://Models/KhachHang.cs#L5-L16)
- [FloriSys_Database.sql:34-44](file://FloriSys_Database.sql#L34-L44)

### Customer Registration and Profile Updates
- Registration: generates a new customer ID, inserts personal and contact info.
- Profile update: updates name, phone, address, email.
- Inline edit dialog validates required fields and persists changes.

```mermaid
sequenceDiagram
participant UI as "ucKhachHang"
participant DAO as "KhachHangDAO"
participant DBH as "DatabaseHelper"
participant DB as "SQL Server"
UI->>UI : "ShowEditDialog()"
UI->>DAO : "ThemKhachHang(kh)"
DAO->>DBH : "GenerateCode('KH','KHACH_HANG','MaKH')"
DBH-->>DAO : "new MaKH"
DAO->>DB : "INSERT INTO KHACH_HANG"
DB-->>DAO : "rows affected"
DAO-->>UI : "return MaKH"
UI-->>UI : "Refresh grid"
```

**Diagram sources**
- [ucKhachHang.cs:133-179](file://7_DanhMuc/ucKhachHang.cs#L133-L179)
- [KhachHangDAO.cs:32-46](file://DataAccess/KhachHangDAO.cs#L32-L46)
- [DatabaseHelper.cs:189-209](file://DataAccess/DatabaseHelper.cs#L189-L209)

**Section sources**
- [ucKhachHang.cs:133-179](file://7_DanhMuc/ucKhachHang.cs#L133-L179)
- [KhachHangDAO.cs:32-60](file://DataAccess/KhachHangDAO.cs#L32-L60)
- [DatabaseHelper.cs:189-209](file://DataAccess/DatabaseHelper.cs#L189-L209)

### Customer Search and Listing
- Keyword search across name, phone, and email.
- Grid displays customer info and computed total orders.
- Search triggers reload with filtered results.

```mermaid
flowchart TD
Start(["User types in search box"]) --> BuildSQL["Build SQL with optional LIKE filters"]
BuildSQL --> Execute["ExecuteRawList<KhachHang>()"]
Execute --> Map["Map DataTable to List<KhachHang>"]
Map --> Bind["Bind to DataGridView"]
Bind --> End(["Grid updated"])
```

**Diagram sources**
- [ucKhachHang.cs:24-58](file://7_DanhMuc/ucKhachHang.cs#L24-L58)
- [KhachHangDAO.cs:11-24](file://DataAccess/KhachHangDAO.cs#L11-L24)
- [DatabaseHelper.cs:28-32](file://DataAccess/DatabaseHelper.cs#L28-L32)

**Section sources**
- [ucKhachHang.cs:24-58](file://7_DanhMuc/ucKhachHang.cs#L24-L58)
- [KhachHangDAO.cs:11-24](file://DataAccess/KhachHangDAO.cs#L11-L24)
- [DatabaseHelper.cs:28-32](file://DataAccess/DatabaseHelper.cs#L28-L32)

### Customer Deletion and Referential Integrity
- Deletion is prevented if the customer has existing orders.
- If safe to delete, the record is removed.

```mermaid
flowchart TD
Start(["Delete Selected"]) --> CheckOrders["SELECT COUNT(*) FROM DON_HANG WHERE MaKH = ?"]
CheckOrders --> HasOrders{"Count > 0?"}
HasOrders --> |Yes| Block["Throw error: Cannot delete customer with orders"]
HasOrders --> |No| Remove["DELETE FROM KHACH_HANG WHERE MaKH = ?"]
Remove --> Done(["Deleted"])
Block --> Done
```

**Diagram sources**
- [ucKhachHang.cs:213-234](file://7_DanhMuc/ucKhachHang.cs#L213-L234)
- [KhachHangDAO.cs:62-72](file://DataAccess/KhachHangDAO.cs#L62-L72)

**Section sources**
- [ucKhachHang.cs:213-234](file://7_DanhMuc/ucKhachHang.cs#L213-L234)
- [KhachHangDAO.cs:62-72](file://DataAccess/KhachHangDAO.cs#L62-L72)

### Customer Service Workflows
- Complaint handling: log feedback linked to an order; track processing status.
- Returns: create return ticket, capture items and inventory impact; update order status.

```mermaid
sequenceDiagram
participant UI as "Sales UI"
participant Orders as "DonHangDAO"
participant Feedback as "PhanHoiDAO"
participant Returns as "TraHangDAO"
participant DB as "SQL Server"
UI->>Orders : "LayThongTinDon(maDon)"
Orders->>DB : "SELECT JOIN with KHACH_HANG/NHAN_VIEN"
DB-->>Orders : "DonHang with customer info"
Orders-->>UI : "Order details"
UI->>Feedback : "GhiNhan(maDon, noiDung)"
Feedback->>DB : "INSERT PHAN_HOI"
DB-->>Feedback : "OK"
UI->>Returns : "ThemPhieuTra(maDon, lyDo, hinhThuc, ghiChu)"
Returns->>DB : "INSERT TRA_HANG"
Returns->>Orders : "CapNhatTrangThai(maDon,'HoanHang')"
Orders->>DB : "UPDATE DON_HANG TrangThai"
DB-->>Returns : "OK"
DB-->>Orders : "OK"
```

**Diagram sources**
- [DonHangDAO.cs:53-64](file://DataAccess/DonHangDAO.cs#L53-L64)
- [PhanHoiDAO.cs:27-37](file://DataAccess/PhanHoiDAO.cs#L27-L37)
- [TraHangDAO.cs:9-25](file://DataAccess/TraHangDAO.cs#L9-L25)
- [DonHangDAO.cs:91-98](file://DataAccess/DonHangDAO.cs#L91-L98)

**Section sources**
- [DonHangDAO.cs:53-64](file://DataAccess/DonHangDAO.cs#L53-L64)
- [PhanHoiDAO.cs:9-37](file://DataAccess/PhanHoiDAO.cs#L9-L37)
- [TraHangDAO.cs:9-25](file://DataAccess/TraHangDAO.cs#L9-L25)

### Integration with Sales Order Processing
- Orders reference customers; customer info is included in order queries.
- Order status transitions trigger inventory adjustments.
- Dashboard surfaces recent orders and customer-related metrics.

```mermaid
sequenceDiagram
participant Cashier as "Cashier UI"
participant Orders as "DonHangDAO"
participant DB as "SQL Server"
Cashier->>Orders : "TaoDonHang(maKH, maNV, hinhThuc, ghiChu)"
Orders->>DB : "EXEC sp_TaoDonHang"
DB-->>Orders : "OK"
Cashier->>Orders : "ThemChiTiet(maDon, maSP, soLuong, donGia)"
Orders->>DB : "EXEC sp_ThemChiTietDon"
DB-->>Orders : "OK"
Cashier->>Orders : "CapNhatTrangThai(maDon, trangThai)"
Orders->>DB : "EXEC sp_CapNhatTrangThaiDon"
DB-->>Orders : "OK"
```

**Diagram sources**
- [DonHangDAO.cs:66-98](file://DataAccess/DonHangDAO.cs#L66-L98)
- [FloriSys_Database.sql:282-358](file://FloriSys_Database.sql#L282-L358)

**Section sources**
- [DonHangDAO.cs:11-98](file://DataAccess/DonHangDAO.cs#L11-L98)
- [FloriSys_Database.sql:282-358](file://FloriSys_Database.sql#L282-L358)
- [ucDashboard.cs:112-138](file://2_QuanLy/ucDashboard.cs#L112-L138)

### Customer Analytics and Retention
- Dashboard statistics include recent orders and shipping metrics.
- Reporting DAO provides aggregated insights for daily, weekly, monthly trends.
- Customer segment insights can be derived from order counts and spending patterns.

```mermaid
flowchart TD
Start(["Load Dashboard"]) --> Stats["ThongKeDashboard()"]
Stats --> Orders["DonHangGanDay(top)"]
Orders --> Grid["Bind to grid"]
Start --> Reports["DoanhThu7Ngay()"]
Reports --> Chart["Render chart"]
Grid --> End(["Dashboard ready"])
Chart --> End
```

**Diagram sources**
- [ucDashboard.cs:18-138](file://2_QuanLy/ucDashboard.cs#L18-L138)
- [BaoCaoDAO.cs:72-155](file://DataAccess/BaoCaoDAO.cs#L72-L155)

**Section sources**
- [ucDashboard.cs:18-138](file://2_QuanLy/ucDashboard.cs#L18-L138)
- [BaoCaoDAO.cs:72-155](file://DataAccess/BaoCaoDAO.cs#L72-L155)

## Dependency Analysis
- ucKhachHang depends on KhachHangDAO for data operations and on Windows Forms for UI.
- DAOs depend on DatabaseHelper for SQL execution and code generation.
- Orders, feedback, and returns DAOs integrate with the customer domain via foreign keys.
- Reporting DAOs rely on stored procedures and ad-hoc queries to aggregate data.

```mermaid
graph LR
UC["ucKhachHang"] --> KHD["KhachHangDAO"]
UC --> OHD["DonHangDAO"]
UC --> PHD["PhanHoiDAO"]
UC --> THD["TraHangDAO"]
UC --> BSD["BaoCaoDAO"]
KHD --> DBH["DatabaseHelper"]
OHD --> DBH
PHD --> DBH
THD --> DBH
BSD --> DBH
DBH --> DB["SQL Server"]
```

**Diagram sources**
- [ucKhachHang.cs:10-237](file://7_DanhMuc/ucKhachHang.cs#L10-L237)
- [KhachHangDAO.cs:9-75](file://DataAccess/KhachHangDAO.cs#L9-L75)
- [DonHangDAO.cs:9-114](file://DataAccess/DonHangDAO.cs#L9-L114)
- [PhanHoiDAO.cs:7-51](file://DataAccess/PhanHoiDAO.cs#L7-L51)
- [TraHangDAO.cs:7-51](file://DataAccess/TraHangDAO.cs#L7-L51)
- [BaoCaoDAO.cs:9-167](file://DataAccess/BaoCaoDAO.cs#L9-L167)
- [DatabaseHelper.cs:10-212](file://DataAccess/DatabaseHelper.cs#L10-L212)

**Section sources**
- [ucKhachHang.cs:10-237](file://7_DanhMuc/ucKhachHang.cs#L10-L237)
- [KhachHangDAO.cs:9-75](file://DataAccess/KhachHangDAO.cs#L9-L75)
- [DonHangDAO.cs:9-114](file://DataAccess/DonHangDAO.cs#L9-L114)
- [PhanHoiDAO.cs:7-51](file://DataAccess/PhanHoiDAO.cs#L7-L51)
- [TraHangDAO.cs:7-51](file://DataAccess/TraHangDAO.cs#L7-L51)
- [BaoCaoDAO.cs:9-167](file://DataAccess/BaoCaoDAO.cs#L9-L167)
- [DatabaseHelper.cs:10-212](file://DataAccess/DatabaseHelper.cs#L10-L212)

## Performance Considerations
- Prefer indexed columns in search filters (phone number is unique and indexed).
- Use stored procedures for write-heavy operations to reduce round trips.
- Avoid loading unnecessary computed fields when not displayed.
- Batch UI refreshes after bulk operations to minimize flicker.

## Troubleshooting Guide
- Duplicate phone number: Registration/update requires a unique phone number; ensure uniqueness before saving.
- Deletion blocked: If deletion fails, the customer likely has orders; cancel or fulfill orders first.
- Search yields no results: Verify keyword matches name, phone, or email; check casing and special characters.
- Import/export: Use the database export/import facilities; ensure referential integrity when importing customer data.

**Section sources**
- [FloriSys_Database.sql](file://FloriSys_Database.sql#L39)
- [ucKhachHang.cs:133-179](file://7_DanhMuc/ucKhachHang.cs#L133-L179)
- [KhachHangDAO.cs:62-72](file://DataAccess/KhachHangDAO.cs#L62-L72)

## Conclusion
The customer management module provides robust CRUD operations, strong referential integrity, and seamless integration with sales order processing. It supports customer service workflows, offers actionable analytics, and maintains data quality through constraints and validations. Extending segmentation and targeted marketing features would involve deriving cohort and behavioral segments from order history and feedback data.

## Appendices

### Customer Data Model Fields
- Customer ID, Name, Phone (unique), Address, Email, Creation Date, Total Orders (computed).

**Section sources**
- [KhachHang.cs:5-16](file://Models/KhachHang.cs#L5-L16)
- [FloriSys_Database.sql:34-44](file://FloriSys_Database.sql#L34-L44)

### Customer Segmentation and Targeting
- Segment by frequency (orders per period), recency (last order), monetary value (total spent).
- Target campaigns using order categories and product preferences from order details.

**Section sources**
- [DonHangDAO.cs:11-42](file://DataAccess/DonHangDAO.cs#L11-L42)
- [DonHang.cs:6-26](file://Models/DonHang.cs#L6-L26)

### Customer Service Scenarios
- Complaint resolution: Log feedback against an order; update processing status.
- Returns processing: Create return ticket, adjust inventory if applicable, update order status.

**Section sources**
- [PhanHoiDAO.cs:9-48](file://DataAccess/PhanHoiDAO.cs#L9-L48)
- [TraHangDAO.cs:9-48](file://DataAccess/TraHangDAO.cs#L9-L48)

### Data Import/Export and Privacy
- Import/export via database tools; ensure GDPR-compliant handling of personal data.
- Enforce unique constraints (phone) to prevent duplicates during import.

**Section sources**
- [FloriSys_Database.sql](file://FloriSys_Database.sql#L39)