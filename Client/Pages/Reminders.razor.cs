using CSharpToDo.Api;
using CSharpToDo.Client.Components.Dialogs;
using CSharpToDo.Client.DataProviders;
using CSharpToDo.Shared.Models;
using Microsoft.AspNetCore.Components;
using PanoramicData.Blazor;
using PanoramicData.Blazor.Interfaces;
using PanoramicData.Blazor.Models;

namespace CSharpToDo.Client.Pages;

public partial class Reminders
{
	private readonly PageCriteria _pageCriteria = new(1, 10);

	private RemindersDataProvider? _dataProvider;

	private PDTable<Reminder>? _table;

	private EditDialog<Reminder>? _editDialog;

	private Reminder? _selectedItem;

	[Inject]
	public ApiClient? ApiClient { get; set; }

	[Inject]
	public IBlockOverlayService BlockOverlayService { get; set; } = null!;

	protected override Task OnInitializedAsync()
	{
		_dataProvider = new(ApiClient!, BlockOverlayService!);
		return Task.CompletedTask;
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			await RefreshAsync().ConfigureAwait(true);
			StateHasChanged();
		}
	}

	private Task OnCreate() =>
		_editDialog!.ShowCreateAsync();

	private async Task OnEdit()
	{
		if (_selectedItem is not null)
		{
			await _editDialog!.ShowEditAsync(_selectedItem).ConfigureAwait(true);
		}
	}

	private void OnSelectionChanged() =>
		_selectedItem = _table!.GetSelectedItems().FirstOrDefault();

	private Task RefreshAsync() =>
		_table!.RefreshAsync();
}
