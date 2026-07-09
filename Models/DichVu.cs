using System.ComponentModel.DataAnnotations;

namespace SPASMART.Models
{
    public class DichVu
    {
        [Key]
        public int MaDichVu { get; set; }

        public string TenDichVu { get; set; } = string.Empty;

        public string MoTa { get; set; } = string.Empty;

        public decimal Gia { get; set; }

        public int ThoiLuong { get; set; }

        public string TrangThai { get; set; } = "Đang cung cấp";

        public ICollection<LichHen>? LichHens { get; set; }

        public ICollection<ChiTietHoaDon>? ChiTietHoaDons { get; set; }
    }
}