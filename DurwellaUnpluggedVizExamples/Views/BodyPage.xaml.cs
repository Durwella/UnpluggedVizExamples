using System;
using System.Collections.Generic;
using Durwella.Unplugged.Viz;
using Microsoft.Xna.Framework;
using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	public partial class BodyPage : ContentPage
	{
		public BodyPage()
		{
			InitializeComponent();

			BindingContext = new BodyPageViewModel(tumbleView);
		}
	}
}

