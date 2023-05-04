namespace LibroMind_BE.DAL.Entities
{
    public class BookUserCard
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        public BookCard Book { get; set; } = null!;
    }
}
