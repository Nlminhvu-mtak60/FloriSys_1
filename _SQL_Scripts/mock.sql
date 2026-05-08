DECLARE @StartDate DATE = '2026-03-01';
DECLARE @EndDate DATE = '2026-04-19';

DECLARE @MaKH NVARCHAR(20) = (SELECT TOP 1 MaKH FROM KHACH_HANG);
DECLARE @MaNV NVARCHAR(20) = (SELECT TOP 1 MaNV FROM NHAN_VIEN WHERE ChucVu IN ('Cashier', 'Admin'));
DECLARE @MaSP NVARCHAR(20) = (SELECT TOP 1 MaSP FROM SAN_PHAM);
DECLARE @GiaBan DECIMAL(18,2) = (SELECT TOP 1 ISNULL(GiaBan, 100000) FROM SAN_PHAM WHERE MaSP = @MaSP);

DECLARE @Counter INT = (SELECT ISNULL(MAX(CAST(SUBSTRING(MaDon, 3, 6) AS INT)), 0) FROM DON_HANG);
DECLARE @GHCounter INT = (SELECT ISNULL(MAX(CAST(SUBSTRING(MaGiaoHang, 3, 6) AS INT)), 0) FROM GIAO_HANG);

WHILE @StartDate <= @EndDate
BEGIN
    DECLARE @i INT = 0;
    DECLARE @StartMinute INT = 480; -- Bắt đầu từ 8:00 sáng
    WHILE @i < 20
    BEGIN
        -- Tịnh tiến thời gian lên 10-30 phút cho mỗi đơn để đảm bảo tăng dần
        SET @StartMinute = @StartMinute + (ABS(CHECKSUM(NEWID())) % 21) + 10;
        
        SET @Counter = @Counter + 1;
        DECLARE @MaDon NVARCHAR(20) = 'DH' + RIGHT('000000' + CAST(@Counter AS NVARCHAR), 6);
        
        DECLARE @TrangThai NVARCHAR(20);
        DECLARE @Rand INT = ABS(CHECKSUM(NEWID())) % 4;
        IF @Rand = 0 SET @TrangThai = 'Moi';
        IF @Rand = 1 SET @TrangThai = 'DangXuLy';
        IF @Rand = 2 SET @TrangThai = 'HoanThanh';
        IF @Rand = 3 SET @TrangThai = 'DaGiao';
        
        DECLARE @ThoiGian DATETIME = DATEADD(minute, @StartMinute, CAST(@StartDate AS DATETIME));

        DECLARE @SoLuong INT = (ABS(CHECKSUM(NEWID())) % 5) + 1;
        DECLARE @TongTien DECIMAL(18,2) = @SoLuong * @GiaBan;

        INSERT INTO DON_HANG (MaDon, NgayTao, MaKH, MaNV_TaoDon, HinhThucNhanHang, TrangThai, TongTien, GhiChu)
        VALUES (@MaDon, @ThoiGian, @MaKH, @MaNV, 'GiaoTanNoi', @TrangThai, @TongTien, '');
        
        INSERT INTO CHI_TIET_DON_HANG (MaDon, MaSP, SoLuong, DonGia, ThanhTien)
        VALUES (@MaDon, @MaSP, @SoLuong, @GiaBan, @TongTien);

        -- Tạo bảng phân công giao hàng cho các đơn xử lý hoặc đã giao
        IF @Rand IN (1, 2, 3) 
        BEGIN
            DECLARE @MaShipper NVARCHAR(20) = (SELECT TOP 1 MaNV FROM NHAN_VIEN WHERE ChucVu = 'Shipper');
            IF @MaShipper IS NOT NULL
            BEGIN
                SET @GHCounter = @GHCounter + 1;
                DECLARE @MaGH NVARCHAR(20) = 'GH' + RIGHT('000000' + CAST(@GHCounter AS NVARCHAR), 6);
                
                DECLARE @TrangThaiGH NVARCHAR(20) = 'ChoPhanCong';
                IF @Rand = 2 SET @TrangThaiGH = 'DangGiao';
                IF @Rand = 3 SET @TrangThaiGH = 'GiaoThanhCong';
                
                INSERT INTO GIAO_HANG (MaGiaoHang, MaDon, MaNV_Shipper, NgayGiao, TrangThai)
                VALUES (@MaGH, @MaDon, @MaShipper, @ThoiGian, @TrangThaiGH);
            END
        END

        SET @i = @i + 1;
    END

    SET @StartDate = DATEADD(day, 1, @StartDate);
END
PRINT 'Success';
