using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SPASMART.Models;

namespace SPASMART.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Admin()
        {
            var vaiTro = HttpContext.Session.GetString("VaiTro");

            if (string.IsNullOrEmpty(vaiTro))
            {
                return RedirectToAction("Login", "Account");
            }

            if (vaiTro != "Quản lý Spa" && vaiTro != "Nhân viên lễ tân và tư vấn")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            return View();
        }

        public IActionResult LichLamViec()
        {
            var vaiTro = HttpContext.Session.GetString("VaiTro");

            if (string.IsNullOrEmpty(vaiTro))
            {
                return RedirectToAction("Login", "Account");
            }

            if (vaiTro != "Bộ phận kỹ thuật viên")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            return View();
        }

        public IActionResult DatLich()
        {
            return View();
        }
    }
}
