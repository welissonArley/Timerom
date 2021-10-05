namespace Timerom.App.ValueObjects
{
    public class SvgColorTransformationLightModeDarkModeDefaultInverted : FFImageLoading.Transformations.TintTransformation
    {
        public SvgColorTransformationLightModeDarkModeDefaultInverted()
        {
            HexColor = Xamarin.Forms.Application.Current.RequestedTheme == Xamarin.Forms.OSAppTheme.Light ? "#FFFFFF" : "#000000";
            EnableSolidColor = true;
        }
    }
}
