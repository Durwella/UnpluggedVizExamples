using Durwella.Unplugged.Viz;
using Microsoft.Xna.Framework;

namespace DurwellaUnpluggedVizExamples
{
	[DrawWith(PrimitiveType.TriangleStrip, BackFaces.NotCulled)]
	[VertexShader(CommonVertexShader.TextureMappedModel)]
	[FragmentShader(CommonFragmentShader.TextureMappedModel)]
	public class WellboreObjectModel : IndexedGeometryTextureMappedModel
	{
		public WellboreObjectModel(float radius, Vector3 from, Vector3 to) : base(new CylinderGeometry(16, radius, from, to))
		{
			
		}
	}
}

