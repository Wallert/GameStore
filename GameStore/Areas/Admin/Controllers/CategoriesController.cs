using GameStore.Core.Entities;
using GameStore.Core.ServiceContracts;
using GameStore.Infrastructure;
using GameStore.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace GameStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {            
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categories categories)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateCategoryAsync(categories);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }
           return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Categories categories)
        {
            if(ModelState.IsValid)
            {
                await _categoryService.UpdateCategoryAsync(categories);
                return RedirectToAction(nameof(Index));
            }
            return View(categories);
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
