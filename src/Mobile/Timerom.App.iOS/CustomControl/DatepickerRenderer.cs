using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(DatePicker), typeof(Timerom.App.iOS.CustomControl.DatepickerRenderer))]
namespace Timerom.App.iOS.CustomControl
{
    public class DatepickerRenderer : DatePickerRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
		{
			base.OnElementChanged(e);

			if (Control == null)
				return;

			Control.Layer.BorderWidth = 0;
			Control.BorderStyle = UIKit.UITextBorderStyle.None;
		}
	}
}
