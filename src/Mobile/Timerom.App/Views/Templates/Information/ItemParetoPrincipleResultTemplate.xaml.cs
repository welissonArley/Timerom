using System;
using Timerom.App.Converter;
using Timerom.App.Model;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Templates.Information
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemParetoPrincipleResultTemplate : ContentView
    {
        public IAsyncCommand<long> ItemSelectedCommand
        {
            get => (IAsyncCommand<long>)GetValue(ItemSelectedCommandProperty);
            set => SetValue(ItemSelectedCommandProperty, value);
        }
        public static readonly BindableProperty ItemSelectedCommandProperty = BindableProperty.Create(
                                                        propertyName: "ItemSelectedCommand",
                                                        returnType: typeof(IAsyncCommand<long>),
                                                        declaringType: typeof(ItemParetoPrincipleResultTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);
        public RankingParetoPrincipleModel Ranking
        {
            get => (RankingParetoPrincipleModel)GetValue(RankingProperty);
            set => SetValue(RankingProperty, value);
        }
        public static readonly BindableProperty RankingProperty = BindableProperty.Create(
                                                        propertyName: "Ranking",
                                                        returnType: typeof(RankingParetoPrincipleModel),
                                                        declaringType: typeof(ItemParetoPrincipleResultTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: RankingChanged);

        private static void RankingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ranking = (RankingParetoPrincipleModel)(newValue ?? oldValue);

            if (ranking == null)
                return;

            var component = (ItemParetoPrincipleResultTemplate)bindable;

            component.EllipseIndex.Fill = (SolidColorBrush)new CategoryTypeColorConverter().Convert(ranking.Category.Type, typeof(Brush), null, null);
            component.LabelIndex.Text = $"{(ranking.Index < 10 ? $"0{ranking.Index}" : $"{ranking.Index}")}";
            component.LabelName.Text = ranking.Category.Name;
            component.LabelAmountTasks.Text = string.Format(ResourceText.TITLE_TOTAL_OF_TASKS, ranking.AmountOfTasks);
            component.LabelPercentage.Text = $"{(ranking.Percentage < 10 ? $"0{ranking.Percentage}" : $"{ranking.Percentage}")}%";
            component.LabelHours.Text = $"{new HoursStringconverter().Convert(ranking.Time, typeof(Label), null, null)}";
            component.LabelAccumulatedPercentage.Text = $"{ranking.AccumulatedPercentage}%";
            component.IllustrationPartOf80Percent.IsVisible = ranking.IsPartOf80Percent;
            component.IconArrow.IsVisible = ranking.Category.Parent == null;
        }

        public ItemParetoPrincipleResultTemplate()
        {
            InitializeComponent();
        }

        private void Item_Tapped(object sender, EventArgs e)
        {
            if(Ranking.Category.Parent == null)
                ItemSelectedCommand?.Execute(Ranking.Category.Id);
        }
    }
}