﻿using LibroMind_BE.DAL.Repositories.Interfaces;

namespace LibroMind_BE.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAddressRepository AddressRepository { get; }
        IAuthorRepository AuthorRepository { get; }
        IBookRepository BookRepository { get; }
        IBookCategoryRepository BookCategoryRepository { get; }
        IBookLibraryRepository BookLibraryRepository { get; }
        IBookUserRepository BookUserRepository { get; }
        IBorrowRepository BorrowRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ILibraryRepository LibraryRepository { get; }
        IPublisherRepository PublisherRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }
        IReviewRepository ReviewRepository { get; }

        void Commit();
        Task CommitAsync();
        Task RollBackAsync();
    }
}