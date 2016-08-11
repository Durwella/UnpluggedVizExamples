using System;
using Durwella.Unplugged.Viz;
using Microsoft.Xna.Framework;
using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	[DrawWith(PrimitiveType.TriangleStrip, DepthBuffer.Disabled)]
	[Shader("Indicator")]
	public class DepthIndicatorModel : BindableObject, IIndexedGeometry, IColoredGeometry
	{
		public DepthIndicatorModel()
		{
			var geometry = new CylinderGeometry(4, 0.0025f, new Vector3(0.00f, 0, 0), new Vector3(0.175f, 0, 0));
			Vertices = geometry.Vertices;
			Indices = geometry.Indices;
		}

		[VertexAttribute]
		public Array Vertices { get; private set; }

		[VertexIndices]
		public int[] Indices { get; private set; }

		[Uniform]
		public Array Color { get { return new float[] { 0, 0, 0, 1 }; } }
	}
}

