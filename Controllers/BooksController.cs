using dotnet_backend.Models;
using dotnet_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> Get() =>
            await _bookService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Book>> Get(string id)
        {
            var book = await _bookService.GetByIdAsync(id);
            return book is null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Create(Book newBook)
        {
            await _bookService.CreateAsync(newBook);
            return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Book updatedBook)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book is null) return NotFound();

            updatedBook.Id = id;
            await _bookService.UpdateAsync(id, updatedBook);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book is null) return NotFound();

            await _bookService.DeleteAsync(id);
            return NoContent();
        }
    }
}
