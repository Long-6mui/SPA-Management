using System.ComponentModel.DataAnnotations;

namespace SPASMART.Models
{
    public class VaiTro
    {
        [Key]
        public int MaVaiTro { get; set; }

        public string TenVaiTro { get; set; } = string.Empty;

        public ICollection<TaiKhoan>? TaiKhoans { get; set; }
    }
}