namespace CSharpToDo.Client;

public class OptionalParametersTest
{
	public string OptionalParameters(string par1, string par2, string par3 = "test")
	{
		Console.WriteLine(par1);
		Console.WriteLine(par2);
		Console.WriteLine(par3);
		return par1 + par2 + par3;
	}
}
