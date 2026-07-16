using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPASMART.Models
{
    public class ChiTietPhieuNhap
    {
        [Key]
        public int MaChiTietPhieuNhap { get; set; }

        public int SoLuong { get; set; }

        public decimal DonGia { get; set; }

        public decimal ThanhTien { get; set; }

        public int MaPhieuNhap { get; set; }

        [ForeignKey("MaPhieuNhap")]
        public PhieuNhap? PhieuNhap { get; set; }

        public int MaDVSP { get; set; }

        [ForeignKey("MaDVSP")]
        public DichVuSanPham? DichVuSanPham { get; set; }
    }
}