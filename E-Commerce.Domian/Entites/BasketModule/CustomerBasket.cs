using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domian.Entites.BasketModule
{
    public class CustomerBasket : BaseEntity<Guid>
    {
        public ICollection<BasketItem> Items { get; set; } = new List<BasketItem>();
        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal? ShippingPrice { get; set; }

    }
}
