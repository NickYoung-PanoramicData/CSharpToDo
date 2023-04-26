using CSharpToDo.Data.Interfaces;
using CSharpToDo.Repositories.Ef.Models;
using CSharpToDo.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharpToDo.Repositories.Ef
{
	internal class ToDosRepository : IToDosRepository
	{
		private readonly AppDbContext _dbContext;

		public ToDosRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<ToDo>> GetListAsync(CancellationToken cancellationToken) => await _dbContext
				.ToDos
				.Select(t => t.AsToDo())
				.ToListAsync(cancellationToken)
				.ConfigureAwait(false);

		public async Task<ToDo?> GetAsync(int id, CancellationToken cancellationToken) => (await _dbContext
				.ToDos
				.FirstOrDefaultAsync(t => t.Id == id, cancellationToken)
				.ConfigureAwait(false))
				?.AsToDo();

		public async Task<ToDo> AddAsync(ToDo item, CancellationToken cancellationToken)
		{
			var newModel = ToDoModel.AsToDoModel(item);
			_dbContext
			.ToDos.Add(newModel);

			await _dbContext//Asynchronous code to push the changes made to dbContext to the actual database
				.SaveChangesAsync(cancellationToken)
				.ConfigureAwait(false);
			return newModel.AsToDo();
		}

		public async Task<ToDo?> UpdateAsync(int id, ToDo item, CancellationToken cancellationToken)
		{
			var existingItem = _dbContext.ToDos.FirstOrDefault(t => t.Id == id);
			if (existingItem is null)//If existingItem didn't find any toDos with the specified Id return null
			{
				return null;
			}

			existingItem.Name = item.Name;//update the requested items name with the one passed in the parameter
			existingItem.IsCompleted = item.IsCompleted;//update completed with parameter

			await _dbContext//Push changes to database
				.SaveChangesAsync(cancellationToken)
				.ConfigureAwait(false);
			return existingItem.AsToDo();
		}

		public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
		{
			var rowsAffected = await _dbContext//So would this work if defined as an integer?
			   .ToDos
			   .Where(t => t.Id == id)//Use where so we don't make server requests twice?
			   .ExecuteDeleteAsync(cancellationToken)
			   .ConfigureAwait(false);

			return rowsAffected == 1;
		}
	}
}
