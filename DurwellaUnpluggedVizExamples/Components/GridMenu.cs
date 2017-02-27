using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace DurwellaUnpluggedVizExamples
{
	public class GridMenu : StackLayout
	{
		public static readonly BindableProperty ItemsSourceProperty =
			BindableProperty.Create("ItemsSource", typeof(IEnumerable<MenuItem>), typeof(GridMenu), null,
									propertyChanged: (b, oldVal, newVal) => (b as GridMenu).ItemsSource = (IEnumerable<MenuItem>)newVal);
		public static readonly BindableProperty SelectedItemProperty =
			BindableProperty.Create("SelectedItem", typeof(MenuItem), typeof(GridMenu), null,
									propertyChanged: (b, oldVal, newVal) => (b as GridMenu).SelectedItem = (MenuItem)newVal);

		List<MenuFrame> _frames;

		IEnumerable<MenuItem> _itemsSource;
		public IEnumerable<MenuItem> ItemsSource
		{
			get
			{
				return _itemsSource;
			}

			set
			{
				_itemsSource = value;
				SetUpMenu(value);
			}
		}

		void SetUpMenu(IEnumerable<MenuItem> items)
		{
			Children.Clear();
			_frames = new List<MenuFrame>();

			var grid = new Grid();
			foreach (var item in items)
			{
				var frame = new MenuFrame(item, this);
				_frames.Add(frame);
				grid.Children.Add(frame, grid.Children.Count, 0);

				if (grid.Children.Count == 2)
				{
					this.Children.Add(grid);
					grid = new Grid();
				}
			}

			if (grid.Children.Count != 0)
			{
				if (grid.Children.Count == 1) grid.Children.Add(new MenuFrame(), 1, 0);
				this.Children.Add(grid);
			}
		}

		class MenuFrame : Frame
		{
			internal MenuFrame(MenuItem item, GridMenu menu)
			{
				var sampleStackLayout = new StackLayout { VerticalOptions = LayoutOptions.Center };
				sampleStackLayout.Children.Add(new MenuImage(item.Image));
				sampleStackLayout.Children.Add(new MenuLabel(item.Text));

				Content = sampleStackLayout;
				OutlineColor = Color.Black;
				BackgroundColor = Color.FromRgba(255, 255, 255, 192);

				var tgr = new TapGestureRecognizer();
				tgr.Tapped += (s, e) =>
				{
					menu.SelectedItem = item;
				};

				GestureRecognizers.Add(tgr);
			}

			// Hidden frame - space-filler
			internal MenuFrame()
			{
				BackgroundColor = Color.Transparent;
				HasShadow = false;
			}
		}

		class MenuImage : Image
		{
			internal MenuImage(ImageSource source)
			{
				Source = source;
				Aspect = Aspect.AspectFit;
			}
		}

		class MenuLabel : Label
		{
			internal MenuLabel(string text)
			{
				Text = text;
				FontSize = 18;
				FontAttributes = FontAttributes.Bold;
				HorizontalTextAlignment = TextAlignment.Center;
			}
		}

		MenuItem _selectedItem;
		public MenuItem SelectedItem
		{
			get { return _selectedItem; }
			set
			{
				if (value == _selectedItem) return;

				_selectedItem = value;
				SetValue(SelectedItemProperty, value);

				var index = _itemsSource.Count();
				if (value != null)
				{
					index = _itemsSource.TakeWhile(i => !i.Equals(value)).Count();
				}

				foreach (var frame in _frames)
				{
					frame.OutlineColor = Color.Black;
					frame.BackgroundColor = Color.FromRgba(255, 255, 255, 192);
				}
				if (index < _itemsSource.Count())
				{
					_frames[index].OutlineColor = Color.Yellow;
					_frames[index].BackgroundColor = Color.FromRgba(0, 0, 0, 64);
				}
			}
		}
	}
}


