using CSharpToDo.Data.Interfaces;
using CSharpToDo.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpToDo.Tests.Fakes
{
	internal class FakeToDosRepository : IToDosRepository
	{
		public Task<IEnumerable<ToDo>> GetListAsync(CancellationToken cancellationToken) => throw new NotImplementedException();

		public Task<ToDo?> GetAsync(int id, CancellationToken cancellationToken) => throw new NotImplementedException();

		public Task<ToDo> AddAsync(ToDo item, CancellationToken cancellationToken) => throw new NotImplementedException();

		public Task<ToDo?> UpdateAsync(int id, ToDo item, CancellationToken cancellationToken) => throw new NotImplementedException();

		public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken) => throw new NotImplementedException();
	}
}
