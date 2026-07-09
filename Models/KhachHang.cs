using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPASMART.Models
{
    public class KhachHang
    {
        [Key]
        public int MaKhachHang { get; set; }

        public string HoTenKhach { get; set; } = string.Empty;

        public string SoDienThoaiKhach { get; set; } = string.Empty;

        public string EmailKhach { get; set; } = string.Empty;

        public string GioiTinh { get; set; } = string.Empty;

        public DateTime? NgaySinh { get; set; }

        public int? MaTaiKhoan { get; set; }

        [ForeignKey("MaTaiKhoan")]
        public TaiKhoan? TaiKhoan { get; set; }

        public ICollection<LichHen>? LichHens { get; set; }

        public ICollection<Voucher>? Vouchers { get; set; }
    }
}