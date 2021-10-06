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
    public partial class CategoryExpanderComponent : ContentView
    {
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
                                                        propertyName: "Title",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(CategoryExpanderComponent),
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
                                                        declaringType: typeof(CategoryExpanderComponent),
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
                                                        declaringType: typeof(CategoryExpanderComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: CategoryListChanged);

        public IAsyncCommand<Model.Category> AddSubCategoryCommand
        {
            get => (IAsyncCommand<Model.Category>)GetValue(AddSubCategoryCommandProperty);
            set => SetValue(AddSubCategoryCommandProperty, value);
        }
        public static readonly BindableProperty AddSubCategoryCommandProperty = BindableProperty.Create(
                                                        propertyName: "AddSubCategoryCommand",
                                                        returnType: typeof(IAsyncCommand<Model.Category>),
                                                        declaringType: typeof(CategoryExpanderComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        public IAsyncCommand<Model.Category> UpdateSubCategoryCommand
        {
            get => (IAsyncCommand<Model.Category>)GetValue(UpdateSubCategoryCommandProperty);
            set => SetValue(UpdateSubCategoryCommandProperty, value);
        }
        public static readonly BindableProperty UpdateSubCategoryCommandProperty = BindableProperty.Create(
                                                        propertyName: "UpdateSubCategoryCommand",
                                                        returnType: typeof(IAsyncCommand<Model.Category>),
                                                        declaringType: typeof(CategoryExpanderComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        private static void TitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var component = (CategoryExpanderComponent)bindable;
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

            var component = (CategoryExpanderComponent)bindable;
            component.BindingContext = (ObservableCollection<Model.Category>)list;
        }

        private static void ChangeComponentsHeaderColor(BindableObject bindable, Color color, TintTransformation transformation)
        {
            var component = (CategoryExpanderComponent)bindable;

            component.ExpansionIndicatorHeader.Transformations.Add(transformation);
            component.LabelExpanderTitle.TextColor = color;
            component.LineHeader.BackgroundColor = color;
        }

        public CategoryExpanderComponent()
        {
            InitializeComponent();
        }

        private void AddSubcategory_Tapped(object sender, System.EventArgs e)
        {
            var category = (Model.Category)((TappedEventArgs)e).Parameter;

            AddSubCategoryCommand?.Execute(category);
        }

        private void UpdateSubcategory_Tapped(object sender, System.EventArgs e)
        {
            var category = (Model.Category)((TappedEventArgs)e).Parameter;
            UpdateSubCategoryCommand?.Execute(category);
        }
    }
}