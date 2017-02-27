using Android.App;
using Android.Content.PM;
using Android.OS;

namespace DurwellaUnpluggedVizExamples.Droid
{
	[Activity(Label = "Durwella Unplugged Viz Examples", Icon = "@drawable/icon", Theme = "@android:style/Theme.Material.Light",
	          ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			Durwella.Unplugged.Viz.Droid.UnpluggedRenderers.Init(this);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication(new App());
		}
	}
}

