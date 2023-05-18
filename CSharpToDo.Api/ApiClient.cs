using CSharpToDo.Api.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Refit;

namespace CSharpToDo.Api;
public class ApiClient : IDisposable
{
	private readonly HttpClient _httpClient;
	private readonly RefitSettings _refitSettings;
	private bool _disposedValue;

	public ApiClient(ApiClientOptions options, ILogger logger)
	{
		options.Validate();

		_httpClient = new HttpClient()
		{
			BaseAddress = new Uri(options.BaseUrl),
			Timeout = TimeSpan.FromSeconds(options.HttpClientTimeoutSeconds)
		};

		_refitSettings = new RefitSettings()
		{
			ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore,
#if DEBUG
				MissingMemberHandling = MissingMemberHandling.Error,
#endif
				Converters = new List<JsonConverter> { new StringEnumConverter() }
			})
		};

		ToDos = RefitFor(ToDos!);
		Reminders = RefitFor(Reminders!);
	}

	public IToDos ToDos { get; set; }
	public IReminders Reminders { get; set; }

	private T RefitFor<T>(T _)
		=> RestService.For<T>(_httpClient, _refitSettings);

	protected virtual void Dispose(bool disposing)
	{
		if (!_disposedValue)
		{
			if (disposing)
			{
				_httpClient.Dispose();
			}

			_disposedValue = true;
		}
	}

	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}

