using AutoMapper;
using DTOs.DTOs.Category;
using Models.Models;
using Repository.Contract;
using Services.Services.Interface;

namespace Services.Services.Classes
{
    public class CategoryServices : ICategoryServices
    {
        protected ICategoryRepository categoryrepository;
        protected IMapper mapper;
        public CategoryServices(ICategoryRepository categoryrepository, IMapper mapper)
        {
            this.categoryrepository = categoryrepository;
            this.mapper = mapper;
        }
        // _________________________ 1. Create new Category _________________________
        public async Task<GetAllCategoryDTO> Create(CreateOrUpdateCategoryDTO categoryDTO)
        {
            var findCategory = await categoryrepository.GetOneByName(categoryDTO.Name);

            if (findCategory)
            {
                return null;
            }
            else
            {
                var createCategory = mapper.Map<Category>(categoryDTO);
                await categoryrepository.Create(createCategory);
                var categories = await categoryrepository.GetAll();
                var findCreatedCategory = categories.OrderByDescending(o => o.CategoryID).FirstOrDefault();
                var mappingCategory = mapper.Map<GetAllCategoryDTO>(findCreatedCategory);
                return mappingCategory;
            }
        }
        // ___________________________2.  Get All ___________________________
        public async Task<List<GetAllCategoryDTO>> GetAll()
        {
            var categories = await categoryrepository.GetAll();
            var mappedCategories = categories.Select(c => mapper.Map<GetAllCategoryDTO>(c));
            var categoriesList = mappedCategories.ToList();
            return categoriesList;
        }
        // ___________________________ 3. Get One Category ___________________________
        public async Task<GetAllCategoryDTO> GetOne(int ID)
        {
            var findCategory = await categoryrepository.GetOneByID(ID);
            if (findCategory != null)
            {
                var mappedCategory = mapper.Map<GetAllCategoryDTO>(findCategory);
                return mappedCategory;
            }
            else
            {
                return null;
            }
        }
        // __________________________ 4.Update Category ___________________________
        public async Task<bool> Update(CreateOrUpdateCategoryDTO categoryDTO, int ID)
        {
            var findCategory = await categoryrepository.GetOneByID(ID);
            if (findCategory == null)
            {
                return false;
            }
            else
            {
                mapper.Map(categoryDTO, findCategory);
                await categoryrepository.Update();
                return true;
            }
        }
        // __________________________5. Delete Category ___________________________
        public async Task<bool> Delete(int ID)
        {
            var findCategory = await categoryrepository.GetOneByID(ID);
            if (findCategory == null)
            {
                return false;
            }
            else
            {
                await categoryrepository.Delete(findCategory);
                return true;
            }
        }
    }
}
