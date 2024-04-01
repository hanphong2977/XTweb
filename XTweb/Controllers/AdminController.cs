using Microsoft.AspNetCore.Mvc;
using XTweb.Models;

namespace XTweb.Controllers
{
    public class AdminController : Controller
    {

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
        public IActionResult sanpham()
        {
            var model = _context.SanPhams.ToList();
            return View(model);
        }

    }
}
