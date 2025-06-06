namespace dotnet_backend.Models  
{
    public class Book
    {
        public int Id { get; set; }                 // Unique identifier
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public string PublishedDate { get; set; } = ""; // Or DateTime is also possible
        public int Pages { get; set; }
        public string Description { get; set; } = "";
        public string Genre { get; set; } = "";
        public string ImageUrl { get; set; } = "";
    }
}
