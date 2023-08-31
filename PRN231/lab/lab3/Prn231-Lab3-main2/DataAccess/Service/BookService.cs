using BussinessObject.Models;
using DataAccess.Repository.Interface;
using DataAccess.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public Task<bool> DeleteAsync(object id)
        {

            return _bookRepository.DeleteAsync(id);
        }

        public Task<DbSet<Book>> GetAllAsync()
        {
            return _bookRepository.GetAllAsync();
        }

        public Task<Book> GetByIdAsync(object id)
        {
            return _bookRepository.GetByIdAsync(id);
        }

        public Task<Book> UpdateAsync(object id, Book book)
        {
            return _bookRepository.UpdateAsync(id, book);
        }

        public Task<Book> SaveAsync(Book book)
        {
            return _bookRepository.SaveAsync(book);
        }

        public Task<int> GetTotalAsync(string keyword)
        {
            return _bookRepository.GetTotalAsync(keyword);
        }
    }
}
