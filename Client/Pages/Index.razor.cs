using CSharpToDo.Api;
using CSharpToDo.Client.Components.Dialogs;
using CSharpToDo.Client.DataProviders;
using CSharpToDo.Shared.Models;
using Microsoft.AspNetCore.Components;
using PanoramicData.Blazor;
using PanoramicData.Blazor.Interfaces;
using PanoramicData.Blazor.Models;

namespace CSharpToDo.Client.Pages;

public partial class Index
{
	private readonly PageCriteria _pageCriteria = new(1, 10);

	private ToDosDataProvider? _dataProvider;

	private PDTable<ToDo>? _table;

	private EditDialog<ToDo>? _editDialog;

	private ToDo? _selectedItem;

	[Inject]
	public ApiClient? ApiClient { get; set; }

	[Inject]
	protected IBlockOverlayService BlockOverlayService { get; set; } = null!;

	protected override Task OnInitializedAsync()
	{
		_dataProvider = new(ApiClient!);
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

	private static string GetRowClass(ToDo toDo)
	{
		return toDo.IsCompleted ? "" : "todo-disabled";
	}

	//Remove after demo
	protected async Task TriggerDelayAsync()
	{
		BlockOverlayService.Show();
		try
		{
			await Task.Delay(TimeSpan.FromSeconds(3)).ConfigureAwait(true);
		}
		finally
		{
			BlockOverlayService.Hide();
		}
	}
}
