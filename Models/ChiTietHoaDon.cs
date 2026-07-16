using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPASMART.Models
{
    public class ChiTietHoaDon
    {
        [Key]
        public int MaChiTietHoaDon { get; set; }

        public int SoLuong { get; set; }

        public decimal DonGia { get; set; }

        public decimal ThanhTien { get; set; }

        public int MaHoaDon { get; set; }

        [ForeignKey("MaHoaDon")]
        public HoaDon? HoaDon { get; set; }

        public int MaDVSP { get; set; }

        [ForeignKey("MaDVSP")]
        public DichVuSanPham? DichVuSanPham { get; set; }
    }
}