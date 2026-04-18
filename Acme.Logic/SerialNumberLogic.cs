using Acme.Logic.Interfaces;
using Acme.Repository;

namespace Acme.Logic
{
    public class SerialNumberLogic : ISerialNumberLogic
    {
        private readonly IRepositoryFacade _repository;

        public SerialNumberLogic(IRepositoryFacade repository)
        {
            _repository = repository;
        }

        public async Task Create100SerialNumbersAsync()
        {
            await _repository.SerialNumberRepository().Add100SerialNumbersAsync();
        }

        public IEnumerable<string> GetValidSerialNumber()
        {
            return (_repository.SerialNumberRepository().ReadAsync().Result).Select(s => s.SerialNumber);
        }

    }
}
