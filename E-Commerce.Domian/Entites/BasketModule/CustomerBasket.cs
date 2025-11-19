using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domian.Entites.BasketModule
{
    public class CustomerBasket
    {
        public Guid Id { get; set; }
        public ICollection<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}
