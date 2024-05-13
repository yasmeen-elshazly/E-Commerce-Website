using DTOs.DTOs.Customer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public UserManager<User> userManager { get; set; }
        public RoleManager<IdentityRole> roleManager { get; set; }
        public IConfiguration configuration { get; set; }
        public AccountController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCustomer(RegisterDTO registerDTO, string role)
        {
            var findCustomer = await userManager.FindByEmailAsync(registerDTO.Email);
            if (findCustomer != null)
            {
                return BadRequest(" already exists! ");
            }
            else
            {
                User newCustomer = new User()
                {
                    UserName = char.ToUpper(registerDTO.UserName[0]) + registerDTO.UserName.Substring(1),
                    Email = registerDTO.Email,
                    EmailConfirmed = registerDTO.Email == registerDTO.EmailConfirmed,
                    PhoneNumber = registerDTO.PhoneNumber,
                    Address = registerDTO.Address,
                };
                if (newCustomer.EmailConfirmed)
                {
                    await userManager.CreateAsync(newCustomer, registerDTO.Password);

                    var getRole = await roleManager.FindByNameAsync(role);
                    if (getRole == null)
                    {
                        return BadRequest("Invalid Role");
                    }
                    else
                    {
                        await userManager.AddToRoleAsync(newCustomer, getRole.Name);
                        return Ok($" '{newCustomer.UserName}' signed up successfully.....");
                    }
                }
                else
                {
                    return BadRequest("Confirmation Error!!");
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> LoginCustomer(LoginDTO loginDTO)
        {
            var customer = await userManager.FindByNameAsync(loginDTO.Name);
            if (customer == null)
            {
                return BadRequest("Sorry, Not found !");
            }
            else
            {
                if (!await userManager.CheckPasswordAsync(customer, loginDTO.Password))
                {
                    return BadRequest("Invalid, Try again");
                }
                else
                {
                    List<Claim> claims = new List<Claim>()
                    {
                       new Claim(ClaimTypes.DenyOnlySid, customer.Id.ToString())
                    };
                    var customerRoles = await userManager.GetRolesAsync(customer);
                    foreach (var role in customerRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
                    var setToken = new JwtSecurityToken(
                        expires: DateTime.Now.AddHours(1),
                        claims: claims,
                        signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256),
                        issuer: configuration["JWT:issuer"],
                        audience: configuration["JWT:audience"]);
                    var token = new JwtSecurityTokenHandler().WriteToken(setToken);
                    return Ok(token);
                }
            }
        }
    }
}
