using Microsoft.AspNetCore.Mvc;
using RESTAPI.Models;

namespace RESTAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        static private List<Book> books =
        [
            new() { Id = 1, Title = "1984", Author = "George Orwell", YearPublished = 1949 },
            new() { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", YearPublished = 1960 },
            new() { Id = 3, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", YearPublished = 1925 }
        ];


        [HttpGet]
        public ActionResult<List<Book>> GetAllBooks()
        {
            return Ok(books);
        }
        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> AddBook(Book newBook)
        {
            if (newBook == null)
                return BadRequest();

            books.Add(newBook);
            return CreatedAtAction(nameof(AddBook), new { id = newBook.Id}, newBook);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book updatedBook)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            book.Id = updatedBook.Id;
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.YearPublished = updatedBook.YearPublished;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound();

            books.Remove(book);

            return NoContent();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
