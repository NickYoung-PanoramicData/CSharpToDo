namespace CSharpToDo.Api;
public class ApiClientOptions
{
	public string BaseUrl { get; set; } = string.Empty;

	public double HttpClientTimeoutSeconds { get; internal set; } = 100;

	public void Validate()
	{
		if (string.IsNullOrWhiteSpace(BaseUrl))
		{
			throw new ArgumentException("BaseUrl is required");
		}
	}
}
