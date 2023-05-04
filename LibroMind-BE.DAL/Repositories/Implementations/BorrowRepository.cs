using LibroMind_BE.DAL.Entities;
using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibroMind_BE.DAL.Repositories.Implementations
{
    public class BorrowRepository : BaseRepository<Borrow>, IBorrowRepository
    {

        public BorrowRepository(LibroMindContext context) : base(context) { }

        public async Task<IEnumerable<BorrowingDetails>> FindBorrowingsByLibraryIdAsync(int libraryId)
        {
            return await _context.Borrows
                .Include(b => b.BookLibrary)
                    .ThenInclude(b => b.Book)
                .Include(b => b.User)
                .Where(b => b.HasReturnedBook != true && b.BookLibrary.LibraryId == libraryId)
                .Select(b => new BorrowingDetails()
                {
                    Id = b.Id,
                    UserId = b.UserId,
                    BookLibraryId = b.BookLibraryId,
                    BorrowingDate = b.BorrowingDate,
                    ReturnDate = b.ReturnDate,
                    HasReturnedBook = b.HasReturnedBook,
                    WasExtensionRequested = b.WasExtensionRequested,
                    Book = new BookCard()
                    {
                        Id = b.BookLibrary.Book.Id,
                        Title = b.BookLibrary.Book.Title,
                        CoverUrl = b.BookLibrary.Book.CoverUrl,
                        Rating = b.BookLibrary.Book.Rating,
                        Author = b.BookLibrary.Book.Author,
                    },
                    User = new UserBasicInfo()
                    {
                        FirstName = b.User.FirstName,
                        LastName = b.User.LastName,
                        Email = b.User.Email,
                        Phone = b.User.Phone,
                        BirthDate = b.User.BirthDate,
                    }
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<BorrowingDetails>> FindBorrowingsByLibraryIdAndParamAsync(int libraryId, string? searchParam)
        {
            if (!String.IsNullOrEmpty(searchParam))
            {
                return await _context.Borrows
                    .Include(b => b.BookLibrary)
                        .ThenInclude(b => b.Book)
                    .Include(b => b.User)
                    .Where(b => b.HasReturnedBook != true && b.BookLibrary.LibraryId == libraryId &&
                        (b.User.FirstName + b.User.LastName)
                        .ToLower()
                        .Contains(searchParam
                            .Trim()
                            .ToLower()))
                    .Select(b => new BorrowingDetails()
                    {
                        Id = b.Id,
                        UserId = b.UserId,
                        BookLibraryId = b.BookLibraryId,
                        BorrowingDate = b.BorrowingDate,
                        ReturnDate = b.ReturnDate,
                        HasReturnedBook = b.HasReturnedBook,
                        WasExtensionRequested = b.WasExtensionRequested,
                        Book = new BookCard()
                        {
                            Id = b.BookLibrary.Book.Id,
                            Title = b.BookLibrary.Book.Title,
                            CoverUrl = b.BookLibrary.Book.CoverUrl,
                            Rating = b.BookLibrary.Book.Rating,
                            Author = b.BookLibrary.Book.Author,
                        },
                        User = new UserBasicInfo()
                        {
                            FirstName = b.User.FirstName,
                            LastName = b.User.LastName,
                            Email = b.User.Email,
                            Phone = b.User.Phone,
                            BirthDate = b.User.BirthDate,
                        }
                    })
                    .ToListAsync();
            }

            return await FindBorrowingsByLibraryIdAsync(libraryId);
        }

        public async Task<IEnumerable<BorrowingDetails>> FindBorrowingsByUserIdAsync(int userId)
        {
            return await _context.Borrows
                .Include(b => b.BookLibrary)
                    .ThenInclude(b => b.Book)
                .Include(b => b.User)
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.HasReturnedBook != true)
                .Select(b => new BorrowingDetails()
                {
                    Id = b.Id,
                    UserId = b.UserId,
                    BookLibraryId = b.BookLibraryId,
                    BorrowingDate = b.BorrowingDate,
                    ReturnDate = b.ReturnDate,
                    HasReturnedBook = b.HasReturnedBook,
                    WasExtensionRequested = b.WasExtensionRequested,
                    Book = new BookCard()
                    {
                        Id = b.BookLibrary.Book.Id,
                        Title = b.BookLibrary.Book.Title,
                        CoverUrl = b.BookLibrary.Book.CoverUrl,
                        Rating = b.BookLibrary.Book.Rating,
                        Author = b.BookLibrary.Book.Author,
                    },
                    User = new UserBasicInfo()
                    {
                        FirstName = b.User.FirstName,
                        LastName = b.User.LastName,
                        Email = b.User.Email,
                        Phone = b.User.Phone,
                        BirthDate = b.User.BirthDate,
                    }
                })
                .ToListAsync();
        }
    }
}