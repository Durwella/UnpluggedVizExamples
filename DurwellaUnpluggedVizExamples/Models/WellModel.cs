using System;
using System.Collections.Generic;
using System.Linq;
using Durwella.Unplugged.Viz;
using Microsoft.Xna.Framework;

namespace DurwellaUnpluggedVizExamples
{
	class SurveyPoint
	{
		internal SurveyPoint(float md, float inc, float azi)
		{
			MD = md;
			INC = (float)Math.PI / 180 * inc;
			AZI = (float)Math.PI / 180 * azi;
		}

		internal float MD;      // ft
		internal float INC;     // rad
		internal float AZI;     // rad
		internal float TVD;     // ft
		internal float North;   // ft
		internal float East;    // ft
	}

	class WellboreObject
	{
		internal WellboreObject(float md, float od, float id)
		{
			MD = md;
			OD = od;
			ID = id;
		}

		internal float ID;  // ft
		internal float OD;  // ft
		internal float MD;  // ft
	}

	class Casing : WellboreObject
	{
		internal Casing(float md, float od, float id) : base(md, od, id) { }
	}

	class DrillPipe : WellboreObject
	{
		internal DrillPipe(float md, float od, float id) : base(md, od, id) { }
	}

	public class WellModel : List<IGeometry>
	{
		public WellModel()
		{
			DeviationSurvey = new List<SurveyPoint>
			{
				new SurveyPoint(0, 0, 0),
				new SurveyPoint(3000, 0, 0),
				new SurveyPoint(4333, 20, 20),
				new SurveyPoint(6333, 20, 20),
				new SurveyPoint(7583, 45, 45),
				new SurveyPoint(10083, 45, 45),
				new SurveyPoint(11583, 75, 105),
				new SurveyPoint(13333, 90, 135),
				new SurveyPoint(18333, 90, 135)
			};

			CasingsAndDrillPipes = new List<WellboreObject>
			{
				new Casing(4333, 1.6667f, 1.5962f),
				new Casing(7000, 1.3333f, 1.2795f),
				new Casing(10500, 1.1146f, 1.0287f),
				new DrillPipe(16933, 0.5833f, 0.25f),
				new DrillPipe(16933+400, 0.3333f, 0.1696f),
				new DrillPipe(16933+400+1000, 0.4583f, 0.3951f)
			};

			CalculateDeviationSurvey();
		}

		void CalculateDeviationSurvey()
		{
			Clear();

			SurveyPoint prevPoint = null;
			int i = -1;
			var casings = CasingsAndDrillPipes.OrderBy(c => c.MD).ToArray();
			var casingIdx = 0;
			foreach (var point in DeviationSurvey.OrderBy(p => p.MD))
			{
				i++;
				if (point.MD > 0 && prevPoint == null)
				{
					prevPoint = new SurveyPoint(0, 0, 0);
				}
				else if (point.MD == 0)
				{
					prevPoint = point;
					continue;
				}

				// Simple linear interpolation between survey points
				var dTVD = 0.5 * (Math.Cos(prevPoint.INC) + Math.Cos(point.INC));
				var dNorth = 0.5 * (Math.Sin(prevPoint.INC) * Math.Cos(prevPoint.AZI) + Math.Sin(point.INC) * Math.Cos(point.AZI));
				var dEast = 0.5 * (Math.Sin(prevPoint.INC) * Math.Sin(prevPoint.AZI) + Math.Sin(point.INC) * Math.Sin(point.AZI));

				var fromPoint = new Vector3(prevPoint.East, -prevPoint.TVD, prevPoint.North);
				while (true)
				{
					if (casingIdx >= casings.Length) break;

					// Calculate the nearer of of the next survey point or the end of the current casing.
					var deltaMD = Math.Min(point.MD, casings[casingIdx].MD) - prevPoint.MD;

					point.TVD = prevPoint.TVD + deltaMD * (float)dTVD;
					point.North = prevPoint.North + deltaMD * (float)dNorth;
					point.East = prevPoint.East + deltaMD * (float)dEast;

					var toPoint = new Vector3(point.East, -point.TVD, point.North);

					WellboreObjectModel model;
					if (casings[casingIdx] is Casing)
						model = new CasingModel(casings[casingIdx].OD * 100, fromPoint, toPoint);
					else
						model = new DrillPipeModel(casings[casingIdx].OD * 100, fromPoint, toPoint);
					Add(model);

					if (casings[casingIdx].MD <= point.MD) casingIdx++;
					else break;

					fromPoint = toPoint;
				}

				prevPoint = point;
			}
		}

		public Vector3 GetPosition(float md)
		{
			var previousPoint = DeviationSurvey.TakeWhile(p => p.MD <= md).Last();
			var nextPoint = DeviationSurvey.SkipWhile(prop => prop.MD <= md).FirstOrDefault();

			if (nextPoint == null)
				return new Vector3(previousPoint.East, -previousPoint.TVD, previousPoint.North);
			else {
				var ratio = (md - previousPoint.MD) / (nextPoint.MD - previousPoint.MD);
				return new Vector3(
					previousPoint.East + ratio * (nextPoint.East - previousPoint.East),
					-(previousPoint.TVD + ratio * (nextPoint.TVD - previousPoint.TVD)),
					previousPoint.North + ratio * (nextPoint.North - previousPoint.North)
				);
			}
		}

		IEnumerable<SurveyPoint> DeviationSurvey { get; set; }
		IEnumerable<WellboreObject> CasingsAndDrillPipes { get; set; }
	}
}

