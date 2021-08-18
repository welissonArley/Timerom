using Xamarin.Forms;

namespace Timerom.App.ValueObjects
{
    public class SvgColorTransformationLightModeDarkModeUnproductive : FFImageLoading.Transformations.TintTransformation
    {
        public SvgColorTransformationLightModeDarkModeUnproductive()
        {
            var lightColorMode = (Color)Application.Current.Resources["LigthUnproductiveColor"];
            var darkColorMode = (Color)Application.Current.Resources["DarkUnproductiveColor"];

            HexColor = Application.Current.RequestedTheme == OSAppTheme.Light ? lightColorMode.ToHex() : darkColorMode.ToHex();
            EnableSolidColor = true;
        }
    }
}
