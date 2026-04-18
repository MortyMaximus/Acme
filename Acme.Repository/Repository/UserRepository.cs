using Acme.Models.Database;
using Acme.Repository.Models;
using Acme.Repository.Repository.Interfaces;

namespace Acme.Repository.Repository
{
    internal class UserRepository : IUserRepository
    {
        private readonly AcmeContext context;

        public UserRepository(AcmeContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(UserModel model)
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

        public async Task<IEnumerable<UserModel>> ReadAsync(int id)
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

        public Task<IEnumerable<UserModel>> ReadAsync()
        {
            var result = context.Users.Select(m => m.ToModel());

            return Task.FromResult(result.AsEnumerable());
        }

        public Task UpdateAsync(UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}