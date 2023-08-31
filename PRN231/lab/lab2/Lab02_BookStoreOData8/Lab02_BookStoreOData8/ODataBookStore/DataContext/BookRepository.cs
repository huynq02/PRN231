using Entity;
using Microsoft.EntityFrameworkCore;
using ODataBookStore.DataContext.IRepository;
using ODataBookStore.DataContext;
using System.Collections;
using ODataBookStore;

namespace ODataBookStoreAPI.DataContext
{
    public class BookRepository : IBookRepo
    {
        private readonly BookStoreContext db;
        public BookRepository(BookStoreContext context)
        {
            db = context;
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            if (context.Books.Count() == 0)
            {
                foreach (var book in DataSource.GetBooks())
                {
                    context.Books.Add(book);
                    context.Presses.Add(book.Press);
                }
                context.SaveChanges();
            }
        }

        public async Task<bool> CreateBook(Book book)
        {
            db.Books.Add(book);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBook(string id)
        {
            var b = await db.Books.FirstOrDefaultAsync(c => c.Id == id);
            if (b == null)
            {
                return false;
            }
            db.Books.Remove(b);
            await db.SaveChangesAsync();
            return true;

        }

        public async Task<bool> EditBook(Book book)
        {
            db.Books.Update(book);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable> GetBooks()
        {
            return await db.Books.Include(c => c.Press).ToListAsync();
        }

    }
}
