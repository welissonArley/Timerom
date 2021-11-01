using CoreGraphics;
using Timerom.App.CustomControl;
using Timerom.App.iOS.CustomControl;
using UIKit;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(EntryNoLine), typeof(EntryNoLineRenderer))]
namespace Timerom.App.iOS.CustomControl
{
    public class EntryNoLineRenderer : TimeromCustomEntryRenderer
	{
		protected override CGColor GetLineColor()
		{
			return Xamarin.Forms.Application.Current.RequestedTheme == OSAppTheme.Dark ? UIColor.FromRGB(31, 30, 25).CGColor : UIColor.White.CGColor;
		}
		protected override UIColor GetCursor()
		{
			return Xamarin.Forms.Application.Current.RequestedTheme == OSAppTheme.Dark ? UIColor.White : UIColor.Black;
		}
	}
}