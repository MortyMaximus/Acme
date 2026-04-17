using Acme.Models;
using Acme.Models.BaseModels;
using Acme.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Acme.Repository.Repository
{
    internal class SerialNumberRepository : ISerialNumberRepository
    {
        private readonly AcmeContext _context;

        public SerialNumberRepository(AcmeContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<DrawModel>> GetDrawModel()
        {
            var result = _context.SerialNumbers
                .Where(s => s.Customer1Navigation != null)
                .Select(c => ToDrawModel(c.Customer1Navigation, c.SerialNumber))
                .ToList();

            result
                .AddRange(_context.SerialNumbers
                .Where(s => s.Customer2Navigation != null)
                .Select(c => ToDrawModel(c.Customer2Navigation, c.SerialNumber)));

            return Task.FromResult(result.AsEnumerable());
        }

        private static DrawModel ToDrawModel(Customer? customer, string serialNumber)
        {
            if (customer is null)
            {
                throw new Exception("Cannot create a draw model without a customer.");
            }
            return new DrawModel
            {
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                SerialNumber = serialNumber
            };
        }

        async Task IGenericCrudRepository<SerialNumberModel>.CreateAsync(SerialNumberModel model)
        {
            if (model is null)
            {
                throw new NullReferenceException("Cannot create a serial number from nothing.");
            }
            else
            {
                await _context.AddAsync(model.ToDbModel());
                await _context.SaveChangesAsync();
            }
        }

        async Task<IEnumerable<SerialNumberModel>> IGenericCrudRepository<SerialNumberModel>.ReadAsync(int id)
        {
            var result = _context.SerialNumbers.Where(m => m.Id == id);

            if (result is null)
            {
                return Enumerable.Empty<SerialNumberModel>();
            }
            else
            {
                return result.Select(m => m.ToModel()).ToList();
            }
        }

        Task<IEnumerable<SerialNumberModel>> IGenericCrudRepository<SerialNumberModel>.ReadAsync()
        {
            var result = _context.SerialNumbers.Select(m => m.ToModel());

            return Task.FromResult(result.AsEnumerable());
        }

        Task IGenericCrudRepository<SerialNumberModel>.UpdateAsync(SerialNumberModel model)
        {
            if (model.FirstCustomer == null && model.SecondCustomer == null)
                throw new Exception("At least one customer must be provided to update a serial number.");

            var dbmodel = _context.SerialNumbers.Where(s => s.SerialNumber == model.SerialNumber).FirstOrDefault();
            dbmodel = updateModel(model, dbmodel);
            _context.Update(dbmodel);

            _context.SaveChanges();
            return Task.CompletedTask;
        }

        private SerialNumbers updateModel(SerialNumberModel model, SerialNumbers? dbmodel)
        {
            if (dbmodel == null)
                throw new Exception("Serial number not found.");

            if (dbmodel.Customer1 == null)
                dbmodel.Customer1 = _context.Customers.FirstOrDefault(c => c.Email == model.FirstCustomer.Email).Id;

            else if (dbmodel.Customer2 == null)
                dbmodel.Customer2 = _context.Customers.FirstOrDefault(c => c.Email == model.SecondCustomer.Email).Id;

            else
                throw new Exception("Serial number already has two customers.");

            return dbmodel;
        }

        public Task<IEnumerable<SerialNumberModel>> ReadAsync(string serialNumber)
        {
            var result = _context.SerialNumbers
                .Include(x => x.Customer1Navigation)
                .Include(x => x.Customer2Navigation)
                .Where(m => m.SerialNumber == serialNumber).FirstOrDefault();

            if (result is null)
            {
                return Task.FromResult(Enumerable.Empty<SerialNumberModel>());
            }
            else
            {
                return Task.FromResult(new List<SerialNumberModel> { result.ToModel() }.AsEnumerable());
            }
        }

        public async Task Add100SerialNumbersAsync()
        {
            var serialNumber = new List<SerialNumbers>();
            while (serialNumber.Count < 100)
            {
                serialNumber.Add(new SerialNumbers() { SerialNumber = Guid.NewGuid().ToString(), Active = true });
            }
            await _context.SerialNumbers.AddRangeAsync(serialNumber);
            await _context.SaveChangesAsync();
        }
    }
}