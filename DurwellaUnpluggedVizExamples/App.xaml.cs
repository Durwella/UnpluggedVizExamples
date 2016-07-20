using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	public partial class App : Application
	{
		public App()
		{
			LoadLicense();

			InitializeComponent();

			MainPage = new NavigationPage(new MainPage());
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}

		void LoadLicense()
		{
			var assembly = GetType().GetTypeInfo().Assembly;
			var stream = assembly.GetManifestResourceStream("DurwellaUnpluggedVizExamples.Resources.license");

			using (var reader = new StreamReader(stream))
			{
				Durwella.Unplugged.Viz.License.XmlString = reader.ReadToEnd();
			}
		}
	}
}

