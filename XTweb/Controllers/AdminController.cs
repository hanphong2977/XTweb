using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using XTweb.Models;
using XTweb.Models.Authentication;
using XTweb.Repository;


namespace XTweb.Controllers
{
    public class AdminController : Controller
    {
        private readonly ISanPhamRepository _sanPhamRepository;
        private readonly ILoaiSanPhamRepository _loaiSanPhamRepository;
        private readonly ILichHenRepository _lichHenRepository;
        private readonly IDichVuRepository _dichVuRepository;
        private readonly INhanVienRepository _nhanVienRepository;
        private readonly IKhachHangRepository _khachHangRepository;
        private readonly IHoaDonDichVuRepository _hoaDonDichVuRepository;
        public AdminController(ISanPhamRepository sanPhamRepository, ILoaiSanPhamRepository loaiSanPhamRepository,
            ILichHenRepository lichHenRepository, INhanVienRepository nhanVienRepository, IDichVuRepository dichVuRepository,
            IKhachHangRepository khachHangRepository, IHoaDonDichVuRepository hoaDonDichVuRepository)
        {
            _sanPhamRepository = sanPhamRepository;
            _loaiSanPhamRepository = loaiSanPhamRepository;
            _lichHenRepository = lichHenRepository;
            _nhanVienRepository = nhanVienRepository;
            _dichVuRepository = dichVuRepository;
            _khachHangRepository = khachHangRepository;
            _hoaDonDichVuRepository = hoaDonDichVuRepository;
        }

        XuanTamDbContext _context = new XuanTamDbContext();
        //Nhân Viên
        [Authentication_Admin]
        public async Task<IActionResult> Index()
        {
            var model = await _nhanVienRepository.GetAllAsync();
            return View(model);
        }

        public  IActionResult AddNhanVien()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNhanVien(NhanVien nhanvien, IFormFile AnhNhanVien)
        {
            if (ModelState.IsValid)
            {
                if (AnhNhanVien != null)
                {
                    nhanvien.AnhNhanVien = await SaveImage(AnhNhanVien);
                }
                await _nhanVienRepository.AddAsync(nhanvien);
                return RedirectToAction("Index");
            }
            return View(nhanvien);
        }
        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/image", image.FileName);

            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/image/" + image.FileName;
        }

        public async Task<IActionResult> DisplayNhanVien(int id)
        {
            var nhanvien = await _nhanVienRepository.GetByIdAsync(id);
            if (nhanvien == null)
            {
                return NotFound();
            }
            return View(nhanvien);
        }

        public async Task<IActionResult> UpdateNhanVien(int id)
        {
            var nhanvien = await _nhanVienRepository.GetByIdAsync(id);
            if (nhanvien == null)
            {
                return NotFound();
            }   
            return View(nhanvien);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateNhanVien(int id, NhanVien nhanvien,IFormFile AnhNhanVien)
        {
            ModelState.Remove("AnhNhanVien");

            if (id != nhanvien.MaNhanVien)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingNhanVien = await  _nhanVienRepository.GetByIdAsync(id);

                if (AnhNhanVien == null)
                {
                    nhanvien.AnhNhanVien = existingNhanVien.AnhNhanVien;
                }
                else
                {
                    nhanvien.AnhNhanVien = await SaveImage(AnhNhanVien);
                }
                existingNhanVien.TenNhanVien = nhanvien.TenNhanVien;
                existingNhanVien.DiaChi = nhanvien.DiaChi;
                existingNhanVien.Sdt = nhanvien.Sdt;
                existingNhanVien.Email = nhanvien.Email;
                existingNhanVien.AnhNhanVien = nhanvien.AnhNhanVien;
                await _nhanVienRepository.UpdateAsync(existingNhanVien);
                return RedirectToAction(nameof(Index));
            }
            return View(nhanvien);
        }
        public async Task<IActionResult> DeleteNhanVien(int id)
        {
            var nhanvien = await _nhanVienRepository.GetByIdAsync(id);
            if (nhanvien == null)
            {
                return NotFound();
            }
            return View(nhanvien);
        }
        [HttpPost, ActionName("DeleteNhanVienConfirmed")]
        public async Task<IActionResult> DeleteNhanVienConfirmed(int MaNhanVien)
        {
            await _nhanVienRepository.DeleteAsync(MaNhanVien);
            return RedirectToAction(nameof(Index));
        }
        //Khách Hàng
        public async Task<IActionResult> khachhang()
        {
            var model = await _khachHangRepository.GetAllAsync();
            return View(model);
        }
        public IActionResult AddKhachHang()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddKhachHang(KhachHang khachhang)
        {
            if (ModelState.IsValid)
            {
                await _khachHangRepository.AddAsync(khachhang);
                return RedirectToAction("khachhang");
            }
            return View(khachhang);
        }

