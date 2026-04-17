using Acme.Models.Database;

namespace Acme.Repository.Repository
{
    public interface IUserRepository
    {
        Task CreateAsync(UserModel model);
        Task<IEnumerable<UserModel>> ReadAsync();
        Task<IEnumerable<UserModel>> ReadAsync(int id);
        Task UpdateAsync(UserModel model);
    }
}