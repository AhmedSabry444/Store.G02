using AutoMapper;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Products;
using Store.G02.Domain.Exceptions.NotFound;
using Store.G02.Services.Abstractions.Products;
using Store.G02.Services.Specifications;
using Store.G02.Services.Specifications.Products;
using Store.G02.Shared;
using Store.G02.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Products 
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<TypeResponse> GetAllBrandsAsync(int id)
        {
            var brands = await _unitOfWork.GetRepository<int, ProductBrand>().GetAllAsync();
            var result = _mapper.Map<TypeResponse>(brands);
            return result;  
        }

        public Task GetAllBrandsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<PaginationResponse<ProductResponse>> GetAllProductsAsync(ProductQueryParameters parameters)
        {

            //var spec = new BaseSpecifications<int, Product>(null);
            //spec.Include.Add(p => p.Brand);
            //spec.Include.Add(p => p.Type); 
            var spec = new ProductsWithBrandAndTypeSpecifications(parameters);


            var products = await _unitOfWork.GetRepository<int , Product>().GetAllAsync(spec);
            var result = _mapper.Map<IEnumerable<ProductResponse>>(products);

            var specCount = new ProductsCountSpecifications(parameters);
            var count = await _unitOfWork.GetRepository<int,Product>().CountAsync(specCount);

            return new PaginationResponse<ProductResponse>(parameters.PageSize,parameters.PageSize,count,result);
        }

        public async Task<TypeResponse> GetAllTypesAsync(int id)
        {
            var types = await _unitOfWork.GetRepository<int, ProductType>().GetAllAsync();
            var result = _mapper.Map<TypeResponse>(types);
            return result;
        }

        public Task GetAllTypesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ProductResponse> GetProductByIdAsync(int id)
        {
            var spec = new ProductsWithBrandAndTypeSpecifications(id);
            var product = await _unitOfWork.GetRepository<int, Product>().GetAsync(spec);

            if (product is null) throw new ProductNotFoundException(id);
            

            var result = _mapper.Map<ProductResponse>(product);
            return result; 
        }

        Task<IEnumerable<BrandResponse>> IProductService.GetAllBrandsAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<TypeResponse>> IProductService.GetAllTypesAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
