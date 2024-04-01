using Microsoft.EntityFrameworkCore;
using XTweb.Models;

namespace XTweb.Repository
{
    public class LoaiSanPhamRepository : ILoaiSanPhamRepository
    {
        private readonly XuanTamDbContext _context;
        public LoaiSanPhamRepository(XuanTamDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(DanhMucSanPham danhmucsanpham)
        {
            _context.DanhMucSanPhams.Add(danhmucsanpham);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var danhmucsanpham = await _context.DanhMucSanPhams.FindAsync(id);
            _context.DanhMucSanPhams.Remove(danhmucsanpham);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DanhMucSanPham>> GetAllAsync()
        {
            return await _context.DanhMucSanPhams.Include(p => p.SanPhams).ToListAsync();
        }

        public async Task<DanhMucSanPham> GetByIdAsync(int id)
        {
            return await _context.DanhMucSanPhams.Include(p => p.TenDanhMuc).FirstOrDefaultAsync(p => p.MaDanhMuc == id);
        }

        public async Task UpdateAsync(DanhMucSanPham danhmucsanpham)
        {
            _context.DanhMucSanPhams.Update(danhmucsanpham);
            await _context.SaveChangesAsync();
        }
    }
}
