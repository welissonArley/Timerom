namespace Timerom.App.ValueObjects.SvgColorTransformation
{
    public class SvgColorTransformationLightModeDarkModeDefault : FFImageLoading.Transformations.TintTransformation
    {
        public SvgColorTransformationLightModeDarkModeDefault()
        {
            HexColor = Xamarin.Forms.Application.Current.RequestedTheme == Xamarin.Forms.OSAppTheme.Light ? "#000000" : "#FFFFFF";
            EnableSolidColor = true;
        }
    }
}
