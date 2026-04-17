using Acme.Models.BaseModels;
using Acme.Models.Database;
using Acme.Repository.Repository;

namespace Acme.Repository
{
    public interface IRepositoryFacade
    {
        public ICustomerRepository CustomerRepository();
        public IGenericCrudRepository<UserModel> UserRepository();
        public ISerialNumberRepository SerialNumberRepository();
    }
}
