using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPASMART.Models
{
    public class HoaDon
    {
        [Key]
        public int MaHoaDon { get; set; }

        public DateTime NgayLap { get; set; }

        public decimal TongTien { get; set; }

        public decimal GiamGia { get; set; }

        public decimal ThanhTien { get; set; }

        public string? HinhThucThanhToan { get; set; }

        public decimal SoTienDaThanhToan { get; set; }

        public DateTime? NgayThanhToan { get; set; }

        public string TrangThai { get; set; } = "Chưa thanh toán";

        public int MaLichHen { get; set; }

        [ForeignKey("MaLichHen")]
        public LichHen? LichHen { get; set; }

        public int? MaVoucher { get; set; }

        [ForeignKey("MaVoucher")]
        public Voucher? Voucher { get; set; }

        public ICollection<ChiTietHoaDon>? ChiTietHoaDons { get; set; }
    }
}