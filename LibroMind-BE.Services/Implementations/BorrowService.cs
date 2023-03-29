using AutoMapper;
using LibroMind_BE.Common.DateTimeProvider;
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

        public async Task<IEnumerable<BorrowGetDTO>> FindBorrowesAsync()
        {
            return _mapper.Map<IEnumerable<BorrowGetDTO>>(await _unitOfWork.BorrowRepository.FindAllAsync());
        }

        public async Task<BorrowGetDTO> FindBorrowByIdAsync(int id)
        {
            var existingBorrow = await _unitOfWork.BorrowRepository.FindByIdAsync(id);

            if (existingBorrow is null)
            {
                throw new BadHttpRequestException("Borrow not found", StatusCodes.Status404NotFound);
            }

            return _mapper.Map<BorrowGetDTO>(existingBorrow);
        }

        public async Task AddBorrow(BorrowPostDTO borrowToAdd)
        {
            if (await _unitOfWork.BookRepository.FindByIdAsync(borrowToAdd.BookLibraryId) is null)
            {
                throw new BadHttpRequestException("BookLibrary not found", StatusCodes.Status404NotFound);
            }

            if (await _unitOfWork.UserRepository.FindByIdAsync(borrowToAdd.UserId) is null)
            {
                throw new BadHttpRequestException("User not found", StatusCodes.Status404NotFound);
            }

            var newBorrow = _mapper.Map<Borrow>(borrowToAdd);

            newBorrow.BorrowingDate = _dateTimeProvider.UtcNow;
            newBorrow.ReturnDate = newBorrow.BorrowingDate.AddDays(14);
            newBorrow.HasReturnedBook = false;

            _unitOfWork.BorrowRepository.Add(newBorrow);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateBorrow(int id, BorrowPutDTO borrowToUpdate)
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

        public async Task ExtendBorrow(int id)
        {
            var existingBorrow = await _unitOfWork.BorrowRepository.FindByIdAsync(id);

            if (existingBorrow is null)
            {
                throw new BadHttpRequestException("Borrow not found", StatusCodes.Status404NotFound);
            }

            existingBorrow.ReturnDate = existingBorrow.ReturnDate.AddDays(14);

            _unitOfWork.BorrowRepository.Update(existingBorrow);

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteBorrow(int id)
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
