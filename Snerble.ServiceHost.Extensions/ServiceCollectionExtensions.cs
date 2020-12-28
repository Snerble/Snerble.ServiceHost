using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

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

		/// <summary>
		/// Invokes the class constructor on all types in <paramref name="assembly"/> with
		/// the <see cref="InitializeOnStartupAttribute"/>.
		/// </summary>
		/// <param name="assembly">The assembly whose types will be used.</param>
		public static void InitializeTypes(Assembly assembly)
		{
			foreach (var type in assembly.GetTypes()
				.Where(t => Attribute.IsDefined(t, typeof(InitializeOnStartupAttribute))))
				RuntimeHelpers.RunClassConstructor(type.TypeHandle);
		}
	}
}
