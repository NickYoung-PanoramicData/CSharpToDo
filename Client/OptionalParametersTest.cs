namespace CSharpToDo.Client;

public class OptionalParametersTest
{
	//Test comments to change the line numbers of methods
	//Test comments to change the line numbers of methods
	public string OptionalParameters(int par1)
	{
		Console.WriteLine(par1);
		return par1.ToString();
	}
	//Test comments to change the line numbers of methods
	//Test comments to change the line numbers of methods
	//Test comments to change the line numbers of methods
	public string OptionalParameters(string par1, string par2, string par3 = "test")
	{
		Console.WriteLine(par1);
		Console.WriteLine(par2);
		return par1 + par2 + par3;
	}
}
