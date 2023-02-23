using CSharpToDo.Data.Interfaces;
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
		private readonly IToDosRepository _repository;

		public ToDosController(IToDosRepository repository)
		{
			_repository = repository;
		}

		// GET: api/<ToDosController>
		[HttpGet]
		public Task<IEnumerable<ToDo>> GetAsync()
		{
			return _repository.GetListAsync();
		}

		// GET api/<ToDosController>/5
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ToDo>> GetAsync(int id)
		{
			var toDo = await _repository.GetAsync(id);
			if (toDo is not null)
			{
				return toDo;
			}

			return NotFound();
		}

		// POST api/<ToDosController>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task PostAsync([FromBody] ToDo value)
		{
			await _repository.AddAsync(value);
		}

		// PUT api/<ToDosController>/5
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> PutAsync(int id, [FromBody] ToDo value)
		{
			var toDo = await _repository.UpdateAsync(id, value);
			if (toDo is not null)
			{
				return Ok();
			}

			return NotFound();
		}

		// DELETE api/<ToDosController>/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> DeleteAsync(int id)
		{
			if (await _repository.DeleteAsync(id))
			{
				return Ok();
			}

			return NotFound();
		}
	}
}
