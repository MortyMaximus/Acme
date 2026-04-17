using Acme.Models.BaseModels;
using System.Diagnostics.CodeAnalysis;

namespace Acme.Models.Database
{
    public class UserModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the email address associated with the entity.
        /// </summary>
        [NotNull]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password used for authentication.
        /// </summary>
        [NotNull]
        public string Password { get; set; } = string.Empty;
    }
}
