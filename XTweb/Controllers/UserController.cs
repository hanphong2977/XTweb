using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using XTweb.Models;
using XTweb.Repository;

namespace XTBarber.Controllers
{
    public class UserController : Controller
    {
        private readonly ISanPhamRepository _sanPhamRepository;
        private readonly ILoaiSanPhamRepository _loaiSanPhamRepository;
        public UserController(ISanPhamRepository sanPhamRepository, ILoaiSanPhamRepository loaiSanPhamRepository)
        {
            _sanPhamRepository = sanPhamRepository;
            _loaiSanPhamRepository = loaiSanPhamRepository;
        }
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

        public async Task<IActionResult> dichvuAsync(int maloai, int? page)
        {
            int pageSize = 3;
            int pageNumber = page == null || pageSize < 1 ? 1 : page.Value;
            var lstSanPham = _context.SanPhams.AsNoTracking().Where(x => x.MaDanhMuc == maloai).OrderBy(x => x.TenSanPham);

            var pagedList = await lstSanPham.ToPagedListAsync(pageNumber, pageSize);

            ViewBag.maLoai = maloai;
            ViewBag.tenloai = _context.DanhMucSanPhams.Where(x => x.MaDanhMuc == maloai).Select(x=>x.TenDanhMuc).FirstOrDefault();
            return View(pagedList);

        }

        public async Task<IActionResult> chitietsanpham(int masanpham)
        {
            var sanpham = await _context.SanPhams.SingleOrDefaultAsync(x => x.MaSanPham == masanpham);

            if (sanpham == null)
            {
                return RedirectToAction("ProductNotFound");
            }

            return View(sanpham);
        }


        public IActionResult gioithieu()
        {
           
            return View();
        }

        public IActionResult lienhe() {

            return View();
        }

        public IActionResult thanhtoan()
        {
            return View();
        }

        public IActionResult dangnhap()
        {
            return View();
        }

        public IActionResult dangky()
        {
            return View();
        }

        public IActionResult quenmatkhau() {
            return View();
        }

        public IActionResult xacnhanemail()
        {
            return View();
        }

        public IActionResult matkhaumoi()
        {
            return View();
        }
    }
}
