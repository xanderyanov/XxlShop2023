namespace XxlStore.Models
{
    public class Cart : Father
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();
        
        public virtual void AddItem(Product product, int quantity)
        {
            CartLine line = Lines
            .Where(p => p.Product.Id == product.Id)
            .FirstOrDefault();
            if (line == null) {
                Lines.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            } else {
                line.Quantity += quantity;
            }
        }
        
        public virtual void RemoveLine(Product product) =>
            Lines.RemoveAll(l => l.Product.Id == product.Id);
        
        public double ComputeTotalValue() =>
            Lines.Sum(e => e.Product.DiscountPrice * e.Quantity);
        
        public virtual void Clear() => Lines.Clear();
    }
    
    public class CartLine
    {
        public int CartLineID { get; set; }
        public Product Product { get; set; } = new();
        public int Quantity { get; set; }
    }
}
