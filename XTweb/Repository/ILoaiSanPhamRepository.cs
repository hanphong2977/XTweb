using XTweb.Models;

namespace XTweb.Repository
{
    public interface ILoaiSanPhamRepository
    {
        Task<IEnumerable<DanhMucSanPham>> GetAllAsync();
        Task<DanhMucSanPham> GetByIdAsync(int id);
        Task AddAsync(DanhMucSanPham danhmucsanpham);
        Task UpdateAsync(DanhMucSanPham danhmucsanpham);
        Task DeleteAsync(int id);
    }
}
