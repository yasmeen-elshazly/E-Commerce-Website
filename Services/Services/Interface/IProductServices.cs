using DTOs.DTOs.Products;

namespace Services.Services.Interface
{
    public interface IProductServices
    {
        public Task<GetCreatedorUpdatedProductDTO> Create(CreateOrUpdateProductDTO productDTO);
        public Task<List<GetAllProductsDTO>> GetAll();
        public Task<List<GetProductsByCategoryDTO>> GetbyCategory(string categoryName);
        public Task<GetAllProductsDTO> GetOne(int ID);
        public Task<GetCreatedorUpdatedProductDTO> Update(CreateOrUpdateProductDTO productDTO, int ID);
        public Task<bool> Delete(int ID);
    }
}
