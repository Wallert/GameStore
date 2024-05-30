using GameStore.Core.Entities;
using GameStore.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Core.Services
{
    internal class ProductService : IProductService
    {
        public Task<Products> CreateProductAsync(Products product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Products>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Products> GetProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Products> UpdateProductAsync(Products product)
        {
            throw new NotImplementedException();
        }
    }
}
