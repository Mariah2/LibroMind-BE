namespace LibroMind_BE.Services.Models
{
    public class BookDetailsGetDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public DateTime PublishingDate { get; set; }

        public string Description { get; set; } = null!;

        public int PagesNumber { get; set; }

        public string? CoverUrl { get; set; }

        public double Rating { get; set; }

        public AuthorGetDTO Author { get; set; } = null!;

        public PublisherGetDTO Publisher { get; set; } = null!;

        public IEnumerable<ReviewGetDTO> Reviews { get; } = new List<ReviewGetDTO>();

        public IEnumerable<BookCategoryDetailsGetDTO> BookCategories { get; } = new List<BookCategoryDetailsGetDTO>();

        public IEnumerable<BookLibraryDetailsGetDTO> BookLibraries { get; } = new List<BookLibraryDetailsGetDTO>();

    }
}
