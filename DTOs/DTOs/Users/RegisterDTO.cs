using ECommerce.Validations;

namespace DTOs.DTOs.Customer
{
    public class RegisterDTO
    {
        public string UserName { get; set; }
        [ContainValidation]
        public string Email { get; set; }
        public string EmailConfirmed { get; set; }
        public string PhoneNumber {  get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
    }
}
