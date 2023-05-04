namespace LibroMind_BE.DAL.Entities
{
    public class BookLibraryCard
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int LibraryId { get; set; }

        public int Quantity { get; set; }

        public BookCard Book { get; set; } = null!;
    }
}
