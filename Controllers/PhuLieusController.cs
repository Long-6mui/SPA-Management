using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPASMART.Data;
using SPASMART.Models;

namespace SPASMART.Controllers
{
    public class PhuLieusController : Controller
    {
        private readonly AppDbContext _context;

        public PhuLieusController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? keyword, string? trangThai)
        {
            var check = KiemTraQuyen();
            if (check != null)
            {
                return check;
            }

            var query = _context.DichVuSanPhams
                .Where(x => x.Loai == "Sản phẩm");

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(x => x.TenDVSP.Contains(keyword));
            }

            if (!string.IsNullOrWhiteSpace(trangThai))
            {
                query = query.Where(x => x.TrangThai == trangThai);
            }

            var allPhuLieu = await _context.DichVuSanPhams
                .Where(x => x.Loai == "Sản phẩm")
                .ToListAsync();

            ViewBag.Keyword = keyword;
            ViewBag.TrangThai = trangThai;

            ViewBag.TongPhuLieu = allPhuLieu.Count;
            ViewBag.SapHetHang = allPhuLieu.Count(x => x.SoLuongTon.HasValue && x.SoLuongTon.Value > 0 && x.SoLuongTon.Value <= 5);
            ViewBag.HetHang = allPhuLieu.Count(x => x.SoLuongTon.HasValue && x.SoLuongTon.Value == 0);
            ViewBag.DangSuDung = allPhuLieu.Count(x => x.TrangThai == "Đang sử dụng");

            var danhSach = await query
                .OrderBy(x => x.TenDVSP)
                .ToListAsync();

            return View(danhSach);
        }

        public IActionResult Create()
        {
            var check = KiemTraQuyen();
            if (check != null)
            {
                return check;
            }

            var model = new DichVuSanPham
            {
                Loai = "Sản phẩm",
                TrangThai = "Đang sử dụng",
                SoLuongTon = 0
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DichVuSanPham model)
        {
            var check = KiemTraQuyen();
            if (check != null)
            {
                return check;
            }

            model.Loai = "Sản phẩm";
            model.ThoiLuong = null;

            if (ModelState.IsValid)
            {
                _context.DichVuSanPhams.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var check = KiemTraQuyen();
            if (check != null)
            {
                return check;
            }
            var model = await _context.DichVuSanPhams.FindAsync(id);

            if (model == null || model.Loai != "Sản phẩm")
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DichVuSanPham model)
        {
            var check = KiemTraQuyen();
            if (check != null)
            {
                return check;
            }

            if (id != model.MaDVSP)
            {
                return NotFound();
            }

            model.Loai = "Sản phẩm";
            model.ThoiLuong = null;

            if (ModelState.IsValid)
            {
                _context.Update(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var check = KiemTraQuyen();
            if (check != null)
            {
                return check;
            }

            var model = await _context.DichVuSanPhams.FindAsync(id);

            if (model == null || model.Loai != "Sản phẩm")
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var check = KiemTraQuyen();
            if (check != null)
            {
                return check;
            } 

            var model = await _context.DichVuSanPhams.FindAsync(id);

            if (model != null)
            {
                _context.DichVuSanPhams.Remove(model);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private IActionResult? KiemTraQuyen()
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

            return null;
        }
    }
}