using DTOs.DTOs.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Services.Services.Interface;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrderServices orderServices { get; set; }
        public OrdersController(IOrderServices orderServices)
        {
            this.orderServices = orderServices;
        }

        //---1)Create
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create(CreateOrderDTO orderDTO)
        {
            var customerIDClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DenyOnlySid);
            if (customerIDClaims == null)
            {
                return Unauthorized(" Not found!! ");
            }
            var customerID = customerIDClaims.Value;
            var order = await orderServices.Create(orderDTO, customerID);
            if (order == null)
            {
                return BadRequest(" Can't be found, Try again !..");
            }
            else
            {
                return Ok(order);
            }
        }

        // ___2)GetAll
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            List<GetAllOrdersDTO> orders;
            var roleClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaims == null)
            {
                return Unauthorized("Not Found!");
            }
            string userRole = roleClaims.Value;
            switch (userRole)
            {
                case "User":
                    var userIDClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DenyOnlySid);
                    if (userIDClaims == null)
                    {
                        return Unauthorized("Not Found!");
                    }
                    string userID = userIDClaims.Value;
                    orders = await orderServices.GetAll(userID);
                    break;
                case "Admin":
                    orders = await orderServices.GetAll(null);
                    break;
                default:
                    return Unauthorized("The user unauthorized to use this action");
            }
            if (orders != null)
            {
                return Ok(orders);
            }
            else
            {
                return BadRequest("Could'nt reach to orders data ..");
            }
        }
        //---3)GetOne
        [HttpGet("{ID:int}")]
        [Authorize]
        public async Task<IActionResult> GetOne(int ID)
        {
            GetAllOrdersDTO order;
            var roleClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaims == null)
            {
                return Unauthorized("Not Found!");
            }
            string userRole = roleClaims.Value;
            switch (userRole)
            {
                case "User":
                    var userIDClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DenyOnlySid);
                    if (userIDClaims == null)
                    {
                        return Unauthorized("Not Found!");
                    }
                    string userID = userIDClaims.Value;
                    order = await orderServices.GetOne(ID, userID);
                    break;
                case "Admin":
                    order = await orderServices.GetOne(ID, null);
                    break;
                default:
                    return Unauthorized("The user unauthorized to use this action ");
            }
            if (order != null)
            {
                return Ok(order);
            }
            else
            {
                return BadRequest("There is no order with this ID");
            }
        }

        // ___3) Update

        [HttpPut("{ID:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UpdateOrderDTO orderDTO, int ID)
        {
            var updateOrder = await orderServices.Update(orderDTO, ID);
            if (updateOrder == null)
            {
                return BadRequest("There is no order with this ID");
            }
            else
            {
                return Ok(updateOrder);
            }
        }

        // ___4) Delete 

        [HttpDelete("{ID:int}")]
        [Authorize]
        public async Task<IActionResult> Delete(int ID)
        {
            bool deleteOrder;
            var roleClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaims == null)
            {
                return Unauthorized("Not Found!");
            }
            string userRole = roleClaims.Value;
            switch (userRole)
            {
                case "User":
                    var userIDClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DenyOnlySid);
                    if (userIDClaims == null)
                    {
                        return Unauthorized("Not Found!");
                    }
                    string userID = userIDClaims.Value;
                    deleteOrder = await orderServices.Delete(ID, userID);
                    break;
                case "Admin":
                    deleteOrder = await orderServices.Delete(ID, null);
                    break;
                default:
                    return Unauthorized("The user unauthorized to use this action ");
            }
            if (!deleteOrder)
            {
                return BadRequest("There is no order with this ID ");
            }
            else
            {
                return Ok(" Deleted successfully !");
            }
        }
    }
}
