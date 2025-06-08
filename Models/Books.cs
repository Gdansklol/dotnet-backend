using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace dotnet_backend.Models
{
    public class Book
    {
        [BsonId] // 이 속성은 MongoDB의 _id 필드로 사용됨
        [BsonRepresentation(BsonType.ObjectId)] // C#에선 string, MongoDB에선 ObjectId로 저장
        public string? Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; } = "";

        [BsonElement("author")]
        public string Author { get; set; } = "";

        [BsonElement("publishedDate")]
        public string PublishedDate { get; set; } = "";

        [BsonElement("pages")]
        public int Pages { get; set; }

        [BsonElement("description")]
        public string Description { get; set; } = "";

        [BsonElement("genre")]
        public string Genre { get; set; } = "";

        [BsonElement("imageUrl")]
        public string ImageUrl { get; set; } = "";
    }
}
