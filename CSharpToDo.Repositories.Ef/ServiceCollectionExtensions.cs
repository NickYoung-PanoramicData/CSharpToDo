using CSharpToDo.Data.Interfaces;
using CSharpToDo.Repositories.Ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddEfRepository(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("ToDos")));
			services.AddScoped<IToDosRepository, ToDosRepository>();
			services.AddScoped<IRemindersRepository, RemindersRepository>();
			services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();

			return services;
		}
	}
}
