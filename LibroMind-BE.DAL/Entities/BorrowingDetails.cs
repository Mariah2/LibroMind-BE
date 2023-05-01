namespace LibroMind_BE.DAL.Entities
{
    public class BorrowingDetails
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int BookLibraryId { get; set; }

        public DateTime BorrowingDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public bool? HasReturnedBook { get; set; }

        public bool WasExtensionRequested { get; set; }

        public BookCard Book { get; set; } = null!;

        public UserBasicInfo User { get; set; } = null!;
    }
}
