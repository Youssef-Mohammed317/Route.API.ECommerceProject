namespace E_Commerce.Shared.DTOs.OrderDTOs;

public class OrderToReturnDto
{
    public int Id { get; set; }
    public string UserEmail { get; set; } = null!;
    public string BuyerEmail => UserEmail;
    public DateTimeOffset OrderDate { get; set; }
    public ShippingAddressDto Address { get; set; } = null!;
    public ShippingAddressDto ShipToAddress => Address;
    public string DeliveryMethod { get; set; } = null!;
    public int? DeliveryMethodId { get; set; } = null!;
    public string OrderStatus { get; set; } = null!;
    public string Status => OrderStatus;
    public ICollection<OrderItemDto> Items { get; set; } = [];
    public decimal SubTotal { get; set; }
    public decimal Total { get; set; }

    public string PaymentIntentId { get; set; } = string.Empty;

}

