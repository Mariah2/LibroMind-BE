namespace LibroMind_BE.Services.Models
{
    public class AuthorGetDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateTime BirthDate { get; set; }

        public string? Nationality { get; set; }
    }
}