using CSharpToDo.Shared.Interfaces;

namespace CSharpToDo.Shared.Models
{
	public class Reminder : INamedIdentifiedEntity
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public DateTime CreatedUtc { get; set; } = DateTime.Now;

		public DateTime DueUtc { get; set; } = DateTime.Now.AddDays(1);

		public DateTime? LastModifiedUtc { get; set; }

		public string DueUtcString => DueUtc.ToString("f");

		public string Description { get; set; } = string.Empty;
	}
}
