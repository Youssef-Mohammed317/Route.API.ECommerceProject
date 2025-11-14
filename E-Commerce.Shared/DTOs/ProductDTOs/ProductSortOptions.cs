using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DTOs.ProductDTOs
{
    [Flags]
    public enum ProductSortOptions
    {
        NameAsc = 1, NameDesc = 2,
        PriceAsc = 3, PriceDesc = 4,
    }
}
