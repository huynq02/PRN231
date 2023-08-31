using BusinessObjects;
using BusinessObjects;
using IdentityAjaxClient.Data;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAjaxClient.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> IndexAsync(int currentPage = 1)
        {

            int pageSize = 5; // Kích thước trang
            return View();
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Product product = new Product();
            product.ProductId = id;
            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Product product = new Product();
            product.ProductId = id;
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
        {
            return View();
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            return View();
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            return View();
        }

        private bool ProductExists(int id)
        {
            return (_context.Product?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
