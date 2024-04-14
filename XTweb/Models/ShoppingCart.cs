using XTweb.Models; 
namespace XTweb.Models
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public void AddItem(CartItem item)
        {
            var existingItem = Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
                existingItem.TongTien = (float)existingItem.Quantity * (float)existingItem.Price;
            }
            else
            {
                Items.Add(item);
            }
        }

        public void MinusItem(CartItem item)
        {
            var existingItem = Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if ( existingItem != null )
            {
                if (existingItem.Quantity == 1)
                {
                    RemoveItem(item.ProductId);
                }
                else
                {
                    if (existingItem.Quantity > 1)
                    {
                        existingItem.Quantity -= item.Quantity;
                        existingItem.TongTien = (float)existingItem.Quantity * (float)existingItem.Price;
                    }
                }
            }
        
        }
      
        public void RemoveItem(int productId)
        {
            Items.RemoveAll(i => i.ProductId == productId);
        }

    }
}
