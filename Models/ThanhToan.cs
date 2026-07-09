using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPASMART.Models
{
    public class ThanhToan
    {
        [Key]
        public int MaThanhToan { get; set; }

        public string HinhThucThanhToan { get; set; } = string.Empty;

        public decimal SoTien { get; set; }

        public DateTime NgayThanhToan { get; set; }

        public string TrangThai { get; set; } = "Thành công";

        public int MaHoaDon { get; set; }

        [ForeignKey("MaHoaDon")]
        public HoaDon? HoaDon { get; set; }
    }
}