using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Snerble.ServiceHost.Extensions
{
	public static class ServiceProviderExtensions
	{
		/// <summary>
		/// Injects services into the properties of the specified object that
		/// have the <see cref="InjectAttribute"/>.
		/// </summary>
		/// <param name="sp">The service provider to get services from.</param>
		/// <param name="obj">The object to inject services into.</param>
		/// <exception cref="InvalidOperationException">Thrown when a required
		/// service could not be provided.</exception>
		public static void InjectServices(this IServiceProvider sp, object obj)
		{
			if (obj is null)
				return;

			// Get the properties and their InjectAttribute
			var properties = from prop in obj.GetType().GetProperties(
								BindingFlags.Public
								| BindingFlags.NonPublic
								| BindingFlags.Instance)
							 let attr = prop.GetCustomAttribute<InjectAttribute>()
							 where attr is not null
							 select (prop, attr);

			// Set the service for each property
			foreach (var (prop, attr) in properties)
			{
				// Set the service
				prop.SetValue(obj, attr.Required switch
				{
					true => sp.GetRequiredService(prop.PropertyType),
					false => sp.GetService(prop.PropertyType)
				});
			}
		}
	}
}
