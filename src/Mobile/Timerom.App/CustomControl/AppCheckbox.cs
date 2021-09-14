using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Timerom.App.CustomControl
{
    public partial class AppCheckbox : CheckBox
    {
        public IAsyncCommand CheckChangedCommand
        {
            get => (IAsyncCommand)GetValue(CheckChangedCommandProperty);
            set => SetValue(CheckChangedCommandProperty, value);
        }

        public static readonly BindableProperty CheckChangedCommandProperty = BindableProperty.Create(
            nameof(CheckChangedCommand),
            typeof(IAsyncCommand),
            typeof(AppCheckbox),
            null
        );

        public AppCheckbox()
        {
            CheckedChanged += OnCheckChanged;
        }

        private void OnCheckChanged(object sender, CheckedChangedEventArgs e)
        {
            if (CheckChangedCommand?.CanExecute(e.Value) ?? false)
            {
                CheckChangedCommand.Execute(e.Value);
            }
        }
    }
}
