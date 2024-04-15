using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XTweb.Extensions;
using Microsoft.AspNetCore.Mvc;
using XTweb.Models;
using XTweb.Repository;
using Microsoft.AspNetCore.Identity;
using Hangfire.Server;
using Microsoft.EntityFrameworkCore;
using XTweb.Models.Authentication;


namespace XTweb.Controllers
{
    public class GioHangController : Controller
    {

        private readonly ISanPhamRepository _sanPhamRepository;
        private readonly XuanTamDbContext _context;

        public GioHangController(ISanPhamRepository sanPhamRepository, XuanTamDbContext context)
        {
            _sanPhamRepository = sanPhamRepository;
            _context = context;
        }
        public ActionResult Checkout()
        {

            return View(new HoaDonSanPham());
            
        }
        
        [HttpPost]
        public async Task<IActionResult> Checkout(HoaDonSanPham order)
        {

            
            var user = HttpContext.Session.GetInt32("MaKhachHang");
            var khachHang = await _context.KhachHangs.FirstOrDefaultAsync(kh => kh.MaKhachHang == user);
            var username = khachHang.HoTen;
            if (user == null)
            {
                return RedirectToAction("dangnhap", "User");
            }
            else 
            {
                var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
                if (cart == null || !cart.Items.Any())
                {
                    // Xử lý giỏ hàng trống...
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.TenKhachHang = username;
                    order.MaKhachHang = (int)user;
                    order.NgayMua = DateTime.UtcNow;
                    order.TongTien = cart.Items.Sum(i => i.Price * i.Quantity);
                    order.CthdsanPhams = cart.Items.Select(i => new CthdsanPham
                    {
                        MaSanPham = i.ProductId,
                        SoLuongMua = i.Quantity,
                        Gia = Convert.ToDecimal(i.Price),
                    }).ToList();
                    _context.HoaDonSanPhams.Add(order);
                    await _context.SaveChangesAsync();

                    HttpContext.Session.Remove("Cart");
                    return View("OrderCompleted", order);

                }
              // Trang xác nhận hoàn thành đơn hàng
            }
         
            
            //var user = await _userManager.GetUserAsync(User);
            //order.UserId = user.Id;

        
        }
        [HttpPost]
        public async Task<IActionResult> Tangsoluong(int productId)
        {
            var u = await GetProductFromDatabase(productId);
            if (u != null)
            {
                u.SoLuong = +1;
            }
          
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> AddToCart(int masanpham, int soluongmua)
        {
            // Giả sử bạn có phương thức lấy thông tin sản phẩm từ productId
            if (HttpContext.Session.GetString("sdt") == null)
            {
                
                    return RedirectToAction("dangnhap", "User");
            }
            else    
            {
               var product = await GetProductFromDatabase(masanpham);    
                    var cartItem = new CartItem
                    {
                        ProductId = masanpham,
                        Name = product.TenSanPham,
                        Price = product.Gia,
                        Quantity = soluongmua,
                        ImageUrl = product.HinhAnh,
                        MotaSanPham = product.MoTaSanPham,
                        TongTien = (float)product.Gia * soluongmua
                    };
                    var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
                    cart.AddItem(cartItem);
                  
                    HttpContext.Session.SetObjectAsJson("Cart", cart);
                    return RedirectToAction("Index");        
            }
           
        }
        [Authentication]
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            return View(cart);
        }
        // Các actions khác...
        private async Task<SanPham> GetProductFromDatabase(int productId)
        {
            // Truy vấn cơ sở dữ liệu để lấy thông tin sản phẩm
            var product = await _sanPhamRepository.GetByIdAsync(productId);
            return product;
        }

        public IActionResult RemoveFromCart(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");

            if (cart is not null)
            {
                cart.RemoveItem(productId);
                // Lưu lại giỏ hàng vào Session sau khi đã xóa mục
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> MinusToCart(int masanpham, int soluongmua)
        {
            // Giả sử bạn có phương thức lấy thông tin sản phẩm từ productId
            if (HttpContext.Session.GetString("sdt") == null)
            {

                return RedirectToAction("dangnhap", "User");
            }
            else
            {
                var product = await GetProductFromDatabase(masanpham);
                var cartItem = new CartItem
                {
                    ProductId = masanpham,
                    Name = product.TenSanPham,
                    Price = product.Gia,
                    Quantity = soluongmua,
                    ImageUrl = product.HinhAnh,
                    MotaSanPham = product.MoTaSanPham,
                    TongTien = (float)product.Gia * soluongmua
                };
                var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
         
                cart.MinusItem(cartItem);
        
                HttpContext.Session.SetObjectAsJson("Cart", cart);
                return RedirectToAction("Index");
            }

        }


    }
}
