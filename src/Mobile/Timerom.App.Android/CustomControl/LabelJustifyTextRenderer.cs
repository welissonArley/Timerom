using Android.Content;
using Android.OS;
using Timerom.App.CustomControl;
using Timerom.App.Droid.CustomControl;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LabelJustifyText), typeof(LabelJustifyTextRenderer))]
namespace Timerom.App.Droid.CustomControl
{
    public class LabelJustifyTextRenderer : LabelRenderer
    {
        public LabelJustifyTextRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.OMr1)
                    Control.JustificationMode = Android.Text.JustificationMode.InterWord;
                else
                    Control.TextAlignment = Android.Views.TextAlignment.Center;
            }
        }
    }
}