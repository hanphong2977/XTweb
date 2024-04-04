using X.PagedList;

namespace XTweb.Models
{
    public class IndexViewModel
    {
        public  IEnumerable<DichVu> DanhSachDichVu { get; set; }
        public  IEnumerable<NhanVien> DanhSachNhanVien { get; set; }
        public  IEnumerable<LichHen> LichHen { get; set;}

        public  PagedList<DichVu> pagelstDichVu { get; set; }

        public LichHenViewModel LichHenViewModel { get; set;}
    }
}
