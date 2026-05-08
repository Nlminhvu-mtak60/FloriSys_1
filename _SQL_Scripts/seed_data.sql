[ignoring loop detection]
USE FloriSys;
GO

-- 1. THÊM PHIẾU NHẬP KHO (Để có hàng bán)
DECLARE @MaNV_Kho NVARCHAR(20) = (SELECT TOP 1 MaNV FROM NHAN_VIEN WHERE ChucVu = 'Warehouse');
IF @MaNV_Kho IS NULL SET @MaNV_Kho = 'NV003';

IF NOT EXISTS (SELECT 1 FROM PHIEU_NHAP_KHO WHERE MaPhieu = 'PN000002')
BEGIN
    INSERT INTO PHIEU_NHAP_KHO (MaPhieu, NgayNhap, MaNV, GhiChu) VALUES
    ('PN000002', '2026-01-05 08:00', @MaNV_Kho, N'Nhập hàng đầu năm'),
    ('PN000003', '2026-03-15 09:00', @MaNV_Kho, N'Nhập hàng bổ sung tháng 3'),
    ('PN000004', '2026-04-25 14:00', @MaNV_Kho, N'Nhập hàng chuẩn bị lễ');

    INSERT INTO CT_NHAP_KHO (MaPhieu, MaSP, SoLuong, GiaNhap)
    SELECT 'PN000002', MaSP, 100, GiaNhap FROM SAN_PHAM;
    
    INSERT INTO CT_NHAP_KHO (MaPhieu, MaSP, SoLuong, GiaNhap)
    SELECT 'PN000003', MaSP, 50, GiaNhap FROM SAN_PHAM WHERE MaSP LIKE '%1%';
END

-- 2. TẠO ĐƠN HÀNG VÀ CHIA ĐỀU CHO NHÂN VIÊN
DECLARE @StartDate DATETIME = '2026-01-10';
DECLARE @EndDate DATETIME = '2026-05-07'; -- Trước ngày 8/5
DECLARE @CurrentOrderNum INT = (SELECT ISNULL(MAX(CAST(SUBSTRING(MaDon, 3, 6) AS INT)), 0) FROM DON_HANG);
DECLARE @GHNum INT = (SELECT ISNULL(MAX(CAST(SUBSTRING(MaGiaoHang, 3, 6) AS INT)), 0) FROM GIAO_HANG);

-- Lấy danh sách nhân viên vào bảng tạm để chia đều
SELECT MaNV, ROW_NUMBER() OVER (ORDER BY MaNV) as ID INTO #Cashiers FROM NHAN_VIEN WHERE ChucVu IN ('Cashier', 'Admin');
SELECT MaNV, ROW_NUMBER() OVER (ORDER BY MaNV) as ID INTO #Shippers FROM NHAN_VIEN WHERE ChucVu = 'Shipper';

DECLARE @NumCashiers INT = (SELECT COUNT(*) FROM #Cashiers);
DECLARE @NumShippers INT = (SELECT COUNT(*) FROM #Shippers);

IF @NumCashiers > 0 AND @NumShippers > 0
BEGIN
    DECLARE @i INT = 1;
    DECLARE @TotalOrders INT = 120; 

    WHILE @i <= @TotalOrders
    BEGIN
        DECLARE @OrderDate DATETIME = DATEADD(SECOND, ABS(CHECKSUM(NEWID())) % DATEDIFF(SECOND, @StartDate, @EndDate), @StartDate);
        
        SET @CurrentOrderNum = @CurrentOrderNum + 1;
        DECLARE @MaDon NVARCHAR(20) = 'DH' + RIGHT('000000' + CAST(@CurrentOrderNum AS NVARCHAR), 6);
        
        DECLARE @CashierID INT = (@i % @NumCashiers) + 1;
        DECLARE @MaNV_Tao NVARCHAR(20) = (SELECT MaNV FROM #Cashiers WHERE ID = @CashierID);
        
        DECLARE @MaKH NVARCHAR(20) = (SELECT TOP 1 MaKH FROM KHACH_HANG ORDER BY NEWID());
        DECLARE @TrangThaiDon NVARCHAR(20) = CASE WHEN (@i % 10 = 0) THEN N'HoanHang' ELSE N'HoanThanh' END;

        INSERT INTO DON_HANG (MaDon, NgayTao, MaKH, MaNV_TaoDon, HinhThucNhanHang, TrangThai, TongTien)
        VALUES (@MaDon, @OrderDate, @MaKH, @MaNV_Tao, N'GiaoTanNoi', @TrangThaiDon, 0);

        INSERT INTO CHI_TIET_DON_HANG (MaDon, MaSP, SoLuong, DonGia)
        SELECT TOP 1 @MaDon, MaSP, (1 + ABS(CHECKSUM(NEWID())) % 2), GiaBan 
        FROM SAN_PHAM ORDER BY NEWID();

        UPDATE DON_HANG SET TongTien = (SELECT SUM(ThanhTien) FROM CHI_TIET_DON_HANG WHERE MaDon = @MaDon) WHERE MaDon = @MaDon;

        SET @GHNum = @GHNum + 1;
        DECLARE @MaGH NVARCHAR(20) = 'GH' + RIGHT('000000' + CAST(@GHNum AS NVARCHAR), 6);
        DECLARE @ShipperID INT = (@i % @NumShippers) + 1;
        DECLARE @MaNV_Ship NVARCHAR(20) = (SELECT MaNV FROM #Shippers WHERE ID = @ShipperID);
        
        INSERT INTO GIAO_HANG (MaGiaoHang, MaDon, MaNV_Shipper, NgayGiao, TrangThai)
        VALUES (@MaGH, @MaDon, @MaNV_Ship, DATEADD(MINUTE, 30 + (ABS(CHECKSUM(NEWID())) % 120), @OrderDate), 
                CASE WHEN @TrangThaiDon = N'HoanHang' THEN N'HoanHang' ELSE N'GiaoThanhCong' END);

        SET @i = @i + 1;
    END
END

DROP TABLE #Cashiers;
DROP TABLE #Shippers;

PRINT N'Done';
GO
