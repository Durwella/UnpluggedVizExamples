using System.Windows.Input;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	public class SampleViewModelBase : BindableObject
	{
		string _information;
		public string Information
		{
			get
			{
				return _information;
			}

			set
			{
				_information = value;
				InfoCommand = GetInfoCommand(value);
			}
		}

		public ICommand InfoCommand { get; private set; }

		ICommand GetInfoCommand(string text)
		{
			return new Command(async () =>
			{
				await Application.Current.MainPage.Navigation.PushPopupAsync(new InfoPopup(text));
			});
		}
	}
}


