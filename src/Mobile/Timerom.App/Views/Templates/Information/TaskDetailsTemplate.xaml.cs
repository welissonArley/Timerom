﻿using System;
using System.Globalization;
using Timerom.App.Model;
using Timerom.App.ValueObjects.Enuns;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Timerom.App.Views.Templates.Information
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskDetailsTemplate : ContentView
    {
        public IAsyncCommand<TaskModel> OnItemSelected
        {
            get => (IAsyncCommand<TaskModel>)GetValue(OnItemSelectedProperty);
            set => SetValue(OnItemSelectedProperty, value);
        }
        public static readonly BindableProperty OnItemSelectedProperty = BindableProperty.Create(
                                                        propertyName: "OnItemSelected",
                                                        returnType: typeof(IAsyncCommand<TaskModel>),
                                                        declaringType: typeof(TaskDetailsTemplate),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: null);
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

                var categoryColor = GetColor(task.Category.Type);

                component.LabelCategory.BackgroundColor = categoryColor;
                component.LabelSubcategory.BackgroundColor = categoryColor.MultiplyAlpha(0.1);
                component.LabelSubcategory.TextColor = categoryColor;

                component.LabelSubcategory.Text = task.Category.Name;
                component.LabelCategory.Text = task.Category.Parent.Name;

                component.LabelDuration.Text = GetDuration(task.StartsAt, task.EndsAt);

                component.BindingContext = new { HasDescription = !string.IsNullOrWhiteSpace(task.Description), Description = task.Description };
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
        private static string GetDuration(DateTime startsAt, DateTime endsAt)
        {
            if (startsAt.Date == endsAt.Date)
                return $"{startsAt.ToShortDateString()}  {startsAt.ToString("t", CultureInfo.CurrentCulture)} - {endsAt.ToString("t", CultureInfo.CurrentCulture)}";

            return $"{startsAt.ToShortDateString()} {startsAt.ToString("t", CultureInfo.CurrentCulture)} - {endsAt.ToShortDateString()} {endsAt.ToString("t", CultureInfo.CurrentCulture)}";
        }

        public TaskDetailsTemplate()
        {
            InitializeComponent();
        }

        private void Item_Tapped(object sender, System.EventArgs e)
        {
            OnItemSelected?.Execute(Task);
        }
    }
}