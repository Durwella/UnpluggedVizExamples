using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	public class MainPageViewModel : BindableObject
	{
		INavigation _navigation;

		public MainPageViewModel(INavigation navigation)
		{
			_navigation = navigation;
		}

		public MenuItem[] Samples { get; } = new MenuItem[] { 
			new MenuItem() { Text = "Human Body", Image = "bodyicon.png" },
			new MenuItem() { Text = "3D Well", Image = "well3dicon.png" },
			new MenuItem() { Text = "3D Seismic Data", Image = "seismic3dicon.png" },
			new MenuItem() { Text = "Seismic Data", Image = "seismicicon.png" },
			new MenuItem() { Text = "Moon Elevation", Image = "moonicon.png" },
			new MenuItem() { Text = "Distribution Plot", Image = "distributionicon.png" },
			new MenuItem() { Text = "Log Plot", Image = "logploticon.png" },
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
					case "3D Well":
						_navigation.PushAsync(new Well3DPage());
						break;
					case "3D Seismic Data":
						_navigation.PushAsync(new Seismic3DPage());
						break;
					case "Human Body":
						_navigation.PushAsync(new BodyPage());
						break;
				}

				OnPropertyChanged("SelectedSample");
			}
		}
	}
}

