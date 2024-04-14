namespace XTweb.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string? ImageUrl { get; set; }
        public string? MotaSanPham { get; set; }

        public float TongTien { get; set; }
    }
}
