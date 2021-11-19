using Moq;
using Timerom.App.Services.Navigation;

namespace Useful.ToTests.Builders.Service
{
    public class MenuPathBuilder
    {
        private static MenuPathBuilder _instance;
        private readonly Mock<IMenuPath> _repository;

        private MenuPathBuilder()
        {
            if (_repository == null)
                _repository = new Mock<IMenuPath>();
        }

        public static MenuPathBuilder Instance()
        {
            _instance = new MenuPathBuilder();
            return _instance;
        }

        public IMenuPath Build()
        {
            return _repository.Object;
        }
    }
}
