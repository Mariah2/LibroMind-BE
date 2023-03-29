using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Interfaces
{
    public interface IReviewService
    {
        public Task<IEnumerable<ReviewGetDTO>> FindReviewesAsync();
        public Task<ReviewGetDTO> FindReviewByIdAsync(int id);
        public Task AddReview(ReviewPostDTO reviewToAdd);
        public Task UpdateReview(int id, ReviewPostDTO reviewToUpdate);
        public Task DeleteReview(int id);
    }
}
