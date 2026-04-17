using Acme.Repository.Repository;

namespace Acme.Repository
{
    public interface IRepositoryFacade
    {
        public ICustomerRepository CustomerRepository();
        public IUserRepository UserRepository();
        public ISerialNumberRepository SerialNumberRepository();
    }
}
