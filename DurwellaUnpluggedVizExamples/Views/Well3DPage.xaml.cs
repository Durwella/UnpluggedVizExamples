using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	public partial class Well3DPage : ContentPage
	{
		public Well3DPage()
		{
			InitializeComponent();

			BindingContext = new Well3DPageViewModel();
		}
	}
}

