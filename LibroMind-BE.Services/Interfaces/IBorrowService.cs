using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Interfaces
{
    public interface IBorrowService
    {
        public Task<IEnumerable<BorrowGetDTO>> FindBorrowesAsync();
        public Task<BorrowGetDTO> FindBorrowByIdAsync(int id);
        public Task AddBorrow(BorrowPostDTO BorrowToAdd);
        public Task UpdateBorrow(int id, BorrowPutDTO BorrowToUpdate);
        public Task DeleteBorrow(int id);
    }
}
