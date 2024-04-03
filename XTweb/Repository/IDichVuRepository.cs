using XTweb.Models;

namespace XTweb.Repository
{
    public interface IDichVuRepository
    {
        Task<IEnumerable<DichVu>> GetAllAsync();
        Task<DichVu> GetByIdAsync(int id);
        Task AddAsync(DichVu dichvu);
        Task UpdateAsync(DichVu dichvu);
        Task DeleteAsync(int id);
    }
}
