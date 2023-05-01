using LibroMind_BE.DAL.Models;

namespace LibroMind_BE.Services.Models
{
    public class BookCardGetDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? CoverUrl { get; set; }

        public double Rating { get; set; }

        public Author Author { get; set; } = null!;
    }
}
