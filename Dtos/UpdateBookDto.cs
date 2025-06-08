using System.ComponentModel.DataAnnotations;

namespace dotnet_backend.Dtos
{
    public class UpdateBookDto
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "Author is required.")]
        public string Author { get; set; } = "";

        public string PublishedDate { get; set; } = "";

        [Range(1, 10000, ErrorMessage = "Pages must be between 1 and 10000.")]
        public int Pages { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = "";

        [Required(ErrorMessage = "Genre is required.")]
        public string Genre { get; set; } = "";

        public string ImageUrl { get; set; } = "";
    }
}
