namespace CSharpToDo.Repositories.Ef.Interfaces
{
	public interface IAuditable
	{
		DateTime CreatedUtc { get; set; }

		DateTime? LastModifiedUtc { get; set; }
	}
}