using CSharpToDo.Data.Interfaces;
using CSharpToDo.Shared.Models;
using System.Collections.Concurrent;

namespace CSharpToDo.Tests.Fakes
{
	internal class FakeToDosRepository : IToDosRepository
	{
		//This Dictionary is only accessed by things making use of the Fake Repository, allowing testing to be self contained
		private readonly IDictionary<int, ToDo> _toDosFake = new ConcurrentDictionary<int, ToDo>();
		public Task<IEnumerable<ToDo>> GetListAsync(CancellationToken _) => Task.FromResult(_toDosFake.Values.AsEnumerable<ToDo>());

		public Task<ToDo?> GetAsync(int id, CancellationToken _)
		{
			if (_toDosFake.TryGetValue(id, out var toDo))
			{
				return Task.FromResult<ToDo?>(toDo);
			}

			return Task.FromResult<ToDo?>(null);
		}

		public Task<ToDo> AddAsync(ToDo item, CancellationToken _)
		{
			var maxId = 0;
			if (_toDosFake.Keys.Count > 0)
			{
				maxId = _toDosFake.Keys.Max();

			}

			item.Id = ++maxId;
			_toDosFake.Add(maxId, item);

			return Task.FromResult(item);
		}

		public Task<ToDo?> UpdateAsync(int id, ToDo item, CancellationToken _)
		{
			if (_toDosFake.ContainsKey(id))
			{
				_toDosFake.Remove(id);
				item.Id = id;
				item.LastModifiedUtc = DateTime.UtcNow;
				_toDosFake[id] = item;
				return Task.FromResult<ToDo?>(item);
			}

			return Task.FromResult<ToDo?>(null);
		}

		public Task<bool> DeleteAsync(int id, CancellationToken _)
		{
			if (_toDosFake.ContainsKey(id))
			{
				_toDosFake.Remove(id);
				return Task.FromResult(true);
			}

			return Task.FromResult(false);
		}
	}
}
