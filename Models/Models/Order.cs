namespace Models.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public string Status { get; set; }
        public User Customer { get; set; }
        public List<OrderProduct> Products { get; set; }
    }
}
