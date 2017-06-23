using System;
using SeidorDemo.Controls;
using SeidorDemo.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedViewCell), typeof(ExtendedViewCellRenderer))]
namespace SeidorDemo.iOS.CustomRenderers
{
	public class ExtendedViewCellRenderer : ViewCellRenderer
	{
		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var cell = base.GetCell(item, reusableCell, tv);
			var view = item as ExtendedViewCell;
			cell.SelectedBackgroundView = new UIView
			{
				BackgroundColor = view.SelectedBackgroundColor.ToUIColor(),
			};

			return cell;
		}

	}
}
