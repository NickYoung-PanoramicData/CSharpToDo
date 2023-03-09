using Microsoft.AspNetCore.Components;

namespace CSharpToDo.Client.Components.Dialogs;

public partial class ConfirmDialog : IDisposable
{
	[Parameter]
	public string CancelButtonText { get; set; } = "No";

	[Parameter]
	public string Caption { get; set; } = "New name?";

	[Parameter]
	public RenderFragment? ChildContent { get; set; }

	[Parameter]
	public string ConfirmButtonText { get; set; } = "Yes";

	public Task ShowAsync(Options options)
	{
		Caption = options.Caption;
		Title = options.Title;
		ConfirmButtonText = options.ConfirmButtonText;
		CancelButtonText = options.CancelButtonText;
		return base.ShowAsync();
	}

	public class Options
	{
		public string Caption { get; set; } = "Are you sure?";

		public string CancelButtonText { get; set; } = "No";

		public string ConfirmButtonText { get; set; } = "Yes";

		public string Title { get; set; } = "Confirm Action";
	}
}
