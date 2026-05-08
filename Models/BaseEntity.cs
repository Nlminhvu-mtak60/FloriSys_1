using System;

namespace FloriSys.Models
{
    /// <summary>
    /// Lớp cơ sở trừu tượng cho tất cả các mô hình thực thể.
    /// Minh họa: TÍNH TRỪU TƯỢNG (lớp trừu tượng, thuộc tính trừu tượng),
    /// TÍNH ĐA HÌNH (các phương thức ảo mà lớp con có thể ghi đè),
    /// TÍNH ĐÓNG GÓI (logic kiểm tra tính hợp lệ được đóng gói ngay trong model).
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Mỗi thực thể cung cấp một chuỗi văn bản hiển thị dễ đọc (ví dụ: Tên hoa, Tên NV).
        /// TÍNH TRỪU TƯỢNG: các lớp con BẮT BUỘC phải triển khai thuộc tính này.
        /// </summary>
        public abstract string DisplayText { get; }

        /// <summary>
        /// Logic kiểm tra tính hợp lệ chung.
        /// TÍNH ĐA HÌNH: là thuộc tính ảo nên lớp con có thể ghi đè bằng các quy tắc tùy chỉnh.
        /// </summary>
        public virtual bool IsValid => !string.IsNullOrEmpty(DisplayText);

        /// <summary>
        /// Trả về giá trị khóa chính của thực thể.
        /// TÍNH TRỪU TƯỢNG: mỗi thực thể tự biết mã định danh (ID) của chính mình.
        /// </summary>
        public abstract string Id { get; }
    }
}
