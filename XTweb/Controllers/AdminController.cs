using Microsoft.AspNetCore.Mvc;
using XTweb.Models;
using XTweb.Repository;

namespace XTweb.Controllers
{
    public class AdminController : Controller
    {
        private readonly ISanPhamRepository _sanPhamRepository;
        private readonly ILoaiSanPhamRepository _loaiSanPhamRepository;
        public AdminController(ISanPhamRepository sanPhamRepository, ILoaiSanPhamRepository loaiSanPhamRepository)
        {
            _sanPhamRepository = sanPhamRepository;
            _loaiSanPhamRepository = loaiSanPhamRepository;
        }

        XuanTamDbContext _context = new XuanTamDbContext();

        public IActionResult Index()
        {
            var model = _context.NhanViens.ToList();
            return View(model);
        }
        public IActionResult khachhang()
        {
            var model = _context.KhachHangs.ToList();
            return View(model);
        }
        public IActionResult dichvu()
        {
            var model = _context.DichVus.ToList();
            return View(model);
        }
        public IActionResult lichhen()
        {
            return View();
        }
        public async Task<IActionResult> sanpham()
        {
            var sanpham = await _sanPhamRepository.GetAllAsync();
            return View(sanpham);
        }

    }
}
