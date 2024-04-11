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

        public async Task<KhachHang> GetByIdAsync(string sdt)
        {
            return await _context.KhachHangs.FindAsync(sdt);
        }

        public Task UpdateAsync(KhachHang khachhang)
        {
            throw new NotImplementedException();
        }
        public bool checkSDT (string sdt)
        {
            return _context.KhachHangs.Count(t=> t.Sdt ==  sdt)>0;
        }
    }
}
