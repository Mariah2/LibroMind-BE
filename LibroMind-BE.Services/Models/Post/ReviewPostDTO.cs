namespace LibroMind_BE.Services.Models
{
    public class ReviewPostDTO
    {
        public int BookId { get; set; }

        public int UserId { get; set; }

        public int Rating { get; set; }

        public string? Text { get; set; }
    }
}
