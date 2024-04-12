using XTweb.Models;

namespace XTweb.Repository
{
    public class ThanhToanRepostiroy : IThanhToanVNPayRepository
    {
            private readonly XuanTamDbContext _context;
            public ThanhToanRepostiroy(XuanTamDbContext context)
            {
                _context = context;
            }
            public async Task AddAsync(Cthd cthds)
            {
                    await _context.Cthds.AddAsync(cthds);
                          _context.SaveChanges();
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

