﻿using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.UseCase.UserTask.Interfaces;
using Timerom.App.Views.Views.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Timerom.App.ViewModels.Tasks
{
    public class SelectSubCategoryForTaskViewModel : ViewModelBase, IInitializeAsync
    {
        private readonly Lazy<ILastTaskTimeForTodayUseCase> useCase;
        private ILastTaskTimeForTodayUseCase _useCase => useCase.Value;

        public IAsyncCommand<string> SearchTextChangedCommand { get; private set; }
        public IAsyncCommand<Model.Category> ItemSelectedCommand { get; private set; }

        private ObservableCollection<Model.Category> _subCategories { get; set; }
        public Model.Category Category { get; private set; }

        public SelectSubCategoryForTaskViewModel(Lazy<ILastTaskTimeForTodayUseCase> useCase,
            Lazy<INavigationService> navigationService) : base(navigationService)
        {
            this.useCase = useCase;

            SearchTextChangedCommand = new AsyncCommand<string>(OnSearchTextChanged);
            ItemSelectedCommand = new AsyncCommand<Model.Category>(ItemSelectedCommandExecuted, allowsMultipleExecutions: false);
        }

        private Task OnSearchTextChanged(string value)
        {
            Category.Childrens = new ObservableCollection<Model.Category>(_subCategories.Where(c => c.Name.ToUpper().Contains(value.ToUpper())).ToList());

            RaisePropertyChanged("Category");

            return Task.CompletedTask;
        }

        private async Task ItemSelectedCommandExecuted(Model.Category category)
        {
            var navParameters = new NavigationParameters
            {
                { "Task", new TaskModel { Category = category, StartsAt = await _useCase.Execute(), EndsAt = DateTime.Now } }
            };

            await _navigationService.NavigateAsync(nameof(AddUpdateTaskPage), navParameters);
        }

        public Task InitializeAsync(INavigationParameters parameters)
        {
            Category = parameters.GetValue<Model.Category>("Category");

            _subCategories = new ObservableCollection<Model.Category>(Category.Childrens);

            RaisePropertyChanged("Category");

            return Task.CompletedTask;
        }
    }
}
