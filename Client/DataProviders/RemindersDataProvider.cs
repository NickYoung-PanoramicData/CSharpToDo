using CSharpToDo.Api;
using CSharpToDo.Client.Extensions;
using CSharpToDo.Shared.Models;
using PanoramicData.Blazor.Interfaces;
using PanoramicData.Blazor.Models;
using Refit;

namespace CSharpToDo.Client.DataProviders;

public class RemindersDataProvider : DataProviderBase<Reminder>
{
	private readonly ApiClient _apiClient;
	private readonly IBlockOverlayService _overlayService;

	public RemindersDataProvider(ApiClient apiClient, IBlockOverlayService overlayService)
	{
		_apiClient = apiClient;
		_overlayService = overlayService;
	}

	public override async Task<DataResponse<Reminder>> GetDataAsync(DataRequest<Reminder> request, CancellationToken cancellationToken)
	{
		var all = await _apiClient.Reminders.GetAllAsync(cancellationToken).ConfigureAwait(true);
		return new DataResponse<Reminder>(all, all.Count);
	}

	public override async Task<OperationResponse> CreateAsync(Reminder item, CancellationToken cancellationToken)
	{
		try
		{
			_overlayService.Show();
			await _apiClient.Reminders.CreateAsync(item, cancellationToken).ConfigureAwait(true);
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

	public override async Task<OperationResponse> UpdateAsync(Reminder item, IDictionary<string, object?> delta, CancellationToken cancellationToken)
	{
		try
		{
			_overlayService.Show();
			ApplyDelta(item, delta);
			await _apiClient.Reminders.UpdateAsync(item.Id, item, cancellationToken).ConfigureAwait(true);
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

	public override async Task<OperationResponse> DeleteAsync(Reminder item, CancellationToken cancellationToken)
	{
		try
		{
			_overlayService.Show();
			await _apiClient.Reminders.DeleteAsync(item.Id, cancellationToken).ConfigureAwait(true);
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
