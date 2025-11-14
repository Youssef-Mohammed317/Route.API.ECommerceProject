using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DTOs.ProductDTOs
{
    public class ProductQueryParams
    {
        public string? Search { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? TypeId { get; set; }
        public ProductSortOptions? Sort { get; set; }
        public int PageSize { get; set; } = 10;
        public int Page { get; set; } = 1;
    }
}
