namespace LibroMind_BE.Services.Models
{
    public class BookCategoryDetailsGetDTO
    {
        public int Id { get; set; }

        public BookGetDTO Book { get; set; } = null!;

        public CategoryGetDTO Category { get; set; } = null!;
    }
}