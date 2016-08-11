using Microsoft.Xna.Framework;

namespace DurwellaUnpluggedVizExamples
{
	public class DrillPipeModel : WellboreObjectModel
	{
		public DrillPipeModel(float radius, Vector3 from, Vector3 to) : base(radius, from, to)
		{
			Image = "metal-dark.jpg";
		}
	}
}