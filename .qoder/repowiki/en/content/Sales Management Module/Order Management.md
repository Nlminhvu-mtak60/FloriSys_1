# Order Management

<cite>
**Referenced Files in This Document**
- [ucDanhSachDon.cs](file://3_BanHang/ucDanhSachDon.cs)
- [ucTaoDon.cs](file://3_BanHang/ucTaoDon.cs)
- [ucChiTietDonHang.cs](file://3_BanHang/ucChiTietDonHang.cs)
- [DonHangDAO.cs](file://DataAccess/DonHangDAO.cs)
- [KhachHangDAO.cs](file://DataAccess/KhachHangDAO.cs)
- [SanPhamDAO.cs](file://DataAccess/SanPhamDAO.cs)
- [GiaoHangDAO.cs](file://DataAccess/GiaoHangDAO.cs)
- [DonHang.cs](file://Models/DonHang.cs)
- [KhachHang.cs](file://Models/KhachHang.cs)
- [SanPham.cs](file://Models/SanPham.cs)
- [SessionManager.cs](file://Services/SessionManager.cs)
- [ucDashboardBanHang.cs](file://3_BanHang/ucDashboardBanHang.cs)
- [FloriSys_Database.sql](file://FloriSys_Database.sql)
- [fix_sp.sql](file://fix_sp.sql)
- [fix_sp2.sql](file://fix_sp2.sql)
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
This document provides comprehensive order management documentation for the FloriSys Sales Management Module. It covers the complete order lifecycle from creation to completion, including processing, modification, cancellation, and status tracking. It also documents the user interface components for order list management, order creation wizard, and order detail views, along with business rules for order states, payment processing, and inventory allocation. Step-by-step workflows are included for creating new orders, modifying existing orders, and handling cancellations. Integration points with the customer database, product catalog, and shipping/goods-out systems are explained, alongside examples of typical order processing scenarios, common errors, and best practices.

## Project Structure
The order management module resides primarily under the Sales area (3_BanHang) and interacts with data access layer (DataAccess), models (Models), and services (Services). The database schema defines core entities and stored procedures that enforce business rules such as inventory allocation and state transitions.

```mermaid
graph TB
subgraph "UI Layer"
UC_List["ucDanhSachDon<br/>Order List"]
UC_Create["ucTaoDon<br/>Order Wizard"]
UC_Detail["ucChiTietDonHang<br/>Order Detail"]
UC_Dashboard["ucDashboardBanHang<br/>Sales Dashboard"]
end
subgraph "Business Logic"
DAO_DH["DonHangDAO"]
DAO_KH["KhachHangDAO"]
DAO_SP["SanPhamDAO"]
DAO_GH["GiaoHangDAO"]
Model_DH["DonHang.cs"]
Model_KH["KhachHang.cs"]
Model_SP["SanPham.cs"]
Session["SessionManager.cs"]
end
subgraph "Database"
Schema["FloriSys_Database.sql"]
SP1["sp_TaoDonHang"]
SP2["sp_ThemChiTietDon"]
SP3["sp_CapNhatTrangThaiDon"]
SP4["sp_TaoGiaoHang"]
SP5["sp_PhanCongShipper"]
SP6["sp_CapNhatTrangThaiGiao"]
end
UC_List --> DAO_DH
UC_Create --> DAO_DH
UC_Create --> DAO_KH
UC_Create --> DAO_SP
UC_Create --> DAO_GH
UC_Detail --> DAO_DH
UC_Dashboard --> DAO_DH
DAO_DH --> Model_DH
DAO_KH --> Model_KH
DAO_SP --> Model_SP
DAO_GH --> Model_DH
DAO_DH --> Schema
DAO_KH --> Schema
DAO_SP --> Schema
DAO_GH --> Schema
Schema --> SP1
Schema --> SP2
Schema --> SP3
Schema --> SP4
Schema --> SP5
Schema --> SP6
```

**Diagram sources**
- [ucDanhSachDon.cs:1-114](file://3_BanHang/ucDanhSachDon.cs#L1-L114)
- [ucTaoDon.cs:1-162](file://3_BanHang/ucTaoDon.cs#L1-L162)
- [ucChiTietDonHang.cs:1-132](file://3_BanHang/ucChiTietDonHang.cs#L1-L132)
- [ucDashboardBanHang.cs:1-85](file://3_BanHang/ucDashboardBanHang.cs#L1-L85)
- [DonHangDAO.cs:1-114](file://DataAccess/DonHangDAO.cs#L1-L114)
- [KhachHangDAO.cs:1-75](file://DataAccess/KhachHangDAO.cs#L1-L75)
- [SanPhamDAO.cs:1-96](file://DataAccess/SanPhamDAO.cs#L1-L96)
- [GiaoHangDAO.cs:1-96](file://DataAccess/GiaoHangDAO.cs#L1-L96)
- [DonHang.cs:1-63](file://Models/DonHang.cs#L1-L63)
- [KhachHang.cs:1-18](file://Models/KhachHang.cs#L1-L18)
- [SanPham.cs:1-42](file://Models/SanPham.cs#L1-L42)
- [SessionManager.cs:1-62](file://Services/SessionManager.cs#L1-L62)
- [FloriSys_Database.sql:64-101](file://FloriSys_Database.sql#L64-L101)
- [fix_sp.sql:1-35](file://fix_sp.sql#L1-L35)
- [fix_sp2.sql:1-34](file://fix_sp2.sql#L1-L34)

**Section sources**
- [ucDanhSachDon.cs:1-114](file://3_BanHang/ucDanhSachDon.cs#L1-L114)
- [ucTaoDon.cs:1-162](file://3_BanHang/ucTaoDon.cs#L1-L162)
- [ucChiTietDonHang.cs:1-132](file://3_BanHang/ucChiTietDonHang.cs#L1-L132)
- [ucDashboardBanHang.cs:1-85](file://3_BanHang/ucDashboardBanHang.cs#L1-L85)
- [DonHangDAO.cs:1-114](file://DataAccess/DonHangDAO.cs#L1-L114)
- [KhachHangDAO.cs:1-75](file://DataAccess/KhachHangDAO.cs#L1-L75)
- [SanPhamDAO.cs:1-96](file://DataAccess/SanPhamDAO.cs#L1-L96)
- [GiaoHangDAO.cs:1-96](file://DataAccess/GiaoHangDAO.cs#L1-L96)
- [DonHang.cs:1-63](file://Models/DonHang.cs#L1-L63)
- [KhachHang.cs:1-18](file://Models/KhachHang.cs#L1-L18)
- [SanPham.cs:1-42](file://Models/SanPham.cs#L1-L42)
- [SessionManager.cs:1-62](file://Services/SessionManager.cs#L1-L62)
- [FloriSys_Database.sql:64-101](file://FloriSys_Database.sql#L64-L101)
- [fix_sp.sql:1-35](file://fix_sp.sql#L1-L35)
- [fix_sp2.sql:1-34](file://fix_sp2.sql#L1-L34)

## Core Components
- Order List Management (ucDanhSachDon): Loads, filters, and displays orders with search and status filtering. Emits events to view details and create new orders.
- Order Creation Wizard (ucTaoDon): Manages cart, validates stock availability, creates orders, and optionally generates delivery records for home-delivery orders.
- Order Detail View (ucChiTietDonHang): Displays order info, items, and allows updating order status.
- Data Access Layer: Centralized DAOs for orders, customers, products, and deliveries.
- Models: Strongly typed domain models for orders, customers, and products.
- Session Manager: Provides current user context for creating orders and dashboards.
- Database Schema and Stored Procedures: Enforce business rules for inventory allocation, order state transitions, and delivery synchronization.

**Section sources**
- [ucDanhSachDon.cs:1-114](file://3_BanHang/ucDanhSachDon.cs#L1-L114)
- [ucTaoDon.cs:1-162](file://3_BanHang/ucTaoDon.cs#L1-L162)
- [ucChiTietDonHang.cs:1-132](file://3_BanHang/ucChiTietDonHang.cs#L1-L132)
- [DonHangDAO.cs:1-114](file://DataAccess/DonHangDAO.cs#L1-L114)
- [KhachHangDAO.cs:1-75](file://DataAccess/KhachHangDAO.cs#L1-L75)
- [SanPhamDAO.cs:1-96](file://DataAccess/SanPhamDAO.cs#L1-L96)
- [GiaoHangDAO.cs:1-96](file://DataAccess/GiaoHangDAO.cs#L1-L96)
- [DonHang.cs:1-63](file://Models/DonHang.cs#L1-L63)
- [KhachHang.cs:1-18](file://Models/KhachHang.cs#L1-L18)
- [SanPham.cs:1-42](file://Models/SanPham.cs#L1-L42)
- [SessionManager.cs:1-62](file://Services/SessionManager.cs#L1-L62)

## Architecture Overview
The order management architecture follows a layered pattern:
- UI Layer: Windows Forms user controls for order list, creation, and detail views.
- Business Logic Layer: DAOs encapsulate data operations and call stored procedures.
- Domain Models: DTOs representing entities and computed display properties.
- Data Access: Uses a shared helper to execute raw SQL and stored procedures.
- Database: Enforces business rules via stored procedures and constraints.

```mermaid
sequenceDiagram
participant UI_List as "ucDanhSachDon"
participant UI_Create as "ucTaoDon"
participant UI_Detail as "ucChiTietDonHang"
participant DAO_DH as "DonHangDAO"
participant DAO_KH as "KhachHangDAO"
participant DAO_SP as "SanPhamDAO"
participant DAO_GH as "GiaoHangDAO"
participant DB as "Database"
UI_List->>DAO_DH : Load orders (filter by keyword/status)
DAO_DH->>DB : SELECT orders with joins
DB-->>DAO_DH : Orders list
DAO_DH-->>UI_List : Bind grid
UI_Create->>DAO_KH : Lookup/create customer by phone
DAO_KH->>DB : Query/Insert customer
DB-->>DAO_KH : Customer record
UI_Create->>DAO_DH : Create order (sp_TaoDonHang)
DAO_DH->>DB : Execute sp_TaoDonHang
UI_Create->>DAO_DH : Add order items (sp_ThemChiTietDon)
DAO_DH->>DB : Execute sp_ThemChiTietDon
alt Home delivery
UI_Create->>DAO_GH : Create delivery (sp_TaoGiaoHang)
DAO_GH->>DB : Execute sp_TaoGiaoHang
end
DB-->>DAO_DH : Success
DAO_DH-->>UI_Create : Order ID
UI_Detail->>DAO_DH : Load order info and items
DAO_DH->>DB : SELECT order and items
DB-->>DAO_DH : Order and items
DAO_DH-->>UI_Detail : Bind info and grid
UI_Detail->>DAO_DH : Update status (sp_CapNhatTrangThaiDon)
DAO_DH->>DB : Execute sp_CapNhatTrangThaiDon
DB-->>DAO_DH : Inventory adjusted if applicable
DAO_DH-->>UI_Detail : Refresh info
```

**Diagram sources**
- [ucDanhSachDon.cs:28-45](file://3_BanHang/ucDanhSachDon.cs#L28-L45)
- [ucTaoDon.cs:102-152](file://3_BanHang/ucTaoDon.cs#L102-L152)
- [ucChiTietDonHang.cs:33-115](file://3_BanHang/ucChiTietDonHang.cs#L33-L115)
- [DonHangDAO.cs:11-42](file://DataAccess/DonHangDAO.cs#L11-L42)
- [KhachHangDAO.cs:26-46](file://DataAccess/KhachHangDAO.cs#L26-L46)
- [SanPhamDAO.cs:35-47](file://DataAccess/SanPhamDAO.cs#L35-L47)
- [GiaoHangDAO.cs:54-83](file://DataAccess/GiaoHangDAO.cs#L54-L83)
- [FloriSys_Database.sql:282-358](file://FloriSys_Database.sql#L282-L358)
- [fix_sp.sql:1-35](file://fix_sp.sql#L1-L35)
- [fix_sp2.sql:1-34](file://fix_sp2.sql#L1-L34)

## Detailed Component Analysis

### Order List Management (ucDanhSachDon)
- Purpose: Display orders with filtering by status and search by order ID or customer name. Supports quick navigation to order detail and creation of new orders.
- Key behaviors:
  - Populates status filter with predefined states.
  - Loads orders via DonHangDAO with optional keyword and status filters.
  - Formats grid columns for readability and sets selection mode.
  - Emits events for viewing details and creating new orders.

```mermaid
flowchart TD
Start(["Load Order List"]) --> InitFilter["Initialize Status Filter"]
InitFilter --> SearchInput["User enters keyword"]
SearchInput --> ApplyFilters["Apply keyword and status filters"]
ApplyFilters --> CallDAO["Call DonHangDAO.LayDanhSach()"]
CallDAO --> BindGrid["Bind DataGridView"]
BindGrid --> SelectRow{"User selects row?"}
SelectRow --> |Yes| EmitDetail["Emit XemChiTiet(maDon)"]
SelectRow --> |No| WaitAction["Wait for action"]
EmitDetail --> End(["Done"])
WaitAction --> End
```

**Diagram sources**
- [ucDanhSachDon.cs:19-45](file://3_BanHang/ucDanhSachDon.cs#L19-L45)

**Section sources**
- [ucDanhSachDon.cs:1-114](file://3_BanHang/ucDanhSachDon.cs#L1-L114)
- [DonHangDAO.cs:11-42](file://DataAccess/DonHangDAO.cs#L11-L42)

### Order Creation Wizard (ucTaoDon)
- Purpose: Build an order with customer info, select products from the catalog, manage cart, and finalize the order.
- Key behaviors:
  - Product search and grid formatting.
  - Cart management (add/remove items) with stock checks.
  - Customer lookup or creation by phone number.
  - Order creation via stored procedure and item insertion.
  - Optional delivery creation for home-delivery orders.

```mermaid
sequenceDiagram
participant User as "User"
participant UI as "ucTaoDon"
participant DAO_KH as "KhachHangDAO"
participant DAO_SP as "SanPhamDAO"
participant DAO_DH as "DonHangDAO"
participant DAO_GH as "GiaoHangDAO"
participant DB as "Database"
User->>UI : Enter customer info and search products
UI->>DAO_KH : TimTheoSDT(phone)
DAO_KH->>DB : Query customer
DB-->>DAO_KH : Customer or null
alt Customer exists
DAO_KH-->>UI : Existing maKH
else New customer
UI->>DAO_KH : ThemKhachHang(new)
DAO_KH->>DB : Insert customer
DB-->>DAO_KH : New maKH
end
User->>UI : Add items to cart
UI->>DAO_SP : LaySanPhamDangBan(keyword)
DAO_SP->>DB : Query products
DB-->>DAO_SP : Products list
UI->>DAO_DH : ThemChiTiet(maDon, maSP, qty, price)
DAO_DH->>DB : Execute sp_ThemChiTietDon
User->>UI : Confirm order
UI->>DAO_DH : TaoDonHang(maKH, maNV, hinhThuc, note)
DAO_DH->>DB : Execute sp_TaoDonHang
alt Home delivery
UI->>DAO_GH : TaoGiaoHang(maDon)
DAO_GH->>DB : Execute sp_TaoGiaoHang
end
DB-->>DAO_DH : Success
DAO_DH-->>UI : maDon
UI-->>User : Success message and reset
```

**Diagram sources**
- [ucTaoDon.cs:38-152](file://3_BanHang/ucTaoDon.cs#L38-L152)
- [KhachHangDAO.cs:26-46](file://DataAccess/KhachHangDAO.cs#L26-L46)
- [SanPhamDAO.cs:35-47](file://DataAccess/SanPhamDAO.cs#L35-L47)
- [DonHangDAO.cs:66-89](file://DataAccess/DonHangDAO.cs#L66-L89)
- [GiaoHangDAO.cs:54-64](file://DataAccess/GiaoHangDAO.cs#L54-L64)
- [FloriSys_Database.sql:282-315](file://FloriSys_Database.sql#L282-L315)

**Section sources**
- [ucTaoDon.cs:1-162](file://3_BanHang/ucTaoDon.cs#L1-L162)
- [KhachHangDAO.cs:1-75](file://DataAccess/KhachHangDAO.cs#L1-L75)
- [SanPhamDAO.cs:1-96](file://DataAccess/SanPhamDAO.cs#L1-L96)
- [DonHangDAO.cs:1-114](file://DataAccess/DonHangDAO.cs#L1-L114)
- [GiaoHangDAO.cs:1-96](file://DataAccess/GiaoHangDAO.cs#L1-L96)

### Order Detail View (ucChiTietDonHang)
- Purpose: Show order details, items, and allow updating order status.
- Key behaviors:
  - Loads order header, items, and formats grids.
  - Provides a dropdown of valid statuses for updates.
  - Calls stored procedure to update order status and adjust inventory accordingly.

```mermaid
sequenceDiagram
participant UI as "ucChiTietDonHang"
participant DAO_DH as "DonHangDAO"
participant DB as "Database"
UI->>DAO_DH : LayThongTinDon(maDon)
DAO_DH->>DB : SELECT order with joins
DB-->>DAO_DH : Order info
DAO_DH-->>UI : Bind labels and status dropdown
UI->>DAO_DH : LayChiTiet(maDon)
DAO_DH->>DB : SELECT items
DB-->>DAO_DH : Items list
DAO_DH-->>UI : Bind items grid
UI->>DAO_DH : CapNhatTrangThai(maDon, newStatus)
DAO_DH->>DB : Execute sp_CapNhatTrangThaiDon
DB-->>DAO_DH : Inventory adjusted if needed
DAO_DH-->>UI : Refresh info and status
```

**Diagram sources**
- [ucChiTietDonHang.cs:25-115](file://3_BanHang/ucChiTietDonHang.cs#L25-L115)
- [DonHangDAO.cs:53-98](file://DataAccess/DonHangDAO.cs#L53-L98)
- [FloriSys_Database.sql:317-358](file://FloriSys_Database.sql#L317-L358)

**Section sources**
- [ucChiTietDonHang.cs:1-132](file://3_BanHang/ucChiTietDonHang.cs#L1-L132)
- [DonHangDAO.cs:1-114](file://DataAccess/DonHangDAO.cs#L1-L114)

### Business Rules and State Transitions
- Order states: New, Processing, Delivered, Completed, Cancelled, Returned.
- Inventory allocation:
  - Transitioning to Processing triggers stock deduction after validating availability.
  - Returning from Processing/Delivered restores stock.
  - Cancelling from New does not adjust stock.
- Delivery synchronization:
  - Delivery updates propagate order state changes to the parent order.

```mermaid
stateDiagram-v2
[*] --> New
New --> Processing : "Create order"
Processing --> Delivered : "Delivery success"
Delivered --> Completed : "Customer confirmed"
Processing --> Returned : "Return processed"
New --> Cancelled : "Cancellation"
Returned --> Completed : "Refund/Replacement"
```

**Diagram sources**
- [DonHang.cs:27-42](file://Models/DonHang.cs#L27-L42)
- [FloriSys_Database.sql:64-74](file://FloriSys_Database.sql#L64-L74)
- [FloriSys_Database.sql:317-358](file://FloriSys_Database.sql#L317-L358)
- [fix_sp.sql:1-35](file://fix_sp.sql#L1-L35)
- [fix_sp2.sql:1-34](file://fix_sp2.sql#L1-L34)

**Section sources**
- [DonHang.cs:1-63](file://Models/DonHang.cs#L1-L63)
- [FloriSys_Database.sql:64-74](file://FloriSys_Database.sql#L64-L74)
- [FloriSys_Database.sql:317-358](file://FloriSys_Database.sql#L317-L358)
- [fix_sp.sql:1-35](file://fix_sp.sql#L1-L35)
- [fix_sp2.sql:1-34](file://fix_sp2.sql#L1-L34)

### Payment Processing and Inventory Allocation
- Payment processing is not modeled in the provided code; payments are handled externally and reflected in order totals.
- Inventory allocation:
  - Adding items enforces stock availability via stored procedure.
  - Transitioning to Processing deducts stock; Returning restores stock.

**Section sources**
- [SanPhamDAO.cs:35-47](file://DataAccess/SanPhamDAO.cs#L35-L47)
- [DonHangDAO.cs:80-89](file://DataAccess/DonHangDAO.cs#L80-L89)
- [FloriSys_Database.sql:296-315](file://FloriSys_Database.sql#L296-L315)
- [FloriSys_Database.sql:317-358](file://FloriSys_Database.sql#L317-L358)

### Integration with Customer Database, Product Catalog, and Delivery Systems
- Customer database: Lookup by phone number; creation of new customers.
- Product catalog: Active product listing with stock visibility; stock checks during order creation.
- Delivery system: Home-delivery orders automatically create delivery records; delivery updates synchronize order state.

**Section sources**
- [KhachHangDAO.cs:26-46](file://DataAccess/KhachHangDAO.cs#L26-L46)
- [SanPhamDAO.cs:35-47](file://DataAccess/SanPhamDAO.cs#L35-L47)
- [GiaoHangDAO.cs:54-83](file://DataAccess/GiaoHangDAO.cs#L54-L83)
- [ucTaoDon.cs:138-141](file://3_BanHang/ucTaoDon.cs#L138-L141)

## Dependency Analysis
- UI components depend on DAOs for data operations.
- DAOs depend on the database schema and stored procedures.
- Models encapsulate domain data and computed display properties.
- SessionManager supplies current user context for order creation and dashboards.

```mermaid
graph LR
UC_List["ucDanhSachDon"] --> DAO_DH["DonHangDAO"]
UC_Create["ucTaoDon"] --> DAO_DH
UC_Create --> DAO_KH["KhachHangDAO"]
UC_Create --> DAO_SP["SanPhamDAO"]
UC_Create --> DAO_GH["GiaoHangDAO"]
UC_Detail["ucChiTietDonHang"] --> DAO_DH
UC_Dashboard["ucDashboardBanHang"] --> DAO_DH
DAO_DH --> Model_DH["DonHang.cs"]
DAO_KH --> Model_KH["KhachHang.cs"]
DAO_SP --> Model_SP["SanPham.cs"]
DAO_DH --> DB["FloriSys_Database.sql"]
DAO_KH --> DB
DAO_SP --> DB
DAO_GH --> DB
```

**Diagram sources**
- [ucDanhSachDon.cs:1-114](file://3_BanHang/ucDanhSachDon.cs#L1-L114)
- [ucTaoDon.cs:1-162](file://3_BanHang/ucTaoDon.cs#L1-L162)
- [ucChiTietDonHang.cs:1-132](file://3_BanHang/ucChiTietDonHang.cs#L1-L132)
- [ucDashboardBanHang.cs:1-85](file://3_BanHang/ucDashboardBanHang.cs#L1-L85)
- [DonHangDAO.cs:1-114](file://DataAccess/DonHangDAO.cs#L1-L114)
- [KhachHangDAO.cs:1-75](file://DataAccess/KhachHangDAO.cs#L1-L75)
- [SanPhamDAO.cs:1-96](file://DataAccess/SanPhamDAO.cs#L1-L96)
- [GiaoHangDAO.cs:1-96](file://DataAccess/GiaoHangDAO.cs#L1-L96)
- [DonHang.cs:1-63](file://Models/DonHang.cs#L1-L63)
- [KhachHang.cs:1-18](file://Models/KhachHang.cs#L1-L18)
- [SanPham.cs:1-42](file://Models/SanPham.cs#L1-L42)
- [FloriSys_Database.sql:64-101](file://FloriSys_Database.sql#L64-L101)

**Section sources**
- [ucDanhSachDon.cs:1-114](file://3_BanHang/ucDanhSachDon.cs#L1-L114)
- [ucTaoDon.cs:1-162](file://3_BanHang/ucTaoDon.cs#L1-L162)
- [ucChiTietDonHang.cs:1-132](file://3_BanHang/ucChiTietDonHang.cs#L1-L132)
- [ucDashboardBanHang.cs:1-85](file://3_BanHang/ucDashboardBanHang.cs#L1-L85)
- [DonHangDAO.cs:1-114](file://DataAccess/DonHangDAO.cs#L1-L114)
- [KhachHangDAO.cs:1-75](file://DataAccess/KhachHangDAO.cs#L1-L75)
- [SanPhamDAO.cs:1-96](file://DataAccess/SanPhamDAO.cs#L1-L96)
- [GiaoHangDAO.cs:1-96](file://DataAccess/GiaoHangDAO.cs#L1-L96)
- [DonHang.cs:1-63](file://Models/DonHang.cs#L1-L63)
- [KhachHang.cs:1-18](file://Models/KhachHang.cs#L1-L18)
- [SanPham.cs:1-42](file://Models/SanPham.cs#L1-L42)
- [FloriSys_Database.sql:64-101](file://FloriSys_Database.sql#L64-L101)

## Performance Considerations
- Prefer server-side filtering and sorting via DAO methods to reduce client-side overhead.
- Batch operations: Use bulk insert procedures for order items when extending functionality.
- Indexes: Ensure appropriate indexes on frequently filtered columns (order date, status, customer phone).
- UI responsiveness: Perform long-running operations asynchronously to keep the UI responsive.

## Troubleshooting Guide
- Common errors and resolutions:
  - Insufficient stock when adding items: Ensure product stock is sufficient before adding to cart; validate against product catalog.
  - Customer deletion blocked due to orders: Cannot delete customers who have placed orders; archive or merge records instead.
  - Order status update failures: Verify the target state is valid and that inventory adjustments are permitted.
  - Delivery synchronization issues: Confirm delivery updates trigger corresponding order state changes.

**Section sources**
- [SanPhamDAO.cs:35-47](file://DataAccess/SanPhamDAO.cs#L35-L47)
- [KhachHangDAO.cs:62-72](file://DataAccess/KhachHangDAO.cs#L62-L72)
- [DonHangDAO.cs:91-98](file://DataAccess/DonHangDAO.cs#L91-L98)
- [fix_sp.sql:1-35](file://fix_sp.sql#L1-L35)
- [fix_sp2.sql:1-34](file://fix_sp2.sql#L1-L34)

## Conclusion
The FloriSys Sales Management Module provides a robust foundation for order management with clear separation of concerns, enforced business rules at the database level, and intuitive UI components for sales staff. By following the documented workflows and best practices, teams can efficiently manage the order lifecycle, maintain accurate inventory, and provide reliable customer experiences.

## Appendices

### Step-by-Step Workflows

- Creating a new order:
  1. Open the order creation wizard.
  2. Search and select products; add to cart ensuring stock availability.
  3. Enter customer information; lookup by phone or create new customer.
  4. Choose pickup or home-delivery option.
  5. Confirm order; system creates order and items, and optionally a delivery record.
  6. Notify warehouse to prepare goods.

- Modifying an existing order:
  1. Navigate to order detail view.
  2. Update order status using the status dropdown.
  3. System adjusts inventory if transitioning to processing or handles returns appropriately.

- Handling order cancellations:
  1. From order detail, set status to cancelled.
  2. System ensures no inventory adjustment is applied for cancellations from new.

**Section sources**
- [ucTaoDon.cs:102-152](file://3_BanHang/ucTaoDon.cs#L102-L152)
- [ucChiTietDonHang.cs:101-115](file://3_BanHang/ucChiTietDonHang.cs#L101-L115)
- [DonHangDAO.cs:91-98](file://DataAccess/DonHangDAO.cs#L91-L98)
- [FloriSys_Database.sql:317-358](file://FloriSys_Database.sql#L317-L358)

### Order History Tracking and Search
- Order list supports filtering by status and keyword search on order ID or customer name.
- Dashboard shows recent orders for the logged-in salesperson.

**Section sources**
- [ucDanhSachDon.cs:28-45](file://3_BanHang/ucDanhSachDon.cs#L28-L45)
- [ucDashboardBanHang.cs:48-62](file://3_BanHang/ucDashboardBanHang.cs#L48-L62)

### Bulk Operations
- Bulk operations are not currently implemented in the provided code. Consider implementing batch creation or status updates via stored procedures and DAO extensions.

[No sources needed since this section provides general guidance]