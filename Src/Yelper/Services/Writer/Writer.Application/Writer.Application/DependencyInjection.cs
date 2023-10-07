using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Writer.Application.Common.Persistence;

namespace Writer.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(
		this IServiceCollection services, IConfiguration configuration)
	{
		ConfigureMediator(services);
		ConfigurePersistence(services, configuration);

		return services;
	}

	private static void ConfigureMediator(IServiceCollection services)
	{
		services.AddMediatR(cfg =>
		{
			cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
		});
	}

	private static void ConfigurePersistence(IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<WriterDbContext>(
			options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("db-docker"));
				options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
			});
	}
}
