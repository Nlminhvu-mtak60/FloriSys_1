
ALTER PROCEDURE sp_CapNhatTrangThaiGiao
    @MaGiaoHang NVARCHAR(20),
    @TrangThai  NVARCHAR(20),
    @GhiChu     NVARCHAR(500) = NULL
AS
BEGIN
    -- Cập nhật bảng GIAO_HANG và chốt NgayGiao nếu thành công
    UPDATE GIAO_HANG 
    SET TrangThai = @TrangThai, 
        GhiChuGiaoHang = ISNULL(@GhiChu, GhiChuGiaoHang),
        NgayGiao = CASE WHEN @TrangThai = 'GiaoThanhCong' THEN GETDATE() ELSE NgayGiao END
    WHERE MaGiaoHang = @MaGiaoHang;

    -- Đồng bộ trạng thái về bảng mẹ DON_HANG để Cửa hàng theo dõi
    IF @TrangThai = 'GiaoThanhCong'
    BEGIN
        UPDATE DON_HANG 
        SET TrangThai = 'DaGiao' 
        WHERE MaDon = (SELECT MaDon FROM GIAO_HANG WHERE MaGiaoHang = @MaGiaoHang);
    END
    ELSE IF @TrangThai = 'HoanHang'
    BEGIN
        UPDATE DON_HANG 
        SET TrangThai = 'HoanHang' 
        WHERE MaDon = (SELECT MaDon FROM GIAO_HANG WHERE MaGiaoHang = @MaGiaoHang);
    END
    ELSE IF @TrangThai = 'GiaoLai' OR @TrangThai = 'DangGiao'
    BEGIN
        UPDATE DON_HANG 
        SET TrangThai = 'DangGiao' 
        WHERE MaDon = (SELECT MaDon FROM GIAO_HANG WHERE MaGiaoHang = @MaGiaoHang);
    END
END;

