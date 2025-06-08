using dotnet_backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace dotnet_backend.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _books = database.GetCollection<Book>(settings.Value.CollectionName);
        }

        public async Task<List<Book>> GetAsync() =>
            await _books.Find(_ => true).ToListAsync();

        public async Task<Book?> GetByIdAsync(string id) =>
            await _books.Find(b => b.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Book book) =>
            await _books.InsertOneAsync(book);

        public async Task UpdateAsync(string id, Book updated) =>
            await _books.ReplaceOneAsync(b => b.Id == id, updated);

        public async Task DeleteAsync(string id) =>
            await _books.DeleteOneAsync(b => b.Id == id);
    }
}
