

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Configuration;
using XTweb.Models;
using XTweb.Repository;
using XTweb.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<XuanTamDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ILoaiSanPhamRepository, LoaiSanPhamRepository>();
builder.Services.AddScoped<ISanPhamRepository, SanPhamRepository>();
builder.Services.AddScoped<IDichVuRepository, DichVuRepository>();
builder.Services.AddScoped<ILichHenRepository, LichHenRepository>();
builder.Services.AddScoped<INhanVienRepository, NhanVienRepository>();
builder.Services.AddScoped<IKhachHangRepository, KhachHangRepository>();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.AddScoped<VnPaymentRequestModel>();
builder.Services.AddScoped<VnPaymentResponseModel>();
builder.Services.AddScoped<IThanhToanVNPayRepository, ThanhToanVNPay>();
builder.Services.AddSingleton<ThucHienXoaLichHenHangNgay>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
/*app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: " default ",
    pattern: "{ controller = Home}/{ action = Index}/{ id ?} ");
});*/
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();
