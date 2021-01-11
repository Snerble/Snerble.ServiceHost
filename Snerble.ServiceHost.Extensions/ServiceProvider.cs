using Microsoft.Extensions.DependencyInjection;
using System;

namespace Snerble.ServiceHost.Extensions
{
	internal sealed class ServiceProvider : IServiceProvider
	{
		private readonly IServiceProvider _serviceProvider;

		public ServiceProvider(IServiceCollection serviceCollection)
		{
			_serviceProvider = serviceCollection.BuildServiceProvider();
		}

		public object GetService(Type serviceType)
		{
			object obj = _serviceProvider.GetService(serviceType);
			
			this.InjectServices(obj);

			return obj;
		}
	}
}
