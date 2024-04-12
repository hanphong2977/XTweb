using Microsoft.EntityFrameworkCore;
using XTweb.Models;

namespace XTweb.Repository
{
    public class ThanhToanVNPay : IThanhToanVNPayRepository
    {
        private readonly XuanTamDbContext _context;
        public ThanhToanVNPay(XuanTamDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Cthd cthds)
        {
            _context.Cthds.Add(cthds);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Cthd>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Cthd> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
