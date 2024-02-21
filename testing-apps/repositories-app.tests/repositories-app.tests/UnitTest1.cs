namespace repositories_app.tests
{
	using Repository_App;
	using Repository_App.Entities;
	using Repository_App.Exceptions;
	using System.IO;

	[TestFixture]
	public class RepositoryTests
	{
		private readonly UserRepository userRepository = 
							new UserRepository(new ApplicationContext(@"C:\Users\Honor\Desktop\MyProjects\CollegeTestsProject\CollegeTestsProject-Database\testing-apps\repositories-app.tests\repositories-app.tests\"));

		[Test]
		[Ignore("")]
		public async Task AddAsync_StandartAddition_Added()
		{
			string genLogin = Guid.NewGuid().ToString();

			User user = new User
			{
				Login = genLogin.Substring(0, genLogin.Length - 18),
				Password = "1234",
				Email = "testmail.com"
			};

			await userRepository.AddAsync(user);
			await userRepository.SaveAsync();

			Assert.That(
				await userRepository.IsHas(user.Login),
				Is.Not.Null,
				"Добавление поьльзователя не произошло");
		}

		[Test]
		public async Task AddAsync_AdditionExistUser_NotAdded()
		{
			string? exceptionMessage = null;

			User user = new User
			{
				Login = "test_user1",
				Password = "1234",
			};
			try
			{
				await userRepository.AddAsync(user);
				await userRepository.SaveAsync();
			}
			catch (UserRepositoryException ex)
			{
				exceptionMessage = ex.Message;
			}

			Assert.That(
				exceptionMessage,
				Is.Null,
				"При добавлении существующего пользователя, метод репозитория это не обработал");
		}
		
	}
}