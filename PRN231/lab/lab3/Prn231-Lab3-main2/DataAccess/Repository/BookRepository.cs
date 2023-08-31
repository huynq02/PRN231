using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessObject.Models;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly MyDBContext db;
        public BookRepository(MyDBContext myDb)
        {
            db = myDb; 
        }
        public Task<bool> DeleteAsync(object id)
        {
            try
            {
                Book book = db.Books.FirstOrDefaultAsync(c => c.Id == (int)id).Result;
                if (book == null)
                {
                    return Task.FromResult(false);
                }

                db.Books.Remove(book);
                db.SaveChanges(true);
                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        public Task<DbSet<Book>> GetAllAsync()
        {
            return Task.FromResult(db.Books);
        }

        public Task<Book> GetByIdAsync(object id)
        {
            try
            {
                var book =  db.Books.Include(b => b.Press).FirstOrDefaultAsync(c => c.Id == (int)id).Result;
                book.Press.Books = null;
                return Task.FromResult(book);
            }
            catch
            {
                return null;
            }
        }

        public Task<Book> UpdateAsync(object id, Book book)
        {
            try
            {
                db.Books.Update(book);
                db.SaveChanges();
                return Task.FromResult(book);
            }
            catch
            {
                return null;
            }
        }

        public Task<Book> SaveAsync(Book book)
        {
            try
            {
                db.Books.Add(book);
                db.SaveChanges();
                return Task.FromResult(book);
            }
            catch
            {
                return null;
            }
        }

        public Task<int> GetTotalAsync(string keyword)
        {
            try {
                int count = db.Books.Where(b =>
                        b.ISBN.Contains(keyword) || b.Title.Contains(keyword) || b.Author.Contains(keyword)
                        ).Count();
                return Task.FromResult(count);
            }
            catch
            {
                return Task.FromResult(0);
            }
        }
    }
}
