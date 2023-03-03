using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.Repositories.Implementations;
using LibroMind_BE.DAL.Repositories.Interfaces;

namespace LibroMind_BE.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibroMindContext _context;

        private IAddressRepository? _addressRepository;
        private IAuthorRepository? _authorRepository;
        private IBookRepository? _bookRepository;
        private IBookCategoryRepository? _bookCategoryRepository;
        private IBookLibraryRepository? _bookLibraryRepository;
        private IBorrowRepository? _borrowRepository;
        private ICategoryRepository? _categoryRepository;
        private ILibraryRepository? _libraryRepository;
        private IPublisherRepository? _publisherRepository;
        private IRoleRepository? _roleRepository;
        private IUserRepository? _userRepository;
        private IReviewRepository? _reviewRepository;

        public UnitOfWork(LibroMindContext context)
        {
            _context = context;
        }

        public IAddressRepository AddressRepository
            => _addressRepository ??= new AddressRepository(_context);
        public IAuthorRepository AuthorRepository
            => _authorRepository ??= new AuthorRepository(_context);
        public IBookRepository BookRepository
            => _bookRepository ??= new BookRepository(_context);
        public IBookCategoryRepository BookCategoryRepository
            => _bookCategoryRepository ??= new BookCategoryRepository(_context);
        public IBookLibraryRepository BookLibraryRepository
            => _bookLibraryRepository ??= new BookLibraryRepository(_context);
        public IBorrowRepository BorrowRepository
            => _borrowRepository ??= new BorrowRepository(_context);
        public ICategoryRepository CategoryRepository
            => _categoryRepository ??= new CategoryRepository(_context);
        public ILibraryRepository LibraryRepository
            => _libraryRepository ??= new LibraryRepository(_context);
        public IPublisherRepository PublisherRepository
            => _publisherRepository ??= new PublisherRepository(_context);
        public IRoleRepository RoleRepository
            => _roleRepository ??= new RoleRepository(_context);
        public IUserRepository UserRepository
            => _userRepository ??= new UserRepository(_context);
        public IReviewRepository ReviewRepository
            => _reviewRepository ??= new ReviewRepository(_context);

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task RollBackAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }
    }
}
