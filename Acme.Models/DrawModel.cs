using Acme.Models.BaseModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Acme.Models
{
    public class DrawModel
    {
        /// <summary>
        /// Gets or sets the email address associated with the user.
        /// </summary>
        /// <remarks>The email address must be in a valid email format and cannot be null. Validation is
        /// performed using a regular expression to ensure the value conforms to standard email address
        /// conventions.</remarks>
        [EmailAddress]
        [Required(ErrorMessage = "E-mail is required, and cannot be empty.")]
        [DisplayName("E-Mail")]
        public required string Email { get; set; }

        /// <summary>
        /// Gets or sets the first name of the person.
        /// </summary>
        [Required(ErrorMessage = "First name is required, and cannot be empty.")]
        [DisplayName("First Name")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the person.
        /// </summary>
        [Required(ErrorMessage = "Last name is required, and cannot be empty.")]
        [DisplayName("Last Name")]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unique serial number associated with the entity.
        /// </summary>
        /// <remarks>The serial number must be a non-null string in the format of a 32-character
        /// hexadecimal GUID (8-4-4-4-12). This property is required and cannot be set to null.</remarks>
        [RegularExpression(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$", ErrorMessage = "Invalid serial number.")]
        [Required(ErrorMessage = "Serial number is required, and cannot be empty.")]
        [DisplayName("Serial-number")]
        public required string SerialNumber { get; set; }
    }

    /// <summary>
    /// Provides extension methods for converting a DrawModel instance to a CustomerModel instance.
    /// </summary>
    /// <remarks>This static class contains extension methods that simplify the transformation of draw-related
    /// models into customer-related models.</remarks>
    public static class DrawModelExtentions
    {
        public static CustomerModel ToCustomerModel(this DrawModel model) => new()
        {
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
        };
    }
}