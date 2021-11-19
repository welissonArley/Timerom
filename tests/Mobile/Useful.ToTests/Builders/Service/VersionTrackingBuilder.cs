using Moq;
using Timerom.App.Services.XamarinEssentials.Interface;

namespace Useful.ToTests.Builders.Service
{
    public class VersionTrackingBuilder
    {
        private static VersionTrackingBuilder _instance;
        private readonly Mock<IVersionTracking> _repository;

        private VersionTrackingBuilder()
        {
            if (_repository == null)
                _repository = new Mock<IVersionTracking>();
        }

        public static VersionTrackingBuilder Instance()
        {
            _instance = new VersionTrackingBuilder();
            return _instance;
        }

        public IVersionTracking Build()
        {
            return _repository.Object;
        }
    }
}
