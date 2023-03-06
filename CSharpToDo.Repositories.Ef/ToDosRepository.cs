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

		public async Task<IEnumerable<ToDo>> GetListAsync(CancellationToken cancellationToken) => await _dbContext//Database variable
				.ToDos//Records within database
				.Select(t => t.AsToDo())//Projects all elements of the sequence
				.ToListAsync(cancellationToken)//Built in method that casts to a list
				.ConfigureAwait(false);

		public async Task<ToDo?> GetAsync(int id, CancellationToken cancellationToken) => (await _dbContext//Database variable
				.ToDos//Records within database
				.FirstOrDefaultAsync(t => t.Id == id, cancellationToken)//Fetches the first element of the list thats Id is equal the one specified in the parameter of the method
				.ConfigureAwait(false))//Ends the await
				?.AsToDo();//Cast to type ToDo

		public async Task<ToDo> AddAsync(ToDo item, CancellationToken cancellationToken)
		{
			var newModel = ToDoModel.AsToDoModel(item);//Takes ToDo parameter and casts as a ToDoModel (What is the difference? Is it required to be of that type to enter the database?) stored in newModel
			_dbContext//Add newModel to database variable
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
			 var  rowsAffected = await _dbContext//So would this work if defined as an integer?
				.ToDos
				.Where(t => t.Id == id)//Use where so we don't make server requests twice?
				.ExecuteDeleteAsync(cancellationToken)
				.ConfigureAwait(false);

			return rowsAffected == 1;
		}
	}
}
