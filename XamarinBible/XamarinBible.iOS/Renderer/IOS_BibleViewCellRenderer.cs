using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinBible.Control;
using XamarinBible.iOS.Renderer;

[assembly: ExportRenderer(typeof(BibleViewCell), typeof(IOS_BibleViewCellRenderer))]
namespace XamarinBible.iOS.Renderer
{
    class IOS_BibleViewCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            return cell;
        }
    }
}