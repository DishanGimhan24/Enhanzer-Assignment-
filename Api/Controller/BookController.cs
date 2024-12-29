using LibraryManagementApi.Models;
using LibraryManagementApi.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {

        // _context provides access to your database
        private readonly LibraryContext _context;

        //Dependency Injection: injects the LibraryContext into the controller
        public BookController(LibraryContext context)
        {
            _context = context;
        }



        // GET: api/Book
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return Ok(_context.Books.ToList());
        }

        // GET: api/Book/{id}
        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = _context.Books.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // POST: api/Book
        [HttpPost]
        public ActionResult CreateBook([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest(new { message = "Book data cannot be null." });
            }

            _context.Books.Add(book);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(GetBook),
                new { id = book.Id },
                new
                {
                    success = true,
                    message = "Book created successfully.",
                    book = book
                }
            );
        }

        // PUT: api/Book/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            if (id != updatedBook.Id)
            {
                return BadRequest();
            }

            _context.Entry(updatedBook).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Books.Any(b => b.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Book/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
