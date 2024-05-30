using GameStore.Core.Entities;

namespace GameStore.Core.ServiceContracts
{
    public interface IProductService
    {
        public Task<bool> IsExistAsync(int id);
        public Task<Products> GetProductAsync(int id);
        public Task<ICollection<Products>> GetAllProductsAsync();
        public Task<Products> CreateProductAsync(Products product);
        public Task<Products> UpdateProductAsync(Products product);
        public Task DeleteProductAsync(int id);
    }
}
