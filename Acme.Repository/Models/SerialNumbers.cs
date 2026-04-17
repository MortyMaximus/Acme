namespace Acme.Repository.Models;

public partial class SerialNumbers
{
    public int Id { get; set; }

    public string SerialNumber { get; set; } = null!;

    public int? Customer1 { get; set; }

    public int? Customer2 { get; set; }

    public bool Active { get; set; }

    public virtual Customer? Customer1Navigation { get; set; }

    public virtual Customer? Customer2Navigation { get; set; }
}

public static class SerialNumberExtentions
{
    public static SerialNumbers ToDbModel(this Acme.Models.BaseModels.SerialNumberModel serialNumber)
    {
        return new SerialNumbers
        {
            Id = serialNumber.Id,
            SerialNumber = serialNumber.SerialNumber,
            Active = !serialNumber.IsActive,
            Customer1 = serialNumber.FirstCustomer?.Id,
            Customer2 = serialNumber.SecondCustomer?.Id
        };
    }

    public static Acme.Models.BaseModels.SerialNumberModel ToModel(this SerialNumbers serialNumber)
    {
        return new Acme.Models.BaseModels.SerialNumberModel
        {
            Id = serialNumber.Id,
            SerialNumber = serialNumber.SerialNumber,
            IsActive = !serialNumber.Active,
            FirstCustomer = serialNumber.Customer1Navigation?.ToModel(),
            SecondCustomer = serialNumber.Customer2Navigation?.ToModel(),
        };
    }
} 
