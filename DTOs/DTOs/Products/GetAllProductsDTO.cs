using DTOs.DTOs.Category;

namespace DTOs.DTOs.Products
{
    public class GetAllProductsDTO
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string? Description { get; set; }
       
        public GetAllCategoryDTO Category { get; set; }
    }
}
