namespace repositories_app.tests
{
	using Repository_App;
	using Repository_App.Entities;
	using System.IO;

	[TestFixture]
	public class RepositoryTests
	{
		private readonly UserRepository userRepository = 
							new UserRepository(new ApplicationContext(@"C:\Users\Honor\Desktop\MyProjects\CollegeTestsProject\CollegeTestsProject-Database\testing-apps\repositories-app.tests\repositories-app.tests\"));

		[Test]
		public async Task AddUserAsync_StandartAddition_Added()
		{
			File.Create(Environment.CurrentDirectory + "text.txt");

			string genLogin = Guid.NewGuid().ToString();

			User user = new User
			{
				Login = genLogin.Substring(0, genLogin.Length - 18),
				Password = "1234",
				Email = "testmail.com"
			};

			await userRepository.AddUserAsync(user);
			await userRepository.SaveAsync();

			Assert.That(
				await userRepository.IsHasUser(user.Login),
				"Добавление поьлзователя не произошло");
		}
	}
}