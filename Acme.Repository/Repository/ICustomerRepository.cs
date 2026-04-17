using Acme.Models;
using Acme.Models.BaseModels;
using Acme.Repository.Repository;

namespace Acme.Repository
{
    public interface ICustomerRepository: IGenericCrudRepository<CustomerModel>
    {
        public Task<IEnumerable<CustomerModel>> ReadAsync(string email);
    }
}