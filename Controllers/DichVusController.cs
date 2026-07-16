using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPASMART.Data;

namespace SPASMART.Controllers
{
    public class DichVusController : Controller
    {
        private readonly AppDbContext _context;

        public DichVusController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? keyword)
        {
            var query = _context.DichVuSanPhams
                .Where(x => x.Loai == "Dịch vụ");

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(x => x.TenDVSP.Contains(keyword));
            }

            ViewBag.Keyword = keyword;

            var danhSach = await query
                .OrderBy(x => x.TenDVSP)
                .ToListAsync();

            return View(danhSach);
        }

        public async Task<IActionResult> Details(int id)
        {
            var dichVu = await _context.DichVuSanPhams
                .FirstOrDefaultAsync(x => x.MaDVSP == id && x.Loai == "Dịch vụ");

            if (dichVu == null)
            {
                return NotFound();
            }

            return View(dichVu);
        }
    }
}