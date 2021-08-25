using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Templates.Search
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchBarComponent : ContentView
    {
        public IAsyncCommand<string> EntryTextChanged
        {
            get { return (IAsyncCommand<string>)GetValue(EntryTextChangedProperty); }
            set
            {
                SetValue(EntryTextChangedProperty, value);
            }
        }

        public static readonly BindableProperty EntryTextChangedProperty = BindableProperty.Create("EntryTextChanged",
            typeof(IAsyncCommand<string>), typeof(SearchBarComponent), defaultValue: null);

        public SearchBarComponent()
        {
            InitializeComponent();
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            EntryTextChanged?.Execute(entry.Text);
        }
    }
}