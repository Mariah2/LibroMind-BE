namespace LibroMind_BE.Services.Models
{
    public class LibraryGetDTO
    {
        public int Id { get; set; }

        public int AddressId { get; set; }

        public string Name { get; set; } = null!;
    }
}
