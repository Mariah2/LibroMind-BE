using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Interfaces
{
    public interface IBorrowService
    {
        public Task<IEnumerable<BorrowGetDTO>> FindBorrowingsAsync();
        public Task<BorrowGetDTO> FindBorrowingByIdAsync(int id);
        public Task<IEnumerable<BorrowingDetailsGetDTO>> FindBorrowingsByLibraryIdAsync(int libraryId);
        public Task<IEnumerable<BorrowingDetailsGetDTO>> FindBorrowingsByLibraryIdAndParamAsync(int libraryId, string? searchParam);
        public Task<IEnumerable<BorrowingDetailsGetDTO>> FindBorrowingsByUserIdAsync(int userId);
        public Task AddBorrowingAsync(BorrowPostDTO borrowingToAdd);
        public Task UpdateBorrowingAsync(int id, BorrowPutDTO borrowingToUpdate);
        public Task AcceptBorrowingAsync(int id);
        public Task ExtendBorrowingAsync(int id);
        public Task ReturnBorrowingAsync(int id);
        public Task RequestExtensionForBorrowingAsync(int id);
        public Task DeleteBorrowingAsync(int id);
    }
}
