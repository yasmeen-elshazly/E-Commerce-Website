namespace DTOs.DTOs.Products
{
    public class GetProductsByCategoryDTO
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string? Description { get; set; }
    }
}
