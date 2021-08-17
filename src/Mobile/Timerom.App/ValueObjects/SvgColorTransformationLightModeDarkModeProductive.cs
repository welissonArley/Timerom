using Xamarin.Forms;

namespace Timerom.App.ValueObjects
{
    public class SvgColorTransformationLightModeDarkModeProductive : FFImageLoading.Transformations.TintTransformation
    {
        public SvgColorTransformationLightModeDarkModeProductive()
        {
            var lightColorMode = (Color)Application.Current.Resources["LigthProductiveColor"];
            var darkColorMode = (Color)Application.Current.Resources["DarkProductiveColor"];

            HexColor = Application.Current.RequestedTheme == OSAppTheme.Light ? lightColorMode.ToHex() : darkColorMode.ToHex();
            EnableSolidColor = true;
        }
    }
}
