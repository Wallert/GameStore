using GameStore.Core.Entities;
using GameStore.Core.ServiceContracts;
using GameStore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.UI.Services
{
    internal class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateCategoryAsync(Categories category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            _context.Categories.Remove(await _context.Categories.FindAsync(id) ?? throw new ArgumentException("Invalid category"));
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Categories>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }

        public async Task<Categories> GetCategoryAsync(int id)
        {
            return await _context.Categories.FindAsync(id) ?? throw new ArgumentException($"Category not found. Id{id}", nameof(id));
        }

        public async Task<bool> IsExistAsync(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }

        public async Task UpdateCategoryAsync(Categories category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
