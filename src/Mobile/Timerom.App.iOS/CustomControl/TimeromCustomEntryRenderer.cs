using CoreAnimation;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Timerom.App.iOS.CustomControl
{
    public abstract class TimeromCustomEntryRenderer : EntryRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);
			if (Control == null || e.NewElement == null)
				return;

			Control.BorderStyle = UITextBorderStyle.None;

			CALayer _line = new CALayer
			{
				BorderColor = GetLineColor(),
				BackgroundColor = GetLineColor(),
				Frame = new CGRect(0, Frame.Height / 2, UIScreen.MainScreen.Bounds.Width - 40, 1f)
			};

			Control.Layer.AddSublayer(_line);
			Control.TintColor = GetCursor();
		}
		protected abstract CGColor GetLineColor();
		protected abstract UIColor GetCursor();
	}
}