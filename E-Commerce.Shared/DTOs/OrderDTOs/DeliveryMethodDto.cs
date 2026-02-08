using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DTOs.OrderDTOs;

public class DeliveryMethodDto
{
    public string ShortName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string DeliveryTime { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal Cost => Price;
    public int Id { get; set; }
}
