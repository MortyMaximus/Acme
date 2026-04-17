using Acme.Models.Database;
using Acme.Repository.Models;

namespace Acme.Repository.Repository
{
    internal class UserRepository : IGenericCrudRepository<UserModel>
    {
        private readonly AcmeContext context;

        public UserRepository(AcmeContext context)
        {
            this.context = context;
        }

        async Task IGenericCrudRepository<UserModel>.CreateAsync(UserModel model)
        {
            if (model is null)
            {
                throw new NullReferenceException("Cannot create an user from nothing.");
            }
            else
            {
                await context.AddAsync(model.ToDbModel());
                await context.SaveChangesAsync();
            }
        }

        async Task<IEnumerable<UserModel>> IGenericCrudRepository<UserModel>.ReadAsync(int id)
        {
            var result = context.Users.Where(m => m.Id == id);

            if (result is null)
            {
                return Enumerable.Empty<UserModel>();
            }
            else
            {
                return result.Select(m => m.ToModel()).ToList();
            }
        }

        Task<IEnumerable<UserModel>> IGenericCrudRepository<UserModel>.ReadAsync()
        {
            var result = context.Users.Select(m => m.ToModel());

            return Task.FromResult(result.AsEnumerable());
        }

        Task IGenericCrudRepository<UserModel>.UpdateAsync(UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}