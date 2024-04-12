
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using X.PagedList;
using XTweb.Models;
using XTweb.Repository;
using XTweb.Services;

namespace XTBarber.Controllers
{
    public class UserController : Controller
    {
        private readonly ISanPhamRepository _sanPhamRepository;
        private readonly ILoaiSanPhamRepository _loaiSanPhamRepository;
        private readonly ILichHenRepository _lichHenRepository;
        private readonly IDichVuRepository _dichVuRepository;
        private readonly INhanVienRepository _nhanVienRepository;
        private readonly VnPaymentRequestModel _vnPaymentRequestModel;
        private readonly VnPaymentResponseModel _vnPaymentResponse1Model;
        private readonly IVnPayService _vnPayService;
        private readonly IThanhToanVNPayRepository _nhanToanVNPayRepository;
        public UserController(ISanPhamRepository sanPhamRepository, ILoaiSanPhamRepository loaiSanPhamRepository, 
            ILichHenRepository lichHenRepository, INhanVienRepository nhanVienRepository, IDichVuRepository dichVuRepository,
            IVnPayService vnPayService, VnPaymentResponseModel vnPaymentResponseModel, VnPaymentRequestModel vnPaymentRequestModel,
            IThanhToanVNPayRepository nhanToanVNPayRepository)
        {
            _sanPhamRepository = sanPhamRepository;
            _loaiSanPhamRepository = loaiSanPhamRepository;
            _lichHenRepository = lichHenRepository;
            _nhanVienRepository = nhanVienRepository;
            _dichVuRepository = dichVuRepository;
            _vnPayService = vnPayService;
            _vnPaymentRequestModel = vnPaymentRequestModel;
            _vnPaymentResponse1Model = vnPaymentResponseModel;
            _nhanToanVNPayRepository = nhanToanVNPayRepository;
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
        public async Task <IActionResult> dangky(RegisterModel model)
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
                return RedirectToAction("thanhtoan", new { id = lichhen.MaLichHen });
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

        public async Task<IActionResult> thanhtoanAsync(int id)
        {
            LichHen lichHen = await _lichHenRepository.GetByIdAsync(id);
            return View(lichHen);
        }

        [HttpPost]
        public IActionResult Checkout(string submitButton, string customername, string customerphone, DateTime datetime, decimal tongtien, int malichhen, string payment = "COD")
        {

            if (payment == "VNPay")
            {
                var vnpaymodel = new VnPaymentRequestModel
                {
                    Amount = Convert.ToDouble(tongtien),
                    customername = customername,
                    customerphone = customerphone,
                    DateCreate = datetime,
                    MalichHen = malichhen,
                };
                return Redirect(_vnPayService.CreatePaymentURL(HttpContext, vnpaymodel));
            }
            if (payment == "Momo")
            {


            }
            else
            {
                TempData["customername"] = customername;
                TempData["customerphone"] = customerphone;
                TempData["datetime"] = datetime.ToString("yyyy-MM-dd HH:mm:ss");
                TempData["tongtien"] = tongtien.ToString();
                TempData["malichhen"] = malichhen.ToString();
                return RedirectToAction("COD");
            }
            return View(Index);
        }

        private string GenerateRandomCode()
        {
            Random random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public IActionResult CreatePaymentUrl(VnPaymentRequestModel model)
        {
            var url = _vnPayService.CreatePaymentURL(HttpContext, model);

            return Redirect(url);
        }

        public IActionResult PaymentFail()
        {
            return View();
        }

        public IActionResult PaymentCallBack()
        {

            var response = _vnPayService.PaymentExecute(Request.Query);
            if (response == null || response.VnPayResponseCode != "00")
            {

                TempData["Message"] = $"Lỗi thanh toán VNPay: {response.VnPayResponseCode}";
                return RedirectToAction("PaymentFail");
            }
            VnPaymentResponseModel model = new VnPaymentResponseModel();
            model = response;
            TempData["Message"] = $"thanh toán VNPay thành công";
            return RedirectToAction("PaySuccess", model);
        }

        [HttpGet]
        public async Task<IActionResult> PaySuccess(VnPaymentResponseModel model)
        {
            HoaDonDichVu hoadondv = new HoaDonDichVu()
            {
                MaHoaDon = model.OrderId,
                TongTien = model.Amount,
            };
            await _context.HoaDonDichVus.AddAsync(hoadondv);
            _context.SaveChanges();

            Cthd cthddv = new Cthd()
            {
                MaGd = model.TransactionId,
                TenKh = model.OrderInfo,
                NgayDat = DateTime.UtcNow,
                TienThanhToan = Convert.ToDecimal(model.Amount),
                Pttt = model.PaymentMethod,
                TinhTrangTt = model.VnPayResponseCode,
                Sđt = model.Tel,
                MaHoaDonDv = model.OrderId,
                MaLichHen = model.malichhen,
            };
            await _nhanToanVNPayRepository.AddAsync(cthddv);
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> COD()
        {

            string customername = TempData["customername"] as string;
            string customerphone = TempData["customerphone"] as string;
            DateTime DateCreate = DateTime.Parse(TempData["datetime"] as string);
            decimal tongtien = decimal.Parse(TempData["tongtien"] as string);
            int malichhen = int.Parse(TempData["malichhen"] as string);
            var payment = "COD";
            var tick = DateTime.Now.Ticks.ToString();
            var MHD = Convert.ToInt64(tick);
            var vnpaymodel = new VnPaymentRequestModel
            {
                Amount = Convert.ToDouble(tongtien),
                customername = customername,
                customerphone = customerphone,
                DateCreate = DateCreate,
                MalichHen = malichhen,
            };
            HoaDonDichVu hoadondv = new HoaDonDichVu()
            {
                MaHoaDon = MHD,
                TongTien = Convert.ToDouble(tongtien),
            };
            await _context.HoaDonDichVus.AddAsync(hoadondv);
            _context.SaveChanges();
            Cthd cthddv = new Cthd()
            {
                MaGd = GenerateRandomCode(),
                TenKh = customername,
                NgayDat = DateCreate,
                TienThanhToan = Convert.ToDecimal(tongtien),
                Pttt = payment,
                TinhTrangTt = "01",
                Sđt = customerphone,
                MaHoaDonDv = MHD,
                MaLichHen = malichhen,
            };
            await _nhanToanVNPayRepository.AddAsync(cthddv);
            return View("PaySuccessCOD", vnpaymodel);
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
