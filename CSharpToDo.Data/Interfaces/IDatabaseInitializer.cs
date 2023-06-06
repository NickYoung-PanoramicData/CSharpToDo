using Microsoft.AspNetCore.Builder;

namespace CSharpToDo.Data.Interfaces;
public interface IDatabaseInitializer
{
	Task EnsureDatabaseOkAsync(IApplicationBuilder app);
}
