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
			Rg.Plugins.Popup.IOS.Popup.Init();

			global::Xamarin.Forms.Forms.Init();

			LoadApplication(new App());
			UnpluggedRenderers.Init();

			return base.FinishedLaunching(app, options);
		}
	}
}

