using CSharpToDo.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Concurrent;

namespace CSharpToDo.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ToDosController : ControllerBase
	{
		private readonly static IDictionary<int, ToDo> _toDos = new ConcurrentDictionary<int, ToDo>();


		// GET: api/<ToDosController>
		[HttpGet]
		public IEnumerable<ToDo> Get()
		{
			return _toDos.Values;
		}

		// GET api/<ToDosController>/5
		[HttpGet("{id}")]
		public ToDo? Get(int id)
		{
			if (_toDos.TryGetValue(id, out var toDo))
			{
				return toDo;
			}

			return null;
		}

		// POST api/<ToDosController>
		[HttpPost]
		public void Post([FromBody] ToDo value)
		{
			var maxId = 0;
			if(_toDos.Keys.Count > 0)
			{
				maxId = _toDos.Keys.Max();
			}

			value.Id = ++maxId;
			_toDos.Add(maxId,value);
		}

		// PUT api/<ToDosController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] ToDo value)
		{
			if (_toDos.Keys.Contains(id))
			{
				_toDos.Remove(id);
				value.Id = id;
				_toDos[id] = value;
			}	
		}

		// DELETE api/<ToDosController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			if (_toDos.Keys.Contains(id))
			{
				_toDos.Remove(id);
			}	
		}
	}
}
