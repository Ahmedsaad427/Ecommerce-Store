using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Services.Specifictions;
using Shared;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork , IMapper mapper) : IProductService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParameter productSpecifications)
        {

            var spec = new ProductWithBrandsAndTypesSpeifications(productSpecifications);

            // Get All Products Through ProductRepository
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);

            // Mapping IEnumerable<Product> to IEnumerable<ProductResultDto> : AutoMapper

            var result = mapper.Map<IEnumerable<ProductResultDto>>(products);
            return result;

        }
        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {

            var spec = new ProductWithBrandsAndTypesSpeifications(id);

            // Get Product By Id Through ProductRepository (await the async call)
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(spec);

            // Check if product is found
            if (product == null)
            {
                return null;  // Return null if product is not found
            }
            else
            {
                // Mapping Product to ProductResultDto using AutoMapper
                var result = mapper.Map<ProductResultDto>(product);
                return result;  
            }
        }

        public  async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var Brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            // Mapping IEnumerable<ProductBrand> to IEnumerable<BrandResultDto> : AutoMapper
            var result = mapper.Map<IEnumerable<BrandResultDto>>(Brands);
            return result;
        }



        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            // Mapping IEnumerable<ProductType> to IEnumerable<TypeResultDto> : AutoMapper
            var result = mapper.Map<IEnumerable<TypeResultDto>>(Types);
            return result;
        }

       
    }
}
