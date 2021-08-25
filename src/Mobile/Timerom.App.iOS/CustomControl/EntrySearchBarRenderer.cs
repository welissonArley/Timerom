using CoreGraphics;
using Timerom.App.CustomControl;
using Timerom.App.iOS.CustomControl;
using UIKit;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(EntrySearchBar), typeof(EntrySearchBarRenderer))]
namespace Timerom.App.iOS.CustomControl
{
    public class EntrySearchBarRenderer : TimeromCustomEntryRenderer
    {
        protected override UIColor GetCursor()
        {
            return Xamarin.Forms.Application.Current.RequestedTheme == OSAppTheme.Dark ? UIColor.White : UIColor.Black;
        }

        protected override CGColor GetLineColor()
        {
            return Xamarin.Forms.Application.Current.RequestedTheme == OSAppTheme.Dark ? UIColor.FromRGB(66, 66, 66).CGColor :
                UIColor.FromRGB(241, 241, 241).CGColor;
        }
    }
}