using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPASMART.Data;
using SPASMART.Models;

namespace SPASMART.Controllers
{
    public class DichVuSanPhamsController : Controller
    {
        private readonly AppDbContext _context;

        public DichVuSanPhamsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? keyword, string? loai)
        {
            var query = _context.DichVuSanPhams.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(x => x.TenDVSP.Contains(keyword));
            }

            if (!string.IsNullOrWhiteSpace(loai))
            {
                query = query.Where(x => x.Loai == loai);
            }

            ViewBag.Keyword = keyword;
            ViewBag.Loai = loai;

            var danhSach = await query
                .OrderBy(x => x.Loai)
                .ThenBy(x => x.TenDVSP)
                .ToListAsync();

            return View(danhSach);
        }

        public async Task<IActionResult> Details(int id)
        {
            var dichVuSanPham = await _context.DichVuSanPhams
                .FirstOrDefaultAsync(x => x.MaDVSP == id);

            if (dichVuSanPham == null)
            {
                return NotFound();
            }

            return View(dichVuSanPham);
        }

        public IActionResult Create()
        {
            var model = new DichVuSanPham
            {
                TrangThai = "Đang cung cấp",
                SoLuongTon = 0
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DichVuSanPham dichVuSanPham)
        {
            if (dichVuSanPham.Loai == "Dịch vụ")
            {
                dichVuSanPham.HanSuDung = null;
                dichVuSanPham.DonViTinh = null;
                dichVuSanPham.SoLuongTon = null;
            }

            if (dichVuSanPham.Loai == "Sản phẩm")
            {
                dichVuSanPham.ThoiLuong = null;
            }

            if (ModelState.IsValid)
            {
                _context.DichVuSanPhams.Add(dichVuSanPham);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(dichVuSanPham);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dichVuSanPham = await _context.DichVuSanPhams.FindAsync(id);

            if (dichVuSanPham == null)
            {
                return NotFound();
            }

            return View(dichVuSanPham);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DichVuSanPham dichVuSanPham)
        {
            if (id != dichVuSanPham.MaDVSP)
            {
                return NotFound();
            }

            if (dichVuSanPham.Loai == "Dịch vụ")
            {
                dichVuSanPham.HanSuDung = null;
                dichVuSanPham.DonViTinh = null;
                dichVuSanPham.SoLuongTon = null;
            }

            if (dichVuSanPham.Loai == "Sản phẩm")
            {
                dichVuSanPham.ThoiLuong = null;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dichVuSanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    bool exists = await _context.DichVuSanPhams.AnyAsync(x => x.MaDVSP == id);

                    if (!exists)
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(dichVuSanPham);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dichVuSanPham = await _context.DichVuSanPhams
                .FirstOrDefaultAsync(x => x.MaDVSP == id);

            if (dichVuSanPham == null)
            {
                return NotFound();
            }

            return View(dichVuSanPham);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dichVuSanPham = await _context.DichVuSanPhams.FindAsync(id);

            if (dichVuSanPham != null)
            {
                _context.DichVuSanPhams.Remove(dichVuSanPham);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}