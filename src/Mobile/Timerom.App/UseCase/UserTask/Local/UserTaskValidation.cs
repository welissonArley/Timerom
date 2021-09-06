using FluentValidation;
using Timerom.App.Model;
using Timerom.Exception;

namespace Timerom.App.UseCase.UserTask.Local
{
    public class UserTaskValidation : AbstractValidator<TaskModel>
    {
        public UserTaskValidation()
        {
            RuleFor(c => c.Title).NotEmpty().WithMessage(ResourceTextException.TASK_TITLE_REQUIRED);
            RuleFor(c => c.StartsAt).LessThan(c => c.EndsAt).WithMessage(ResourceTextException.START_TIME_MUST_BE_LASS_END_TIME);
            RuleFor(c => c).Must(c => (c.EndsAt - c.StartsAt).TotalDays <= 1).WithMessage(ResourceTextException.BREAK_MAXIMUM_ONE_DAY);
        }
    }
}
