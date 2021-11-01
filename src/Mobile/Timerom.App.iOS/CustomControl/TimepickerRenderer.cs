using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TimePicker), typeof(Timerom.App.iOS.CustomControl.TimepickerRenderer))]
namespace Timerom.App.iOS.CustomControl
{
    public class TimepickerRenderer : TimePickerRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
		{
			base.OnElementChanged(e);

			if (Control == null)
				return;

			Control.Layer.BorderWidth = 0;
			Control.BorderStyle = UIKit.UITextBorderStyle.None;
		}
	}
}
