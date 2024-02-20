namespace Repository_App.Entities;

using System.ComponentModel.DataAnnotations;

public class User
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [StringLength(18)]
    public string Login { get; set; }

    [Required]
    [StringLength(18)]
    public string Password { get; set; }

    [Required]
    public string Email { get; set; }
}