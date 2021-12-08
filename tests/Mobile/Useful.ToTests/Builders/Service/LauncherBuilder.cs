using Moq;
using Timerom.App.Services.XamarinEssentials.Interface;

namespace Useful.ToTests.Builders.Service
{
    public class LauncherBuilder
    {
        private static LauncherBuilder _instance;
        private readonly Mock<ILauncher> _repository;

        private LauncherBuilder()
        {
            if (_repository == null)
                _repository = new Mock<ILauncher>();
        }

        public static LauncherBuilder Instance()
        {
            _instance = new LauncherBuilder();
            return _instance;
        }

        public ILauncher Build()
        {
            return _repository.Object;
        }
    }
}
