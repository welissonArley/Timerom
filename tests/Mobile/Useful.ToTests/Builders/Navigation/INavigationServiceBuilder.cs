﻿using Moq;
using Prism.Navigation;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Useful.ToTests.Builders.Navigation
{
    public class INavigationServiceBuilder
    {
        private static INavigationServiceBuilder _instance;
        private readonly Mock<INavigationService> _repository;

        private INavigationServiceBuilder()
        {
            if (_repository == null)
                _repository = new Mock<INavigationService>();
        }

        public static INavigationServiceBuilder Instance()
        {
            _instance = new INavigationServiceBuilder();
            return _instance;
        }

        public INavigationServiceBuilder ExecuteCommandParameter(string parameterName)
        {
            _repository.Setup(c => c.NavigateAsync(It.IsAny<string>(), It.IsAny<INavigationParameters>())).Callback(async (string page, INavigationParameters parameters) =>
            {
                var callbackCommand = parameters.GetValue<AsyncCommand>(parameterName);
                await callbackCommand.ExecuteAsync();
            });

            return this;
        }

        public INavigationService Build()
        {
            return _repository.Object;
        }
    }
}
