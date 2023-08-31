using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using ODataBookStore.DataContext;
using ODataBookStore.DataContext.IRepository;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Entity;

namespace ODataBookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ODataController
    {
        private readonly BookStoreContext db;
        private readonly IBookRepo _bookRepo;
        public BooksController(IBookRepo context, BookStoreContext db)
        {
            _bookRepo = context;
            this.db = db;
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            if (db.Books.Count() == 0)
            {
                foreach (var book in DataSource.GetBooks())
                {
                    db.Books.Add(book);
                    db.Presses.Add(book.Press);
                }
                db.SaveChanges();

            }
        }

        [HttpGet]
        [Route("ListBook")]
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var books = await _bookRepo.GetBooks();
            return Ok(books);
        }

        [HttpPost]
        [Route("Create")]
        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] Book bookDto)
        {
            var book = new Book
            {
                Id = Guid.NewGuid().ToString(),
                ISBN = bookDto.ISBN,
                Title = bookDto.Title,
                Author = bookDto.Author,
                Price = bookDto.Price,
                City = bookDto.City,
                Street = bookDto.Street,
                //Press = bookDto.Press,
                //PressId = bookDto.PressId,
            };
            var res = await _bookRepo.CreateBook(book);


            return Ok(res);
        }


        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var res = await _bookRepo.DeleteBook(id);
            return Ok(res);

            //var b = await db.Books.FirstOrDefaultAsync(c => c.Id == id);
            //if (b == null) return NotFound();
            //db.Books.Remove(b);
            //db.SaveChanges();
            //return Ok();
        }
        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit([FromBody] Book book)
        {
            var bookEdit = new Book
            {
                Id = book.Id,
                ISBN = book.ISBN,
                Title = book.Title,
                Author = book.Author,
                Price = book.Price,
                City = book.City,
                Street = book.Street,
                PressId = book.PressId,
            };
            var res = await _bookRepo.EditBook(bookEdit);
            return Ok(res);
        }
    }
}
