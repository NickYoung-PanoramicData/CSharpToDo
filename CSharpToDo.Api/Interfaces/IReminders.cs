using CSharpToDo.Shared.Models;
using Refit;

namespace CSharpToDo.Api.Interfaces;
public interface IReminders
{
	[Get("/api/reminders")]
	Task<List<Reminder>> GetAllAsync(CancellationToken cancellationToken);

	[Get("/api/reminders/{id}")]
	Task<Reminder> GetAsync(int id, CancellationToken cancellationToken);

	[Post("/api/reminders")]
	Task CreateAsync([Body] Reminder value, CancellationToken cancellationToken);

	[Put("/api/reminders/{id}")]
	Task UpdateAsync(int id, [Body] Reminder value, CancellationToken cancellationToken);

	[Delete("/api/reminders/{id}")]
	Task DeleteAsync(int id, CancellationToken cancellationToken);
}
