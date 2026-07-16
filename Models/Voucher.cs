using System.ComponentModel.DataAnnotations;

namespace SPASMART.Models
{
    public class Voucher
    {
        [Key]
        public int MaVoucher { get; set; }

        public string TenVoucher { get; set; } = string.Empty;

        public string MaCode { get; set; } = string.Empty;

        public decimal GiaTriGiam { get; set; }

        public DateTime NgayBatDau { get; set; }

        public DateTime NgayKetThuc { get; set; }

        public string TrangThai { get; set; } = "Còn hiệu lực";

        public HoaDon? HoaDon { get; set; }
    }
}