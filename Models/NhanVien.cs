using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPASMART.Models
{
    public class NhanVien
    {
        [Key]
        public int MaNhanVien { get; set; }

        public string HoTen { get; set; } = string.Empty;

        public string SoDienThoai { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string ChucVu { get; set; } = string.Empty;

        public string TrangThai { get; set; } = "Đang làm";

        public int? MaTaiKhoan { get; set; }

        [ForeignKey("MaTaiKhoan")]
        public TaiKhoan? TaiKhoan { get; set; }

        public ICollection<LichHen>? LichHens { get; set; }

        public ICollection<PhieuNhap>? PhieuNhaps { get; set; }
    }
}