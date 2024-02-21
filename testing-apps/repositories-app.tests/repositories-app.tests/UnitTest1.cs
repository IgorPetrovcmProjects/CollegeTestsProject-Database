namespace repositories_app.tests
{
	using Repository_App;
	using Repository_App.Entities;
	using Repository_App.Exceptions;
	using System.IO;

	[TestFixture]
	public class RepositoryTests
	{
		private readonly string LoginUserUsingEveryhere = "test-user1";

		private readonly string PasswordUserUsingEveryhere = "1234";

		private readonly string EmailUserUsingEveryhere = "test-user1@mail.ru";

		[Test, Order(1)]
		public async Task AddAsync_AdditionUserUsedEveryhere_Added()
		{
			UserRepository userRepository =
				new UserRepository(
					new ApplicationContext("Host=localhost;Port=5400;Database=student;Username=server_user;Password=server")
					);

			User user = new User()
			{
				Login = LoginUserUsingEveryhere,
				Password = PasswordUserUsingEveryhere,
				Email = EmailUserUsingEveryhere
			};

			await userRepository.AddAsync(user);
			await userRepository.SaveAsync();

			Assert.That(
				userRepository.IsHas("test-user1"),
				Is.Not.Null,
				"Добавление в базу данных пользователя не произошло");
		}

		[Test, Order(2)]
		[Ignore("")]
		public async Task AddAsync_StandartAddition_Added()
		{
			UserRepository userRepository =
				new UserRepository(
					new ApplicationContext("Host=localhost;Port=5400;Database=student;Username=server_user;Password=server")
					);

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

		[Test, Order(3)]
		public async Task AddAsync_AdditionExistUser_NotAdded()
		{
			UserRepository userRepository =
				new UserRepository(
						new ApplicationContext("Host=localhost;Port=5400;Database=student;Username=server_user;Password=server")
						);

			string? exceptionMessage = null;

			User user = new User
			{
				Login = "test-user1",
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
				Is.Not.Null,
				"При добавлении существующего пользователя, метод репозитория это не обработал");
		}

		[Test, Order(4)]
		public async Task RemoveAsync_RemovingDoesNotExistUser_NotRemoved()
		{
			UserRepository userRepository =
				new UserRepository(
					new ApplicationContext("Host=localhost;Port=5400;Database=student;Username=server_user;Password=server")
					);

			string? exceptionMessage = null;

			string currentUserLogin = "test_user1";
			string currentUserPassword = "1234";

			try
			{
				await userRepository.RemoveAsync(currentUserLogin, currentUserPassword);
				await userRepository.SaveAsync();
			}
			catch (UserRepositoryException ex)
			{
				exceptionMessage = ex.Message;
			}


			User checkedUser = await userRepository.IsHas(currentUserLogin);

			Assert.That(
				checkedUser,
				Is.Null,
				exceptionMessage);
		}

		[Test, Order(5)]
		public async Task RemoveAsync_RemovingExistUser_Removed()
		{
			UserRepository userRepository =
				new UserRepository(
					new ApplicationContext("Host=localhost;Port=5400;Database=student;Username=server_user;Password=server")
					);

			string? exceptionMessage = null;

			string genLogin = Guid.NewGuid().ToString();

			User user = new User
			{
				Login = genLogin.Substring(0, genLogin.Length -18),
				Password = "1234",
				Email = "mailusedeveryhere.com"
			};

			await userRepository.AddAsync(user);
			await userRepository.SaveAsync();

			UserRepository userRepository2 =
				new UserRepository(
					new ApplicationContext("Host=localhost;Port=5400;Database=student;Username=server_user;Password=server")
					);

			try
			{
				await userRepository2.RemoveAsync(user.Login, user.Password);
				await userRepository2.SaveAsync();
			}
			catch (UserRepositoryException ex)
			{
				exceptionMessage = ex.Message;
			}

			User sampleUser = await userRepository2.IsHas(user.Login);

			Assert.That(
				sampleUser,
				Is.Null,
				"Пользователь не был удален");
		}

		[Test]
		public async Task RollBack()
		{
			UserRepository userRepository =
				new UserRepository(
					new ApplicationContext("Host=localhost;Port=5400;Database=student;Username=server_user;Password=server")
					);

			await userRepository.RemoveAsync(LoginUserUsingEveryhere, PasswordUserUsingEveryhere);
			await userRepository.SaveAsync();

			UserRepository userRepository2 =
				new UserRepository(
					new ApplicationContext("Host=localhost;Port=5400;Database=student;Username=server_user;Password=server")
					);

			Assert.That(
				await userRepository2.IsHas(LoginUserUsingEveryhere),
				Is.Null,
				"Удаление пользователя используемого во всех теста не произошло");
		}
	}
}