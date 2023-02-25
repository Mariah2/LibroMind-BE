namespace LibroMind_BE.Services.Models
{
    public class BorrowPutDTO
    {
        public DateTime ReturnDate { get; set; }

        public bool HasReturnedBook { get; set; }
    }
}