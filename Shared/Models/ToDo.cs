using CSharpToDo.Shared.Interfaces;

namespace CSharpToDo.Shared.Models
{
	public class ToDo : INamedIdentifiedEntity
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public bool IsCompleted { get; set; }

		public DateTime CreatedUtc { get; set; } = DateTime.Now;

		public DateTime? LastModifiedUtc { get; set; }
	}
}
