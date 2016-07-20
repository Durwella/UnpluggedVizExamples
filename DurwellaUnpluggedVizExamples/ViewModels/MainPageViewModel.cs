using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	public class MainPageViewModel : BindableBase
	{
		INavigation _navigation;

		public MainPageViewModel(INavigation navigation)
		{
			_navigation = navigation;
		}

		public MenuItem[] Samples { get; } = new MenuItem[] { 
			new MenuItem() { Text = "Seismic Data", Image = "seismicicon.png" },
			new MenuItem() { Text = "Moon Elevation", Image = "moonicon.png" },
			new MenuItem() { Text = "Distribution Plot", Image = "distributionicon.png" },
			new MenuItem() { Text = "Log Plot", Image = "logploticon.png" }
		};

		public MenuItem SelectedSample
		{
			get { return null; }

			set
			{
				if (value == null) return;

				switch (value.Text)
				{
					case "Seismic Data":
						_navigation.PushAsync(new SeismicDataPage());
						break;
					case "Distribution Plot":
						_navigation.PushAsync(new DistributionPage());
						break;
					case "Log Plot":
						_navigation.PushAsync(new LogPlotPage());
						break;
					case "Moon Elevation":
						_navigation.PushAsync(new MoonPage());
						break;
				}

				NotifyPropertyChanged("SelectedSample");
			}
		}
	}
}

