using Acme.Repository.Repository.Interfaces;

namespace Acme.Repository
{
    public interface IRepositoryFacade
    {
        public ICustomerRepository CustomerRepository();
        public IUserRepository UserRepository();
        public ISerialNumberRepository SerialNumberRepository();
    }
}
