using CSharpToDo.Data.Interfaces;
using CSharpToDo.Repositories.Ef.Models;
using CSharpToDo.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharpToDo.Repositories.Ef
{
	public sealed class RemindersRepository : IRemindersRepository
	{
		private readonly AppDbContext _dbContext;

		public RemindersRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<Reminder>> GetListAsync(CancellationToken cancellationToken) => await _dbContext
				.Reminders
				.Select(t => t.AsReminder())
				.ToListAsync(cancellationToken)
				.ConfigureAwait(false);

		public async Task<Reminder?> GetAsync(int id, CancellationToken cancellationToken) => (await _dbContext
				.Reminders
				.FirstOrDefaultAsync(t => t.Id == id, cancellationToken)
				.ConfigureAwait(false))
				?.AsReminder();

		public async Task<Reminder> AddAsync(Reminder item, CancellationToken cancellationToken)
		{
			var newModel = ReminderModel.AsReminderModel(item);
			_dbContext
			.Reminders.Add(newModel);

			await _dbContext
				.SaveChangesAsync(cancellationToken)
				.ConfigureAwait(false);
			return newModel.AsReminder();
		}

		public async Task<Reminder?> UpdateAsync(int id, Reminder item, CancellationToken cancellationToken)
		{
			var existingItem = _dbContext.Reminders.FirstOrDefault(t => t.Id == id);
			if (existingItem is null)
			{
				return null;
			}

			existingItem.Name = item.Name;
			existingItem.DueUtc = item.DueUtc;

			await _dbContext
				.SaveChangesAsync(cancellationToken)
				.ConfigureAwait(false);
			return existingItem.AsReminder();
		}

		public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
		{
			var rowsAffected = await _dbContext
			   .Reminders
			   .Where(t => t.Id == id)
			   .ExecuteDeleteAsync(cancellationToken)
			   .ConfigureAwait(false);

			return rowsAffected == 1;
		}
	}
}
