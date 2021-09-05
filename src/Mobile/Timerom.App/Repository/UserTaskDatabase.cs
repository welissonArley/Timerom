using SQLite;
using System;
using System.IO;
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
    }
}
