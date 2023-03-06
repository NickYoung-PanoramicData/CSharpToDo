using Newtonsoft.Json;
using Refit;
using System.Text;

namespace CSharpToDo.Client.Extensions;

internal static class ApiExceptionExtensions
{
	internal static string ToValidationFailureMessage(this ApiException ex)
	{
		if (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
		{
			if (ex.Content is not null)
			{
				var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(ex.Content);
				if (problemDetails is not null)
				{
					var builder = new StringBuilder();
					foreach (var problem in problemDetails.Errors)
					{
						foreach (var problemMessage in problem.Value)
						{
							_ = builder.AppendLine(problemMessage);
						}
					}

					var message = builder.ToString();
					return message.Length > 0 ? message : ex.Message;
				}
			}
		}

		return ex.Message;
	}
}
