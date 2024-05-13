using DTOs.DTOs.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;

namespace Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryServices categoryServices;
        public CategoriesController(ICategoryServices categoryServices)
        {
            this.categoryServices = categoryServices;
        }

        // ___________________________  Create ___________________________
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateOrUpdateCategoryDTO categoryDTO)
        {
            var createCategory = await categoryServices.Create(categoryDTO);
            if (createCategory != null)
            {
                return Ok(createCategory);
            }
            else
            {
                return BadRequest(" already exist !");
            }
        }

        // ___________________________  GetAll ___________________________
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var categories = await categoryServices.GetAll();
            if (categories == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(categories);
            }
        }
        //-----------------------  GetOne-----------------------------

        [HttpGet("{ID:int}")]
        [Authorize]
        public async Task<IActionResult> GetOne(int ID)
        {
            if (ID > 0)
            {
                var categories = await categoryServices.GetOne(ID);
                if (categories == null)
                {
                    return BadRequest("Not Found");
                }
                else
                {
                    return Ok(categories);
                }
            }
            else
            {
                return BadRequest(" ID must be greater then 0 ");
            }
        }

        // ___________________________ Update ___________________________
        [HttpPut("{ID:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(CreateOrUpdateCategoryDTO categoryDTO, int ID)
        {
            var getCategore = await categoryServices.GetOne(ID);
            if (getCategore == null)
            {
                return BadRequest("Not Found");
            }
            else
            {
                await categoryServices.Update(categoryDTO, ID);
                return Ok("Updated Successfully!");
            }
        }

        // ___________________________  Delete ___________________________
        [HttpDelete("{ID:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int ID)
        {
            var deleteCategory = await categoryServices.Delete(ID);
            if (deleteCategory)
            {
                return Ok(" Deleted successfully !");
            }
            else
            {
                return BadRequest("Not Found");
            }
        }
    }
}
