using System.Security.Claims;

namespace Presentation.Controllers
{
    public class User
    {
        public ClaimsIdentity? Id;
        internal string Email;
        public int ID { get; set; }
        public object UserName { get; internal set; }
        public bool EmailConfirmed { get; internal set; }
        public string PhoneNumber { get; internal set; }
        public string Address { get; internal set; }
    }
}