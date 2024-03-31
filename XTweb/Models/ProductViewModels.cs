namespace XTweb.Models
{
    public class ProductViewModels
    {
        public required List<SanPham> sanPhams_DauGoi { get; set; }
        public required List<SanPham> sanPhams_SuaTam { get; set; }
        public required List<SanPham> sanPhams_SapVotToc { get; set; } 
        public required List<SanPham> sanPhams_GelTaoKieuToc { get; set; } 
        public required List<SanPham> sanPhams_NuocHoa { get; set; } 
        public required List<SanPham> sanPhams_XitKhuMui { get; set; }
    }
}
