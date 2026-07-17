using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SPASMART.Data;
using SPASMART.Models;

namespace SPASMART.Controllers
{
    public class LichHenController : Controller
    {
        private readonly AppDbContext _context;

        public LichHenController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(DateTime? filterDate, string? filterStatus)
        {
            var query = _context.LichHens
                .Include(l => l.KhachHang)
                .Include(l => l.DichVuSanPham)
                .Include(l => l.NhanVien)
                .AsQueryable();

            if (filterDate.HasValue) query = query.Where(l => l.NgayHen.Date == filterDate.Value.Date);
            if (!string.IsNullOrEmpty(filterStatus)) query = query.Where(l => l.TrangThai == filterStatus);

            var danhSachLich = await query.OrderByDescending(l => l.NgayHen).ToListAsync();
            ViewBag.CurrentDate = filterDate?.ToString("yyyy-MM-dd");
            ViewBag.CurrentStatus = filterStatus;

            return View("LichHen", danhSachLich);
        }

        public IActionResult Create()
        {
            LoadDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NgayHen,GioHen,GhiChu,MaKhachHang,MaDVSP,MaNhanVien")] LichHen lichHen)
        {
            if (ModelState.IsValid)
            {
                lichHen.TrangThai = "Chờ xác nhận";
                _context.Add(lichHen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            LoadDropdowns(lichHen);
            return View(lichHen);
        }

        // ==================== BỔ SUNG ACTION EDIT (GET) VÀO ĐÂY ====================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichHen = await _context.LichHens
                .Include(l => l.KhachHang)
                .Include(l => l.DichVuSanPham)
                .Include(l => l.NhanVien)
                .FirstOrDefaultAsync(m => m.MaLichHen == id);

            if (lichHen == null)
            {
                return NotFound();
            }

            LoadDropdowns(lichHen);
            return View(lichHen);
        }

        // ==================== BỔ SUNG ACTION EDIT (POST) VÀO ĐÂY ====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaLichHen,NgayHen,GioHen,TrangThai,GhiChu,MaKhachHang,MaDVSP,MaNhanVien")] LichHen lichHen)
        {
            if (id != lichHen.MaLichHen)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lichHen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LichHenExists(lichHen.MaLichHen))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            LoadDropdowns(lichHen);
            return View(lichHen);
        }

        [HttpPost]
        public async Task<IActionResult> ThemNhanhKhachHang(string tenKhachHang, string soDienThoai)
        {
            if (string.IsNullOrEmpty(tenKhachHang) || string.IsNullOrEmpty(soDienThoai))
                return Json(new { success = false, message = "Thiếu thông tin!" });

            var khachHangMoi = new KhachHang { HoTenKhach = tenKhachHang, SoDienThoaiKhach = soDienThoai };
            _context.KhachHangs.Add(khachHangMoi);
            await _context.SaveChangesAsync();

            return Json(new { success = true, maKhachHang = khachHangMoi.MaKhachHang, tenKhachHang = khachHangMoi.HoTenKhach });
        }

        private void LoadDropdowns(LichHen? lichHen = null)
        {
            ViewData["MaKhachHang"] = new SelectList(_context.KhachHangs, "MaKhachHang", "HoTenKhach", lichHen?.MaKhachHang);
            ViewData["MaDVSP"] = new SelectList(_context.DichVuSanPhams, "MaDVSP", "TenDVSP", lichHen?.MaDVSP);
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "HoTen", lichHen?.MaNhanVien);
        }

        // Hàm bổ trợ kiểm tra tồn tại lịch hẹn
        private bool LichHenExists(int id)
        {
            return _context.LichHens.Any(e => e.MaLichHen == id);
        }
    }
}