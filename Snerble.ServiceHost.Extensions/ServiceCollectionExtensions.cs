using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Snerble.ServiceHost.Extensions
{
	/// <summary>
	/// Defines extension methods to the <see cref="IServiceCollection"/> interface
	/// used by the <see cref="ServiceHost"/> library.
	/// </summary>
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Adds services from the specified assembly that inherit from <see cref="IAmSingleton"/>
		/// to the specified <see cref="IServiceCollection"/>.
		/// </summary>
		/// <returns>The original <see cref="IServiceCollection"/>.</returns>
		public static IServiceCollection AddSingletonServices(
			this IServiceCollection services,
			Assembly assembly)
		{
			foreach (var type in assembly.GetTypes()
				.Where(t => t.IsAssignableTo(typeof(IAmSingleton))))
				services.AddSingleton(type);

			return services;
		}

		/// <summary>
		/// Adds services from the specified assembly that inherit from <see cref="IAmScoped"/>
		/// to the specified <see cref="IServiceCollection"/>.
		/// </summary>
		/// <returns>The original <see cref="IServiceCollection"/>.</returns>
		public static IServiceCollection AddScopedServices(
			this IServiceCollection services,
			Assembly assembly)
		{
			foreach (var type in assembly.GetTypes()
				.Where(t => t.IsAssignableTo(typeof(IAmScoped))))
				services.AddScoped(type);

			return services;
		}
		
		/// <summary>
		/// Adds services from the specified assembly that inherit from <see cref="IAmTransient"/>
		/// to the specified <see cref="IServiceCollection"/>.
		/// </summary>
		/// <returns>The original <see cref="IServiceCollection"/>.</returns>
		public static IServiceCollection AddTransientServices(
			this IServiceCollection services,
			Assembly assembly)
		{
			foreach (var type in assembly.GetTypes()
				.Where(t => t.IsAssignableTo(typeof(IAmTransient))))
				services.AddTransient(type);

			return services;
		}
	}
}
