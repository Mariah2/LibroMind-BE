namespace LibroMind_BE.Services.Models.Put
{
    public class UserPutDTO
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateTime BirthDate { get; set; }

        public string Phone { get; set; } = null!;

        public int? LibraryId { get; set; }
    }
}
