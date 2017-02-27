using Android.App;
using Android.Content;
using Android.OS;

namespace DurwellaUnpluggedVizExamples.Droid
{
	[Activity(Label = "Durwella Unplugged Viz Examples", Icon = "@drawable/icon", Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
	public class SplashScreen : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			var intent = new Intent(this, typeof(MainActivity));
			StartActivity(intent);
			Finish();
		}
	}
}
