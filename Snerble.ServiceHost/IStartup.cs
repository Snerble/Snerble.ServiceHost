using Microsoft.Extensions.DependencyInjection;

namespace Snerble.ServiceHost
{
	/// <summary>
	/// Specifies the startup class of an application.
	/// </summary>
	public interface IStartup
	{
		/// <summary>
		/// Adds services to the specified <see cref="IServiceCollection"/>.
		/// </summary>
		/// <param name="services">The service container to add services to.</param>
		public void ConfigureServices(IServiceCollection services);
	}
}
