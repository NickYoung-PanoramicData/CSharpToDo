using CSharpToDo.Shared.Models;


namespace CSharpToDo.Data.Interfaces
{
	public interface IToDosRepository
	{
		Task<IEnumerable<ToDo>> GetListAsync(CancellationToken cancellationToken);

		Task<ToDo?> GetAsync(int id, CancellationToken cancellationToken);

		Task<ToDo> AddAsync(ToDo item, CancellationToken cancellationToken);

		Task<ToDo?> UpdateAsync(int id, ToDo item, CancellationToken cancellationToken);

		Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
	}
}
