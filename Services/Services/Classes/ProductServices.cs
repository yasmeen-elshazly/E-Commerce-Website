using AutoMapper;
using DTOs.DTOs.Products;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Contract;
using Services.Services.Interface;

namespace Services.Services.Classes
{
    public class ProductServices : IProductServices
    {
        protected IProductRepository productRepository;
        protected ICategoryRepository categoryRepository;
        protected IMapper mapper;
        public ProductServices(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        // _________________________ 1. Create a new Product _________________________
        public async Task<GetCreatedorUpdatedProductDTO> Create(CreateOrUpdateProductDTO productDTO)
        {
            var findCategory = await categoryRepository.GetOneByID(productDTO.CategoryID);
            if (findCategory != null)
            {
                productDTO.Name = char.ToUpper(productDTO.Name[0]) + productDTO.Name.Substring(1).ToLower();
                Product newProduct = mapper.Map<Product>(productDTO);
                await productRepository.Create(newProduct);
                var getLastProduct = await productRepository.GetLastProduct();
                var printCreatedProduct = mapper.Map<GetCreatedorUpdatedProductDTO>(getLastProduct);
                return printCreatedProduct;
            }
            else
            {
                return null;
            }
        }
        // ___________________________ 2. Get All Products ___________________________
        public async Task<List<GetAllProductsDTO>> GetAll()
        {
            var products = await productRepository.GetAll();
            var getAllProducts = products.Include(p => p.Category).Select(p => mapper.Map<GetAllProductsDTO>(p));
            var productsList = getAllProducts.ToList();
            return productsList;
        }
        
        // ___________________________ 3.Get One Product ___________________________
        public async Task<GetAllProductsDTO> GetOne(int ID)
        {
            var product = await productRepository.GetOneByID(ID);
            if (product != null)
            {
                var mappedProduct = mapper.Map<GetAllProductsDTO>(product);
                return mappedProduct;
            }
            else
            {
                return null;
            }
        }
        // ________________ 4.Get Products by category name ________________
        public async Task<List<GetProductsByCategoryDTO>> GetbyCategory(string categoryName)
        {
            var findCategory = await categoryRepository.GetOneByName(categoryName);
            if (findCategory)
            {
                var allProducts = await productRepository.GetAll();
                var selectProducts = allProducts
                    .Include(p => p.Category)
                    .Where(p => p.Category.Name.ToLower() == categoryName.ToLower())
                    .Select(c => mapper.Map<GetProductsByCategoryDTO>(c)).ToList();
                return selectProducts;
            }
            else
            {
                return null;
            }
        }
        // __________________________ 5.Update a Products ___________________________
        public async Task<GetCreatedorUpdatedProductDTO> Update(CreateOrUpdateProductDTO productDTO, int ID)
        {
            var product = await productRepository.GetOneByID(ID);
            if (product == null)
            {
                return null;
            }
            else
            {
                var findCategory = await categoryRepository.GetOneByID(productDTO.CategoryID);
                if (findCategory == null)
                {
                    return null;
                }
                else
                {
                    productDTO.Name = char.ToUpper(productDTO.Name[0]) + productDTO.Name.Substring(1).ToLower();
                    mapper.Map(productDTO, product);
                    product.Status = "Updated";
                    await productRepository.Update();
                    var getUpdatedProduct = mapper.Map<GetCreatedorUpdatedProductDTO>(product);
                    return getUpdatedProduct;
                }
            }
        }
        // __________________________ 6.Delete a Products ___________________________
        public async Task<bool> Delete(int ID)
        {
            var product = await productRepository.GetOneByID(ID);
            if (product == null)
            {
                return false;
            }
            else
            {
                await productRepository.Delete(product);
                return true;
            }
        }
    }
}
