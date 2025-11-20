using Store.G02.Shared;
using Store.G02.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Abstractions.Products
{
    public interface IProductService
    {
       Task<PaginationResponse<ProductResponse>> GetAllProductsAsync(ProductQueryParameters parameters);
       Task<ProductResponse> GetProductByIdAsync(int id);
       Task<IEnumerable<BrandResponse>> GetAllBrandsAsync (int id);
       Task<IEnumerable<TypeResponse>> GetAllTypesAsync(int id);
       
    }
}
