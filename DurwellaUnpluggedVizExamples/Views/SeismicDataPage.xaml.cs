using System.Threading.Tasks;
using Durwella.Unplugged.Viz;
using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	public partial class SeismicDataPage : ContentPage
	{
		SeismicDataPageViewModel _vm;
		public SeismicDataPage()
		{
			InitializeComponent();

			_vm = new SeismicDataPageViewModel();
			image.DataRange = new RectangleF(300, 750, 950, -650);
			BindingContext = _vm;
		}

		protected async override void OnAppearing()
		{
			await Task.Run(() => _vm.LoadData());
		}
	}
}

