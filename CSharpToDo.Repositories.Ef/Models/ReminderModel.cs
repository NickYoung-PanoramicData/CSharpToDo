using CSharpToDo.Repositories.Ef.Interfaces;
using CSharpToDo.Shared.Models;

namespace CSharpToDo.Repositories.Ef.Models;
public class ReminderModel : IAuditable
{
	public int Id { get; set; }

	public string Name { get; set; } = string.Empty;

	public DateTime DueUtc { get; set; }

	public DateTime CreatedUtc { get; set; } = DateTime.Now;

	public DateTime? LastModifiedUtc { get; set; }

	internal Reminder AsReminder() => new Reminder
	{
		Id = Id,
		Name = Name,
		DueUtc = DueUtc,
		CreatedUtc = CreatedUtc,
		LastModifiedUtc = LastModifiedUtc
	};

	internal static ReminderModel AsReminderModel(Reminder reminder) => new ReminderModel
	{
		Id = reminder.Id,
		Name = reminder.Name,
		DueUtc = reminder.DueUtc,
		CreatedUtc = reminder.CreatedUtc,
		LastModifiedUtc = reminder.LastModifiedUtc
	};
}
