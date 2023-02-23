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
		Task<IEnumerable<ToDo>> GetListAsync();

		Task<ToDo?> GetAsync(int id);

		Task<ToDo> AddAsync(ToDo item);

		Task<ToDo?> UpdateAsync(int id,ToDo item);

		Task<bool> DeleteAsync(int id);
	}
}
