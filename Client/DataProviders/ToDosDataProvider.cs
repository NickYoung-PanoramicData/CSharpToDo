using CSharpToDo.Api;
using CSharpToDo.Client.Extensions;
using CSharpToDo.Shared.Models;
using PanoramicData.Blazor.Interfaces;
using PanoramicData.Blazor.Models;
using Refit;

namespace CSharpToDo.Client.DataProviders;

public class ToDosDataProvider : DataProviderBase<ToDo>
{
	private readonly ApiClient _apiClient;
	private readonly IBlockOverlayService _overlayService;

	public ToDosDataProvider(ApiClient apiClient, IBlockOverlayService overlayService)
	{
		_apiClient = apiClient;
		_overlayService = overlayService;
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
			_overlayService.Show();
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
		finally
		{
			_overlayService.Hide();
		}
	}

	public override async Task<OperationResponse> UpdateAsync(ToDo item, IDictionary<string, object> delta, CancellationToken cancellationToken)
	{
		try
		{
			_overlayService.Show();
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
		finally
		{
			_overlayService.Hide();
		}
	}

	public override async Task<OperationResponse> DeleteAsync(ToDo item, CancellationToken cancellationToken)
	{
		try
		{
			_overlayService.Show();
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
		finally
		{
			_overlayService.Hide();
		}
	}
}
