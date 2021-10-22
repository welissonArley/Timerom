using Xamarin.Forms;

namespace Timerom.App.CustomControl
{
    public enum TextAlignment
    {
        Left = 0,
        Right = 1
    }

    public class ButtonTextAlignment : Button
    {
        public TextAlignment TextAlignment { get; set; }
    }
}
