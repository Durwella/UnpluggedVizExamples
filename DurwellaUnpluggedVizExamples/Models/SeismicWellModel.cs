using System;
using Durwella.Unplugged.Viz;
using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	[DrawWith(PrimitiveType.LineStrip, BackFaces.NotCulled)]
    [Shader("SeismicWellModel")]
	public class SeismicWellModel : BindableObject, IColoredGeometry
	{
		[Uniform]
		public Array Color { get { return new float[] { 1, 0, 0, 1 }; } }

		[VertexAttribute]
		public Array Vertices
		{
			get
			{
				return new float[,] {
					{ 0.85f, 1.2f, -0.1f },
					{ 0.85f, 1.1f, -0.1f },
					{ 0.84f, 1.0f, -0.1f },
					{ 0.83f, 0.7f, -0.12f },
					{ 0.80f, 0.5f, -0.15f }
			};
			}
		}
	}
}

