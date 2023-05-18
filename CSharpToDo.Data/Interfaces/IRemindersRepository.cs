using CSharpToDo.Shared.Models;


namespace CSharpToDo.Data.Interfaces
{
	public interface IRemindersRepository
	{
		Task<IEnumerable<Reminder>> GetListAsync(CancellationToken cancellationToken);

		Task<Reminder?> GetAsync(int id, CancellationToken cancellationToken);

		Task<Reminder> AddAsync(Reminder item, CancellationToken cancellationToken);

		Task<Reminder?> UpdateAsync(int id, Reminder item, CancellationToken cancellationToken);

		Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
	}
}
