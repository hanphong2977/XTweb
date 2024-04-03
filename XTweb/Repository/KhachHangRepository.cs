using Microsoft.EntityFrameworkCore;
using XTweb.Models;

namespace XTweb.Repository
{
    public class KhachHangRepository : IKhachHangRepository
    {

        private readonly XuanTamDbContext _context;
        public KhachHangRepository(XuanTamDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(KhachHang khachhang)
        {
            _context.KhachHangs.Add(khachhang);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var khachhang = await _context.KhachHangs.FindAsync(id);
            _context.KhachHangs.Remove(khachhang);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<KhachHang>> GetAllAsync()
        {
            return await _context.KhachHangs.ToListAsync();
        }

        public Task<KhachHang> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(KhachHang khachhang)
        {
            throw new NotImplementedException();
        }
    }
}
