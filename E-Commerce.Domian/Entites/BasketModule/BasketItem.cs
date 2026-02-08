using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domian.Entites.BasketModule
{
    public class BasketItem : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = string.Empty;
        public string PictureName { get; set; } = string.Empty;
        public int Quantity { get; set; }

    }
}
