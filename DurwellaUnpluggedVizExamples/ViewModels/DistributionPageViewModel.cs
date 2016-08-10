using System;
using System.Linq;
using Durwella.Unplugged.Viz;
using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	public class DistributionPageViewModel : BindableObject
	{
		XYPlotView<float> _plotView;

		public DistributionPageViewModel(XYPlotView<float> plotView)
		{
			SetData();

			_plotView = plotView;
			_plotView.DataRange = new RangeF(-32, 32);
		}

		float[] _data;
		public float[] Data
		{
			get { return _data; }
			private set { _data = value; OnPropertyChanged("Data"); }
		}

		float _mean = 0;
		public float Mean
		{
			get { return _mean; }
			set { _mean = value; SetData(); }
		}

		float _standardDeviation = 5;
		public float StandardDeviation
		{
			get { return _standardDeviation; }
			set { _standardDeviation = value; SetData(); }
		}

		float _const = 0.5f;
		public float Const
		{
			get { return _const; }
			set { _const = value; SetData(); }
		}

		public void SetData()
		{
			float[] data = new float[33];

			for (var i = 0; i <= 32; i++)
			{
				data[i] = Const * (float)Math.Exp(-0.5 * Math.Pow((i - Mean - 16) / StandardDeviation, 2));
			}

			Data = data;
		}
	}
}

