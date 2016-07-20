using System.Windows.Input;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	public partial class InfoPopup : PopupPage
	{
		public InfoPopup(string information)
		{
			InitializeComponent();

			Information = information;
			IsCloseOnBackgroundClick = true;
			IsSystemPadding = true;

			BindingContext = this;
		}

		public string Information { get; private set; }

		public ICommand DismissCommand { get; } = new Command(async () => await Application.Current.MainPage.Navigation.PopPopupAsync());
	}
}

