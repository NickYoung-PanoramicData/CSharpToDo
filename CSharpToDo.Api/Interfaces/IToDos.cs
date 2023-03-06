using CSharpToDo.Shared.Models;
using Refit;

namespace CSharpToDo.Api.Interfaces;

public interface IToDos
{
	[Get("/api/todos")]
	Task<List<ToDo>> GetAllAsync(CancellationToken cancellationToken);

	[Get("/api/todos/{id}")]
	Task<ToDo> GetAsync(int id, CancellationToken cancellationToken);

	[Post("/api/todos")]
	Task CreateAsync([Body] ToDo value, CancellationToken cancellationToken);

	[Put("/api/todos/{id}")]
	Task UpdateAsync(int id, [Body] ToDo value, CancellationToken cancellationToken);

	[Delete("/api/todos/{id}")]
	Task DeleteAsync(int id, CancellationToken cancellationToken);
}
