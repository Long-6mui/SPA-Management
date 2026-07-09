using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPASMART.Models
{
    public class TaiKhoan
    {
        [Key]
        public int MaTaiKhoan { get; set; }

        public string TenDangNhap { get; set; } = string.Empty;

        public string MatKhau { get; set; } = string.Empty;

        public string TrangThai { get; set; } = "Hoạt động";

        public int MaVaiTro { get; set; }

        [ForeignKey("MaVaiTro")]
        public VaiTro? VaiTro { get; set; }

        public NhanVien? NhanVien { get; set; }

        public KhachHang? KhachHang { get; set; }
    }
}