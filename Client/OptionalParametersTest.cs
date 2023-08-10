namespace CSharpToDo.Client;

public class OptionalParametersTest
{

	private int OptionalParametersTestMethod(int par1)
	{
		Console.WriteLine(par1);
		return par1;
	}

	private int OptionalParametersTestMethod(string par1, string par2, string par3 = "testString")
	{
		Console.WriteLine(par1);
		Console.WriteLine(par2);
		Console.WriteLine(par3);
		return 5;
	}
}
