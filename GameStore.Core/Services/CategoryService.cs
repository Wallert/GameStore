using GameStore.Core.Entities;
using GameStore.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Core.Services
{
    internal class CategoryService : ICategoryService
    {
        public Task<Categories> CreateCategoryAsync(Categories category)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Categories>> GetAllCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Categories> GetCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Categories> UpdateCategoryAsync(Categories category)
        {
            throw new NotImplementedException();
        }
    }
}