        public async Task<IActionResult> DisplayKhachHang(int id)
        {
            var khachhang = await _khachHangRepository.GetByIdAsync(id);
            if (khachhang == null)
            {
                return NotFound();
            }
            return View(khachhang);
        }

        public async Task<IActionResult> UpdateKhachHang(int id)
        {
            var khachhang = await _khachHangRepository.GetByIdAsync(id);
            if (khachhang == null)
            {
                return NotFound();
            }
            return View(khachhang);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateKhachHang(int id, KhachHang khachhang)
        {

            if (id != khachhang.MaKhachHang)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingKhachHang = await _khachHangRepository.GetByIdAsync(id);

                existingKhachHang.HoTen = khachhang.HoTen;
                existingKhachHang.Sdt = khachhang.Sdt;
                await _khachHangRepository.UpdateAsync(existingKhachHang);
                return RedirectToAction(nameof(khachhang));
            }
            return View(khachhang);
        }
        public async Task<IActionResult> DeleteKhachHang(int id)
        {
            var khachhang = await _khachHangRepository.GetByIdAsync(id);
            if (khachhang == null)
            {
                return NotFound();
            }
            return View(khachhang);
        }
        [HttpPost, ActionName("DeleteKhachHangConfirmed")]
        public async Task<IActionResult> DeleteKhachHangConfirmed(int MaKhachHang)
        {
            await _khachHangRepository.DeleteAsync(MaKhachHang);
            return RedirectToAction(nameof(khachhang));
        }
        //Dịch Vụ
        public async Task<IActionResult> dichvu()
        {
            var model = await _dichVuRepository.GetAllAsync();
            return View(model);
        }
        public IActionResult AddDichVu()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDichVu(DichVu dichvu, IFormFile AnhDichVu)
        {
            if (ModelState.IsValid)
            {
                if (AnhDichVu != null)
                {
                    dichvu.AnhDichVu = await SaveImage(AnhDichVu);
                }
                await _dichVuRepository.AddAsync(dichvu);
                return RedirectToAction("dichvu");
            }
            return View(dichvu);
        }

        public async Task<IActionResult> DisplayDichVu(int id)
        {
            var dichvu = await _dichVuRepository.GetByIdAsync(id);
            if (dichvu == null)
            {
                return NotFound();
            }
            return View(dichvu);
        }

        public async Task<IActionResult> UpdateDichVu(int id)
        {
            var dichvu = await _dichVuRepository.GetByIdAsync(id);
            if (dichvu == null)
            {
                return NotFound();
            }
            return View(dichvu);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDichVu(int id, DichVu dichvu, IFormFile AnhDichVu)
        {
            ModelState.Remove("AnhDichVu");

            if (id != dichvu.MaDichVu)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingDichVu = await _dichVuRepository.GetByIdAsync(id);

                if (AnhDichVu == null)
                {
                    dichvu.AnhDichVu = existingDichVu.AnhDichVu;
                }
                else
                {
                    dichvu.AnhDichVu = await SaveImage(AnhDichVu);
                }
                // Cập nhật các thông tin khác của sản phẩm
                existingDichVu.TenDichVu = dichvu.TenDichVu;
                existingDichVu.Gia = dichvu.Gia;
                existingDichVu.AnhDichVu = dichvu.AnhDichVu;
                await _dichVuRepository.UpdateAsync(existingDichVu);
                return RedirectToAction(nameof(dichvu));
            }
            return View(dichvu);
        }

        public async Task<IActionResult> DeleteDichVu(int id)
        {
            var dichvu = await _dichVuRepository.GetByIdAsync(id);
            if (dichvu == null)
            {
                return NotFound();
            }
            return View(dichvu);
        }
        [HttpPost, ActionName("DeleteDichVuConfirmed")]
        public async Task<IActionResult> DeleteDichVuConfirmed(int MaDichVu)
        {
            await _dichVuRepository.DeleteAsync(MaDichVu);
            return RedirectToAction(nameof(dichvu));
        }
        //Lịch Hẹn
        public async Task<IActionResult> lichhen()
        {
            var model = await _lichHenRepository.GetAllAsync();
            return View(model);
        }

        public async Task<IActionResult> DisplayLichHen(int id)
        {
            var lichhen = await _lichHenRepository.GetByIdAsync(id);
            if (lichhen == null)
            {
                return NotFound();
            }
            return View(lichhen);
        }

