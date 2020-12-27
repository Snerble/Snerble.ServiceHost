using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Snerble.ServiceHost.Extensions;

namespace Snerble.ServiceHost
{
	/// <summary>
	/// Provides functions for configuring and running a program using dependency
	/// injection.
	/// </summary>
	public static class ServiceHostStartup
	{
		/// <summary>
		/// Builds and starts an <see cref="IHost"/> using the specified startup type
		/// <typeparamref name="T"/>.
		/// </summary>
		/// <remarks>
		/// Consider registering an <see cref="IHostedService"/> in the startup type
		/// <typeparamref name="T"/> if you wish to run your program indefinitely.
		/// </remarks>
		/// <typeparam name="T">
		/// The type to use for starting the program.
		/// <para/>
		/// An instance of <typeparamref name="T"/> is obtained through dependency
		/// injection and thus may inject default services, including <see cref="IConfiguration"/>.
		/// </typeparam>
		public static void Start<T>() where T : class, IStartup
			=> Build<T>().Run();

		/// <summary>
		/// Builds an <see cref="IHost"/> instance using the specified startup type
		/// <typeparamref name="T"/>.
		/// </summary>
		/// <remarks>
		/// Use this method if services or other resources need to be obtained from
		/// the program without starting it.
		/// </remarks>
		/// <typeparam name="T">
		/// The type to use for starting the program.
		/// <para/>
		/// An instance of <typeparamref name="T"/> is obtained through dependency
		/// injection and thus may inject default services, including <see cref="IConfiguration"/>.
		/// </typeparam>
		/// <returns>The <see cref="IHost"/> instance representing the program.</returns>
		public static IHost Build<T>() where T : class, IStartup
		{
			IStartup startup = InitializeStartup<T>(Host.CreateDefaultBuilder());
			return InitializeHost(Host.CreateDefaultBuilder(), startup);
		}

		/// <summary>
		/// Obtains an instance of <typeparamref name="T"/> by registering it as a service
		/// and obtaining it after building the program.
		/// </summary>
		/// <typeparam name="T">A type inheriting from <see cref="IStartup"/>.</typeparam>
		/// <param name="hostBuilder">A temporary instance of <see cref="IHostBuilder"/>.
		/// <para/>
		/// This instance should not be reused after this method.</param>
		/// <returns>An instance of <typeparamref name="T"/>.</returns>
		private static IStartup InitializeStartup<T>(IHostBuilder hostBuilder) where T : class, IStartup
		{
			hostBuilder.ConfigureServices(services => services.AddTransient<T>());
			return hostBuilder.Build().Services.GetRequiredService<T>();
		}

		/// <summary>
		/// Initializes the program by populating the <paramref name="hostBuilder"/>'s
		/// services using the <paramref name="startup"/> instance.
		/// </summary>
		/// <param name="hostBuilder">The <see cref="IHostBuilder"/> that will build
		/// the program.</param>
		/// <param name="startup">An instance of <see cref="IStartup"/> that will
		/// provide services for the program.</param>
		/// <returns>The <see cref="IHost"/> instance representing the program.</returns>
		private static IHost InitializeHost(IHostBuilder hostBuilder, IStartup startup)
		{
			// Add services from the startup's assembly using the extension methods
			hostBuilder.ConfigureServices(services =>
			{
				var assembly = startup.GetType().Assembly;
				services.AddSingletonServices(assembly);
				services.AddScopedServices(assembly);
				services.AddTransientServices(assembly);
			});
			// Add services using the startup object
			hostBuilder.ConfigureServices(startup.ConfigureServices);
			return hostBuilder.Build();
		}
	}
}
