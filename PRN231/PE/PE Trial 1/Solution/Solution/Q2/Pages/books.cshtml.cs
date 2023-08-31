
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Q2.Pages
{
    public class booksModel : PageModel
    {
        private HttpClient httpClient;
        public List<Book> books { get; set; }
        public List<bookDto> booksDto { get; set; } = new List<bookDto>();

        public booksModel()
        {
            httpClient = new HttpClient();
        }

        public async Task OnGet(int? id)
        {

            string url = @"http://localhost:5000/api/Book/List/" + id;
            HttpResponseMessage result = await httpClient.GetAsync(url);
            string jsonStr = await result.Content.ReadAsStringAsync();
            books = JsonSerializer.Deserialize<List<Book>>(jsonStr);
           
            foreach(var book in books)
            {
                booksDto.Add(new bookDto { BookId = book.bookId, Title = book.title, AuthorId = book.authorId });
            }

            string authurl = @"http://localhost:5000/api/Author/List";
            HttpResponseMessage authResult = await httpClient.GetAsync(authurl);
            string authJsonStr = await authResult.Content.ReadAsStringAsync();

            // Author author = JsonSerializer.Deserialize<Author>(authJsonStr);
            List<Author> authorList = JsonSerializer.Deserialize<List<Author>>(authJsonStr);
            foreach (var book in booksDto)
            {
                foreach (var item in authorList)
                {
                    if(book.AuthorId == item.authorId)
                    {
                        book.AuthorName = item.name;
                    }
                }

              
            }
        }
    }
    public class bookDto
    {
        public int? BookId { get; set; }
        public string? Title { get; set; }
        public int? AuthorId { get; set; }
        public string AuthorName { get; set; } = string.Empty;
    }
    public class Book
    {
        public int? bookId { get; set; }
        public string? title { get; set; }
        public int? authorId { get; set; }
    }
    public class Author
    {
        public int authorId { get; set; }
        public string name { get; set; }
    }
    public class AuthorDto
    {
        public int authorId { get; set; }
        public string? name { get; set; }
        public List<Book>? books { get; set; }
    }
}
