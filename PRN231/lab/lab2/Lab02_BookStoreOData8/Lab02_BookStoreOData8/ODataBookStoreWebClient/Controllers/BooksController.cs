using Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;

namespace ODataBookStoreWebClient.Controllers
{
    public class BooksController : Controller
    {
        private readonly HttpClient client = null;
        private string BookApiUrl = "";
        private string PressApiUrl = "";
        public BooksController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            BookApiUrl = $"http://localhost:5236/api/Books/";
            PressApiUrl = $"http://localhost:5236/api/Press/";

        }
        public async Task<IActionResult> Index()
        {
            string url = BookApiUrl + "ListBook";
            HttpResponseMessage repoonse = await client.GetAsync(url);
            var result = repoonse.Content.ReadFromJsonAsync<List<Book>>().Result;
            return View(result);
            //string strData = await repoonse.Content.ReadAsStringAsync();
            //Console.WriteLine(strData);
            //dynamic temp = JObject.Parse(strData);

            //var lst = temp.value;
            //List<Book> items = ((JArray)temp.value).Select(x => new Book
            //{
            //    Id = (int)x["Id"],
            //    Author = (string)x["Author"],
            //    ISBN = (string)x["ISBN"],
            //    Title = (string)x["Title"],
            //    Price = (decimal)x["Price"],
            //}).ToList();
            //return View(items);
        }
        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            //ViewBag.Press = await GetPress();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Book book)
        {
            //ViewBag.Press = await GetPress();
            if (!ModelState.IsValid) return View(book);
            HttpResponseMessage repoonse = await client.PostAsJsonAsync(BookApiUrl + "Create", book);
            var result = repoonse.Content.ReadFromJsonAsync<bool>().Result;
            if (result) { ViewData["msg"] = "Create success!"; }
            else { ViewData["msg"] = "Create failed!"; }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> GetPress()
        {
            HttpResponseMessage repoonse = await client.GetAsync(PressApiUrl);
            var result = repoonse.Content.ReadFromJsonAsync<List<Press>>().Result;
            return View(result);
        }


       

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(Book bookEdit)
        {
            //ViewBag.Press = await GetPress();
            if (!ModelState.IsValid) return View(bookEdit);
            HttpResponseMessage repoonse = await client.PutAsJsonAsync(BookApiUrl + "Edit", bookEdit);
            var result = repoonse.Content.ReadFromJsonAsync<bool>().Result;
            if (result) { ViewData["msg"] = "Create success!"; }
            else { ViewData["msg"] = "Create failed!"; }
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(string? id)
        {
            var book = await GetByIdAsync(id);
            if (book == null) return NotFound();

            string url = BookApiUrl + $"Delete/{id}";

            HttpResponseMessage response = await client.DeleteAsync(url);
            var result = response.Content.ReadFromJsonAsync<bool>().Result;
            if (result) { ViewData["msg"] = "Create success!"; }
            else { ViewData["msg"] = "Create failed!"; }
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(string? id)
        {
            var book = await GetByIdAsync(id);
            if (book == null) return NotFound();
            ViewBag.Press = await GetPress();
            return View(book);
        }

        private async Task<Book> GetByIdAsync(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            string url = BookApiUrl + $"ListBook?$filter=Id eq '{id}'";
            HttpResponseMessage response = await client.GetAsync(url);
            var result = response.Content.ReadFromJsonAsync<List<Book>>().Result;

            if (result == null || result.Count == 0) return null;

            return new Book
            {
                Id = result[0].Id,
                ISBN = result[0].ISBN,
                Title = result[0].Title,
                Author = result[0].Author,
                Price = result[0].Price,
                City = result[0].City,
                Street = result[0].Street,
            };
        }



    }
}
