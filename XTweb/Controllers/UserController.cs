using Microsoft.AspNetCore.Mvc;
using XTweb.Models;

namespace XTBarber.Controllers
{
    public class UserController : Controller
    {
        XuanTamDbContext context = new XuanTamDbContext();

        public IActionResult Index()
        {
            var lstDichVu = context.DichVus.ToList();

            return View(lstDichVu);
        }

        public ActionResult dichvu()
        {
            return View();

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
