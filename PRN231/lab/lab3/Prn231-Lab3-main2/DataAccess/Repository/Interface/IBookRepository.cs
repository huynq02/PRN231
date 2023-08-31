using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface IBookRepository
    {
        public Task<DbSet<Book>> GetAllAsync();
        public Task<Book> SaveAsync(Book book);
        public Task<Book> GetByIdAsync(object id);
        public Task<Book> UpdateAsync(object id, Book book);
        public Task<bool> DeleteAsync(object id);
        public Task<int> GetTotalAsync(string keyword);
    }
}
