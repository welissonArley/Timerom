using FFImageLoading.Transformations;
using System.Collections.ObjectModel;
using Timerom.App.ValueObjects.Enuns;
using Timerom.App.ValueObjects.SvgColorTransformation;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Templates.Information
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectCategoryExpanderComponent : ContentView
    {
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
                                                        propertyName: "Title",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(SelectCategoryExpanderComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: TitleChanged);

        public CategoryType Category
        {
            get => (CategoryType)GetValue(CategoryTypeProperty);
            set => SetValue(CategoryTypeProperty, value);
        }
        public static readonly BindableProperty CategoryTypeProperty = BindableProperty.Create(
                                                        propertyName: "Category",
                                                        returnType: typeof(CategoryType),
                                                        declaringType: typeof(SelectCategoryExpanderComponent),
                                                        defaultValue: (CategoryType)1000,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: CategoryTypeChanged);

        public ObservableCollection<Model.Category> CategoryList
        {
            get => (ObservableCollection<Model.Category>)GetValue(CategoryListProperty);
            set => SetValue(CategoryListProperty, value);
        }
        public static readonly BindableProperty CategoryListProperty = BindableProperty.Create(
                                                        propertyName: "CategoryList",
                                                        returnType: typeof(ObservableCollection<Model.Category>),
                                                        declaringType: typeof(SelectCategoryExpanderComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: CategoryListChanged);

        public IAsyncCommand<Model.Category> SelectedItem
        {
            get => (IAsyncCommand<Model.Category>)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
                                                        propertyName: "SelectedItem",
                                                        returnType: typeof(IAsyncCommand<Model.Category>),
                                                        declaringType: typeof(SelectCategoryExpanderComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        private static void TitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var component = (SelectCategoryExpanderComponent)bindable;
            component.LabelExpanderTitle.Text = newValue.ToString();
        }
        private static void CategoryTypeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            switch ((CategoryType)newValue)
            {
                case CategoryType.Productive:
                    {
                        var color = Application.Current.RequestedTheme == OSAppTheme.Light ? 
                            (Color)Application.Current.Resources["LigthProductiveColor"] : (Color)Application.Current.Resources["DarkProductiveColor"];

                        ChangeComponentsHeaderColor(bindable, color, new SvgColorTransformationLightModeDarkModeProductive());
                    }
                    break;
                case CategoryType.Neutral:
                    {
                        var color = Application.Current.RequestedTheme == OSAppTheme.Light ?
                            (Color)Application.Current.Resources["LigthNeutralColor"] : (Color)Application.Current.Resources["DarkNeutralColor"];

                        ChangeComponentsHeaderColor(bindable, color, new SvgColorTransformationLightModeDarkModeNeutral());
                    }
                    break;
                case CategoryType.Unproductive:
                    {
                        var color = Application.Current.RequestedTheme == OSAppTheme.Light ?
                            (Color)Application.Current.Resources["LigthUnproductiveColor"] : (Color)Application.Current.Resources["DarkUnproductiveColor"];

                        ChangeComponentsHeaderColor(bindable, color, new SvgColorTransformationLightModeDarkModeUnproductive());
                    }
                    break;
                default:
                    break;
            }
        }
        private static void CategoryListChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var list = oldValue != null && newValue == null ? oldValue : newValue;

            var component = (SelectCategoryExpanderComponent)bindable;
            component.BindingContext = (ObservableCollection<Model.Category>)list;
        }

        private static void ChangeComponentsHeaderColor(BindableObject bindable, Color color, TintTransformation transformation)
        {
            var component = (SelectCategoryExpanderComponent)bindable;

            component.ExpansionIndicatorHeader.Transformations.Add(transformation);
            component.LabelExpanderTitle.TextColor = color;
            component.LineHeader.BackgroundColor = color;
        }

        public SelectCategoryExpanderComponent()
        {
            InitializeComponent();
        }

        private void ItemList_Tapped(object sender, System.EventArgs e)
        {
            var tappedEvent = (TappedEventArgs)e;

            SelectedItem?.Execute(tappedEvent.Parameter);
        }
    }
}