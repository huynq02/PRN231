using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Text.Json.Serialization;
using System.Text.Json;
using DataAccess.Service.Interface;
using BussinessObject.Models;

namespace ODataBookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController (IBookService bookService)
        {
            this._bookService = bookService;
        }

        [EnableQuery(PageSize = 3)]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetBooks()
        {
            try
            {
                return Ok(_bookService.GetAllAsync().Result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("total")]
        [Authorize]
        public async Task<ActionResult> GetTotalBooks(string? value ="")
        {
            try
            {
                value = value ?? "";
                return Ok(_bookService.GetTotalAsync(value).Result);
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetBook(int id)
        {
            try
            {
                return Ok(_bookService.GetByIdAsync(id).Result);
            }
            catch
            {
                return BadRequest();
            }
        }


        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutBook(int id, Book bookModel)
        {
            try
            {
                return Ok(_bookService.UpdateAsync(id, bookModel).Result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> PostBook(Book bookModel)
        {
            try
            {
                return Ok(_bookService.SaveAsync(bookModel).Result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _bookService.DeleteAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
