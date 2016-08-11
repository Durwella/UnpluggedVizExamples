
using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	public partial class Seismic3DPage : ContentPage
	{
		public Seismic3DPage()
		{
			InitializeComponent();

			BindingContext = new Seismic3DPageViewModel();
		}
	}
}

