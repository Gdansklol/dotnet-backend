using Microsoft.AspNetCore.Mvc;
using dotnet_backend.Models;
using dotnet_backend.Dtos;
using System.ComponentModel.DataAnnotations;

namespace dotnet_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        //  Temporär lista för böcker (minnesbaserad, ingen databas)
        private static readonly List<Book> books = new()
        {
            new Book
            {
                Id = 1,
                Title = "Pride and Prejudice",
                Author = "Jane Austen",
                PublishedDate = "1813-01-28",
                Pages = 432,
                Description = "En klassisk roman om etikett och kärlek i 1800-talets England.",
                Genre = "Klassiker",
                ImageUrl = "https://images.unsplash.com/photo-1512820790803-83ca734da794"
            },
            new Book
            {
                Id = 2,
                Title = "Forest Adventure",
                Author = "Nature Lover",
                PublishedDate = "2021-06-15",
                Pages = 240,
                Description = "Utforska gamla skogar och vilda djur.",
                Genre = "Fantasy",
                ImageUrl = "https://images.unsplash.com/photo-1506744038136-46273834b3fb"
            }
        };

        //  Hämta alla böcker
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAll()
        {
            return Ok(books);
        }

        //  Hämta en bok med specifikt ID
        [HttpGet("{id}")]
        public ActionResult<Book> GetById(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            return book == null ? NotFound() : Ok(book);
        }

        //  Lägg till ny bok (med validering)
        [HttpPost]
        public ActionResult<Book> Create([FromBody] CreateBookDto newBookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newBook = new Book
            {
                Id = books.Any() ? books.Max(b => b.Id) + 1 : 1,
                Title = newBookDto.Title,
                Author = newBookDto.Author,
                PublishedDate = newBookDto.PublishedDate,
                Pages = newBookDto.Pages,
                Description = newBookDto.Description,
                Genre = newBookDto.Genre,
                ImageUrl = newBookDto.ImageUrl
            };

            books.Add(newBook);
            return CreatedAtAction(nameof(GetById), new { id = newBook.Id }, newBook);
        }

        //  Uppdatera en bok (med validering via DTO)
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateBookDto updatedDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var index = books.FindIndex(b => b.Id == id);
            if (index == -1) return NotFound();

            var updatedBook = new Book
            {
                Id = id,
                Title = updatedDto.Title,
                Author = updatedDto.Author,
                PublishedDate = updatedDto.PublishedDate,
                Pages = updatedDto.Pages,
                Description = updatedDto.Description,
                Genre = updatedDto.Genre,
                ImageUrl = updatedDto.ImageUrl
            };

            books[index] = updatedBook;
            return NoContent(); //  204 No Content om allt gick bra
        }

        //  Ta bort en bok med ID
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
