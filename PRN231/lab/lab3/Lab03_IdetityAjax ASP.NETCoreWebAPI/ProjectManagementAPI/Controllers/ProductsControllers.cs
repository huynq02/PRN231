using AutoMapper;
using BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementAPI.DTO;
using Repositories;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsControllers : ControllerBase
    {
        public ProductsControllers(IMapper mapper)
        {
            _mapper = mapper;
        }
        private IProductRepository repository = new ProductRepository();
        private readonly IMapper _mapper;

        // GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<ProductResponse>> GetPrducts() 
        {
            var list = repository.GetProducts();
            var map = _mapper.Map<List<ProductResponse>>(list);
            return map;
        }

        // Post: ProductsControllers/Products
        [HttpPost]
        public IActionResult PostProduct(ProductAddRequest p)
        {
            var map = _mapper.Map<Product>(p);
            repository.SaveProduct(map);
            return NoContent();
        }

        //GET: ProductsController/Delete/5
        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            var p = repository.GetProductById(id);
            if (p == null)
                return NotFound();
            repository.DeleteProduct(p);
            return NoContent();
        }


        [HttpDelete]
        public IActionResult DeleteProduct(List<int> ids) {
            return new JsonResult("");
        }

        [HttpPut("id")]
        public IActionResult UpdateProduct(int id, Product p)
        {
            var ptmp = repository.GetProductById(id);

            if (ptmp == null)
                return NotFound();
            repository.UpdateProduct(p);
            return NoContent();
        }

    }
}
