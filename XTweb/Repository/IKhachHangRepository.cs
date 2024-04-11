using XTweb.Models;

namespace XTweb.Repository
{
    public interface IKhachHangRepository
    {
        Task<IEnumerable<KhachHang>> GetAllAsync();
        Task<KhachHang> GetByIdAsync(int id);
        Task AddAsync(KhachHang khachhang);
        Task UpdateAsync(KhachHang khachhang);
        Task <KhachHang> GetBySdtAsync(string sdt);
        Task DeleteAsync(int id);
    }
}
