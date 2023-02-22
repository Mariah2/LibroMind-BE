using LibroMind_BE.DAL.Repositories.Interfaces;

namespace LibroMind_BE.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAddressRepository AddressRepository { get; }
    }
}