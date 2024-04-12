using Microsoft.EntityFrameworkCore;
using XTweb.Models;
namespace XTweb.Repository
{
    public interface IThanhToanVNPayRepository
    {
        Task<IEnumerable<Cthd>> GetAllAsync();
        Task<Cthd> GetByIdAsync(int id);
        Task AddAsync(Cthd cthds);
    }
}
