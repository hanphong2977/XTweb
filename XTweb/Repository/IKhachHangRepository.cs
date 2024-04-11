using XTweb.Models;

namespace XTweb.Repository
{
    public interface IKhachHangRepository
    {
        Task<IEnumerable<KhachHang>> GetAllAsync();
        Task<KhachHang> GetByIdAsync(string sdt);
        Task AddAsync(KhachHang khachhang);
        Task UpdateAsync(KhachHang khachhang);
        Task DeleteAsync(int id);
    }
}
