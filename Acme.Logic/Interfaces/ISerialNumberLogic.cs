namespace Acme.Logic.Interfaces
{
    public interface ISerialNumberLogic
    {
        /// <summary>
        /// Asynchronously creates 100 new serial numbers.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task Create100SerialNumbersAsync();

        /// <summary>
        /// Retrieves a collection of valid serial numbers.
        /// </summary>
        /// <returns>An enumerable collection of strings, each representing a valid serial number. The collection will be empty
        /// if no valid serial numbers are available.</returns>
        IEnumerable<string> GetValidSerialNumber();
    }
}