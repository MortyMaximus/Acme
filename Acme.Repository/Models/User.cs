namespace Acme.Repository.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}

public static class UserExtentions
{
    public static User ToDbModel(this Acme.Models.Database.UserModel user)
    {
        return new User
        {
            Id = user.Id,
            Email = user.Email,
            Password = user.Password
        };
    }

    public static Acme.Models.Database.UserModel ToModel(this User user)
    {
        return new Acme.Models.Database.UserModel
        {
            Id = user.Id,
            Email = user.Email,
            Password = user.Password
        };
    }
}
