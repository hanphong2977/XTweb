using Microsoft.EntityFrameworkCore;
using XTweb.Models;

namespace XTweb.Repository
{
    public class HoaDonDichVuRepository : IHoaDonDichVuRepository
    {
        private readonly XuanTamDbContext _context;
        public HoaDonDichVuRepository(XuanTamDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(HoaDonDichVu hoadondichvu)
        {
            _context.HoaDonDichVus.Add(hoadondichvu);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var hoadondichvu = await _context.HoaDonDichVus.FindAsync(id);
            _context.HoaDonDichVus.Remove(hoadondichvu);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<HoaDonDichVu>> GetAllAsync()
        {
            return await _context.HoaDonDichVus.Include(p => p.MaLichHenNavigation).ToListAsync();
        }

        public async Task<HoaDonDichVu> GetByIdAsync(int id)
        {
            return await _context.HoaDonDichVus.Include(p => p.MaLichHenNavigation).FirstOrDefaultAsync(x => x.MaHoaDon == id);
        }

        public async Task UpdateAsync(HoaDonDichVu hoadondichvu)
        {
            _context.HoaDonDichVus.Update(hoadondichvu);
            await _context.SaveChangesAsync();
        }
    }
}
