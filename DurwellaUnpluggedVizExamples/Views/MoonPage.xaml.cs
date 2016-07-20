
using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	public partial class MoonPage : ContentPage
	{
		public MoonPage()
		{
			InitializeComponent();

			BindingContext = new MoonPageViewModel();
		}
	}
}

