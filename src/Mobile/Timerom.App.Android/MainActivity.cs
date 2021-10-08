using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using FFImageLoading.Forms.Platform;
using FFImageLoading.Svg.Forms;

namespace Timerom.App.Droid
{
    [Activity(Label = "Timerom", Icon = "@mipmap/ic_launcher", RoundIcon = "@mipmap/ic_launcher_round", Theme = "@style/MainTheme", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            CachedImageRenderer.Init(true);
            _ = typeof(SvgCachedImage);

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);

            base.OnBackPressed();
        }
    }
}