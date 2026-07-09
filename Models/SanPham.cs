using System.ComponentModel.DataAnnotations;

namespace SPASMART.Models
{
    public class SanPham
    {
        [Key]
        public int MaSanPham { get; set; }

        public string TenSanPham { get; set; } = string.Empty;

        public string LoaiSanPham { get; set; } = string.Empty;

        public DateTime? HanSuDung { get; set; }

        public string DonViTinh { get; set; } = string.Empty;

        public int SoLuongTon { get; set; }

        public ICollection<ChiTietPhieuNhap>? ChiTietPhieuNhaps { get; set; }
    }
}