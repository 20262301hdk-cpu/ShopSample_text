using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopSample.Data;
using ShopSample.Filter;
using ShopSample.Models;
using ShopSample.ViewModels;

namespace ShopSample.Controllers;

[Authorize]
[LoggingActionFilter]
[ServiceFilter(typeof(CustomExceptionFilter))]
public class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Helper method to build category list
    private async Task<List<SelectListItem>> GetCategoryListAsync()
    {
        return await _context.Categories
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            })
            .ToListAsync();
    }

    // GET: /Product
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .ToListAsync();
        return View(products);
    }

    // GET: /Product/Details/5
    [AllowAnonymous]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // GET: /Product/Create
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        var viewModel = new ProductFormViewModel
        {
            CategoryList = await GetCategoryListAsync()
        };
        return View(viewModel);
    }

    // POST: /Product/Create
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductFormViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var product = new Product
            {
                Name = viewModel.Name,
                Price = viewModel.Price,
                CategoryId = viewModel.CategoryId
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        viewModel.CategoryList = await GetCategoryListAsync();
        return View(viewModel);
    }

    // GET: /Product/Edit/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        var viewModel = new ProductFormViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            CategoryId = product.CategoryId,
            CategoryList = await GetCategoryListAsync()
        };

        return View(viewModel);
    }

    // POST: /Product/Edit/5
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProductFormViewModel viewModel)
    {
        if (id != viewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            product.Name = viewModel.Name;
            product.Price = viewModel.Price;
            product.CategoryId = viewModel.CategoryId;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        viewModel.CategoryList = await GetCategoryListAsync();
        return View(viewModel);
    }

    // GET: /Product/Delete/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // POST: /Product/Delete/5
    [Authorize(Roles = "Admin")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
    // ⚠ テスト用: 動作確認後に削除すること 
    public IActionResult ThrowTest()
    {
        throw new Exception("テスト例外: CustomExceptionFilter の動作確認");
    }
}
