using Durwella.Unplugged.Viz;
using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	public partial class LogPlotPage : ContentPage
	{
		public LogPlotPage()
		{
			InitializeComponent();

			plotView.DataRange = new RangeF(0, 4096);
			BindingContext = new LogPlotPageViewModel();
		}
	}
}

