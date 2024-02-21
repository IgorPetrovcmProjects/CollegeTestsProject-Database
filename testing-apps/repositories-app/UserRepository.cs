namespace Repository_App;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repository_App.Entities;

public class UserRepository 
{
    private readonly ApplicationContext context;
    
    public UserRepository(ApplicationContext context)
    {
        this.context = context;
    }

    public async Task AddUserAsync(User user)
    {
        user.Id = Guid.NewGuid();

        await context.Users.AddAsync(user);
    }

    public async Task RemoveUserAsync(string login)
    {
        User? user = await context.Users.FirstOrDefaultAsync(x => x.Login == login);
        
        if (user == null)
            return;

        context.Users.Remove(user);
    }

    public async Task<bool> IsHasUser(string login)
    {
        User user = await context.Users.FirstOrDefaultAsync(x => x.Login == login);

        if (user != null)
            return true;
        else 
            return false;
    }

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }
}