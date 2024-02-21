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

    public async Task<Test> IsHas(Test test, string login)
    {
        IQueryable<Test> queryableTests = context.Tests.AsNoTracking().Include(x => x.User);

        List<Test> tests = await queryableTests.Where(x => x.User.Login == login).ToListAsync();

        if (tests.Count == 0) {
            return test;
        }

        if (tests.FirstOrDefault(x => x.Title == test.Title) != null ) {
            return null;
        }

        test.User = tests[0].User;
        test.UserId = tests[0].UserId;

        return test;
    }

    public async Task AddAsync(Test test, string userLogin)
    {
        if ((test = await IsHas(test, userLogin)) == null){
            throw new TestRepositoryException("The user of the test not found or a test with such title already exists");
        }

        if (test.User == null)
        {
            
        }

        test.Id = Guid.NewGuid();

        context.Tests.Add(test);
    }

    
}