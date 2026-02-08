using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DTOs.BasketDTOs
{
    public class BasketDTO
    {
        public Guid Id { get; set; }
        public ICollection<BasketItemDTO> Items { get; set; } = new List<BasketItemDTO>();
        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal? ShippingPrice { get; set; }
    }
}
