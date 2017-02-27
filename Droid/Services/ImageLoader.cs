using System;
using Android.App;
using Android.Graphics;
using Android.Widget;
using Java.Nio;

[assembly: Xamarin.Forms.Dependency(typeof(DurwellaUnpluggedVizExamples.Droid.ImageLoader))]
namespace DurwellaUnpluggedVizExamples.Droid
{
	public class ImageLoader : IImageLoader
	{
		public object LoadImageFromResource(string resourceName, int inSampleSize = 1) {
			var resourceId = Application.Context.Resources.GetIdentifier(resourceName, "drawable", Application.Context.PackageName);

			var options = new BitmapFactory.Options
			{
				InSampleSize = inSampleSize
			};

			using (var bitmap = BitmapFactory.DecodeResource(Application.Context.Resources, resourceId, options))
			{
				var image = new ImageView(Application.Context);

				image.SetImageBitmap(bitmap);

				return image;
			}
		}
	}
}
