using CSharpToDo.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
