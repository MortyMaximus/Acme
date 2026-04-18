using Acme.Models;
using Acme.Models.BaseModels;

namespace Acme.Repository.Repository.Interfaces
{
    public interface ISerialNumberRepository
    {
        /// <summary>
        /// Adds 100 new serial numbers to the repository asynchronously.
        /// </summary>
        /// <returns></returns>
        Task Add100SerialNumbersAsync();

        /// <summary>
        /// Get a list of DrawModel for each serial number that has an user attached to it. This is used for the draw.
        /// </summary>
        /// <returns></returns>
        Task<Pagination<DrawModel>> GetDrawModel(int pageSize, int pageIndex);

        /// <summary>
        /// Get a full list of SerialNumberModel.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SerialNumberModel>> ReadAsync();

        /// <summary>
        /// Get a specific SerialNumberModel by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<SerialNumberModel>> ReadAsync(int id);

        /// <summary>
        /// Get a specific SerialNumberModel by its serial number.
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        Task<IEnumerable<SerialNumberModel>> ReadAsync(string serialNumber);
        
        /// <summary>
        /// Update user attached to the specified serial number.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task UpdateAsync(SerialNumberModel model);
    }
}