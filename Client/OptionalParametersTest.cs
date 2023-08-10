﻿namespace CSharpToDo.Client;

public class OptionalParametersTest
{

	private string OptionalParametersTestMethod(int par1)
	{
		Console.WriteLine(par1);
		return par1.ToString();
	}

	private string OptionalParametersTestMethod(string par1, string par2, string par3 = "testString")
	{
		Console.WriteLine(par1);
		Console.WriteLine(par2);
		Console.WriteLine(par3);
		return par1 + par2 + par3;
	}
}
