using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Q2.Pages
{
    public class CreateBookModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public CreateBookModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        [BindProperty]
        public int BookId { get; set; }

        [BindProperty]
        public string Title { get; set; }

        public List<Author> AllAuthor { get; set; }  // Assuming you have a list of available books

        [BindProperty]
        public int SelectedAuthors { get; set; }  // To bind selected book ids from the form

        public async void OnGet()
        {
            AllAuthor = GetAvailableAuthos().Result;
            
            string authurl = @"http://localhost:5000/api/Author/List";
            HttpResponseMessage authResult = await _httpClient.GetAsync(authurl);
            string authJsonStr = await authResult.Content.ReadAsStringAsync();
            List<Author> authorList = JsonSerializer.Deserialize<List<Author>>(authJsonStr);
            AllAuthor = authorList;
        }

        public async Task< IActionResult> OnPost()
        {
            string authurl = @"http://localhost:5000/api/Author/List";
            HttpResponseMessage authResult = await _httpClient.GetAsync(authurl);
            string authJsonStr = await authResult.Content.ReadAsStringAsync();
            List<Author> authorList = JsonSerializer.Deserialize<List<Author>>(authJsonStr);
            AllAuthor = authorList;
            if (!ModelState.IsValid)
            {
               
                return Page();
            }
            var book = new Book
            {
                bookId = BookId,
                title = Title,
                authorId= SelectedAuthors
            };

            // Serialize the book object to JSON
            var json = JsonSerializer.Serialize(book);

            // Define the API endpoint URL for creating a book
            var apiUrl = "http://localhost:5000/api/Book/Add";

            // Create the HTTP content with the JSON payload
            //var content = new StringContent(json, Encoding.UTF8, "application/json");
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(BookId.ToString()), "BookId");
            content.Add(new StringContent(Title), "Title");
            content.Add(new StringContent(SelectedAuthors.ToString()), "AuthorId");
            // Make an HTTP POST request to the API endpoint
            var response = await _httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                var responseContent = await response.Content.ReadAsStringAsync();

                // Check if the response content equals 1 to determine success
                if (responseContent == "1")
                {
                    // Book creation successful, redirect to the index page
                    return RedirectToPage("/Index");
                }
            }
            else
            {
                // Book creation failed, display an error message
                ModelState.AddModelError("", "Failed to create the book.");
                return Page();
            }


            return RedirectToPage("/Index");
        }
        private async Task< List<Author>> GetAvailableAuthos()
        {
            string authurl = @"http://localhost:5000/api/Author/List";
            HttpResponseMessage authResult = await _httpClient.GetAsync(authurl);
            string authJsonStr = await authResult.Content.ReadAsStringAsync();
            List<Author> authorList = JsonSerializer.Deserialize<List<Author>>(authJsonStr);
            AllAuthor = authorList;
            return AllAuthor;
            
        }

    }
}