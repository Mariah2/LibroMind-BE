namespace LibroMind_BE.Services.Models
{
    public class BorrowGetDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int BookLibraryId { get; set; }

        public DateTime BorrowingDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public bool HasReturnedBook { get; set; }
    }
}
