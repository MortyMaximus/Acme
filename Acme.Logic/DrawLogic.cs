using Acme.Models;
using Acme.Models.BaseModels;
using Acme.Repository;

namespace Acme.Logic
{
    public class DrawLogic : IDrawLogic
    {
        private IRepositoryFacade _repository { get; }

        public DrawLogic(IRepositoryFacade repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DrawModel>> GetAllAsync()
        {
            try
            {
                return await _repository.SerialNumberRepository().GetDrawModel();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving models", ex);
            }
        }

        public void AddToSerialNumber(DrawModel model)
        {
            try
            {
                var serialNumber = _repository.SerialNumberRepository().ReadAsync(model.SerialNumber).Result.FirstOrDefault();
                if (serialNumber == null) throw new NullReferenceException("Couldn't find any match.");
                if (serialNumber.FirstCustomer != null && serialNumber.SecondCustomer != null) throw new Exception($"Serial number {model.SerialNumber} already has 2 customers.");

                AddCustomer(model);
                AddCustomerToSerialNumber(model, serialNumber);
            }
            catch (NullReferenceException)
            {
                throw;
            }

            catch (Exception)
            {
                throw;
            }
        }

        private void AddCustomerToSerialNumber(DrawModel model, SerialNumberModel serialNumber)
        {
            if (serialNumber.FirstCustomer == null)
                serialNumber.FirstCustomer = model.ToCustomerModel();

            else if (serialNumber.SecondCustomer == null)
                serialNumber.SecondCustomer = model.ToCustomerModel();

            else
                return;

            _repository.SerialNumberRepository().UpdateAsync(serialNumber).Wait();
        }

        private void AddCustomer(DrawModel model)
        {
            try
            {
                var customer = _repository.CustomerRepository().ReadAsync(model.Email).Result.FirstOrDefault();
                if (customer == null)
                    _repository.CustomerRepository().CreateAsync(model.ToCustomerModel()).Wait();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
