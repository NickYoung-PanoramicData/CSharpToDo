using CSharpToDo.Data.Interfaces;
using CSharpToDo.Repositories.Ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
		public static IServiceCollection AddEfRepository(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ToDoDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("ToDos")));
			services.AddScoped<IToDosRepository, ToDosRepository>();
			return services;
		}
	}
}
