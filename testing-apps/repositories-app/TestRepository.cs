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
            return null;
        }

        if ((test = tests.FirstOrDefault(x => x.Title == test.Title)) != null ) {
            return test;
        }

        test.User = tests[0].User;

        return test;
    }

    public async Task AddAsync(Test test, string userLogin)
    {
        Test? sampleTest;

        if ((sampleTest = await IsHas(test, userLogin)).Title == test.Title){
            throw new TestRepositoryException("The user of the test not found or a test with such title already exists");
        }

        if (sampleTest == null)
        {
            sampleTest = test;
        }

        if (sampleTest.User == null)
        {
            try 
            {
                User user = await context.Users.FirstOrDefaultAsync(x => x.Login == userLogin); 

                sampleTest.User = user;
            }
            catch  
            {
                throw new TestRepositoryException("The user with this login not found");
            }
        }

        sampleTest.Id = Guid.NewGuid();

        context.Tests.Add(sampleTest);
    }

    public async Task RemoveAsync(string title, string login, string password)
    {
        Test test = new Test(title);
        Test sampleTest;

        if ((sampleTest = await IsHas(test, login)).Title != test.Title){
            throw new TestRepositoryException("The user of the test not found or a test with this title not found");
        }

        context.Tests.Remove(sampleTest);
    }

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }
}