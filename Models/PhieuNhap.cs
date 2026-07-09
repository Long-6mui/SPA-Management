using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPASMART.Models
{
    public class PhieuNhap
    {
        [Key]
        public int MaPhieuNhap { get; set; }

        public DateTime NgayNhap { get; set; }

        public decimal TongTien { get; set; }

        public int MaNhanVien { get; set; }

        [ForeignKey("MaNhanVien")]
        public NhanVien? NhanVien { get; set; }

        public ICollection<ChiTietPhieuNhap>? ChiTietPhieuNhaps { get; set; }
    }
}