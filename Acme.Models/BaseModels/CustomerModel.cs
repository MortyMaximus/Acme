using System.Diagnostics.CodeAnalysis;

namespace Acme.Models.BaseModels
{
    public class CustomerModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the email address associated with the entity.
        /// </summary>
        [NotNull]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the first name of the person.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the person.
        /// </summary>
        public string LastName { get; set; } = string.Empty;
    }
}