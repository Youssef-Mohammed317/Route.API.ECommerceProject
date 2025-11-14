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
    public class ProductWithBrandAndTypeSpecification : BaseSpecification<Product>
    {
        public ProductWithBrandAndTypeSpecification(ProductQueryParams productQueryParams) :
            base(ProductSpecificationHelper.GetProductCriteria(productQueryParams))
        {
            AddInclude(e => e.ProductBrand);
            AddInclude(e => e.ProductType);

            switch (productQueryParams.Sort)
            {
                case ProductSortOptions.NameAsc:
                    AddOrderBy(e => e.Name);
                    break;
                case ProductSortOptions.NameDesc:
                    AddOrderByDescending(e => e.Name);
                    break;
                case ProductSortOptions.PriceAsc:
                    AddOrderBy(e => e.Price);
                    break;
                case ProductSortOptions.PriceDesc:
                    AddOrderByDescending(e => e.Price);
                    break;
                default:
                    break;
            }

            ApplyPagination(productQueryParams.PageSize, productQueryParams.Page);
        }
        public ProductWithBrandAndTypeSpecification(Guid id) : base(e => e.Id == id)
        {
            AddInclude(e => e.ProductBrand);
            AddInclude(e => e.ProductType);
        }
    }
}
