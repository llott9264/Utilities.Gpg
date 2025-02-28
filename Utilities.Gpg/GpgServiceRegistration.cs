using Microsoft.Extensions.DependencyInjection;

namespace Utilities.Gpg;

public static class GpgServiceRegistration
{
	public static IServiceCollection AddGpgServices(this IServiceCollection services)
	{
		services.AddSingleton<IGpg, Gpg>();
		return services;
	}
}