using System;
using Durwella.Unplugged.Viz;
using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	public class Seismic3DModelBase : BindableObject, IIndexedGeometry, IColorMappedGeometry, ITextureMappedGeometry, ILitGeometry
	{
		[ColorMap]
		public virtual Array ColorMap
		{
			get
			{
				return new byte[,] {
					{ 255, 000, 000, 255 },
					{ 255, 255, 255, 255 },
					{ 000, 000, 255, 255 },
				};
			}
		}

		float _colorMapMinimum = 0;
		[Uniform]
		public float ColorMapMinimum
		{
			get { return _colorMapMinimum; }
			set
			{
				_colorMapMinimum = value;
				OnPropertyChanged("ColorMapMinimum");
			}
		}
		float _colorMapMaximum = 1;
		[Uniform]
		public float ColorMapMaximum
		{
			get { return _colorMapMaximum; }
			set
			{
				_colorMapMaximum = value;
				OnPropertyChanged("ColorMapMaximum");
			}
		}

		protected Array _textureMap;
		[TextureMap(MagnificationFilter.LinearInterpolation, WrapMode.ClampToEdge)]
		public virtual Array TextureMap
		{
			get
			{
				return _textureMap;
			}
			protected set
			{
				_textureMap = value;
				OnPropertyChanged("TextureMap");
			}
		}

		[Uniform]
		public virtual float[] LightPosition { get { return new float[] { 0.5f, 2, 2 }; } }

		public virtual float[,] Normals { get; }

		protected Array _textureCoordinates;
		[VertexAttribute]
		public virtual Array TextureCoordinates
		{
			get { return _textureCoordinates; }
			protected set { _textureCoordinates = value; OnPropertyChanged("TextureCoordinates"); }
		}

		protected int[] _indices;
		[VertexIndices]
		public virtual int[] Indices
		{
			get { return _indices; }
			protected set { _indices = value; OnPropertyChanged("Indices"); }
		}

		protected Array _vertices;
		[VertexAttribute]
		public virtual Array Vertices
		{
			get { return _vertices; }
			protected set { _vertices = value; OnPropertyChanged("Vertices"); }
		}
	}
}

