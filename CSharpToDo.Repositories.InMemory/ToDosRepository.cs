using CSharpToDo.Data.Interfaces;
using CSharpToDo.Shared.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpToDo.Repositories.InMemory
{
	internal class ToDosRepository : IToDosRepository
	{
		private readonly IDictionary<int, ToDo> _toDos = new ConcurrentDictionary<int, ToDo>();

		public Task<IEnumerable<ToDo>> GetListAsync(CancellationToken _) => Task.FromResult(_toDos.Values.AsEnumerable<ToDo>());

		public Task<ToDo?> GetAsync(int id, CancellationToken _)
		{
			if (_toDos.TryGetValue(id, out var toDo))
			{
				return Task.FromResult<ToDo?>(toDo);
			}

			return Task.FromResult<ToDo?>(null);
		}

		public Task<ToDo> AddAsync(ToDo item, CancellationToken _)
		{
			var maxId = 0;
			if (_toDos.Keys.Count > 0)
			{
				maxId = _toDos.Keys.Max();

			}

			item.Id = ++maxId;
			_toDos.Add(maxId, item);

			return Task.FromResult(item);
		}

		public Task<ToDo?> UpdateAsync(int id, ToDo item, CancellationToken _)
		{
			if (_toDos.ContainsKey(id))
			{
				_toDos.Remove(id);
				item.Id = id;
				item.LastModifiedUtc = DateTime.UtcNow;
				_toDos[id] = item;
				return Task.FromResult<ToDo?>(item);
			}

			return Task.FromResult<ToDo?>(null);
		}

		public Task<bool> DeleteAsync(int id, CancellationToken _)
		{
			if (_toDos.ContainsKey(id))
			{
				_toDos.Remove(id);
				return Task.FromResult(true);
			}

			return Task.FromResult(false);
		}
	}
}
