using System;

namespace FloriSys.Services
{
    public class ReceiverInfo
    {
        public string TenNhan { get; set; } = "";
        public string SdtNhan { get; set; } = "";
        public string DiaChiNhan { get; set; } = "";
        public string GhiChuRutGon { get; set; } = "";
    }

    public static class OrderParser
    {
        /// <summary>
        /// Phân tích thông tin người nhận và địa chỉ từ chuỗi ghi chú định dạng [Giao cho: Tên - SĐT - Địa chỉ]
        /// </summary>
        public static ReceiverInfo ParseReceiverInfo(string ghiChu, string diaChiMacDinh)
        {
            var info = new ReceiverInfo
            {
                DiaChiNhan = diaChiMacDinh ?? "",
                GhiChuRutGon = ghiChu ?? ""
            };

            string rawGhiChu = ghiChu ?? "";
            if (rawGhiChu.StartsWith("[Giao cho:"))
            {
                int closeIdx = rawGhiChu.IndexOf("]");
                if (closeIdx > 0)
                {
                    string receiverInfo = rawGhiChu.Substring(11, closeIdx - 11);
                    string[] parts = receiverInfo.Split(new string[] { " - " }, StringSplitOptions.None);
                    if (parts.Length >= 2)
                    {
                        info.TenNhan = parts[0].Trim();
                        info.SdtNhan = parts[1].Trim();
                        if (parts.Length >= 3) info.DiaChiNhan = parts[2].Trim();
                        info.GhiChuRutGon = rawGhiChu.Substring(closeIdx + 1).Trim();
                    }
                }
            }

            return info;
        }
    }
}
