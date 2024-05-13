using DTOs.DTOs.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductServices productServices { get; set; }
        public ProductsController(IProductServices productServices)
        {
            this.productServices = productServices;
        }

        // ___ 1) Create

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateOrUpdateProductDTO productDTO)
        {
            var createProduct = await productServices.Create(productDTO);
            if (createProduct == null)
            {
                return BadRequest("Not Found!, please try again");
            }
            else
            {
                return Ok(createProduct);
            }
        }

        //____2)GetAll 

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(string? categoryName)
        {
            if (categoryName != null)
            {
                var productsByCatecory = await productServices.GetbyCategory(categoryName);
                if (productsByCatecory != null)
                {
                    return Ok(productsByCatecory);
                }
                else
                {
                    return BadRequest("Not Found!");
                }
            }
            else
            {
                var products = await productServices.GetAll();
                if (products != null)
                {
                    return Ok(products);
                }
                else
                {
                    return BadRequest("Could'nt reach to orders data ..");
                }
            }
        }

        //---3)GetOne

        [HttpGet("{ID:int}")]
        [Authorize]
        public async Task<IActionResult> GetOne(int ID)
        {
            if (ID > 0)
            {
                var product = await productServices.GetOne(ID);
                if (product != null)
                {
                    return Ok(product);
                }
                else
                {
                    return BadRequest("There is no product with this ID");
                }
            }
            else
            {
                return BadRequest("The ID must be greater then 0");
            }
        }

        // ___4)Update 

        [HttpPut("{ID:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(CreateOrUpdateProductDTO productDTO, int ID)
        {
            var updatedProduct = await productServices.Update(productDTO, ID);
            if (updatedProduct != null)
            {
                return Ok(updatedProduct);
            }
            else
            {
                return BadRequest("Not Found!");
            }
        }

        // ___ 5)Delete
        
        [HttpDelete("{ID:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int ID)
        {
            var deleteProduct = await productServices.Delete(ID);
            if (!deleteProduct)
            {
                return BadRequest("Not Found!");
            }
            else
            {
                return Ok(" Deleted successfully !");
            }
        }
    }
}
