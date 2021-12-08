using Moq;
using Timerom.App.Services.BackGroundService;

namespace Useful.ToTests.Builders.Service
{
    public class TimerUserTaskBuilder
    {
        private static TimerUserTaskBuilder _instance;
        private readonly Mock<ITimerUserTask> _repository;

        private TimerUserTaskBuilder()
        {
            if (_repository == null)
                _repository = new Mock<ITimerUserTask>();
        }

        public static TimerUserTaskBuilder Instance()
        {
            _instance = new TimerUserTaskBuilder();
            return _instance;
        }

        public TimerUserTaskBuilder TimerRunning()
        {
            _repository.Setup(c => c.IsRunning()).Returns(true);
            return this;
        }

        public ITimerUserTask Build()
        {
            return _repository.Object;
        }
    }
}
