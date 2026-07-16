using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPASMART.Data;
using SPASMART.Models;
using SPASMART.ViewModels;

namespace SPASMART.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var taiKhoan = await _context.TaiKhoans
                .Include(x => x.VaiTro)
                .FirstOrDefaultAsync(x =>
                    x.TenDangNhap == model.TenDangNhap &&
                    x.MatKhau == model.MatKhau &&
                    x.TrangThai == "Hoạt động");

            if (taiKhoan == null)
            {
                ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng";
                return View(model);
            }

            HttpContext.Session.SetInt32("MaTaiKhoan", taiKhoan.MaTaiKhoan);
            HttpContext.Session.SetString("TenDangNhap", taiKhoan.TenDangNhap);
            HttpContext.Session.SetString("VaiTro", taiKhoan.VaiTro?.TenVaiTro ?? "");

            var vaiTro = taiKhoan.VaiTro?.TenVaiTro;

            if (vaiTro == "Quản lý Spa")
            {
                return RedirectToAction("Admin", "Home");
            }

            if (vaiTro == "Nhân viên lễ tân và tư vấn")
            {
                return RedirectToAction("Admin", "Home");
            }

            if (vaiTro == "Bộ phận kỹ thuật viên")
            {
                return RedirectToAction("LichLamViec", "Home");
            }

            if (vaiTro == "Khách hàng")
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existed = await _context.TaiKhoans
                .AnyAsync(x => x.TenDangNhap == model.TenDangNhap);

            if (existed)
            {
                ViewBag.Error = "Tên đăng nhập đã tồn tại";
                return View(model);
            }

            var vaiTroKhachHang = await _context.VaiTros
                .FirstOrDefaultAsync(x => x.TenVaiTro == "Khách hàng");

            if (vaiTroKhachHang == null)
            {
                vaiTroKhachHang = new VaiTro
                {
                    TenVaiTro = "Khách hàng"
                };

                _context.VaiTros.Add(vaiTroKhachHang);
                await _context.SaveChangesAsync();
            }

            var taiKhoan = new TaiKhoan
            {
                TenDangNhap = model.TenDangNhap,
                MatKhau = model.MatKhau,
                TrangThai = "Hoạt động",
                MaVaiTro = vaiTroKhachHang.MaVaiTro
            };

            _context.TaiKhoans.Add(taiKhoan);
            await _context.SaveChangesAsync();

            var khachHang = new KhachHang
            {
                HoTenKhach = model.HoTenKhach,
                SoDienThoaiKhach = model.SoDienThoaiKhach,
                EmailKhach = model.EmailKhach,
                GioiTinh = model.GioiTinh,
                NgaySinh = model.NgaySinh,
                MaTaiKhoan = taiKhoan.MaTaiKhoan
            };

            _context.KhachHangs.Add(khachHang);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}