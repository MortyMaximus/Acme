using System.Diagnostics.CodeAnalysis;

namespace Acme.Models.BaseModels
{
    public class SerialNumberModel: BaseModel
    {
        /// <summary>
        /// Gets or sets the unique serial number that identifies the device.
        /// </summary>
        [NotNull]
        public string SerialNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the object is currently active.
        /// </summary>
        public bool IsActive { get; set; } = false;

        /// <summary>
        /// Gets or sets the first customer in the collection.
        /// </summary>
        public CustomerModel? FirstCustomer { get; set; }

        /// <summary>
        /// Gets or sets the second customer in the collection.
        /// </summary>
        public CustomerModel? SecondCustomer { get; set; }
    }
}
