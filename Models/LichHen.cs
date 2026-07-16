using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPASMART.Models
{
    public class LichHen
    {
        [Key]
        public int MaLichHen { get; set; }

        public DateTime NgayHen { get; set; }

        public TimeSpan GioHen { get; set; }

        public string TrangThai { get; set; } = "Chờ xác nhận";

        public string? GhiChu { get; set; }

        public int MaKhachHang { get; set; }

        [ForeignKey("MaKhachHang")]
        public KhachHang? KhachHang { get; set; }

        public int MaDVSP { get; set; }

        [ForeignKey("MaDVSP")]
        public DichVuSanPham? DichVuSanPham { get; set; }

        public int? MaNhanVien { get; set; }

        [ForeignKey("MaNhanVien")]
        public NhanVien? NhanVien { get; set; }

        public HoaDon? HoaDon { get; set; }
    }
}