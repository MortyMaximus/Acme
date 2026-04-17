using Acme.Repository.Models;
using Acme.Repository.Repository;

namespace Acme.Repository
{
    public class RepositoryFacade : IRepositoryFacade
    {
        private readonly AcmeContext context;

        public RepositoryFacade(AcmeContext context)
        {
            this.context = context;
        }

        ICustomerRepository IRepositoryFacade.CustomerRepository() =>
            new CustomerRepository(context);

        ISerialNumberRepository IRepositoryFacade.SerialNumberRepository() => 
            new SerialNumberRepository(context);

        IUserRepository IRepositoryFacade.UserRepository() => 
            new UserRepository(context);
    }
}
