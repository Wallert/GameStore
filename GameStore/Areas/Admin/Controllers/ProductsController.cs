using GameStore.Core.Entities;
using GameStore.Core.ServiceContracts;
using GameStore.Infrastructure;
using GameStore.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace GameStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {

        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly string _imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "media", "products");

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryName = new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Products products)
        {
            if (ModelState.IsValid)
            {
                if (products.ImageUpload != null && products.ImageUpload.Length > 0)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(products.ImageUpload.FileName);
                    string filePath = Path.Combine(_imagePath, uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await products.ImageUpload.CopyToAsync(stream);
                    }
                    products.Image = uniqueFileName;
                }

                await _productService.CreateProductAsync(products);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoryName = new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Name");
            return View(products);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.CategoryName = new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Name");
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Products products)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = await _productService.GetProductAsync(products.Id);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                existingProduct.Name = products.Name;
                existingProduct.Price = products.Price;
                existingProduct.Description = products.Description;
                existingProduct.CategoryId = products.CategoryId;

                if (products.ImageUpload != null && products.ImageUpload.Length > 0)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(products.ImageUpload.FileName);
                    string filePath = Path.Combine(_imagePath, uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await products.ImageUpload.CopyToAsync(stream);
                    }
                    existingProduct.Image = uniqueFileName;
                }

                await _productService.UpdateProductAsync(existingProduct);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoryName = new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Name", products.CategoryId);
            return View(products);
        }


        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
