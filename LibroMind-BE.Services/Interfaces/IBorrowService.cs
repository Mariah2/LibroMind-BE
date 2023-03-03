using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Interfaces
{
    public interface IBorrowService
    {
        public Task<IEnumerable<BorrowGetDTO>> FindBorrowesAsync();
        public Task<BorrowGetDTO> FindBorrowByIdAsync(int id);
        public Task AddBorrow(BorrowPostDTO borrowToAdd);
        public Task UpdateBorrow(int id, BorrowPutDTO borrowToUpdate);
        public Task DeleteBorrow(int id);
        public Task ExtendBorrow(int id);
    }
}
