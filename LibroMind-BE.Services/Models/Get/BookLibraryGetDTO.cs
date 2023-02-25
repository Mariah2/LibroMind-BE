namespace LibroMind_BE.Services.Models
{
    public class BookLibraryGetDTO
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int LibraryId { get; set; }

        public int Quantity { get; set; }

        public DateTime AddedDate { get; set; }
    }
}