namespace Repository_App.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class User
{
    public Guid Id { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public ICollection<Test>? Tests { get; }

    public User(string login, string password)
    {
        Login = login;
        Password = password;
    }
}