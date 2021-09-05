using FFImageLoading.Work;
using System.Globalization;
using Timerom.App.Model;
using Timerom.App.ValueObjects;
using Timerom.App.ValueObjects.Enuns;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Templates.Information
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskDetailsTemplate : ContentView
    {
        public TaskModel Task
        {
            get => (TaskModel)GetValue(TaskProperty);
            set => SetValue(TaskProperty, value);
        }
        public static readonly BindableProperty TaskProperty = BindableProperty.Create(
                                                        propertyName: "Task",
                                                        returnType: typeof(TaskModel),
                                                        declaringType: typeof(TaskDetailsTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: TaskChanged);

        private static void TaskChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var component = (TaskDetailsTemplate)bindable;

            if(newValue != null)
            {
                var task = (TaskModel)newValue;
                component.LabelTitle.Text = task.Title;
                component.LabelDuration.Text = string.Format(ResourceText.TITLE_FROM_TO, task.StartsAt.ToString("t", CultureInfo.CurrentCulture), task.EndsAt.ToString("t", CultureInfo.CurrentCulture));
                component.LabelDuration.TextColor = GetColor(task.Category.Type);
                component.LabelPercentageInfo.Text = string.Format(ResourceText.TITLE_THIS_IS_PERCENTAGE_YOUR_DAY, task.Percentage);
                component.IconChecked.Transformations.Add(GetTransformation(task.Category.Type));
            }
        }

        private static Color GetColor(CategoryType categoryType)
        {
            switch (categoryType)
            {
                case CategoryType.Productive:
                    return Application.Current.RequestedTheme == OSAppTheme.Dark ? (Color)Application.Current.Resources["DarkProductiveColor"] : (Color)Application.Current.Resources["LigthProductiveColor"];
                case CategoryType.Neutral:
                    return Application.Current.RequestedTheme == OSAppTheme.Dark ? (Color)Application.Current.Resources["DarkNeutralColor"] : (Color)Application.Current.Resources["LigthNeutralColor"];
                case CategoryType.Unproductive:
                    return Application.Current.RequestedTheme == OSAppTheme.Dark ? (Color)Application.Current.Resources["DarkUnproductiveColor"] : (Color)Application.Current.Resources["LigthUnproductiveColor"];
                default:
                    return Color.Black;
            }
        }
        private static ITransformation GetTransformation(CategoryType categoryType)
        {
            switch (categoryType)
            {
                case CategoryType.Productive:
                    return new SvgColorTransformationLightModeDarkModeProductive();
                case CategoryType.Neutral:
                    return new SvgColorTransformationLightModeDarkModeNeutral();
                case CategoryType.Unproductive:
                    return new SvgColorTransformationLightModeDarkModeUnproductive();
                default:
                    return new SvgColorTransformationLightModeDarkModeDefault();
            }
        }

        public TaskDetailsTemplate()
        {
            InitializeComponent();
        }
    }
}