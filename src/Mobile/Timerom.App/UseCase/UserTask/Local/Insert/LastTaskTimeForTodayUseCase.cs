using System;
using System.Threading.Tasks;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.UserTask.Interfaces;

namespace Timerom.App.UseCase.UserTask.Local.Insert
{
    public class LastTaskTimeForTodayUseCase : ILastTaskTimeForTodayUseCase
    {
        private readonly Lazy<IUserTaskReadOnlyRepository> repository;
        private IUserTaskReadOnlyRepository _repository => repository.Value;

        public LastTaskTimeForTodayUseCase(Lazy<IUserTaskReadOnlyRepository> repository)
        {
            this.repository = repository;
        }

        public async Task<DateTime> Execute()
        {
            var model = await _repository.GetLast(DateTime.Now);

            return model == null ? DateTime.Now : model.EndsAt;
        }
    }
}
