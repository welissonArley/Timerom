using Timerom.App.Droid.CustomControl;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DatePicker), typeof(DatepickerRenderer))]
namespace Timerom.App.Droid.CustomControl
{
    public class DatepickerRenderer : DatePickerRenderer
    {
        public DatepickerRenderer(Android.Content.Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
                Control.SetBackgroundResource(0);
        }
    }
}