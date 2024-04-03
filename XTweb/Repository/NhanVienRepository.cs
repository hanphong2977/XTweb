
using Microsoft.EntityFrameworkCore;
using XTweb.Models;

namespace XTweb.Repository
{
    public class NhanVienRepository : INhanVienRepository
    {
        private readonly XuanTamDbContext _context;
        public NhanVienRepository(XuanTamDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(NhanVien nhanvien)
        {
            _context.NhanViens.Add(nhanvien);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var nhanvien = await _context.NhanViens.FindAsync(id);
            _context.NhanViens.Remove(nhanvien);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<NhanVien>> GetAllAsync()
        {
            return await _context.NhanViens.ToListAsync();
        }

        public async Task<NhanVien> GetByIdAsync(int id)
        {
            return await _context.NhanViens.Include(p => p.TenNhanVien).FirstOrDefaultAsync(p => p.MaNhanVien == id);
        }

        public async Task UpdateAsync(NhanVien nhanvien)
        {
            _context.NhanViens.Update(nhanvien);
            await _context.SaveChangesAsync();
        }
    }
}
