namespace XTweb.Models
{
    public class IndexViewModel
    {
        public required IEnumerable<DichVu> DanhSachDichVu { get; set; }
        public required IEnumerable<NhanVien> DanhSachNhanVien { get; set; }
        public required IEnumerable<LichHen> LichHen { get; set;}
    }
}
