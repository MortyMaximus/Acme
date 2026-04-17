using Acme.Models.BaseModels;
using Acme.Repository.Models;

namespace Acme.Repository.Repository
{
    internal class CustomerRepository : ICustomerRepository
    {
        private readonly AcmeContext context;

        public CustomerRepository(AcmeContext context)
        {
            this.context = context;
        }

        public Task<IEnumerable<CustomerModel>> ReadAsync(string email)
        {
            var result = context.Customers.Where(m => m.Email == email);
            if (result is null)
            {
                return Task.FromResult(Enumerable.Empty<CustomerModel>());
            }
            else
            {
                return Task.FromResult(result.Select(m => m.ToModel()).ToList().AsEnumerable());
            }
        }

        public async Task CreateAsync(CustomerModel model)
        {
            if (model is null)
            {
                throw new NotImplementedException("Only Customer entities are supported in this implementation.");
            }
            else
            {
                await context.AddAsync(model.ToDbModel());
                await context.SaveChangesAsync();
            }
        }
    }
}