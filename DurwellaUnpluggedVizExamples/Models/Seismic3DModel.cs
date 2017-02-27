using System;
using System.Collections.Generic;
using System.Reflection;
using Durwella.Unplugged.Viz;
using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	[FragmentShader(CommonFragmentShader.ColorMapping)]
	[FragmentShader("Seismic3DModel.fsh", isResource: true)]
	[VertexShader("Seismic3DModel.vsh", isResource: true)]
	[DrawWith(PrimitiveType.TriangleStrip, BackFaces.NotCulled)]
	public class Seismic3DModel : Seismic3DModelBase, ITranslatableGeometry
	{
		public Seismic3DModel()
		{
			Load3D();
		}

		[VertexIndices]
		public override int[] Indices { get { return new[] { 0, 1, 2, 3 }; } }

		[VertexAttribute]
		public override Array Vertices
		{
			get
			{
				return new float[,]
				{
					{ (100.0f / 188.0f), 1, 0 },
					{ 1, 1, 0 },
					{ (100.0f / 188.0f), 0, 0 },
					{ 1, 0, 0 },
				};
			}
		}

		[VertexAttribute]
		public override float[,] Normals
		{
			get
			{
				return new float[,] {
					{ 0, 0, 1 },
					{ 0, 0, 1 },
					{ 0, 0, 1 },
					{ 0, 0, 1 },
				};
			}
		}

		[TextureMap(MagnificationFilter.LinearInterpolation, WrapMode.ClampToEdge, Components = 1)]
		public override Array TextureMap
		{
			get { return _textureMap; }
			protected set
			{
				_textureMap = value;
				OnPropertyChanged("TextureMap");
			}
		}

		[Uniform]
		public override float[] LightPosition { get { return new float[] { 0.5f, 1, 1 }; } }

		float[] _sliceTranslation;
		[Uniform]
		public float[] Translation
		{
			get { return _sliceTranslation; }
			set
			{
				_sliceTranslation = value;
				OnPropertyChanged("Translation");
			}
		}

		public List<byte[,]> Inlines { get; set; }

		int _inlineIndex;
		public int InlineIndex
		{
			get { return _inlineIndex; }
			set
			{
				_inlineIndex = value;
				TextureMap = Inlines[InlineIndex];
				Translation = new float[] { 0, 0, 0.5f - InlineIndex / 122.0f };
				OnPropertyChanged("InlineIndex");
			}
		}

		public int MaximumInlineIndex { get { return Inlines == null ? 0 : Inlines.Count - 1; } }
		public int InlineCount { get { return Inlines.Count; } }
		public int TraceCount { get; private set; }

		public PowerOfTwoTextureAdapter TextureAdapter { get; private set; }

		public Dictionary<int, int> InlineNumberToIndex = new Dictionary<int, int>();
		public Dictionary<int, int> TraceNumberToIndex = new Dictionary<int, int>();

		public void Load3D()
		{
			var dataWidth = 88;
			var dataHeight = 1501;
			var sliceCount = 122;

			var assembly = this.GetType().GetTypeInfo().Assembly;
			var stream = assembly.GetManifestResourceStream("DurwellaUnpluggedVizExamples.Resources.seismicdata");

			byte[] data = new byte[dataWidth * dataHeight * sliceCount];
			stream.Read(data, 0, dataWidth * dataHeight * sliceCount);

			TraceCount = dataWidth;

			TextureAdapter = new PowerOfTwoTextureAdapter(dataWidth, dataHeight);
			TextureCoordinates = TextureAdapter.TextureCoordinates;

			if (Inlines == null)
				Inlines = new List<byte[,]>();
			Inlines.Capacity = sliceCount;

			for (int i = Inlines.Count; i < sliceCount; i++)
			{
				// A new texture map for each inline
				var map = TextureAdapter.NewTextureMap();
				for (int t = 0; t < dataWidth; t++)
				{
					InlineNumberToIndex[i] = i;
					// The first 100 traces were dropped from the data for demo purposes.
					TraceNumberToIndex[t+100] = t;

					for (int v = 0; v < dataHeight; v++)
					{
						var value = data[i * dataWidth * dataHeight + v * dataWidth + t];
						map[v, t] = value;
					}
				}
				Inlines.Add(map);

				OnPropertyChanged("MaximumInlineIndex");
			}

			// Initialize model w/ particular slice
			InlineIndex = MaximumInlineIndex / 2;
		}
	}
}

