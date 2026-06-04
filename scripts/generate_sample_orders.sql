
-- SCRIPT TẠO DỮ LIỆU MẪU ĐƠN HÀNG (01/01/2026 - 14/05/2026)
-- Mỗi ngày ~5 đơn, phân bổ đều cho nhân viên Cashier (NV002, NV005, NV006)

SET NOCOUNT ON;

DECLARE @StartDate DATE = '2026-01-01';
DECLARE @EndDate DATE = '2026-05-14';
DECLARE @CurrentDate DATE = @StartDate;

-- Danh sách ID cần thiết
DECLARE @Cashiers TABLE (ID NVARCHAR(20));
INSERT INTO @Cashiers VALUES ('NV000002'), ('NV000005'), ('NV000006');

DECLARE @Customers TABLE (ID NVARCHAR(20));
INSERT INTO @Customers SELECT MaKH FROM KHACH_HANG;

DECLARE @Products TABLE (ID NVARCHAR(20), Price DECIMAL(18,2));
INSERT INTO @Products SELECT MaSP, GiaBan FROM SAN_PHAM WHERE TrangThai = N'DangBan';

WHILE @CurrentDate <= @EndDate
BEGIN
    DECLARE @OrderCount INT = 0;
    WHILE @OrderCount < 5
    BEGIN
        -- 1. Sinh mã đơn hàng mới
        DECLARE @MaDon NVARCHAR(20);
        EXEC sp_SinhMa 'DH', 'DON_HANG', 'MaDon', @MaDon OUTPUT;

        -- 2. Chọn ngẫu nhiên Khách hàng và Nhân viên
        DECLARE @MaKH NVARCHAR(20) = (SELECT TOP 1 ID FROM @Customers ORDER BY NEWID());
        DECLARE @MaNV NVARCHAR(20) = (SELECT TOP 1 ID FROM @Cashiers ORDER BY NEWID());
        
        -- 3. Quyết định Trạng thái
        DECLARE @TrangThai NVARCHAR(20) = N'HoanThanh';
        IF @CurrentDate >= '2026-05-11'
        BEGIN
            -- Sau ngày 11/05 thì trộn lẫn Hoàn thành và Mới/Đang xử lý
            DECLARE @Rand FLOAT = RAND();
            IF @Rand > 0.6 SET @TrangThai = N'Moi';
            ELSE IF @Rand > 0.3 SET @TrangThai = N'DangXuLy';
        END

        -- 4. Chèn vào DON_HANG (Dùng giờ ngẫu nhiên trong ngày)
        DECLARE @FullDate DATETIME = CAST(@CurrentDate AS DATETIME) + DATEADD(SECOND, RAND() * 86400, 0);
        
        INSERT INTO DON_HANG (MaDon, MaKH, MaNV_TaoDon, NgayTao, TongTien, TrangThai, HinhThucNhanHang, GhiChu)
        VALUES (@MaDon, @MaKH, @MaNV, @FullDate, 0, @TrangThai, N'TaiQuay', N'Dữ liệu mẫu hệ thống');

        -- 5. Chèn CHI_TIET_DON_HANG (1-3 sản phẩm khác nhau mỗi đơn)
        DECLARE @ItemCount INT = CAST(RAND() * 3 + 1 AS INT);
        DECLARE @i INT = 0;
        DECLARE @Total DECIMAL(18,2) = 0;
        
        -- Dùng bảng tạm để lưu các SP đã chọn cho đơn này
        DECLARE @SelectedProducts TABLE (MaSP NVARCHAR(20), Price DECIMAL(18,2));
        DELETE FROM @SelectedProducts;
        INSERT INTO @SelectedProducts SELECT TOP (@ItemCount) ID, Price FROM @Products ORDER BY NEWID();

        INSERT INTO CHI_TIET_DON_HANG (MaDon, MaSP, SoLuong, DonGia, ThanhTien)
        SELECT @MaDon, MaSP, 1, Price, Price FROM @SelectedProducts;
        
        SELECT @Total = SUM(Price) FROM @SelectedProducts;

        -- 6. Cập nhật lại tổng tiền đơn hàng
        UPDATE DON_HANG SET TongTien = @Total WHERE MaDon = @MaDon;

        -- 7. Nếu đơn là Hoàn thành, tạo thêm Giao hàng
        IF @TrangThai = N'HoanThanh'
        BEGIN
            DECLARE @MaGH NVARCHAR(20);
            EXEC sp_SinhMa 'GH', 'GIAO_HANG', 'MaGiaoHang', @MaGH OUTPUT;

            INSERT INTO GIAO_HANG (MaGiaoHang, MaDon, MaNV_Shipper, TrangThai, NgayGiao, GhiChuGiaoHang)
            VALUES (@MaGH, @MaDon, (SELECT TOP 1 MaNV FROM NHAN_VIEN WHERE ChucVu = N'Shipper' ORDER BY NEWID()), N'GiaoThanhCong', @FullDate, N'Giao hàng hoàn tất');
        END

        SET @OrderCount = @OrderCount + 1;
    END

    SET @CurrentDate = DATEADD(DAY, 1, @CurrentDate);
END
