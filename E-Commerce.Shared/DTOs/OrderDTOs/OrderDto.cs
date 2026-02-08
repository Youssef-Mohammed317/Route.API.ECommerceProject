using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DTOs.OrderDTOs;

public class OrderDto
{
    public Guid BasketId { get; set; }
    public int DeliveryMethodId { get; set; }

    public ShippingAddressDto? Address { get; set; }
    public ShippingAddressDto? ShipToAddress => Address;

}

