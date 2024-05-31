using GameStore.Core.Entities;
using GameStore.Core.ServiceContracts;
using GameStore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.UI.Services
{
    internal class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateProductAsync(Products products)
        {
            await _context.Products.AddAsync(products);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            _context.Products.Remove(await _context.Products.FindAsync(id) ?? throw new ArgumentException("Invalid product"));
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Products>> GetAllProductsAsync()
        {
            return await _context.Products.Include(p => p.Categories).ToListAsync();
        }
        public async Task<int> GetProductsQuantityAsync()
        {
            var products = await _context.Products.ToListAsync();
            return products.Count();
        }
        public async Task<Products> GetProductAsync(int id)
        {
            return await _context.Products.FindAsync(id) ?? throw new ArgumentException($"Product not found. Id{id}", nameof(id));
        }       

        public async Task<bool> IsExistAsync(int id)
        {
            return await _context.Products.AnyAsync(c => c.Id == id);
        }

        public async Task UpdateProductAsync(Products products)
        {
            _context.Products.Update(products);
            await _context.SaveChangesAsync();
        }
    }
}
