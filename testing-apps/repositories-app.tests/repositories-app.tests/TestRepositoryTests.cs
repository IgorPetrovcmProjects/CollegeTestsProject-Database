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

		private const string PatternTitle = "test";

		[Test, Order(1)]
		public async Task InitializationTest_InitializationNeedObjects()
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

			UserRepository userRepository2 =
				new UserRepository(
					new ApplicationContext("Host=localhost;Port=5400;Database=student;Username=server_user;Password=server")
					);

			Assert.That(
				userRepository2.IsHas(PatternLogin + "1"),
				Is.Not.Null,
				@"Пользователь ""test-rep-user1"" не был добавлен в бд");
			Assert.That(
				userRepository2.IsHas(PatternLogin + "2"),
				Is.Not.Null,
				@"Пользователь ""test-rep-user2"" не был добавлен в бд");
		}

		[Test, Order(2)]
		public async Task AddAsync_SingleTest_TestAdded()
		{
			TestRepository testRepository =
				new TestRepository(
					new ApplicationContext("Host=localhost;Port=5400;Database=student;Username=server_user;Password=server")
					);

			Test test1 = new Test()
			{
				Title = PatternTitle + "1",
				Description = "random"
			};

			await testRepository.AddAsync(test1, PatternLogin + "1");
			await testRepository.SaveAsync();

			Assert.That(
				testRepository.IsHas(test1, PatternLogin + "1"),
				Is.Not.Null,
				@"Тест ""test1"" для пользователя ""test-rep-user1"" не был добавлен");
		}

	}
}
