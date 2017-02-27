using System.Windows.Input;
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
				await Application.Current.MainPage.DisplayAlert("Information", text, "Dismiss");
			});
		}
	}
}


