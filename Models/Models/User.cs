using Microsoft.AspNetCore.Identity;

namespace Models.Models
{
    public class User : IdentityUser
    {
        public string? Address { get; set; }

        public List<Order> Orders { get; set; }
    }
}
