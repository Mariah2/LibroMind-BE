namespace LibroMind_BE.Services.Models.Get
{
    public class UserProfileGetDTO
    {
        public int? AddressId { get; set; }

        public int? LibraryId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateTime BirthDate { get; set; }

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public LibraryGetDTO Library { get; set; } = null!;
    }
}
