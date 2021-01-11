using Microsoft.Extensions.DependencyInjection;
using System;

namespace Snerble.ServiceHost.Extensions
{
	internal class ServiceProviderFactory : IServiceProviderFactory<ServiceProvider>
	{
		public ServiceProvider CreateBuilder(IServiceCollection services)
			=> new(services);

		public IServiceProvider CreateServiceProvider(ServiceProvider containerBuilder)
			=> containerBuilder;
	}
}
