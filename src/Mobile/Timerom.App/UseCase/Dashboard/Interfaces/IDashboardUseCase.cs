using System;
using System.Threading.Tasks;
using Timerom.App.Model;

namespace Timerom.App.UseCase.Dashboard.Interfaces
{
    public interface IDashboardUseCase
    {
        Task<DashboardModel> Execute(DateTime date);
    }
}
