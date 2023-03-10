using CSharpToDo.Data.Interfaces;
using CSharpToDo.Repositories.InMemory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddInMemoryRepository(this IServiceCollection services)
		{
			services.AddSingleton<IToDosRepository, ToDosRepository>();
			return services;
		}
	}
}
