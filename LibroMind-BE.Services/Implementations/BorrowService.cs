using AutoMapper;
using LibroMind_BE.Common.DateTimeProvider;
using LibroMind_BE.DAL.Entities;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.UnitOfWork;
using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models;
using Microsoft.AspNetCore.Http;

namespace LibroMind_BE.Services.Implementations
{
    public class BorrowService : IBorrowService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeProvider _dateTimeProvider;

        public BorrowService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IDateTimeProvider dateTimeProvider)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<IEnumerable<BorrowGetDTO>> FindBorrowingsAsync()
        {
            return _mapper.Map<IEnumerable<BorrowGetDTO>>(await _unitOfWork.BorrowRepository.FindAllAsync());
        }

        public async Task<IEnumerable<BorrowingDetailsGetDTO>> FindBorrowingsByLibraryIdAsync(int libraryId)
        {
            return _mapper.Map<IEnumerable<BorrowingDetailsGetDTO>>(await _unitOfWork.BorrowRepository.FindBorrowingsByLibraryIdAsync(libraryId));
        }

        public async Task<IEnumerable<BorrowingDetailsGetDTO>> FindBorrowingsByLibraryIdAndParamAsync(int libraryId, string? searchParam)
        {
            return _mapper.Map<IEnumerable<BorrowingDetailsGetDTO>>(
                await _unitOfWork.BorrowRepository.FindBorrowingsByLibraryIdAndParamAsync(libraryId, searchParam));
        }

        public async Task<IEnumerable<BorrowingDetailsGetDTO>> FindBorrowingsByUserIdAsync(int userId)
        {
            return _mapper.Map<IEnumerable<BorrowingDetailsGetDTO>>(await _unitOfWork.BorrowRepository.FindBorrowingsByUserIdAsync(userId));
        }

        public async Task<BorrowGetDTO> FindBorrowingByIdAsync(int id)
        {
            var existingBorrow = await _unitOfWork.BorrowRepository.FindByIdAsync(id);

            if (existingBorrow is null)
            {
                throw new BadHttpRequestException("Borrow not found", StatusCodes.Status404NotFound);
            }

            return _mapper.Map<BorrowGetDTO>(existingBorrow);
        }

        public async Task AddBorrowingAsync(BorrowPostDTO borrowToAdd)
        {
            if (await _unitOfWork.BookLibraryRepository.FindByIdAsync(borrowToAdd.BookLibraryId) is not BookLibrary bookLibrary)
            {
                throw new BadHttpRequestException("BookLibrary not found", StatusCodes.Status404NotFound);
            }

            if (await _unitOfWork.UserRepository.FindByIdAsync(borrowToAdd.UserId) is null)
            {
                throw new BadHttpRequestException("User not found", StatusCodes.Status404NotFound);
            }

            if (await _unitOfWork.BorrowRepository.CountAsync(b => b.UserId == borrowToAdd.UserId && b.HasReturnedBook != true) > 2)
            {
                throw new BadHttpRequestException("User has reached the maximum number of borrows!");
            }

            if (bookLibrary.Quantity < 1)
            {
                throw new BadHttpRequestException("All available books have been borrowed!", StatusCodes.Status400BadRequest);
            }

            var newBorrow = _mapper.Map<Borrow>(borrowToAdd);

            newBorrow.BorrowingDate = _dateTimeProvider.UtcNow;
            newBorrow.ReturnDate = newBorrow.BorrowingDate.AddDays(14);
            newBorrow.HasReturnedBook = null;
            newBorrow.WasExtensionRequested = false;

            _unitOfWork.BorrowRepository.Add(newBorrow);
            _unitOfWork.BookLibraryRepository.Update(bookLibrary);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateBorrowingAsync(int id, BorrowPutDTO borrowToUpdate)
        {
            var existingBorrow = await _unitOfWork.BorrowRepository.FindByIdAsync(id);

            if (existingBorrow is null)
            {
                throw new BadHttpRequestException("Borrow not found", StatusCodes.Status404NotFound);
            }

            _mapper.Map(borrowToUpdate, existingBorrow);

            _unitOfWork.BorrowRepository.Update(existingBorrow);

            await _unitOfWork.CommitAsync();
        }

        public async Task AcceptBorrowingAsync(int id)
        {
            var existingBorrow = await _unitOfWork.BorrowRepository.FindByIdAsync(id);

            if (existingBorrow is null)
            {
                throw new BadHttpRequestException("Borrow not found", StatusCodes.Status404NotFound);
            }

            if (existingBorrow.HasReturnedBook != null)
            {
                throw new BadHttpRequestException("Borrowing has already been accepted or was returned!", StatusCodes.Status404NotFound);
            }

            if (await _unitOfWork.BookLibraryRepository.FindByIdAsync(existingBorrow.BookLibraryId) is not BookLibrary existingBookLibrary)
            {
                throw new BadHttpRequestException("BookLibrary from borrow not found", StatusCodes.Status404NotFound);
            }

            if (existingBookLibrary.Quantity < 1)
            {
                throw new BadHttpRequestException("All available books have been borrowed!", StatusCodes.Status400BadRequest);
            }

            existingBookLibrary.Quantity--;
            existingBorrow.HasReturnedBook = false;

            _unitOfWork.BorrowRepository.Update(existingBorrow);
            _unitOfWork.BookLibraryRepository.Update(existingBookLibrary);

            await _unitOfWork.CommitAsync();
        }

        public async Task ExtendBorrowingAsync(int id)
        {
            var existingBorrow = await _unitOfWork.BorrowRepository.FindByIdAsync(id);

            if (existingBorrow is null)
            {
                throw new BadHttpRequestException("Borrow not found", StatusCodes.Status404NotFound);
            }

            if (existingBorrow.HasReturnedBook == true)
            {
                throw new BadHttpRequestException(
                    "Borrow cannot be extended because book was returned!",
                    StatusCodes.Status400BadRequest);
            }

            if (existingBorrow.WasExtensionRequested == false)
            {
                throw new BadHttpRequestException(
                    "Extension was not requested or has already been granted!",
                    StatusCodes.Status404NotFound);
            }

            if (existingBorrow.ReturnDate.Subtract(_dateTimeProvider.UtcNow).TotalDays > 3)
            {
                throw new BadHttpRequestException(
                    "Extension can only be approved with less than 3 days before the return date!",
                    StatusCodes.Status400BadRequest);
            }

            existingBorrow.ReturnDate = existingBorrow.ReturnDate.AddDays(14);
            existingBorrow.WasExtensionRequested = false;

            _unitOfWork.BorrowRepository.Update(existingBorrow);

            await _unitOfWork.CommitAsync();
        }

        public async Task ReturnBorrowingAsync(int id)
        {
            var existingBorrow = await _unitOfWork.BorrowRepository.FindByIdAsync(id);

            if (existingBorrow is null)
            {
                throw new BadHttpRequestException("Borrowing not found", StatusCodes.Status404NotFound);
            }

            if (existingBorrow.HasReturnedBook == true)
            {
                throw new BadHttpRequestException("Borrowing has already been returned!", StatusCodes.Status404NotFound);
            }

            if (await _unitOfWork.BookLibraryRepository.FindByIdAsync(existingBorrow.BookLibraryId) is not BookLibrary existingBookLibrary)
            {
                throw new BadHttpRequestException("BookLibrary from borrowing not found", StatusCodes.Status404NotFound);
            }

            existingBookLibrary.Quantity++;
            existingBorrow.HasReturnedBook = true;
            existingBorrow.ReturnDate = _dateTimeProvider.UtcNow;

            _unitOfWork.BorrowRepository.Update(existingBorrow);

            await _unitOfWork.CommitAsync();
        }

        public async Task RequestExtensionForBorrowingAsync(int id)
        {
            var existingBorrow = await _unitOfWork.BorrowRepository.FindByIdAsync(id);

            if (existingBorrow is null)
            {
                throw new BadHttpRequestException("Borrow not found", StatusCodes.Status404NotFound);
            }

            if (existingBorrow.HasReturnedBook == true)
            {
                throw new BadHttpRequestException(
                    "Borrow cannot be extended because book was returned!",
                    StatusCodes.Status400BadRequest);
            }

            if (existingBorrow.WasExtensionRequested == true)
            {
                throw new BadHttpRequestException(
                    "Extension for this borrowing has already been requested!",
                    StatusCodes.Status400BadRequest);
            }

            if (existingBorrow.ReturnDate.Subtract(_dateTimeProvider.UtcNow).TotalDays > 3) {
                throw new BadHttpRequestException(
                    "Extension can only be requested with less than 3 days before the return date!",
                    StatusCodes.Status400BadRequest);
            }

            existingBorrow.WasExtensionRequested = true;

            _unitOfWork.BorrowRepository.Update(existingBorrow);

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteBorrowingAsync(int id)
        {
            var existingBorrow = await _unitOfWork.BorrowRepository.FindByIdAsync(id);

            if (existingBorrow is null)
            {
                throw new BadHttpRequestException("Borrow not found", StatusCodes.Status404NotFound);
            }

            _unitOfWork.BorrowRepository.Remove(existingBorrow);

            await _unitOfWork.CommitAsync();
        }
    }
}
