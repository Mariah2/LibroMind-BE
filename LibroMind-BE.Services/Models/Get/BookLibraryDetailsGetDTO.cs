namespace LibroMind_BE.Services.Models
{
    public class BookLibraryDetailsGetDTO
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public DateTime AddedDate { get; set; }

        public AuthorGetDTO Author { get; set; } = null!;

        public BookGetDTO Book { get; set; } = null!;

        public LibraryGetDTO Library { get; set; } = null!;
    }
}