namespace Repository_App;

using Microsoft.EntityFrameworkCore;
using Repository_App.Entities;
using Repository_App.Exceptions;

public class UserRepository 
{
    private readonly ApplicationContext context;
    
    public UserRepository(ApplicationContext context)
    {
        this.context = context;
    }

    public async Task<User> IsHas(string login)
    {
        User? user = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Login == login);

        if (user != null){
            return user;
        }

        else {
            return null;
        }
    }

    public async Task AddAsync(User user)
    {
        if (await IsHas(user.Login) != null){
            throw new UserRepositoryException("This user already exists");
        }

        user.Id = Guid.NewGuid();

        await context.Users.AddAsync(user);
    }

    public async Task RemoveAsync(string login, string password)
    {
        User user;
        if ((user = await IsHas(login)) == null){
            throw new UserRepositoryException("This user does not exists");
        }

        if (user.Password != password){
            throw new UserRepositoryException("Password does not match");
        }

        context.Users.Remove(user);
    }

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }
}