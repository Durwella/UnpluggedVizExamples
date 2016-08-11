using System;
using System.Collections.ObjectModel;
using Durwella.Unplugged.Viz;
using Microsoft.Xna.Framework;
using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	public class Seismic3DPageViewModel : SampleViewModelBase
	{
		Seismic3DModel _model;
		public Seismic3DPageViewModel()
		{
			_model = new Seismic3DModel();
			var heightModel = new HeightMapModel();
			heightModel.LoadHorizon(_model, "CrowMountain");

			_model.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == "MaximumInlineIndex") OnPropertyChanged("MaximumInlineValue");
				else if (e.PropertyName == "InlineIndex") OnPropertyChanged("Inline");
			};

			Models = new ObservableCollection<IGeometry>
			{
				_model,
				new SeismicWellModel(),
				heightModel,
			};

			Information = "Seismic data from Teapot Dome, courtesy RMOTC and the U.S. Department of Energy. The red line indicates a wellbore, and the heightmap represents the Crow Mountain horizon.";
		}

		public ObservableCollection<IGeometry> Models { get; private set; }

		public Vector3 Translation { get; } = new Vector3(-(144.0f / 188.0f), -0.5f, +0.2f);
		
		public Vector3 CameraPosition { get; } = new Vector3(0.5f, 0.35f, 1f);

		public int MaximumInlineValue
		{
			get { return _model.MaximumInlineIndex == 0 ? 1 : _model.MaximumInlineIndex; }
		}

		public int Inline
		{
			get { return _model.InlineIndex; }
			set { _model.InlineIndex = value; }
		}
	}
}


