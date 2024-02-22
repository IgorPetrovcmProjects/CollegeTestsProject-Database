namespace Repository_App.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Test 
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public Guid UserId { get; set; }

    public User? User { get; set; }

    public Test()
    {
        
    }

    public Test(string title)
    {
        Title = title;
    }
}