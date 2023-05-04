using LibroMind_BE.DAL.Entities;

namespace LibroMind_BE.Services.Models
{
    public class BookLibraryCardGetDTO
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int LibraryId { get; set; }

        public int Quantity { get; set; }

        public BookCard Book { get; set; } = null!;
    }
}