        public IActionResult dangxuat()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("sdt");
            return RedirectToAction("dangnhap", "Admin");
        }

        [HttpGet]
        public IActionResult dangnhap()
        {
            if (HttpContext.Session.GetString("sdt") == null)
                return View();
            else
                return RedirectToAction("Index", "Admin");
        }
        [HttpPost]
        public IActionResult dangnhap(Admin_LoginModel model)
        {
            if (HttpContext.Session.GetString("sdt") == null)
            {
                var u = _context.NhanViens.Where(t => t.Sdt.Equals(model.sdt) && t.MatKhau == model.password).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("sdt", u.Sdt.ToString());
                    HttpContext.Session.SetInt32("IdNV", u.MaNhanVien);
                    return RedirectToAction("Index","Admin");
                }
                else { return RedirectToAction("Index"); }
            }
            return View(model);    
        }

        public void ThucHienXoaLichHenHangNgay()
        {
            RecurringJob.AddOrUpdate("xoalichhenhangngay", () => DeleteLichHen(), Cron.Daily);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLichHen()
        {
            var malichhen = _context.LichHens.Where(d => d.NgayHen < DateTime.UtcNow.AddDays(-1)).Select(x =>x.MaLichHen).FirstOrDefault();
            await _sanPhamRepository.DeleteAsync(malichhen);
                return RedirectToAction(nameof(sanpham));
        }
        //Sản Phẩm
        public async Task<IActionResult> sanpham()
        {
            var sanpham = await _sanPhamRepository.GetAllAsync();
            return View(sanpham);
        }

        public IActionResult AddSanPham()
        {
            var lstdanhmuc = _context.DanhMucSanPhams.OrderBy(x =>x.TenDanhMuc).ToList();
            ViewBag.LoaiSanPham = new SelectList(lstdanhmuc, "MaDanhMuc", "TenDanhMuc");
            return View();
        }

        [HttpPost]
        [Authentication_Admin(IdChucNang =1)]
        public async Task<IActionResult> AddSanPham(SanPham sanpham, IFormFile HinhAnh)
        {
            if (ModelState.IsValid)
            {
                if (HinhAnh != null)
                {
                    sanpham.HinhAnh = await SaveImage(HinhAnh);
                }
                await _sanPhamRepository.AddAsync(sanpham);
                return RedirectToAction("sanpham");
            }
            return View(sanpham);
        }

        public async Task<IActionResult> DisplaySanPham(int id)
        {
            var sanpham = await _sanPhamRepository.GetByIdAsync(id);
            if (sanpham == null)
            {
                return NotFound();
            }
            return View(sanpham);
        }

        public async Task<IActionResult> UpdateSanPham(int id)
        {
            var sanpham = await _sanPhamRepository.GetByIdAsync(id);
            if (sanpham == null)
            {
                return NotFound();
            }
            var lstdanhmuc = _context.DanhMucSanPhams.OrderBy(x => x.TenDanhMuc).ToList();
            ViewBag.LoaiSanPham = new SelectList(lstdanhmuc, "MaDanhMuc", "TenDanhMuc");
            return View(sanpham);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSanPham(int id, SanPham sanpham, IFormFile HinhAnh)
        {
            ModelState.Remove("HinhAnh");

            if (id != sanpham.MaSanPham)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingSanPham = await _sanPhamRepository.GetByIdAsync(id);

                if (HinhAnh == null)
                {
                    sanpham.HinhAnh = existingSanPham.HinhAnh;
                }
                else
                {
                    sanpham.HinhAnh = await SaveImage(HinhAnh);
                }
                // Cập nhật các thông tin khác của sản phẩm
                existingSanPham.TenSanPham = sanpham.TenSanPham;
                existingSanPham.MoTaSanPham = sanpham.MoTaSanPham;
                existingSanPham.Gia = sanpham.Gia;
                existingSanPham.SoLuong = sanpham.SoLuong;
                existingSanPham.MaDanhMuc = sanpham.MaDanhMuc;
                existingSanPham.HinhAnh = sanpham.HinhAnh;
                await _sanPhamRepository.UpdateAsync(existingSanPham);
                return RedirectToAction(nameof(sanpham));
            }
            return View(sanpham);
        }
        public async Task<IActionResult> DeleteSanPham(int id)
        {
            var sanpham = await _sanPhamRepository.GetByIdAsync(id);
            if (sanpham == null)
            {
                return NotFound();
            }
            return View(sanpham);
        }
        [HttpPost, ActionName("DeleteSanPhamConfirmed")]
        public async Task<IActionResult> DeleteSanPhamConfirmed(int MaSanPham)
        {
            await _sanPhamRepository.DeleteAsync(MaSanPham);
            return RedirectToAction(nameof(sanpham));
        }

        public IActionResult hoadon()
        {
            return View();
        }
    }
}
