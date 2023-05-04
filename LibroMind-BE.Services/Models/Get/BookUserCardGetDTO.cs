using LibroMind_BE.DAL.Entities;

namespace LibroMind_BE.Services.Models
{
    public class BookUserCardGetDTO
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        public BookCard Book { get; set; } = null!;
    }
}
