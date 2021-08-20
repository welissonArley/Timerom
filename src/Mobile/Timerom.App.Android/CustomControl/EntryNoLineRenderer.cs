using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using System;
using Timerom.App.CustomControl;
using Timerom.App.Droid.CustomControl;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EntryNoLine), typeof(EntryNoLineRenderer))]
namespace Timerom.App.Droid.CustomControl
{
    public class EntryNoLineRenderer : EntryRenderer
    {
        public EntryNoLineRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Android.Graphics.Color color = GetLineColor();
                int cursor = GetCursor();

                if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
                {
                    Control.Background.SetColorFilter(new BlendModeColorFilter(color, BlendMode.SrcAtop));
                    Control.SetTextCursorDrawable(cursor);
                }
                else
                {
                    Control.BackgroundTintList = ColorStateList.ValueOf(color);

                    IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
                    IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "mCursorDrawableRes", "I");
                    JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, cursor);
                }
            }
        }

        private Android.Graphics.Color GetLineColor()
        {
            return Application.Current.RequestedTheme == OSAppTheme.Dark ? Android.Graphics.Color.ParseColor("#1F1E19") : Android.Graphics.Color.White;
        }

        private int GetCursor()
        {
            return Application.Current.RequestedTheme == OSAppTheme.Dark ? Resource.Drawable.CursorEntryWhite : Resource.Drawable.CursorEntryBlack;
        }
    }
}