using System;
namespace DurwellaUnpluggedVizExamples
{
	public interface IImageLoader
	{
		object LoadImageFromResource(string resourceName, int inSampleSize = 1);
	}
}
