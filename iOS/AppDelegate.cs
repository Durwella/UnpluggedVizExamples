using Durwella.Unplugged.Viz.iOS;
using Foundation;
using UIKit;

namespace DurwellaUnpluggedVizExamples.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			UnpluggedRenderers.Init();
			LoadApplication(new App());


			return base.FinishedLaunching(app, options);
		}
	}
}

