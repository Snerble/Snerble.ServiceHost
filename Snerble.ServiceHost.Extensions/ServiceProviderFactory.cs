using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
