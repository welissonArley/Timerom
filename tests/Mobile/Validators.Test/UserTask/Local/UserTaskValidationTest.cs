using FluentAssertions;
using System;
using System.Threading.Tasks;
using Timerom.App.UseCase.UserTask.Local;
using Timerom.Exception;
using Useful.ToTests.Builders.Request;
using Xunit;

namespace Validators.Test.UserTask.Local
{
    public class UserTaskValidationTest
    {
        [Fact]
        public async Task Validade_Sucess()
        {
            var request = RequestTask.Instance().Build();

            var validator = new UserTaskValidation();

            var validationResult = await validator.ValidateAsync(request);

            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Validade_Empty_Title()
        {
            var request = RequestTask.Instance().Build();
            request.Title = "";

            var validator = new UserTaskValidation();

            var validationResult = await validator.ValidateAsync(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.TASK_TITLE_REQUIRED));
        }

        [Fact]
        public async Task Validade_EndsAt_LessThan_StartsAt()
        {
            var request = RequestTask.Instance().Build();
            request.StartsAt = DateTime.Now;
            request.EndsAt = DateTime.Now.AddMinutes(-1);

            var validator = new UserTaskValidation();

            var validationResult = await validator.ValidateAsync(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.START_TIME_MUST_BE_LASS_END_TIME));
        }

        [Fact]
        public async Task Validade_GreaterThan_OneDay()
        {
            var request = RequestTask.Instance().Build();
            request.StartsAt = DateTime.Now.AddDays(-2);
            request.EndsAt = DateTime.Now;

            var validator = new UserTaskValidation();

            var validationResult = await validator.ValidateAsync(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.BREAK_MAXIMUM_ONE_DAY));
        }
    }
}
