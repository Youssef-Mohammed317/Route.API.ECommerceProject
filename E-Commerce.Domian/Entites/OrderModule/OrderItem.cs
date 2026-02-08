namespace E_Commerce.Domian.Entites.OrderModule;

public class OrderItem : BaseEntity<int>
{
    public ProductItemOrdered Product { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
