using XTweb.Models;

namespace XTweb.Repository
{
    public interface ILichHenRepository
    {
        Task<IEnumerable<LichHen>> GetAllAsync();
        Task<LichHen> GetByIdAsync(int id);
        Task AddAsync(LichHen lichhen);
        Task UpdateAsync(LichHen lichhen);
        Task DeleteAsync(int id);
    }
}
