using CSharpToDo.Client.Components.Dialogs;
using CSharpToDo.Shared.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using PanoramicData.Blazor.Interfaces;
using System.Globalization;

namespace CSharpToDo.Client.Components.Shared;

public partial class AdminToolbar<TItem> where TItem : class, IIdentifiedEntity, new()
{
	private ConfirmDialog? _confirmDialog;

	[Inject]
	public NavigationManager? NavigationManager { get; set; }

	[Parameter]
	public EventCallback Create { get; set; }

	[Parameter]
	public EventCallback Edit { get; set; }

	[Parameter]
	public IDataProviderService<TItem>? DataProvider { get; set; }

	[Parameter]
	[EditorRequired]
	public string EntityTypeName { get; set; } = string.Empty;

	public Func<TItem?, string> GetItemName { get; set; } = (x) => x is null
	? string.Empty
	: x is INamedIdentifiedEntity namedEntity
		? namedEntity.Name
		: x.Id.ToString(CultureInfo.InvariantCulture);

	[Parameter]
	public EventCallback<string> Refresh { get; set; }

	[Parameter]
	public string? SearchText { get; set; } = string.Empty;

	[Parameter]
	public bool ShowDelete { get; set; } = true;

	[Parameter]
	public bool ShowSearchField { get; set; } = true;

	[Parameter]
	public EventCallback<string> SearchTextChanged { get; set; }

	[Parameter]
	public TItem? SelectedItem { get; set; }

	private async Task OnDeleteConfirmedAsync()
	{
		if (DataProvider is not null)
		{
			_ = await DataProvider.DeleteAsync(SelectedItem!, CancellationToken.None).ConfigureAwait(true);
		}

		await Refresh.InvokeAsync(SearchText).ConfigureAwait(true);
	}

	protected override async Task OnInitializedAsync()
	{
		var uri = new Uri(NavigationManager!.Uri);
		var query = QueryHelpers.ParseQuery(uri.Query);
		if (query.TryGetValue("q", out var searchText))
		{
			SearchText = searchText!;
			await SearchTextChanged.InvokeAsync(SearchText).ConfigureAwait(true);
		}
	}

	private Task OnRefreshAsync()
		=> Refresh.InvokeAsync(SearchText);

	private async Task OnSearchTextChanged(string text)
	{
		SearchText = text;
		await SearchTextChanged.InvokeAsync(SearchText).ConfigureAwait(true);
		await Refresh.InvokeAsync(SearchText).ConfigureAwait(true);
	}
}
