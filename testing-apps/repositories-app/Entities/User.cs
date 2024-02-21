namespace Repository_App.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class User : IDisposable
{
    public Guid? Id { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public ICollection<Test>? Tests { get; }

    public void Dispose()
    {
        
    }
}