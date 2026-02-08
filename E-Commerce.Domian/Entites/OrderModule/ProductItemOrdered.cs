using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Domian.Entites.OrderModule;

public class ProductItemOrdered
{

    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string PictureUrl { get; set; } = null!;

}
