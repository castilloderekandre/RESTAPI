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

        public IActionResult Index()
        {
            return View();
        }
    }
}
