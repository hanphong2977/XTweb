using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XTweb.Models;
using XTweb.Repository;

internal class ThucHienXoaLichHenHangNgay

{
    private Timer _timer;

    public ThucHienXoaLichHenHangNgay()
    {
        // Thực hiện kiểm tra và xóa lịch hẹn mỗi 1 phút
        _timer = new Timer(DeleteExpiredLichHen, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
    }

    private void DeleteExpiredLichHen(object state)
    {
        using (var context = new XuanTamDbContext()) // Thay thế YourDbContext bằng DbContext của bạn
        {
            var utcNow = DateTime.UtcNow;
            var expiredLichHen = context.LichHens.Where(l => l.NgayHen < utcNow).ToList();

            foreach (var lichHen in expiredLichHen)
            {
                context.LichHens.Remove(lichHen);
            }

            context.SaveChanges();
        }
    }
}