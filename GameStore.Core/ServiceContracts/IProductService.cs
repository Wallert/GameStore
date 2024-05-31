using GameStore.Core.Entities;

namespace GameStore.Core.ServiceContracts
{
    public interface IProductService
    {
        public Task<bool> IsExistAsync(int id);
        public Task<Products> GetProductAsync(int id);
        public Task<ICollection<Products>> GetAllProductsAsync();
        public Task<int> GetProductsQuantityAsync();
        public Task CreateProductAsync(Products product);
        public Task UpdateProductAsync(Products product);
        public Task DeleteProductAsync(int id);
    }
}
