using LibroMind_BE.DAL.Models;
using LibroMind_BE.DAL.Repositories.Implementations;
using LibroMind_BE.DAL.Repositories.Interfaces;

namespace LibroMind_BE.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibroMindContext _context;

        private IAddressRepository _addressRepository;

        public UnitOfWork(LibroMindContext context)
        {
            _context = context;
        }

        public IAddressRepository AddressRepository
            => _addressRepository ??= new AddressRepository(_context);
    }
}
