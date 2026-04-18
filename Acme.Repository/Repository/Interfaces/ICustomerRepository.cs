using Acme.Models.BaseModels;
using Acme.Repository.Repository;

namespace Acme.Repository.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Gets a specified customer based on the email address.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<IEnumerable<CustomerModel>> ReadAsync(string email);

        /// <summary>
        /// Create a customer based on the model input.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task CreateAsync(CustomerModel model);
    }
}