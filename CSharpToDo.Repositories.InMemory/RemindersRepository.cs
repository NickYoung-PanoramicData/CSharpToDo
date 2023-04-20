using CSharpToDo.Shared.Models;
using System.Collections.Concurrent;

namespace CSharpToDo.Repositories.InMemory
{
	internal class RemindersRepository //: IToDosRepository
	{
		private readonly IDictionary<int, Reminder> _reminders = new ConcurrentDictionary<int, Reminder>();

		//public Task<IEnumerable<Reminder>> GetListAsync(CancellationToken _) => Task.FromResult(.Values.AsEnumerable<Reminder>());

		public Task<Reminder?> GetAsync(int id, CancellationToken _)
		{
			if (_reminders.TryGetValue(id, out var toDo))
			{
				return Task.FromResult<Reminder?>(toDo);
			}

			return Task.FromResult<Reminder?>(null);
		}

		public Task<Reminder> AddAsync(Reminder item, CancellationToken _)
		{
			var maxId = 0;
			if (_reminders.Keys.Count > 0)
			{
				maxId = _reminders.Keys.Max();

			}

			item.Id = ++maxId;
			_reminders.Add(maxId, item);

			return Task.FromResult(item);
		}

		public Task<Reminder?> UpdateAsync(int id, Reminder item, CancellationToken _)
		{
			if (_reminders.ContainsKey(id))
			{
				_reminders.Remove(id);
				item.Id = id;
				item.LastModifiedUtc = DateTime.UtcNow;
				_reminders[id] = item;
				return Task.FromResult<Reminder?>(item);
			}

			return Task.FromResult<Reminder?>(null);
		}

		public Task<bool> DeleteAsync(int id, CancellationToken _)
		{
			if (_reminders.ContainsKey(id))
			{
				_reminders.Remove(id);
				return Task.FromResult(true);
			}

			return Task.FromResult(false);
		}
	}
}
