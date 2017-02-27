using System;
using Durwella.Unplugged.Viz;
using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	[DrawWith(PrimitiveType.TriangleStrip, BackFaces.NotCulled)]
	[VertexShader(CommonVertexShader.TextureMappedModel)]
	[FragmentShader(CommonFragmentShader.TextureMappedModel)]
	public class BodySliceModel : IndexedGeometryTextureMappedModel
	{
		int _index;
		bool _isVisible = true;
		readonly object _texture;
		readonly byte[,,] _emptyImage = { { { 0, 0, 0, 0 } } };
		public BodySliceModel(int index)
		{
			_index = index;
			float width = 0.4f;
			float height = 0.2f;
			float z = 0.5f - index / 145.0f;

			Vertices = new float[,] {
				{ -width / 2, -height / 2, z },
				{ width / 2, -height / 2, z },
				{ -width / 2, height / 2, z },
				{ width / 2, height / 2, z }
			};

			Indices = new int[] { 0, 1, 2, 3 };
			TextureCoordinates = new float[,] {
				{0, 1},
				{1, 1},
				{0, 0},
				{1, 0}
			};

			if (Device.OS == TargetPlatform.iOS)
				Image = $"body/body{index}.png";
			else
				// We load the byte array ourselves to have more control and avoid memory issues on Android
				Image = _texture = DependencyService.Get<IImageLoader>().LoadImageFromResource($"body{index}", 2);
		}

		public void SetVisibility(int sliceIndex)
		{
			if (_index >= sliceIndex && !_isVisible)
			{
				_isVisible = true;

				if (Device.OS == TargetPlatform.iOS)
					Image = $"body/body{_index}.png";
				else
					Image = _texture;
			}
			else if (_index < sliceIndex && _isVisible)
			{
				_isVisible = false;

				// Set the slice fully transparent
				Image = _emptyImage;
			}
		}
	}
}

