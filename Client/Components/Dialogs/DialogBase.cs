using CSharpToDo.Api;
using CSharpToDo.Client.Components.Dialogs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PanoramicData.Blazor;
using PanoramicData.Blazor.Models;

public abstract class DialogBase : ComponentBase, IDisposable
{
	private IJSObjectReference? _module;
	private IJSInProcessObjectReference? _bsModal;
	private bool _disposed;

	[Inject] public IJSRuntime JSRuntime { get; set; } = null!;

	[Inject] public ApiClient ApiClient { get; set; } = null!;

	//[Inject] public INotificationService NotificationService { get; set; } = null!;

	[Parameter] public EventCallback Cancel { get; set; }

	[Parameter] public EventCallback Okay { get; set; }

	[Parameter] public string Title { get; set; } = "Dialog";

	protected PDModal? Modal { get; set; }
	protected ToolbarButton OkButton { get; set; } = new() { Text = "OK", CssClass = "btn-primary", ShiftRight = true };
	protected ToolbarButton CancelButton { get; set; } = new() { Text = "Cancel" };
	protected ToolbarButton DeleteButton { get; set; } = new() { Text = "Delete", CssClass = "btn-danger", IsEnabled = false };
	protected ConfirmDialog? ConfirmDeleteDialog { get; set; }

	public Task HideAsync()
	=> _bsModal?.InvokeVoidAsync("hide").AsTask() ?? Task.CompletedTask;

	public void Dispose()
	{
		// Dispose of unmanaged resources.
		Dispose(true);
		//Suppress finalization.
		GC.SuppressFinalize(this);
	}
	protected virtual void Dispose(bool disposing)
	{
		if (_disposed)
		{
			return;
		}
		if (disposing)
		{            // dispose managed state (managed objects).
			_ = (_bsModal?.InvokeVoidAsync("dispose").AsTask());
			_bsModal?.Dispose(); _ = (_module?.DisposeAsync().AsTask());
		}
		// free unmanaged resources (unmanaged objects) and override a finalizer below.
		// set large fields to null.
		_disposed = true;
	}
	protected virtual ConfirmDialog.Options GetDeleteConfirmOptions()
   => new()
   {
	   Title = "Delete",
	   Caption = "Are you sure you want to delete this item?"
   };
	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			_module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/boostrap-interop.js").ConfigureAwait(true);
			_bsModal = await _module.InvokeAsync<IJSInProcessObjectReference>("createModal", Modal!.Id, new
			{
				keyboard = Modal.CloseOnEscape,
				backdrop = (object)(Modal.HideOnBackgroundClick ? true : "static")
			}).ConfigureAwait(true);
			Modal.Buttons.Clear(); Modal.Buttons.AddRange(new[] { DeleteButton, OkButton, CancelButton });
			StateHasChanged();
		}
	}
	protected virtual async Task OnButtonClick(string key)
	{
		if (key == "Cancel")
		{
			await OnCancel().ConfigureAwait(true);
		}
		else if (key == "Delete" && ConfirmDeleteDialog != null)
		{
			await HideAsync().ConfigureAwait(true);
			await ConfirmDeleteDialog.ShowAsync(GetDeleteConfirmOptions()).ConfigureAwait(true);
		}
		else if (key == "OK")
		{
			await OnOkay().ConfigureAwait(true);
		}
	}

	protected async Task OnCancel()
	{
		await HideAsync().ConfigureAwait(true);
		await Cancel.InvokeAsync().ConfigureAwait(true);
	}

	protected async Task OnOkay()
	{
		await HideAsync().ConfigureAwait(true);
		await Okay.InvokeAsync().ConfigureAwait(true);
	}

	public virtual async Task ShowAsync(string? focusId = null, bool showDelete = false)
	{
		if (_bsModal != null)
		{
			await _bsModal.InvokeVoidAsync("show").ConfigureAwait(true);
			DeleteButton.IsVisible = showDelete && ConfirmDeleteDialog != null;
			if (focusId != null && _module != null)
			{
				await _module.InvokeVoidAsync("focus", focusId).ConfigureAwait(true);
			}
		}
	}
}