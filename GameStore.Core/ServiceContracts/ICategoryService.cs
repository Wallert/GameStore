using GameStore.Core.Entities;

namespace GameStore.Core.ServiceContracts
{
    public interface ICategoryService
    {
        public Task<bool> IsExistAsync(int id);
        public Task<Categories> GetCategoryAsync(int id);
        public Task<ICollection<Categories>> GetAllCategoriesAsync();
        public Task<Categories> CreateCategoryAsync(Categories category);
        public Task<Categories> UpdateCategoryAsync(Categories category);
        public Task DeleteCategoryAsync(int id);
    }
}
