using System.Collections.ObjectModel;
using Durwella.Unplugged.Viz;
using Microsoft.Xna.Framework;
using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	public class Well3DPageViewModel : SampleViewModelBase
	{
		WellModel _well = new WellModel();
		public Well3DPageViewModel()
		{
			Models = new ObservableCollection<IGeometry>
			{
				_well,
				new DepthIndicatorModel(),
				new GroundModel(),
			};

			Information = "An example illustration of a 3D model of a directional wellbore.  The well trajectory is calculated from specified survey data.  Relative diameters of sections of the wellbore represent different casing outer diameters; the darker cylinders at bottom represent drillpipe extending beyond the cashing shoe.";
		}

		public ObservableCollection<IGeometry> Models { get; private set; }

		public Vector3 CameraPosition { get; } = new Vector3(0, 0.1f, 1);

		Vector3 _translation = Vector3.Zero;
		public Vector3 Translation
		{
			get { return _translation; }
			set
			{
				_translation = value;
				OnPropertyChanged("Translation");
			}
		}

		public float ZoomScale { get; } = 0.0001f;

		float _tvd = 0;
		public float TrueVerticalDepth
		{
			get { return _tvd; }
			set { _tvd = value; OnPropertyChanged("TrueVerticalDepth"); }
		}

		float _north = 0;
		public float North
		{
			get { return _north; }
			set { _north = value; OnPropertyChanged("North"); }
		}

		float _east = 0;
		public float East
		{
			get { return _east; }
			set { _east = value; OnPropertyChanged("East"); }
		}

		float _measuredDepth = 0;
		public float MeasuredDepth
		{
			get { return _measuredDepth; }
			set
			{
				_measuredDepth = value;
				var position = _well.GetPosition(value);
				TrueVerticalDepth = -position.Y;
				North = position.Z;
				East = position.X;
				Translation = -position;
				OnPropertyChanged("MeasuredDepth");
			}
		}
	}
}