namespace Repository_App.Entities;

using System.ComponentModel.DataAnnotations;

public class Test 
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Title { get; set; }

    public string Description { get; set; }

    [Required]
    public User UserId { get; set; }
}