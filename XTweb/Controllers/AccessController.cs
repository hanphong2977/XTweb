using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XTweb.Models;
using XTweb.Repository;

namespace XTweb.Controllers
{
    public class AccessController : Controller
    {
        XuanTamDbContext _context = new XuanTamDbContext();
        [HttpGet]
        public IActionResult dangnhap()
        {
            if (HttpContext.Session.GetString("sdt") == null)
                return View();
            else
                return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public IActionResult dangnhap(LoginModel model)
        {

            if (HttpContext.Session.GetString("sdt") == null)
            {
                var u = _context.KhachHangs.Where(t => t.Sdt.Equals(model.sdt) && t.MatKhau == model.password).FirstOrDefault();
                if (u != null)
                {
                    ViewBag.Success = "Đăng nhập thành công!!";
                    HttpContext.Session.SetString("sdt", u.Sdt.ToString());
                    return RedirectToAction("Index", "User");
                }
            }
            return View(model);
        }

        public IActionResult dangxuat()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("sdt");
            return RedirectToAction("dangnhap", "User");
        }
        [HttpGet]
        public IActionResult dangky()
        {
            return View();
        }

        [HttpPost]
        public IActionResult dangky(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var tmp = new KhachHangRepository(_context);
                if (tmp.checkSDT(model.sdt))
                {
                    ModelState.AddModelError("", "SDT đã tồn tại");

                }
                else
                {
                    var us = new KhachHang();
                    us.Sdt = model.sdt;
                    us.HoTen = model.hoten;
                    us.MatKhau = model.password;

                    var result = tmp.AddAsync(us);
                    if (result != null)
                    {
                        ViewBag.Success = "Đăng ký thành công!";
                        model = new RegisterModel();

                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng Ký không thành công!!");
                    }

                }
            }
            return View(model);
        }
        private bool checkSDT(String sdt)
        {

            return true;
        }
    }
}
