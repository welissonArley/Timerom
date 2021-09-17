﻿using FFImageLoading.Forms.Platform;
using FFImageLoading.Svg.Forms;
using Foundation;
using Timerom.App.iOS.Settings;
using UIKit;

namespace Timerom.App.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();

            global::Xamarin.Forms.Forms.Init();
            Matcha.BackgroundService.iOS.BackgroundAggregator.Init(this);

            CachedImageRenderer.Init();
            var ignore = typeof(SvgCachedImage);

            LoadApplication(new App(new iOSPlatformInitializer()));

            return base.FinishedLaunching(app, options);
        }
    }
}
