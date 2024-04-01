using XTweb.Models;

namespace XTweb.Repository
{
    public interface ISanPhamRepository
    {
        Task<IEnumerable<SanPham>> GetAllAsync();
        Task<SanPham> GetByIdAsync(int id);
        Task AddAsync(SanPham sanpham);
        Task UpdateAsync(SanPham sanpham);
        Task DeleteAsync(int id);
    }
}
