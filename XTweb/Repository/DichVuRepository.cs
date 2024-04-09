using Microsoft.EntityFrameworkCore;
using XTweb.Models;

namespace XTweb.Repository
{
    public class DichVuRepository : IDichVuRepository
    {
        private readonly XuanTamDbContext _context;
        public DichVuRepository(XuanTamDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(DichVu dichvu)
        {
            _context.DichVus.Add(dichvu);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var dichvu = await _context.DichVus.FindAsync(id);
            _context.DichVus.Remove(dichvu);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DichVu>> GetAllAsync()
        {
            return await _context.DichVus.ToListAsync();
        }

        public async Task<DichVu> GetByIdAsync(int id)
        {
            return await _context.DichVus.Include(p => p.TenDichVu).FirstOrDefaultAsync(p => p.MaDichVu == id);
        }

        public async Task UpdateAsync(DichVu dichvu)
        {
            _context.DichVus.Update(dichvu);
            await _context.SaveChangesAsync();
        }
    }
}
