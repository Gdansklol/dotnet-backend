using Microsoft.AspNetCore.Mvc;
using dotnet_backend.Models;

namespace dotnet_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private static List<Book> books = new()
        {
            new Book
            {
                Id = "1",
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
                Id = "2",
                Title = "Forest Adventure",
                Author = "Nature Lover",
                PublishedDate = "2021-06-15",
                Pages = 240,
                Description = "Explore the mystery of ancient forests and wild animals.",
                Genre = "Fantasy",
                ImageUrl = "https://images.unsplash.com/photo-1506744038136-46273834b3fb?auto=format&fit=crop&w=800&q=60"
            },
            new Book
            {
                Id = "3",
                Title = "The Old Man and the Sea",
                Author = "Ernest Hemingway",
                PublishedDate = "1952-09-01",
                Pages = 127,
                Description = "An aging fisherman struggles with a giant marlin in the Gulf Stream.",
                Genre = "Classic",
                ImageUrl = "https://mblogthumb-phinf.pstatic.net/MjAyNDAyMDRfMjIx/MDAxNzA3MDQ2NzgzMjk1.4uWHJ40TFs6ekkffYyDCvsDivaTEp4ZsAUa3699kmo4g.wSxpx6-XCdvYkOsStg2r7LY0LQCAeWw-eTvQ7eCDl0Mg.JPEG.ksnv69552/%EC%8A%AC%EB%9D%BC%EC%9D%B4%EB%93%9C37.JPG?type=w800"
            },
            new Book
            {
                Id = "4",
                Title = "Cartoon World",
                Author = "Animator",
                PublishedDate = "2018-09-09",
                Pages = 120,
                Description = "Fun adventures in a colorful cartoon world.",
                Genre = "Fairy Tale",
                ImageUrl = "https://cdn.pixabay.com/photo/2015/04/23/22/00/tree-736885_1280.jpg"
            },
            new Book
            {
                Id = "5",
                Title = "The Chronicles of Narnia",
                Author = "C.S. Lewis",
                PublishedDate = "1950-10-16",
                Pages = 568,
                Description = "An epic fantasy adventure in a mythical land ruled by magic and courage.",
                Genre = "Fantasy",
                ImageUrl = "https://images.unsplash.com/photo-1507842217343-583bb7270b66?auto=format&fit=crop&w=800&q=60"
            }
        };

        // GET /api/books  AND  GET /
        [HttpGet]
        [Route("/")] // root route
        public ActionResult<IEnumerable<Book>> Get() => Ok(books);

        // GET /api/books/{id}
        [HttpGet("{id}")]
        public ActionResult<Book> GetById(string id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            return book == null ? NotFound() : Ok(book);
        }

        // POST /api/books
        [HttpPost]
        public ActionResult<Book> Create([FromBody] Book newBook)
        {
            newBook.Id = Guid.NewGuid().ToString();
            books.Add(newBook);
            return CreatedAtAction(nameof(GetById), new { id = newBook.Id }, newBook);
        }

        // PUT /api/books/{id}
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Book updatedBook)
        {
            var index = books.FindIndex(b => b.Id == id);
            if (index == -1) return NotFound();

            updatedBook.Id = id;
            books[index] = updatedBook;
            return NoContent();
        }

        // DELETE /api/books/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();

            books.Remove(book);
            return NoContent();
        }
    }
}
