namespace E_Commerce.Shared.DTOs.OrderDTOs;

public class ShippingAddressDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string Street { get; set; } = null!;
}

