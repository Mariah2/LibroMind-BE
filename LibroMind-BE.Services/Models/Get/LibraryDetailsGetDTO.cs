namespace LibroMind_BE.Services.Models
{
    public class LibraryDetailsGetDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public AddressGetDTO Address { get; set; } = null!;
    }
}
