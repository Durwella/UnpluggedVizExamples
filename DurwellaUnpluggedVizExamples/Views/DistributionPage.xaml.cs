using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	public partial class DistributionPage : ContentPage
	{
		public DistributionPage()
		{
			InitializeComponent();

			BindingContext = new DistributionPageViewModel(plotView);
		}
	}
}

