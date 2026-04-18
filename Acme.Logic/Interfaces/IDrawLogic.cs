using Acme.Models;

namespace Acme.Logic.Interfaces
{
    public interface IDrawLogic
    {
        /// <summary>
        /// Asynchronously retrieves all draw records.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of all
        /// draw records. The collection will be empty if no records are found.</returns>
        Task<Pagination<DrawModel>> GetAllAsync(int pageSize, int pageIndex);

        /// <summary>
        /// Adds the specified draw model to the serial number collection.
        /// </summary>
        /// <param name="model">The draw model to add to the serial number collection. Cannot be null.</param>
        void AddToSerialNumber(DrawModel model);

    }
}
