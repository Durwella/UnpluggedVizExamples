using System;
using System.IO;
using System.Reflection;
using Durwella.Unplugged.Viz;
using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	public class SeismicDataPageViewModel : SampleViewModelBase
	{
		const int dataCount = 17;
		RawDataArray[] _allData = new RawDataArray[dataCount];
		Assembly _assembly;
		public SeismicDataPageViewModel()
		{
			_assembly = this.GetType().GetTypeInfo().Assembly;

			_allData[0] = LoadDataFile(0);
			Data = _allData[0];

			Information = "Sample seismic data from F3, a block in the Dutch sector of the North Sea.  This sample data consists of 17 time-domain slices representing 16.25 km x 23.75 km regions; slice spacing is approximately 30 ms. Data obtained under the CC BY-SA license from dGB Earth Sciences B.V.";
		}

		RawDataArray _data;
		public RawDataArray Data
		{
			get { return _data; }
			private set { _data = value; OnPropertyChanged("Data"); }
		}

		int _sliceIndex = 0;
		public int SliceIndex
		{
			get { return _sliceIndex; }
			set
			{
				if (_sliceIndex != value)
				{
					_sliceIndex = value;

					if (_allData[value] == null) _allData[value] = LoadDataFile(value);

					Data = _allData[value];
				}
			}
		}

		RawDataArray LoadDataFile(int index)
		{
			Stream stream = _assembly.GetManifestResourceStream($"DurwellaUnpluggedVizExamples.Resources.{index * (384 - 128) / (dataCount - 1) + 128}");

			byte[] data = new byte[651 * 951 * 4];
			stream.Read(data, 0, 651 * 951 * 4);

			float[] floatData = new float[651 * 951];
			for (var j = 0; j < data.Length; j += 4)
			{
				floatData[j / 4] = BitConverter.ToSingle(data, j);
			}

			return new RawDataArray(floatData, 651, 951, typeof(float));
		}

		public void LoadData()
		{
			for (var i = 0; i < dataCount; i++)
			{
				if (_allData[i] == null) _allData[i] = LoadDataFile(i);
			}
		}
	}
}

