using CSharpToDo.Data.Interfaces;
using Microsoft.AspNetCore.Builder;

namespace CSharpToDo.Data.Implementations;
public class NullDatabaseInitializer : IDatabaseInitializer
{
	public Task EnsureDatabaseOkAsync(IApplicationBuilder app) => Task.CompletedTask;
}
