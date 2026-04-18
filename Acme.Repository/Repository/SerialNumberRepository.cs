using Acme.Models;
using Acme.Models.BaseModels;
using Acme.Repository.Extension;
using Acme.Repository.Models;
using Acme.Repository.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Acme.Repository.Repository
{
    public class SerialNumberRepository : ISerialNumberRepository
    {
        private readonly AcmeContext _context;

        public SerialNumberRepository(AcmeContext context)
        {
            _context = context;
        }

        public async Task<Pagination<DrawModel>> GetDrawModel(int pageSize, int pageIndex)
        {
            var query1 = _context.SerialNumbers
                .Where(s => s.Customer1Navigation != null)
                .Select(s => new DrawModel
                {
                    Email = s.Customer1Navigation.Email,
                    FirstName = s.Customer1Navigation.FirstName,
                    LastName = s.Customer1Navigation.LastName,
                    SerialNumber = s.SerialNumber
                });

            var query2 = _context.SerialNumbers
                .Where(s => s.Customer2Navigation != null)
                .Select(s => new DrawModel
                {
                    Email = s.Customer2Navigation.Email,
                    FirstName = s.Customer2Navigation.FirstName,
                    LastName = s.Customer2Navigation.LastName,
                    SerialNumber = s.SerialNumber
                });

            var query = query1.Concat(query2);

            return await query.ToPaginationAsync(pageSize, pageIndex);
        }

        private static DrawModel ToDrawModel(SerialNumbers SerialNumber, Customer customer1Navigation) => new() { Email = customer1Navigation.Email, FirstName = customer1Navigation.FirstName, LastName = customer1Navigation.LastName, SerialNumber = SerialNumber.SerialNumber };

        public async Task<IEnumerable<SerialNumberModel>> ReadAsync(int id)
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

        public Task<IEnumerable<SerialNumberModel>> ReadAsync()
        {
            var result = _context.SerialNumbers.Select(m => m.ToModel());

            return Task.FromResult(result.AsEnumerable());
        }

        public Task UpdateAsync(SerialNumberModel model)
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