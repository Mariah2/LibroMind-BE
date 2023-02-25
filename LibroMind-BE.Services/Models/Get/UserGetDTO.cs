namespace LibroMind_BE.Services.Models
{
    public class UserGetDTO
    {
        public int Id { get; set; }

        public int? AddressId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateTime BirthDate { get; set; }

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;
    }
}
