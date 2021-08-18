using Xamarin.Forms;

namespace Timerom.App.ValueObjects
{
    public class SvgColorTransformationLightModeDarkModeNeutral : FFImageLoading.Transformations.TintTransformation
    {
        public SvgColorTransformationLightModeDarkModeNeutral()
        {
            var lightColorMode = (Color)Application.Current.Resources["LigthNeutralColor"];
            var darkColorMode = (Color)Application.Current.Resources["DarkNeutralColor"];

            HexColor = Application.Current.RequestedTheme == OSAppTheme.Light ? lightColorMode.ToHex() : darkColorMode.ToHex();
            EnableSolidColor = true;
        }
    }
}
