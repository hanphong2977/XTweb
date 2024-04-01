using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XTweb.Models;
using XTweb.Repository;

namespace XTweb.ViewComponents
{
    public class LoaiSpMenuViewComponent : ViewComponent
    {
        private readonly XuanTamDbContext _context;

        public LoaiSpMenuViewComponent(XuanTamDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var danhMucSanPhams = await _context.DanhMucSanPhams.OrderBy(d => d.TenDanhMuc).ToListAsync();
            return View(danhMucSanPhams);
        }
    }
}
