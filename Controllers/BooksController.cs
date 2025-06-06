using Microsoft.AspNetCore.Mvc;
using dotnet_backend.Models;

namespace dotnet_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        // Tillfällig datalagring (minnesbaserad)
        private static readonly List<Book> books = new()
        {
            new Book
            {
                Id = 1,
                Title = "Pride and Prejudice",
                Author = "Jane Austen",
                PublishedDate = "1813-01-28",
                Pages = 432,
                Description = "A classic novel of manners and love in 19th-century England.",
                Genre = "Classic",
                ImageUrl = "https://images.unsplash.com/photo-1512820790803-83ca734da794"
            },
            new Book
            {
                Id = 2,
                Title = "Forest Adventure",
                Author = "Nature Lover",
                PublishedDate = "2021-06-15",
                Pages = 240,
                Description = "Explore the mystery of ancient forests and wild animals.",
                Genre = "Fantasy",
                ImageUrl = "https://images.unsplash.com/photo-1506744038136-46273834b3fb"
            }
        };

        // View All
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAll()
        {
            return Ok(books);
        }

        // get by ID
        [HttpGet("{id}")]
        public ActionResult<Book> GetById(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            return book == null ? NotFound() : Ok(book);
        }

        // Add new book
        [HttpPost]
        public ActionResult<Book> Create(Book newBook)
        {
            newBook.Id = books.Max(b => b.Id) + 1;
            books.Add(newBook);
            return CreatedAtAction(nameof(GetById), new { id = newBook.Id }, newBook);
        }

        // update
        [HttpPut("{id}")]
        public IActionResult Update(int id, Book updatedBook)
        {
            var index = books.FindIndex(b => b.Id == id);
            if (index == -1) return NotFound();

            updatedBook.Id = id;
            books[index] = updatedBook;
            return NoContent();
        }

        // delete
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();

            books.Remove(book);
            return NoContent();
        }
    }
}
