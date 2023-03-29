namespace LibroMind_BE.Services.Models
{
    public class BookGetDTO
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public int PublisherId { get; set; }

        public string Title { get; set; } = null!;

        public DateTime PublishingDate { get; set; }

        public string Description { get; set; } = null!;

        public int PagesNumber { get; set; }

        public string? CoverUrl { get; set; }

        public double Rating { get; set; }
    }
}