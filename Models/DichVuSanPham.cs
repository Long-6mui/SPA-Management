using System.ComponentModel.DataAnnotations;

namespace SPASMART.Models
{
    public class DichVuSanPham
    {
        [Key]
        public int MaDVSP { get; set; }

        public string TenDVSP { get; set; } = string.Empty;

        public string Loai { get; set; } = string.Empty;
        // Dịch vụ hoặc Sản phẩm

        public string? MoTa { get; set; }

        public decimal Gia { get; set; }

        public int? ThoiLuong { get; set; }

        public DateTime? HanSuDung { get; set; }

        public string? DonViTinh { get; set; }

        public int? SoLuongTon { get; set; }

        public string? HinhAnh { get; set; }

        public string TrangThai { get; set; } = "Đang cung cấp";

        public ICollection<LichHen>? LichHens { get; set; }

        public ICollection<ChiTietHoaDon>? ChiTietHoaDons { get; set; }

        public ICollection<ChiTietPhieuNhap>? ChiTietPhieuNhaps { get; set; }
    }
}