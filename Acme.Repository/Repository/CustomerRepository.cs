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

        async Task IGenericCrudRepository<CustomerModel>.CreateAsync(CustomerModel model)
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

        async Task<IEnumerable<CustomerModel>> IGenericCrudRepository<CustomerModel>.ReadAsync(int id)
        {
            var result = context.Customers.Where(m => m.Id == id);

            if (result is null)
            {
                return Enumerable.Empty<CustomerModel>();
            }
            else
            {
                return result.Select(m => m.ToModel()).ToList();
            }
        }

        Task<IEnumerable<CustomerModel>> IGenericCrudRepository<CustomerModel>.ReadAsync()
        {
            var result = context.Customers.Select(m => m.ToModel());

            return Task.FromResult(result.AsEnumerable());
        }

        Task IGenericCrudRepository<CustomerModel>.UpdateAsync(CustomerModel model)
        {
            throw new NotImplementedException();
        }
    }
}