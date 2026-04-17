using Acme.Models;
using Acme.Models.BaseModels;
using Acme.Repository.Repository;

namespace Acme.Repository
{
    public interface ISerialNumberRepository: IGenericCrudRepository<SerialNumberModel>
    {
        public Task<IEnumerable<DrawModel>> GetDrawModel();      
        public Task<IEnumerable<SerialNumberModel>> ReadAsync(string serialNumber);
        public Task Add100SerialNumbersAsync();
    }
}