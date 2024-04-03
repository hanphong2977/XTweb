using Microsoft.EntityFrameworkCore;
using XTweb.Models;

namespace XTweb.Repository
{
    public class SanPhamRepository : ISanPhamRepository
    {
        private readonly XuanTamDbContext _context;
        public SanPhamRepository(XuanTamDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SanPham sanpham)
        {
            _context.SanPhams.Add(sanpham);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var sanpham = await _context.SanPhams.FindAsync(id);
            _context.SanPhams.Remove(sanpham);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SanPham>> GetAllAsync()
        {
            return await _context.SanPhams.Include(p => p.MaDanhMucNavigation).ToListAsync();
        }

        public async Task<SanPham> GetByIdAsync(int id)
        {
            return await _context.SanPhams.Include(p => p.MaDanhMucNavigation).FirstOrDefaultAsync(x => x.MaSanPham == id);
        }

        public async Task UpdateAsync(SanPham sanpham)
        {
            _context.SanPhams.Update(sanpham);
            await _context.SaveChangesAsync();
        }
    }
}
