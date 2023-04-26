using CSharpToDo.Data.Interfaces;
using CSharpToDo.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSharpToDo.Server.Controllers
{
	[Route("api/[controller]")]
	//[ApiController]
	public class RemindersController : ControllerBase
	{
		private readonly IRemindersRepository _repository;

		public RemindersController(IRemindersRepository repository)
		{
			_repository = repository;
		}

		// GET: api/<RemindersController>
		[HttpGet]
		public Task<IEnumerable<Reminder>> GetAsync(CancellationToken cancellationToken) => _repository.GetListAsync(cancellationToken);

		// GET api/<RemindersController>/5
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Reminder>> GetAsync(int id, CancellationToken cancellationToken)
		{
			var reminder = await _repository.GetAsync(id, cancellationToken);
			if (reminder is not null)
			{
				return reminder;
			}

			return NotFound();
		}

		// POST api/<RemindersController>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task PostAsync([FromBody] Reminder value, CancellationToken cancellationToken) => await _repository.AddAsync(value, cancellationToken);

		// PUT api/<RemindersController>/5
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> PutAsync(int id, [FromBody] Reminder value, CancellationToken cancellationToken)
		{
			var reminder = await _repository.UpdateAsync(id, value, cancellationToken);
			if (reminder is not null)
			{
				return Ok();
			}

			return NotFound();
		}

		// DELETE api/<RemindersController>/5
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