using System;
using System.IO;
using System.Reflection;
using Durwella.Unplugged.Viz;
using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	[DrawWith(PrimitiveType.TriangleStrip, BackFaces.NotCulled)]
	[FragmentShader(CommonFragmentShader.ColorMapping)]
	[FragmentShader("HeightMapModel.fsh", isResource: true)]
	[VertexShader("HeightMapModel.vsh", isResource: true)]
	// Order of attributes matters... 
	public class HeightMapModel : Seismic3DModelBase
	{
		[ColorMap]
		public override Array ColorMap
		{
			get
			{
				return new byte[,] {
					{ 255, 255, 255, 255 },
					{ 255, 223, 203, 255 },
					{216,206,62, 255},
					{76,201,0, 255 },
					{0,148,59, 255},
					{0,111,107, 255},
					{0,68,143, 255},
					{37,27,125, 255},
					{48,15,39, 255},
					{0, 0, 0, 0},
				};
			}
		}

		public HeightMapModel()
		{
			TextureMap = new byte[4, 8];
			UpdateVertices(1, 1);
		}

		void UpdateVertices(float uMax, float vMax)
		{
			var rows = 1 + TextureMap.GetLength(0);
			var columns = 1 + TextureMap.GetLength(1);
			var verts = new float[rows, columns, 3];
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					float u = j / (columns - 1f) * (88.0f / 188.0f) + (100.0f / 188.0f);
					float v = i / (rows - 1f);
					verts[i, j, 0] = u;            // x : Width
					verts[i, j, 1] = 0;            // y : Height
					verts[i, j, 2] = 0.5f - v;     // z : Depth (negative forward)
				}
			}
			Vertices = verts;
			TextureCoordinates = GridGeometry.GenerateTextureCoordinates(rows, columns, uMax, vMax);
			Indices = GridGeometry.GenerateTriangleStripIndices(rows, columns);

			OnPropertyChanged("TextureMap");
		}

		public void LoadHorizon(Seismic3DModel seismic, string horizonToLoad)
		{
			var traceCount = seismic.TraceCount;
			var inlineCount = seismic.InlineCount;

			var textureMapAdapter = new PowerOfTwoTextureAdapter(traceCount, inlineCount);
			var heightMap = textureMapAdapter.NewTextureMap();
			var nullMap = textureMapAdapter.NewTextureMap();

			var assembly = this.GetType().GetTypeInfo().Assembly;
			var stream = assembly.GetManifestResourceStream("DurwellaUnpluggedVizExamples.Resources.3DHorizons.xyz");

			using (var reader = new StreamReader(stream))
			{
				var horizonName = "";
				string[] columns = { };
				while (horizonName != horizonToLoad)
				{
					var line = reader.ReadLine();
					columns = line.Split('\t');
					horizonName = columns[4];
				}

				var timeRange = new RangeF(3, 0); // seconds
				float minTime = float.MaxValue;
				float maxTime = float.MinValue;
				while (horizonName == horizonToLoad)
				{
					var inline = int.Parse(columns[0]);
					var trace = int.Parse(columns[1]);
					var time = float.Parse(columns[5]);

					if (time > maxTime)
						maxTime = time;
					if (time < minTime)
						minTime = time;
					
					if (trace < traceCount+100 && inline < inlineCount)
					{
						var row = seismic.InlineNumberToIndex[inline];
						var col = seismic.TraceNumberToIndex[trace];
						heightMap[row, col] = (byte)timeRange.MapToClamped(RangeF.OfByte, time);
						nullMap[row, col] = 255;
					}

					var line = reader.ReadLine();

					if (line == null) break;

					columns = line.Split('\t');
					horizonName = columns[4];
				}

				ColorMapMinimum = (float)timeRange.MapTo(RangeF.Normal, maxTime);
				ColorMapMaximum = (float)timeRange.MapTo(RangeF.Normal, minTime);
				TextureMap = heightMap;
				UpdateVertices(textureMapAdapter.UMax, textureMapAdapter.VMax);
			}
		}
	}
}

