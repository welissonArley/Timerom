using Timerom.App.Model;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Templates.Information
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemSubCategoryComponent : ContentView
    {
        public Category Category
        {
            get => (Category)GetValue(CategoryProperty);
            set => SetValue(CategoryProperty, value);
        }
        public static readonly BindableProperty CategoryProperty = BindableProperty.Create(
                                                        propertyName: "Category",
                                                        returnType: typeof(Category),
                                                        declaringType: typeof(ItemSubCategoryComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: CategoryChanged);

        public IAsyncCommand<Category> OnOptionCommand
        {
            get => (IAsyncCommand<Category>)GetValue(OnOptionCommandProperty);
            set => SetValue(OnOptionCommandProperty, value);
        }
        public static readonly BindableProperty OnOptionCommandProperty = BindableProperty.Create(
                                                        propertyName: "OnOptionCommand",
                                                        returnType: typeof(IAsyncCommand<Category>),
                                                        declaringType: typeof(ItemSubCategoryComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);

        private static void CategoryChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var component = (ItemSubCategoryComponent)bindable;

            var newCategory = oldValue != null && newValue == null ? (Category)oldValue : (Category)newValue;

            component.LabelName.Text = newCategory.Name;
        }

        public ItemSubCategoryComponent()
        {
            InitializeComponent();
        }

        private void OptionExecuteAction_Tapped(object sender, System.EventArgs e)
        {
            OnOptionCommand?.Execute(Category);
        }
    }
}