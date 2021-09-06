using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Timerom.App.UseCase.UserTask.Interfaces
{
    public interface IGetAllUserTaskUseCase
    {
        Task<IList<Model.TaskModel>> Execute(DateTime date);
    }
}
