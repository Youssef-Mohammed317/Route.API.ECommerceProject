namespace E_Commerce.Shared.DTOs.OrderDTOs;

public class OrderItemDto
{
    public string ProductName { get; set; } = null!;
    public int ProductId { get; set; }
    public string PictureUrl { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

