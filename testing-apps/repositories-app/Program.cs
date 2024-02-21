namespace Repository_App;

using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using  Microsoft.Extensions.Configuration.Json;
using Repository_App.Entities;

public class Program 
{
    static async Task Main()
    {
        string genLogin = Guid.NewGuid().ToString();

		User user = new User
		{
			Login = genLogin.Substring(0, genLogin.Length - 18),
			Password = "1234",
			Email = "testmail.com"
		};
        
        UserRepository rep = new UserRepository(new ApplicationContext(@"C:\Users\Honor\Desktop\MyProjects\CollegeTestsProject\CollegeTestsProject-Database\testing-apps\repositories-app"));

        await rep.AddUserAsync(user);

        await rep.SaveAsync();

        System.Console.WriteLine(await rep.IsHasUser(user.Login));
    }
}