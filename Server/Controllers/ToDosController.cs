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
		public Task<IEnumerable<ToDo>> GetAsync(CancellationToken cancellationToken)
		{
			return _repository.GetListAsync(cancellationToken);
		}

		// GET api/<ToDosController>/5
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ToDo>> GetAsync(int id, CancellationToken cancellationToken)
		{
			var toDo = await _repository.GetAsync(id, cancellationToken);
			if (toDo is not null)
			{
				return toDo;
			}

			return NotFound();
		}

		// POST api/<ToDosController>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task PostAsync([FromBody] ToDo value, CancellationToken cancellationToken)
		{
			await _repository.AddAsync(value, cancellationToken);
		}

		// PUT api/<ToDosController>/5
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> PutAsync(int id, [FromBody] ToDo value, CancellationToken cancellationToken)
		{
			var toDo = await _repository.UpdateAsync(id, value, cancellationToken);
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
		public async Task<ActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
		{
			if (await _repository.DeleteAsync(id, cancellationToken))
			{
				return Ok();
			}

			return NotFound();
		}
	}
}
