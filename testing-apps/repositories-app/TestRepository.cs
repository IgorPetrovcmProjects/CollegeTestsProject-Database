namespace Repository_App;

using Microsoft.EntityFrameworkCore;
using Repository_App.Entities;
using Repository_App.Exceptions;

public class TestRepository 
{
    private readonly ApplicationContext context;

    public TestRepository(ApplicationContext context)
    {
        this.context = context;
    }

    public async Task<Test> IsHas(string title, Guid userId)
    {
        List<Test> tests = await context.Tests.AsNoTracking().Where(x => x.UserId == userId).ToListAsync();

        if (tests.Count == 0){
            return null;
        }

        Test? test;

        if ( (test = tests.FirstOrDefault(x => x.Title == title)) != null )
        {
            return test;
        }

        return null;
    }

    public async Task AddAsync(Test test)
    {
        if (await IsHas(test.Title, test.UserId) != null){
            throw new TestRepositoryException("The user of the test not found or a test with such title already exists");
        }

        test.Id = Guid.NewGuid();

        context.Tests.Add(test);
    }

    
}