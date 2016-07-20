using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	public partial class MainPage : ContentPage
	{
		MainPageViewModel _vm;
		public MainPage()
		{
			InitializeComponent();

			image.Source = "logo.png";
			frame.BackgroundColor = Color.FromRgba(255, 255, 255, 192);

			_vm = new MainPageViewModel(Navigation);

			BindingContext = _vm;
		}
	}
}

