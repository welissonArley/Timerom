using Moq;
using Prism.Navigation;

namespace Useful.ToTests.Builders.Navigation
{
    public class INavigationParametersBuilder
    {
        private static INavigationParametersBuilder _instance;
        private readonly Mock<INavigationParameters> _repository;

        private INavigationParametersBuilder()
        {
            if (_repository == null)
                _repository = new Mock<INavigationParameters>();
        }

        public static INavigationParametersBuilder Instance()
        {
            _instance = new INavigationParametersBuilder();
            return _instance;
        }

        public INavigationParametersBuilder Parameter(string key, object parameter)
        {
            _repository.Setup(x => x.GetValue<object>(key)).Returns(parameter);
            return this;
        }

        public INavigationParameters Build()
        {
            return _repository.Object;
        }
    }
}