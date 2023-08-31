using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Q2.Pages
{
    public class authorsModel : PageModel
    {
        private HttpClient httpClient;
        public List<Book> books { get; set; }
        public List<Author> authors { get; set; }
        public List<AuthorDto> authorDtos { get; set; } = new List<AuthorDto>();

        public List<bookDto> booksDto { get; set; } = new List<bookDto>();

        public authorsModel()
        {
            httpClient = new HttpClient();
        }

        public async Task OnGet(int? id)
        {

            string url = @"http://localhost:5000/api/Book/List/" + id;
            HttpResponseMessage result = await httpClient.GetAsync(url);
            string jsonStr = await result.Content.ReadAsStringAsync();
            books = JsonSerializer.Deserialize<List<Book>>(jsonStr);

            foreach (var book in books)
            {
                booksDto.Add(new bookDto { BookId = book.bookId, Title = book.title, AuthorId = book.authorId });
            }

            string authurl = @"http://localhost:5000/api/Author/List";
            HttpResponseMessage authResult = await httpClient.GetAsync(authurl);
            string authJsonStr = await authResult.Content.ReadAsStringAsync();

            // Author author = JsonSerializer.Deserialize<Author>(authJsonStr);
            List<Author> authorList = JsonSerializer.Deserialize<List<Author>>(authJsonStr);

            foreach (var item in authorList)
            {
                var auth = new AuthorDto();
                var bookss = new List<Book>();
                foreach (var book in booksDto)
                {
                    if (book.AuthorId == item.authorId)
                    {

                       bookss.Add(new Book { bookId = book.BookId, title = book.Title, authorId = item.authorId });
                    }
                }
                auth.authorId = item.authorId;
                auth.name = item.name;
                auth.books = bookss;
                authorDtos.Add(auth);
            }

        }
    }

   
}
