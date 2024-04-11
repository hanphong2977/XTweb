using Microsoft.EntityFrameworkCore;
using XTweb.Models;

namespace XTweb.Repository
{
    public class LichHenRepository : ILichHenRepository
    {
        private readonly XuanTamDbContext _context;
        public LichHenRepository(XuanTamDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(LichHen lichhen)
        {
            _context.LichHens.Add(lichhen);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var lichhen = await _context.LichHens.FindAsync(id);
            _context.LichHens.Remove(lichhen);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LichHen>> GetAllAsync()
        {
            return await _context.LichHens.Include(p => p.MaKhachHangNavigation)
                                            .Include(p =>p.MaNhanVienNavigation)
                                            .Include(p =>p.MaDichVuNavigation)
                                            .ToListAsync();
        }

        public async Task<LichHen> GetByIdAsync(int id)
        {
            return await _context.LichHens.Include(p => p.MaKhachHangNavigation)
                                            .Include(p => p.MaNhanVienNavigation)
                                            .Include(p => p.MaDichVuNavigation)
                                            .FirstOrDefaultAsync(p => p.MaLichHen == id);
        }

        public async Task UpdateAsync(LichHen lichhen)
        {
            _context.LichHens.Update(lichhen);
            await _context.SaveChangesAsync();
        }
    }
}
