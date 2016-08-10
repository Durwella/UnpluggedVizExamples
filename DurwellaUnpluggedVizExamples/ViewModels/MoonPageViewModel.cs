using System.Reflection;
using Durwella.Unplugged.Viz;
using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	public class MoonPageViewModel : SampleViewModelBase
	{
		public MoonPageViewModel()
		{
			LoadData();

			Information = "A digital elevation model of the lunar surface obtained using the Lunar Orbiter Laser Altimeter (LOLA; Smith and other, 2010) on NASA's Lunar Reconnaissance Orbiter (LR; Tooley and others, 2010).";
		}

		void LoadData()
		{
			var assembly = typeof(App).GetTypeInfo().Assembly;


			var stream = assembly.GetManifestResourceStream("DurwellaUnpluggedVizExamples.Resources.moondata");

			byte[] data = new byte[1024 * 512];
			stream.Read(data, 0, 1024*512);

			Data = new RawDataArray(data, 1024, 512, typeof(byte));
		}

		RawDataArray _data;
		public RawDataArray Data
		{
			get { return _data; }
			set { _data = value; OnPropertyChanged("Data"); }
		}

		Color[] _colorMap = { Color.Black, Color.White };
		public Color[] ColorMap
		{
			get { return _colorMap; }
			set { _colorMap = value; OnPropertyChanged("ColorMap"); }
		}

		int _colorMapIndex = 0;
		public int ColorMapIndex
		{
			get { return _colorMapIndex; }
			set
			{
				_colorMapIndex = value;

				switch (value)
				{
					case 0:
						ColorMap = new Color[] { Color.Black, Color.White };
						break;
					case 1:
						ColorMap = new Color[] { Color.Red, Color.White, Color.Blue };
						break;
					case 2:
						ColorMap = new Color[] { Color.Yellow, Color.FromHex("#FFA500"), Color.FromHex("#FF4500"), Color.Black };
						break;
					case 3:
						ColorMap = new Color[] { Color.Red, Color.White, Color.Green };
						break;
					case 4:
						ColorMap = new Color[] { Color.Red, Color.FromHex("#FFA500"), Color.Yellow, Color.Green, Color.Blue, Color.FromHex("#4B0082"), Color.FromHex("#8F5E99") };
						break;
				}
			}
		}
	}
}


