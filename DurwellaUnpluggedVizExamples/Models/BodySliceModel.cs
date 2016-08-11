using System;
using Durwella.Unplugged.Viz;

namespace DurwellaUnpluggedVizExamples
{
	[DrawWith(PrimitiveType.TriangleStrip, BackFaces.NotCulled)]
	[VertexShader(CommonVertexShader.TextureMappedModel)]
	[FragmentShader(CommonFragmentShader.TextureMappedModel)]
	public class BodySliceModel : IndexedGeometryTextureMappedModel
	{
		int _index;
		bool _isVisible = true;
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

			Image = $"body/body{index}.png";
		}

		public void SetVisibility(int sliceIndex)
		{
			if (_index >= sliceIndex && !_isVisible)
			{
				_isVisible = true;
				Image = $"body/body{_index}.png";
			}
			else if (_index < sliceIndex && _isVisible)
			{
				_isVisible = false;

				// Set the slice fully transparent
				Image = new byte[,,] { { { 0, 0, 0, 0 } } };
			}
		}
	}
}

