using GameStore.Core.Entities;

namespace GameStore.Core.ServiceContracts
{
    public interface ICategoryService
    {
        public Task<bool> IsExistAsync(int id);
        public Task<Categories> GetCategoryAsync(int id);
        public Task<ICollection<Categories>> GetAllCategoriesAsync();
        public Task CreateCategoryAsync(Categories category);
        public Task UpdateCategoryAsync(Categories category);
        public Task DeleteCategoryAsync(int id);
    }
}
