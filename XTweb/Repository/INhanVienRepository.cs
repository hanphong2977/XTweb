using XTweb.Models;

namespace XTweb.Repository
{
    public interface INhanVienRepository
    {
        Task<IEnumerable<NhanVien>> GetAllAsync();
        Task<NhanVien> GetByIdAsync(int id);
        Task AddAsync(NhanVien nhanvien);
        Task UpdateAsync(NhanVien nhanvien);
        Task DeleteAsync(int id);
    }
}
