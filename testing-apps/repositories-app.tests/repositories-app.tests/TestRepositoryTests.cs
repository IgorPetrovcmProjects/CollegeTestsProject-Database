namespace repositories_app.tests
{
	using Repository_App;
	using Repository_App.Entities;
	using Repository_App.Exceptions;

	[TestFixture]
	public class TestRepositoryTests
	{
		private const string PatternLogin = "test-rep-user";

		private const string PasswordUserUsinEveryhere = "1234";

		[Test, Order(1)]
		public async Task AddAsync_InitializationTest_InitializationNeedObjects()
		{
			UserRepository userRepository =
				new UserRepository(
					new ApplicationContext("Host=localhost;Port=5400;Database=student;Username=server_user;Password=server")
					);
			User[] needUsers =
			{
				new User(PatternLogin + "1", PasswordUserUsinEveryhere),
				new User(PatternLogin + "2", PasswordUserUsinEveryhere)
			};

			for (int i = 0; i < needUsers.Length; i++) 
			{
				await userRepository.AddAsync(needUsers[i]);
			}

			await userRepository.SaveAsync();


		}

	}
}
