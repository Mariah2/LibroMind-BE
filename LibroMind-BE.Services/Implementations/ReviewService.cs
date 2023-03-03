using AutoMapper;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.UnitOfWork;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Http;

namespace LibroMind_BE.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReviewGetDTO>> FindReviewesAsync()
        {
            return _mapper.Map<IEnumerable<ReviewGetDTO>>(await _unitOfWork.ReviewRepository.FindAllAsync());
        }

        public async Task<ReviewGetDTO> FindReviewByIdAsync(int id)
        {
            var existingReview = await _unitOfWork.ReviewRepository.FindByIdAsync(id);

            if (existingReview is null)
            {
                throw new BadHttpRequestException("Review not found", StatusCodes.Status404NotFound);
            }

            return _mapper.Map<ReviewGetDTO>(existingReview);
        }

        public async Task AddReview(ReviewPostDTO reviewToAdd)
        {
            var newReview = _mapper.Map<Review>(reviewToAdd);

            _unitOfWork.ReviewRepository.Add(newReview);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateReview(int id, ReviewPutDTO reviewToUpdate)
        {
            var existingReview = await _unitOfWork.ReviewRepository.FindByIdAsync(id);

            if (existingReview is null)
            {
                throw new BadHttpRequestException("Review not found", StatusCodes.Status404NotFound);
            }

            _mapper.Map(reviewToUpdate, existingReview);

            _unitOfWork.ReviewRepository.Update(existingReview);

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteReview(int id)
        {
            var existingReview = await _unitOfWork.ReviewRepository.FindByIdAsync(id);

            if (existingReview is null)
            {
                throw new BadHttpRequestException("Review not found", StatusCodes.Status404NotFound);
            }

            _unitOfWork.ReviewRepository.Remove(existingReview);

            await _unitOfWork.CommitAsync();
        }
    }
}
