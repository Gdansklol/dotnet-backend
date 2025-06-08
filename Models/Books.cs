using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace dotnet_backend.Models
{
    public class Book
    {
        [BsonId] // Used as the _id field in MongoDB
        [BsonRepresentation(BsonType.ObjectId)] // MongoDB handles ObjectId, C# handles it as a string
        public string? Id { get; set; }

        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public string PublishedDate { get; set; } = "";
        public int Pages { get; set; }
        public string Description { get; set; } = "";
        public string Genre { get; set; } = "";
        public string ImageUrl { get; set; } = "";
    }
}
