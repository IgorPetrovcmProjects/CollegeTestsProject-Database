﻿namespace Repository_App;

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
        
        UserRepository rep = new UserRepository(new ApplicationContext(@"C:\Users\Honor\Desktop\MyProjects\CollegeTestsProject\CollegeTestsProject-Database\testing-apps\repositories-app"));
    }
}