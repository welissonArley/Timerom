using Moq;
using Prism.Navigation;

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

        public INavigationService Build()
        {
            return _repository.Object;
        }
    }
}
