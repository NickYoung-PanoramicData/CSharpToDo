using CSharpToDo.Api;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PanoramicData.Blazor.Extensions;

namespace CSharpToDo.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");
			builder.RootComponents.Add<HeadOutlet>("head::after");

			builder.Services.AddPanoramicDataBlazor();
			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
			builder.Services.AddScoped(sp => new ApiClient(new ApiClientOptions { BaseUrl = builder.HostEnvironment.BaseAddress }, sp.GetRequiredService<ILogger<ApiClient>>()));

			await builder.Build().RunAsync();
		}
	}
}