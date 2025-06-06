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
        // Temporär lista för böcker (minnesbaserad, ingen databas)
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
            },
            new Book
            {
                Id = 3,
                Title = "The Old Man and the Sea",
                Author = "Ernest Hemingway",
                PublishedDate = "1952-09-01",
                Pages = 127,
                Description = "En gammal fiskare slåss med en marlin i havet.",
                Genre = "Klassiker",
                ImageUrl = "https://mblogthumb-phinf.pstatic.net/..."
            },
            new Book
            {
                Id = 4,
                Title = "The Chronicles of Narnia",
                Author = "C.S. Lewis",
                PublishedDate = "1950-10-16",
                Pages = 568,
                Description = "Ett episkt fantasyäventyr i ett magiskt land.",
                Genre = "Fantasy",
                ImageUrl = "https://images.unsplash.com/photo-150784221..."
            },
            new Book
            {
                Id = 5,
                Title = "Cartoon World",
                Author = "Animator",
                PublishedDate = "2018-09-09",
                Pages = 120,
                Description = "Roliga äventyr i en färgglad tecknad värld.",
                Genre = "Sagor",
                ImageUrl = "https://cdn.pixabay.com/photo/2015/04/23/22/00/tree-736885_1280.jpg"
            }
        };

        // Hämta alla böcker
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAll()
        {
            return Ok(books);
        }

        // Hämta en bok med specifikt ID
        [HttpGet("{id}")]
        public ActionResult<Book> GetById(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            return book == null ? NotFound() : Ok(book);
        }

        // Lägg till ny bok (med validering)
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

        // Uppdatera en bok (med validering via DTO)
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
            return NoContent(); // 204 No Content om allt gick bra
        }

        // Ta bort en bok med ID
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
