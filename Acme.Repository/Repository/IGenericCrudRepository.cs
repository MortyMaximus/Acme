namespace Acme.Repository.Repository
{
    public interface IGenericCrudRepository<Models>
    {
        /// <summary>
        /// Creates a new entity based on the specified model.
        /// </summary>
        /// <param name="model">The model containing the data for the entity to be created. Cannot be null.</param>
        public Task CreateAsync(Models model);

        /// <summary>
        /// Retrieves a collection of model entities.
        /// </summary>
        /// <returns>Contains an enumerable collection of
        /// model entities. The collection is empty if no entities are found.</returns>
        public Task<IEnumerable<Models>> ReadAsync();

        /// <summary>
        /// Retrieves a collection of models associated with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier used to select the models to retrieve.</param>
        /// <returns>Contains an enumerable collection of
        /// models associated with the specified identifier. The collection is empty if no models are found.</returns>
        public Task<IEnumerable<Models>> ReadAsync(int id);

        /// <summary>
        /// Updates the specified model in the data store.
        /// </summary>
        /// <param name="model">The model instance containing the updated values to be saved. Cannot be null.</param>
        public Task UpdateAsync(Models model);
    }
}