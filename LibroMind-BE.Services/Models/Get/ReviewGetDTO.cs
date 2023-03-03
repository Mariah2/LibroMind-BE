namespace LibroMind_BE.Services.Models
{
    public class ReviewGetDTO
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        public int Rating { get; set; }

        public string? Text { get; set; }
    }
}
