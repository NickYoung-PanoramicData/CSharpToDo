namespace CSharpToDo.Shared.Interfaces;
public interface INamedIdentifiedEntity : IIdentifiedEntity
{
	string Name { get; set; }
}
