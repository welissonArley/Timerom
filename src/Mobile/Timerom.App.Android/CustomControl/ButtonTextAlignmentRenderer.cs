using Android.Content;
using Android.Views;
using Timerom.App.CustomControl;
using Timerom.App.Droid.CustomControl;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ButtonTextAlignment), typeof(ButtonTextAlignmentRenderer))]
namespace Timerom.App.Droid.CustomControl
{
    public class ButtonTextAlignmentRenderer : ButtonRenderer
    {
        public ButtonTextAlignmentRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
                Control.Gravity = HorizontalGravity() | GravityFlags.CenterVertical;
        }

        private GravityFlags HorizontalGravity()
        {
            ButtonTextAlignment element = Element as ButtonTextAlignment;

            return element.TextAlignment == Timerom.App.CustomControl.TextAlignment.Left ?  GravityFlags.Start : GravityFlags.End;
        }
    }
}