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
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<ToDo> Get(int id)
		{
			if (_toDos.TryGetValue(id, out var toDo))
			{
				return toDo;
			}

			return NotFound();
		}

		// POST api/<ToDosController>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
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
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult Put(int id, [FromBody] ToDo value)
		{
			if (_toDos.ContainsKey(id))
			{
				_toDos.Remove(id);
				value.Id = id;
				_toDos[id] = value;
			}

			return NotFound();
		}

		// DELETE api/<ToDosController>/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult Delete(int id)
		{
			if (_toDos.ContainsKey(id))
			{
				_toDos.Remove(id);
			}

			return NotFound();
		}
	}
}
