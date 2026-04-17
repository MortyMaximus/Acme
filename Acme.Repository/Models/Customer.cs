using Acme.Models.BaseModels;

namespace Acme.Repository.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual ICollection<SerialNumbers> SerialNumberCustomer1Navigations { get; set; } = new List<SerialNumbers>();

    public virtual ICollection<SerialNumbers> SerialNumberCustomer2Navigations { get; set; } = new List<SerialNumbers>();
}

public static class CustomerExtentions
{
    public static Customer ToDbModel(this CustomerModel customer) => new()
    {
        Id = customer.Id,
        Email = customer.Email,
        FirstName = customer.FirstName,
        LastName = customer.LastName
    };

    public static CustomerModel ToModel(this Customer customer) => new()
    {
        Id = customer.Id,
        Email = customer.Email,
        FirstName = customer.FirstName,
        LastName = customer.LastName,
    };
}