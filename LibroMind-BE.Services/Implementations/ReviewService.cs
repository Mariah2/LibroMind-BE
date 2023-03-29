using AutoMapper;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.UnitOfWork;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Http;
using System.Transactions;

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
            if (await _unitOfWork.BookRepository.FindByIdAsync(reviewToAdd.BookId) is not Book book)
            {
                throw new BadHttpRequestException(
                    "Book not found",
                    StatusCodes.Status404NotFound);
            }

            if (await _unitOfWork.UserRepository.FindByIdAsync(reviewToAdd.UserId) is null)
            {
                throw new BadHttpRequestException(
                    "User not found",
                    StatusCodes.Status404NotFound);
            }
            if (await _unitOfWork.ReviewRepository.CountAsync(r =>
                r.UserId == reviewToAdd.UserId && r.BookId == reviewToAdd.BookId) > 0)
            {
                throw new BadHttpRequestException(
                    "User has already reviewed this book!",
                    StatusCodes.Status400BadRequest);
            }

            var newReview = _mapper.Map<Review>(reviewToAdd);

            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                _unitOfWork.ReviewRepository.Add(newReview);

                await _unitOfWork.CommitAsync();

                await CalculateRating(book);

                transaction.Complete();
            }
            catch
            {
                await _unitOfWork.RollBackAsync();
            }
        }

        public async Task UpdateReview(int id, ReviewPostDTO reviewToUpdate)
        {
            var existingReview = await _unitOfWork.ReviewRepository.FindByIdAsync(id);

            if (existingReview is null)
            {
                throw new BadHttpRequestException("Review not found", StatusCodes.Status404NotFound);
            }

            if (await _unitOfWork.BookRepository.FindByIdAsync(reviewToUpdate.BookId) is not Book book)
            {
                throw new BadHttpRequestException("Book not found", StatusCodes.Status404NotFound);
            }

            if (await _unitOfWork.UserRepository.FindByIdAsync(reviewToUpdate.UserId) is null)
            {
                throw new BadHttpRequestException("User not found", StatusCodes.Status404NotFound);
            }

            _mapper.Map(reviewToUpdate, existingReview);

            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                _unitOfWork.ReviewRepository.Update(existingReview);

                await _unitOfWork.CommitAsync();

                await CalculateRating(book);

                transaction.Complete();
            }
            catch
            {
                await _unitOfWork.RollBackAsync();
            }
        }

        public async Task DeleteReview(int id)
        {
            var existingReview = await _unitOfWork.ReviewRepository.FindByIdAsync(id);

            if (existingReview is null)
            {
                throw new BadHttpRequestException("Review not found", StatusCodes.Status404NotFound);
            }

            if (await _unitOfWork.BookRepository.FindByIdAsync(existingReview.BookId) is not Book book)
            {
                throw new BadHttpRequestException("Book not found", StatusCodes.Status404NotFound);
            }

            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                _unitOfWork.ReviewRepository.Remove(existingReview);

                await _unitOfWork.CommitAsync();

                await CalculateRating(book);

                transaction.Complete();
            }
            catch
            {
                await _unitOfWork.RollBackAsync();
            }
        }

        private async Task CalculateRating(Book book)
        {
            var reviews = await _unitOfWork.ReviewRepository.FindAsync(r => r.BookId == book.Id);

            double rating = 0;

            foreach (var review in reviews)
            {
                rating += review.Rating;
            }

            var reviewsCount = reviews.Count();

            if (reviewsCount > 0)
            {
                rating /= reviewsCount;
            }

            book.Rating = rating;

            _unitOfWork.BookRepository.Update(book);

            await _unitOfWork.CommitAsync();
        }
    }
}
