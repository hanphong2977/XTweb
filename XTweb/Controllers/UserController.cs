using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using XTweb.Models;

namespace XTBarber.Controllers
{
    public class UserController : Controller
    {
        XuanTamDbContext _context = new XuanTamDbContext();

        public IActionResult Index()
        {
            var model = new IndexViewModel
            {
                DanhSachDichVu = _context.DichVus.ToList(),
                DanhSachNhanVien = _context.NhanViens.ToList(),
            };

            return View(model);
        }

        public ActionResult dichvu()
        {
            var model = new ProductViewModels
            {
                sanPhams_DauGoi = _context.SanPhams.Where(x => x.MaDanhMuc == 1).ToList(),
                sanPhams_SuaTam = _context.SanPhams.Where(x => x.MaDanhMuc == 2).ToList(),
                sanPhams_SapVotToc = _context.SanPhams.Where(x => x.MaDanhMuc== 3).ToList(),
                sanPhams_GelTaoKieuToc = _context.SanPhams.Where(x => x.MaDanhMuc == 4).ToList(),
                sanPhams_NuocHoa = _context.SanPhams.Where(x => x.MaDanhMuc == 5).ToList(),
                sanPhams_XitKhuMui = _context.SanPhams.Where(x => x.MaDanhMuc == 6).ToList(),
            };

            return View(model);

        }

        public ActionResult gioithieu()
        {
           
            return View();
        }

        public ActionResult lienhe() {

            return View();
        }

        public ActionResult thanhtoan()
        {
            return View();
        }

        public ActionResult dangnhap()
        {
            return View();
        }

        public ActionResult dangky()
        {
            return View();
        }

        public ActionResult quenmatkhau() {
            return View();
        }

        public ActionResult xacnhanemail()
        {
            return View();
        }

        public ActionResult matkhaumoi()
        {
            return View();
        }
    }
}
