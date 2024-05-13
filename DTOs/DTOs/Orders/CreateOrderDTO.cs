using DTOs.DTOs.OrderProducts;
using System.Text.Json.Serialization;

namespace DTOs.DTOs.Orders
{
    public class CreateOrderDTO
    {
        public List<GetOrCreateOrderProductDTO> Products { get; set; }
    }
}
