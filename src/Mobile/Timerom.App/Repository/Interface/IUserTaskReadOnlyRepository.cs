using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timerom.App.ValueObjects.Entity;

namespace Timerom.App.Repository.Interface
{
    public interface IUserTaskReadOnlyRepository
    {
        Task<bool> ExistTaskForSubcategory(long subCategoryId);
        Task<List<UserTask>> GetAll(DateTime date);
        Task<List<UserTask>> GetBetweenDates(DateTime firstDate, DateTime secondDate);
        Task<UserTask> GetById(long id);
        Task<UserTask> GetLast(DateTime date);
    }
}
