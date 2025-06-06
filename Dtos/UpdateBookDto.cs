using System.ComponentModel.DataAnnotations;

namespace dotnet_backend.Dtos
{
    public class UpdateBookDto
    {
        [Required]
        public string Title { get; set; } = "";

        [Required]
        public string Author { get; set; } = "";

        public string PublishedDate { get; set; } = "";
        public int Pages { get; set; }
        public string Description { get; set; } = "";
        public string Genre { get; set; } = "";
        public string ImageUrl { get; set; } = "";
    }
}
