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
	internal static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Adds services from the specified assembly with the
		/// <see cref="SingletonAttribute"/> to the <paramref name="services"/>.
		/// </summary>
		/// <returns>The original <see cref="IServiceCollection"/>.</returns>
		public static IServiceCollection AddSingletonServices(
			this IServiceCollection services,
			Assembly assembly)
		{
			foreach (var (type, attr) in from type in assembly.GetTypes()
										 let attr = type.GetCustomAttribute<SingletonAttribute>()
										 where attr is not null
										 select (type, attr))
				services.AddSingleton(attr.ServiceType ?? type, type);

			return services;
		}

		/// <summary>
		/// Adds services from the specified assembly with the
		/// <see cref="ScopedAttribute"/> to the <paramref name="services"/>.
		/// </summary>
		/// <returns>The original <see cref="IServiceCollection"/>.</returns>
		public static IServiceCollection AddScopedServices(
			this IServiceCollection services,
			Assembly assembly)
		{
			foreach (var (type, attr) in from type in assembly.GetTypes()
										 let attr = type.GetCustomAttribute<ScopedAttribute>()
										 where attr is not null
										 select (type, attr))
				services.AddScoped(attr.ServiceType ?? type, type);

			return services;
		}

		/// <summary>
		/// Adds services from the specified assembly with the
		/// <see cref="TransientAttribute"/> to the <paramref name="services"/>.
		/// </summary>
		/// <returns>The original <see cref="IServiceCollection"/>.</returns>
		public static IServiceCollection AddTransientServices(
			this IServiceCollection services,
			Assembly assembly)
		{
			foreach (var (type, attr) in from type in assembly.GetTypes()
										 let attr = type.GetCustomAttribute<TransientAttribute>()
										 where attr is not null
										 select (type, attr))
				services.AddTransient(attr.ServiceType ?? type, type);

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
