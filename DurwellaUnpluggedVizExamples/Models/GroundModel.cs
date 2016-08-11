using System;
using Durwella.Unplugged.Viz;

namespace DurwellaUnpluggedVizExamples
{
	[DrawWith(PrimitiveType.TriangleStrip, BackFaces.NotCulled)]
	[Shader("Ground")]
	public class GroundModel : IndexedGeometryTextureMappedLitModel
	{
		public GroundModel()
		{
			Vertices = new float[,] {
				{ -10000, 1, -10000 },
				{ 10000, 1, -10000 },
				{ -10000, 1, 10000 },
				{ 10000, 1, 10000 }
			};

			Indices = new int[] { 0, 1, 2, 3 };

			TextureCoordinates = new float[,] {
				{-5, -5},
				{5, -5},
				{-5, 5},
				{5, 5}
			};

			Normals = new float[,] {
				{0, 1, 0},
				{0, 1, 0},
				{0, 1, 0},
				{0, 1, 0}
			};

			LightPosition = new float[] { 0, 1000, 0 };

			Image = "sand.png";
		}
	}
}

