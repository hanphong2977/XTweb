
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.Metrics;
using X.PagedList;
using XTweb.Models;
using XTweb.Repository;

namespace XTBarber.Controllers
{
    public class UserController : Controller
    {
        private readonly ISanPhamRepository _sanPhamRepository;
        private readonly ILoaiSanPhamRepository _loaiSanPhamRepository;
        private readonly ILichHenRepository _lichHenRepository;
        private readonly IDichVuRepository _dichVuRepository;
        private readonly INhanVienRepository _nhanVienRepository;
        public UserController(ISanPhamRepository sanPhamRepository, ILoaiSanPhamRepository loaiSanPhamRepository, 
            ILichHenRepository lichHenRepository, INhanVienRepository nhanVienRepository, IDichVuRepository dichVuRepository)
        {
            _sanPhamRepository = sanPhamRepository;
            _loaiSanPhamRepository = loaiSanPhamRepository;
            _lichHenRepository = lichHenRepository;
            _nhanVienRepository = nhanVienRepository;
            _dichVuRepository = dichVuRepository;
        }
        XuanTamDbContext _context = new XuanTamDbContext();


        public IActionResult dangxuat()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("sdt");
            return RedirectToAction("dangnhap", "User");
        }
        [HttpGet]
        public IActionResult dangnhap()
        {
            if (HttpContext.Session.GetString("sdt") == null)
                return View(); 
            else
                return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public  IActionResult dangnhap(LoginModel model)
        {
            if (HttpContext.Session.GetString("sdt") == null)
            {
                var u = _context.KhachHangs.Where(t => t.Sdt.Equals(model.sdt) && t.MatKhau == model.password).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("sdt", u.Sdt.ToString());
                    return RedirectToAction("Index", "User");
                }
            }
            return View(model);
        }
      
        [HttpGet]
        public IActionResult dangky()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> dangky( RegisterModel model)
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
                    KhachHang us = new KhachHang()
                    {
                        Sdt = model.sdt,
                        HoTen = model.hoten,
                        MatKhau = model.password,
                    };


                    var result = _context.KhachHangs.AddAsync(us);
                    await _context.SaveChangesAsync();
                    if (await result != null)
                    {
                        ViewBag.Success = "Đăng ký thành công!";
                        model = new RegisterModel();
                        return RedirectToAction("dangnhap");
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
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 6;
            int pageNumber = page == null || pageSize < 1 ? 1 : page.Value;
            var lstDichVu = await _dichVuRepository.GetAllAsync();
            PagedList<DichVu> pagedList = new PagedList<DichVu>(lstDichVu ,pageNumber, pageSize);
            var lichhenviewmodels = new LichHenViewModel();
            var nhanViens = await _nhanVienRepository.GetAllAsync();
            var model = new IndexViewModel
            {
                DanhSachDichVu = await _dichVuRepository.GetAllAsync(),
                DanhSachNhanVien = await _nhanVienRepository.GetAllAsync(),
                LichHen = await _lichHenRepository.GetAllAsync(),
                pagelstDichVu = pagedList,
                LichHenViewModel = lichhenviewmodels,
            };
            ViewBag.DichVu = new SelectList(lstDichVu, "MaDichVu", "TenDichVu");
            ViewBag.NhanVien = new SelectList(nhanViens, "MaNhanVien", "TenNhanVien");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddLichHen(LichHenViewModel model)
        {
            var khachhang = _context.KhachHangs.Where(x => x.Sdt == model.CustomerPhone).FirstOrDefault();
            if (khachhang == null)
            {
                return View("dangky");
            }
            if (ModelState.IsValid)
            {
                LichHen lichhen = new LichHen
                {
                    MaKhachHang = Convert.ToInt32(khachhang.MaKhachHang),
                    MaDichVu = model.SelectedDichVuId,
                    MaNhanVien = model.SelectedNhanVienId,
                    NgayHen = model.NgayHen,
                };
                await _lichHenRepository.AddAsync(lichhen);
                return RedirectToAction("ThanhToan");
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
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

        public async Task<IActionResult> chitietsanpham(int id)
        {
            var product = await _sanPhamRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
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
//6LeMMrcpAAAAACp1s9WzTTajVifZTe9kjD1lo7oN
//6LeMMrcpAAAAAM0blFoH8omK97kBjrPzn-oL_7RC