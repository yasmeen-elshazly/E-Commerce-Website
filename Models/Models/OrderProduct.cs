namespace Models.Models
{
    public class OrderProduct
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int ProductQuantity { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
