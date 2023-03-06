using CSharpToDo.Api;
using CSharpToDo.Client.Extensions;
using CSharpToDo.Shared.Models;
using PanoramicData.Blazor.Models;
using Refit;

namespace CSharpToDo.Client.DataProviders;

public class ToDosDataProvider : DataProviderBase<ToDo>
{
	private readonly ApiClient _apiClient;

	public ToDosDataProvider(ApiClient apiClient)
	{
		_apiClient = apiClient;
	}

	public override async Task<DataResponse<ToDo>> GetDataAsync(DataRequest<ToDo> request, CancellationToken cancellationToken)
	{
		var all = await _apiClient.ToDos.GetAllAsync(cancellationToken).ConfigureAwait(true);
		return new DataResponse<ToDo>(all, all.Count);
	}

	public override async Task<OperationResponse> CreateAsync(ToDo item, CancellationToken cancellationToken)
	{
		try
		{
			await _apiClient.ToDos.CreateAsync(item, cancellationToken).ConfigureAwait(true);
			return new OperationResponse() { Success = true };
		}
		catch (ApiException ex)
		{
			var message = ex.ToValidationFailureMessage();
			return new OperationResponse() { ErrorMessage = message };
		}
		catch (Exception ex)
		{
			return new OperationResponse() { ErrorMessage = ex.Message };
		}
	}

	public override async Task<OperationResponse> UpdateAsync(ToDo item, IDictionary<string, object?> delta, CancellationToken cancellationToken)
	{
		try
		{
			ApplyDelta(item, delta);
			await _apiClient.ToDos.UpdateAsync(item.Id, item, cancellationToken).ConfigureAwait(true);
			return new OperationResponse() { Success = true };
		}
		catch (ApiException ex)
		{
			var message = ex.ToValidationFailureMessage();
			return new OperationResponse() { ErrorMessage = message };
		}
		catch (Exception ex)
		{
			return new OperationResponse() { ErrorMessage = ex.Message };
		}
	}

	public override async Task<OperationResponse> DeleteAsync(ToDo item, CancellationToken cancellationToken)
	{
		try
		{
			await _apiClient.ToDos.DeleteAsync(item.Id, cancellationToken).ConfigureAwait(true);
			return new OperationResponse() { Success = true };
		}
		catch (ApiException ex)
		{
			var message = ex.ToValidationFailureMessage();
			return new OperationResponse() { ErrorMessage = message };
		}
		catch (Exception ex)
		{
			return new OperationResponse() { ErrorMessage = ex.Message };
		}
	}
}
