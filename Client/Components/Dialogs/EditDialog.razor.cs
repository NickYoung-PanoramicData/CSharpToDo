using CSharpToDo.Shared.Interfaces;
using Microsoft.AspNetCore.Components;
using PanoramicData.Blazor;
using PanoramicData.Blazor.Interfaces;
using PanoramicData.Blazor.Models;
using System.Globalization;

namespace CSharpToDo.Client.Components.Dialogs;

public partial class EditDialog<TItem> where TItem : class, IIdentifiedEntity, new()
{
	private PDForm<TItem>? _form;

	private PDModal? _formPopUp;

	private TItem _item = new();

	[Parameter]
	public IDataProviderService<TItem>? DataProvider { get; set; }

	[Parameter]
	public Func<TItem, string> GetItemName { get; set; } = (x) => x is null
		? string.Empty
		: x is INamedIdentifiedEntity namedEntity
			? namedEntity.Name
			: x.Id.ToString(CultureInfo.InvariantCulture);

	[Parameter]
	public EventCallback Refresh { get; set; }

	[Parameter]
	public PDTable<TItem>? Table { get; set; }

	[Parameter]
	public int TitleWidth { get; set; } = 200;

	[Parameter]
	public ModalSizes Size { get; set; } = ModalSizes.Medium;

	public Task ShowCreateAsync(TItem? item = null)
	{
		_item = item ?? new TItem();
		_form!.EditItemAsync(_item, FormModes.Create);
		return _formPopUp!.ShowAsync();
	}

	public Task ShowEditAsync(TItem item)
	{
		_item = item ?? new TItem();
		_form!.EditItemAsync(_item, FormModes.Edit);
		return _formPopUp!.ShowAsync();
	}

	private async Task OnFormButtonClickAsync(string button)
	{
		if (button == "Cancel")
		{
			await _formPopUp!.HideAsync().ConfigureAwait(true);
		}
	}

	private async Task OnRefreshAsync()
	{
		await _formPopUp!.HideAsync().ConfigureAwait(true);
		await Refresh.InvokeAsync().ConfigureAwait(true);
	}
}
