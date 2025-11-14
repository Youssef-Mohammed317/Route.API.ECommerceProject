using E_Commerce.Shared.DTOs;
using E_Commerce.Shared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Abstraction.Interfaces
{
    public interface IProductService
    {
        Task<PaginateResult<ProductDTO>> GetAllProductsAsync(ProductQueryParams productQueryParams);
        Task<IEnumerable<ProductBrandDTO>> GetAllProductBrandsAsync();
        Task<IEnumerable<ProductTypeDTO>> GetAllProductTypesAsync();
        Task<ProductDTO> GetProductByIdAsync(Guid id);
    }
}
