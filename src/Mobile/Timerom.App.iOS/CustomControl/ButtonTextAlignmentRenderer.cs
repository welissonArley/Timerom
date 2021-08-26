using Timerom.App.CustomControl;
using Timerom.App.iOS.CustomControl;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ButtonTextAlignment), typeof(ButtonTextAlignmentRenderer))]
namespace Timerom.App.iOS.CustomControl
{
    public class ButtonTextAlignmentRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.VerticalAlignment = UIKit.UIControlContentVerticalAlignment.Center;
                Control.HorizontalAlignment = HorizontalGravity();
            }
        }

        private UIKit.UIControlContentHorizontalAlignment HorizontalGravity()
        {
            ButtonTextAlignment element = Element as ButtonTextAlignment;

            return element.TextAlignment == Timerom.App.CustomControl.TextAlignment.Left ? UIKit.UIControlContentHorizontalAlignment.Left : UIKit.UIControlContentHorizontalAlignment.Right;
        }
    }
}