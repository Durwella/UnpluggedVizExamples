using System;
using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	public class NumericEntry : Grid
	{
		public static readonly BindableProperty TextProperty =
			BindableProperty.Create("Text", typeof(string), typeof(NumericEntry), "",
									propertyChanged: (b, oldVal, newVal) => (b as NumericEntry).Text = (string)newVal);
		public static readonly BindableProperty ValueProperty =
			BindableProperty.Create("Value", typeof(object), typeof(NumericEntry), "",
			                        propertyChanged: (b, oldVal, newVal) => (b as NumericEntry).Value = string.Format("{0:#,###0}", newVal));
		public static readonly BindableProperty UnitsProperty =
			BindableProperty.Create("Units", typeof(string), typeof(NumericEntry), "",
									propertyChanged: (b, oldVal, newVal) => (b as NumericEntry).Units = (string)newVal);

		Label _text = new Label { FontSize = 12 };
		Label _value = new Label { FontSize = 12 };
		Label _units = new Label { FontSize = 12 };
		public NumericEntry()
		{
			ColumnDefinitions = new ColumnDefinitionCollection {
				new ColumnDefinition { Width = 36 },
				new ColumnDefinition(),
			};

			SetColumn(_text, 0);

			var valueStack = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				Children = { _value, _units }
			};
			SetColumn(valueStack, 1);
			Children.Add(_text);
			Children.Add(valueStack);
		}

		string Text
		{
			get { return _text.Text; }
			set { _text.Text = value; }
		}

		string Value
		{
			get { return _value.Text; }
			set { _value.Text = value; }
		}

		string Units
		{
			get { return _units.Text; }
			set { _units.Text = value; }
		}
	}
}

