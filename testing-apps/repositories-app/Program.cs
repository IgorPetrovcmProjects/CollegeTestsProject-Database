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
        TestRepository testRepository = 
            new TestRepository(
                new ApplicationContext("Host=localhost;Port=5400;Database=student;Username=server_user;Password=server")
            );

        Test test1 = new Test()
        {
	        Title = "test1",
	        Description = "random"
        };

        await testRepository.AddAsync(test1, "test-rep-user1");
        await testRepository.SaveAsync();
    }
}