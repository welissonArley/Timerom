using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timerom.App.Model;

namespace Timerom.App.UseCase.UserTask.Interfaces
{
    public interface IGetAllUserTaskUseCase
    {
        Task<TasksDetailsModel> Execute(DateTime date, IList<long> categoriesToFilterIds);
    }
}
