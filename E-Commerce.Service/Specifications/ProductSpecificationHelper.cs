using E_Commerce.Domian.Entites.ProductModule;
using E_Commerce.Shared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Implementation.Specifications
{
    public static class ProductSpecificationHelper
    {
        public static Expression<Func<Product, bool>> GetProductCriteria(ProductQueryParams productQueryParams)
        {
            return e =>
            (
            (!productQueryParams.TypeId.HasValue || productQueryParams.TypeId.Value == e.ProductTypeId)
                    &&
            (!productQueryParams.BrandId.HasValue || productQueryParams.BrandId.Value == e.ProductBrandId)
                    &&
            (string.IsNullOrEmpty(productQueryParams.Search) || e.Name.ToLower().Contains(productQueryParams.Search.ToLower()))
            );
        }
    }
}
