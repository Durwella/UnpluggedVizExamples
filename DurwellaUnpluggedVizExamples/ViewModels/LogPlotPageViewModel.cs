using System;
using System.IO;
using System.Reflection;
using Durwella.Unplugged.Viz;
using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	public class LogPlotPageViewModel : BindableObject
	{
		public LogPlotPageViewModel()
		{
			LoadData();
		}

		float[] _data;
		public float[] Data
		{
			get { return _data; }
			private set { _data = value; OnPropertyChanged("Data"); }
		}

		RangeF _colorMapRange;
		public RangeF ColorMapRange
		{
			get { return _colorMapRange; }
			private set { _colorMapRange = value; OnPropertyChanged("ColorMapRange"); }
		}

		void LoadData()
		{
			var assembly = typeof(App).GetTypeInfo().Assembly;
			var stream = assembly.GetManifestResourceStream("DurwellaUnpluggedVizExamples.Resources.logdata");

			using (var reader = new StreamReader(stream))
			{
				var contents = reader.ReadToEnd();
				var lines = contents.Split('\n');
				var data = new float[4096];

				float min = 9999;
				float max = -0000;
				for (var i = 0; i < 4096; i++)
				{
					double val;
					if (!Double.TryParse(lines[i], out val)) break;

					data[i] = (float)val;

					if (data[i] < min) min = data[i];
					if (data[i] > max) max = data[i];
				}
				ColorMapRange = new RangeF(min, max);
				Data = data;
			}
		}
	}
}

