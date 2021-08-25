using Android.Content;
using Timerom.App.CustomControl;
using Timerom.App.Droid.CustomControl;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(EntrySearchBar), typeof(EntrySearchBarRenderer))]
namespace Timerom.App.Droid.CustomControl
{
    public class EntrySearchBarRenderer : TimeromCustomEntryRenderer
    {
        public EntrySearchBarRenderer(Context context) : base(context) { }

        protected override Android.Graphics.Color GetLineColor()
        {
            return Application.Current.RequestedTheme == OSAppTheme.Dark ? Android.Graphics.Color.ParseColor("#424242") : Android.Graphics.Color.ParseColor("#F1F1F1");
        }

        protected override int GetCursor()
        {
            return Application.Current.RequestedTheme == OSAppTheme.Dark ? Resource.Drawable.CursorEntryWhite : Resource.Drawable.CursorEntryBlack;
        }
    }
}