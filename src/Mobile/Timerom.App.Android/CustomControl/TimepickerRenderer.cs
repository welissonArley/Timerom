using Timerom.App.Droid.CustomControl;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TimePicker), typeof(TimepickerRenderer))]
namespace Timerom.App.Droid.CustomControl
{
    public class TimepickerRenderer : TimePickerRenderer
    {
        public TimepickerRenderer(Android.Content.Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
                Control.SetBackgroundResource(0);
        }
    }
}