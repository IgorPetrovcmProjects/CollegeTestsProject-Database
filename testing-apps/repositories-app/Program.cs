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

		User user = new User();
        
    }
}