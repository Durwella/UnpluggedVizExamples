using Durwella.Unplugged.Viz;
using Microsoft.Xna.Framework;

namespace DurwellaUnpluggedVizExamples
{
	public class CasingModel : WellboreObjectModel
	{
		public CasingModel(float radius, Vector3 from, Vector3 to) : base(radius, from, to)
		{
			Image = "metal.jpg";
		}
	}
}

