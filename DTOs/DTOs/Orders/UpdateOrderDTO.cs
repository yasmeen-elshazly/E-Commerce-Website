using ECommerce.DTOs.Validations;
using ECommerce.Validations;

namespace DTOs.DTOs.Orders
{
    public class UpdateOrderDTO
    {
        [ShippedStatus]
        public string Status { get; set; }
    }
}