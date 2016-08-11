
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Durwella.Unplugged.Viz;
using Microsoft.Xna.Framework;

namespace DurwellaUnpluggedVizExamples
{
	enum CameraDirection
	{
		Head,
		Foot
	}

	public class BodyPageViewModel : SampleViewModelBase
	{
		CameraDirection _cameraDirection = CameraDirection.Head;
		public BodyPageViewModel(TumbleView view)
		{
			view.CameraControl.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == "Orientation")
				{
					var cameraVector = Vector3.Transform(-Vector3.UnitZ, (sender as CameraControl).Orientation);

					var direction = cameraVector.Z <= 0 ? CameraDirection.Head : CameraDirection.Foot;

					// In order for the transparency to work correctly, the order of the slice models needs to be reversed
					// when viewing the body from the opposite end.
					if (direction != _cameraDirection)
					{
						_cameraDirection = direction;
						AddModels(_cameraDirection);
					}
				}
			};

			AddModels(_cameraDirection);

			Information = "A sample of transverse sections of a human body. Images are originally from the Visible Human Project by the National Library of Medicine.";
		}

		void AddModels(CameraDirection cameraDirection)
		{
			var newModels = new List<BodySliceModel>();
			for (var i = 0; i <= 144; i+=3)
			{
				var idx = cameraDirection == CameraDirection.Head ? 144 - i : i;

				var model = new BodySliceModel(idx);
				model.SetVisibility(_inline);

				newModels.Add(model);
			}

			Models = new ObservableCollection<IGeometry>(newModels);
		}

		public int MaximumInlineValue { get; } = 145;

		int _inline = 0;
		public int Inline
		{
			get { return _inline; }
			set
			{
				_inline = value;

				foreach (var model in Models)
				{
					(model as BodySliceModel).SetVisibility(value);
				}
				OnPropertyChanged("Inline");
			}
		}

		public Vector3 CameraPosition { get; } = new Vector3(0, 0.25f, 0.75f);

		ObservableCollection<IGeometry> _models;
		public ObservableCollection<IGeometry> Models
		{
			get { return _models; }
			private set { _models = value; OnPropertyChanged("Models"); }
		}
	}
}

