using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.ValueObjects.Entity;

namespace Timerom.App.Repository
{
    public class UserTaskDatabase
    {
        private static SQLiteAsyncConnection _database;
        private static readonly AsyncLazy<UserTaskDatabase> _instance = new AsyncLazy<UserTaskDatabase>(async () =>
        {
            UserTaskDatabase instance = new UserTaskDatabase();
            CreateTableResult result = await _database.CreateTableAsync<UserTask>();

            return instance;
        });

        public UserTaskDatabase()
        {
            _database = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "timerom.db3"));
        }

        public static async Task<UserTaskDatabase> Instance()
        {
            return await _instance;
        }

        public async Task Save(UserTask task)
        {
            _ = await _database.InsertAsync(task);
        }

        public async Task<List<UserTask>> GetAll(DateTime date)
        {
            var list = await _database.Table<UserTask>().ToListAsync();

            return list.Where(c => c.StartsAt.Date == date.Date || c.EndsAt.Date == date.Date).OrderBy(c => c.StartsAt).ToList();
        }

        public async Task<List<UserTask>> GetBetweenDates(DateTime firstDate, DateTime secondDate)
        {
            var list = await _database.Table<UserTask>().ToListAsync();

            var response = new List<UserTask>();

            for (var date = firstDate; date <= secondDate; date = date.AddDays(1))
            {
                var taskOfTheDate = list.Where(c => c.StartsAt.Date == date.Date || c.EndsAt.Date == date.Date);

                response.AddRange(taskOfTheDate.Where(c => response.All(w => w.Id != c.Id)));
            }

            return response;
        }

        public async Task<UserTask> GetLast(DateTime date)
        {
            var list = await _database.Table<UserTask>().ToListAsync();

            return list.Where(c => c.EndsAt.Date == date.Date).OrderBy(c => c.EndsAt).LastOrDefault();
        }

        public async Task<UserTask> GetById(long id)
        {
            return await _database.Table<UserTask>().FirstAsync(c => c.Id == id);
        }

        public async Task Delete(UserTask task)
        {
            _ = await _database.DeleteAsync(task);
        }

        public async Task Update(UserTask task)
        {
            _ = await _database.UpdateAsync(task);
        }

        public async Task<bool> ExistTaskForSubcategory(long subCategoryId)
        {
            var count = await _database.Table<UserTask>().CountAsync(c => c.CategoryId == subCategoryId);

            return count > 0;
        }
    }
}
