using XTweb.Models;

namespace XTweb.Repository
{
    public interface IHoaDonDichVuRepository
    {
        Task<IEnumerable<HoaDonDichVu>> GetAllAsync();
        Task<HoaDonDichVu> GetByIdAsync(int id);
        Task AddAsync(HoaDonDichVu hoadondichvu);
        Task UpdateAsync(HoaDonDichVu hoadondichvu);
        Task DeleteAsync(int id);
    }
}