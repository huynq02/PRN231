using BusinessObjects.Dtos;
using BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private IProductRepository repository = new ProductRepository();
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts() => repository.GetProducts();

        [HttpGet("name")]
        public ActionResult<IEnumerable<Product>> GetProductsByName(string name) => repository.GetProductByName(name);
        //[HttpGet("sort")]
        //public ActionResult<IEnumerable<Product>> GetProductsBySort(string sort) => repository.GetProductBySort(sort);
        [HttpGet("Categories")]
        public ActionResult<IEnumerable<Category>> GetCategories() => repository.GetCategories();
        [HttpGet("id")]
        public ActionResult<Product> GetProductById(int id) => repository.GetProductById(id);
        [HttpPost]
        public IActionResult PostProduct(CreateProductDto p)
        {
            repository.SaveProduct(p);
            return NoContent();
        }
        [HttpDelete("id")]
        public IActionResult DeleteProduct(int id)
        {
            var p = repository.GetProductById(id);
            if (p == null) return NotFound();
            repository.DeleteProduct(p);
            return NoContent();
        }
        [HttpPut("id")]
        public IActionResult UpdateProduct(int id, UpdateProductDto p)
        {
            if (p == null) return NotFound();
            repository.UpdateProduct(id, p);
            return NoContent();
        }
        [HttpGet("Paging")]
        public ActionResult<IEnumerable<Product>> GetProductPaging(int? currentPage, int? pageSize)
        {
            var products = repository.GetProducts();
            if (products.Count() <= pageSize)
            {
                currentPage = 1;
            }
            if (currentPage < 0 || pageSize < 0)
            {
                return NotFound();
            }
            if (currentPage == null && pageSize == null)
            {
                return products;
            }
            return repository.GetProductsByPaging(currentPage, pageSize);
        }
    }
}
