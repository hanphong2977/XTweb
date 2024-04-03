using XTweb.Models;

namespace XTweb.Repository
{
    public interface IKhachHangRepository
    {
        Task<IEnumerable<KhachHang>> GetAllAsync();
        Task<KhachHang> GetByIdAsync(int id);
        Task AddAsync(KhachHang khachhang);
        Task UpdateAsync(KhachHang khachhang);
        Task DeleteAsync(int id);
    }
}
