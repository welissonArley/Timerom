using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timerom.App.Repository.Database;
using Timerom.App.Repository.Interface;
using Timerom.App.ValueObjects.Entity;

namespace Timerom.App.Repository
{
    public class UserTaskRepository : IUserTaskWriteOnlyRepository, IUserTaskReadOnlyRepository
    {
        private UserTaskDatabase database;

        public async Task Delete(UserTask task)
        {
            await Instance();
            await database.Delete(task);
        }

        public async Task<bool> ExistTaskForSubcategory(long subCategoryId)
        {
            await Instance();
            return await database.ExistTaskForSubcategory(subCategoryId);
        }

        public async Task<List<UserTask>> GetAll(DateTime date)
        {
            await Instance();
            return await database.GetAll(date);
        }

        public async Task<List<UserTask>> GetBetweenDates(DateTime firstDate, DateTime secondDate)
        {
            await Instance();
            return await database.GetBetweenDates(firstDate, secondDate);
        }

        public async Task<UserTask> GetById(long id)
        {
            await Instance();
            return await database.GetById(id);
        }

        public async Task<UserTask> GetLast(DateTime date)
        {
            await Instance();
            return await database.GetLast(date);
        }

        public async Task Save(UserTask task)
        {
            await Instance();
            await database.Save(task);
        }

        public async Task Update(UserTask task)
        {
            await Instance();
            await database.Update(task);
        }

        private async Task Instance()
        {
            if (database == null)
                database = await UserTaskDatabase.Instance();
        }
    }
}
