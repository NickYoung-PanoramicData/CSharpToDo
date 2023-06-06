using CSharpToDo.Data.Implementations;
using CSharpToDo.Data.Interfaces;
using CSharpToDo.Repositories.InMemory;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddInMemoryRepository(this IServiceCollection services)
		{
			services.AddSingleton<IToDosRepository, ToDosRepository>();
			services.AddSingleton<IRemindersRepository, RemindersRepository>();
			services.AddTransient<IDatabaseInitializer, NullDatabaseInitializer>();
			return services;
		}
	}
}
