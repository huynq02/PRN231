using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using BusinessObjects;
using System.Net.Http;
using BusinessObjects.ModelDTO;

namespace ProductManagementWebClient.Controllers
{
    public class ProductController : Controller
    {

        private readonly HttpClient client = null;
        private string ProductApiUrl = "";
        private string CategoryApiUrl = "";


        public ProductController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "http://localhost:5169/api/product";
            CategoryApiUrl = "http://localhost:5169/api/category";
        }
        public async Task<IActionResult> Index()
        {
            List<Product> products = await GetProducts();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Category"] = await GetCategories();
            return View();
        }

        private async Task<List<Category>> GetCategories()
        {
            HttpResponseMessage response = await client.GetAsync(CategoryApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<List<Category>>(strData, options);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] ProductDTO product)
        {
            product.ProductId = 0;
            using (var respone = await client.PostAsJsonAsync(ProductApiUrl, product))
            {
                string apiResponse = await respone.Content.ReadAsStringAsync();
            }
            return Redirect("/Product/Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            List<Product> products = await GetProducts();
            Product product = products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);

        }

        public async Task<IActionResult> Delete(int id)
        {
            List<Product> products = await GetProducts();
            Product product = products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            String url = ProductApiUrl + "/id?id=" + id;
            await client.DeleteAsync(url);
            return Redirect("/Product/Index");
        }

        public async Task<IActionResult> EditProduct([FromForm] ProductDTO product)
        {
            using (var respone = await client.PutAsJsonAsync(ProductApiUrl + "/id?id=" + product.ProductId, product))
            {
                string apiResponse = await respone.Content.ReadAsStringAsync();
            }
            return Redirect("/Product/Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            ViewData["Category"] = await GetCategories();
            List<Product> products = await GetProducts();
            Product product = products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        private async Task<List<Product>> GetProducts()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<List<Product>>(strData, options);
        }


    }
}
