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
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart == null || !cart.Items.Any())
            {
                // Xử lý giỏ hàng trống...
                return RedirectToAction("Index");
            }
            var user = HttpContext.Session.GetInt32("MaKhachHang");
            if (user == null)
            {
                return RedirectToAction("dangnhap", "User");
            }
            else
            {
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
                return View("OrderCompleted", order.MaHoaDon); // Trang xác nhận hoàn thành đơn hàng
            }
         
            
            //var user = await _userManager.GetUserAsync(User);
            //order.UserId = user.Id;

        
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
                };
                var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
                cart.AddItem(cartItem);

                HttpContext.Session.SetObjectAsJson("Cart", cart);

                return RedirectToAction("Index");
            }
           
        }
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


    }
}
