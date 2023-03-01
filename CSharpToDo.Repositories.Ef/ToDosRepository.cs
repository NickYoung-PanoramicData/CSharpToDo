using CSharpToDo.Data.Interfaces;
using CSharpToDo.Repositories.Ef.Models;
using CSharpToDo.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpToDo.Repositories.Ef
{
	internal class ToDosRepository : IToDosRepository
	{
		private readonly ToDoDbContext _dbContext;

		public ToDosRepository(ToDoDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<ToDo>> GetListAsync(CancellationToken cancellationToken)
		{
			return await _dbContext
				.ToDos
				.Select(t => t.AsToDo())
				.ToListAsync(cancellationToken)
				.ConfigureAwait(false);
		}

		public async Task<ToDo?> GetAsync(int id, CancellationToken cancellationToken)
		{
			return (await _dbContext
				.ToDos
				.FirstOrDefaultAsync(t => t.Id == id, cancellationToken)
				.ConfigureAwait(false))
				?.AsToDo();
		}

		public async Task<ToDo> AddAsync(ToDo item, CancellationToken cancellationToken)
		{
			var newModel = ToDoModel.AsToDoModel(item);
			_dbContext
			.ToDos.Add(newModel);

			await _dbContext
				.SaveChangesAsync(cancellationToken)
				.ConfigureAwait(false);
			return newModel.AsToDo();
		}

		public async Task<ToDo?> UpdateAsync(int id, ToDo item, CancellationToken cancellationToken)
		{
			var existingItem = _dbContext.ToDos.FirstOrDefault(t => t.Id == id);
			if (existingItem is null)
			{
				return null;
			}

			existingItem.Name = item.Name;
			existingItem.IsCompleted = item.IsCompleted;

			await _dbContext
				.SaveChangesAsync(cancellationToken)
				.ConfigureAwait(false);
			return existingItem.AsToDo();
		}

		public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
		{
			 var  rowsAffected = await _dbContext
				.ToDos
				.Where(t => t.Id == id)
				.ExecuteDeleteAsync(cancellationToken)
				.ConfigureAwait(false);

			return rowsAffected == 1;
		}
	}
}
