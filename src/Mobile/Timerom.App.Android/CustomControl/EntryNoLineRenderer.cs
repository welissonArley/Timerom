using Android.Content;
using Timerom.App.CustomControl;
using Timerom.App.Droid.CustomControl;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(EntryNoLine), typeof(EntryNoLineRenderer))]
namespace Timerom.App.Droid.CustomControl
{
    public class EntryNoLineRenderer : TimeromCustomEntryRenderer
    {
        public EntryNoLineRenderer(Context context) : base(context) { }

        protected override int GetCursor()
        {
            return Application.Current.RequestedTheme == OSAppTheme.Dark ? Resource.Drawable.CursorEntryWhite : Resource.Drawable.CursorEntryBlack;
        }

        protected override Android.Graphics.Color GetLineColor()
        {
            return Application.Current.RequestedTheme == OSAppTheme.Dark ? Android.Graphics.Color.ParseColor("#1F1E19") : Android.Graphics.Color.White;
        }
    }
}