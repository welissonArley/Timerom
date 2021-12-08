using System.Collections.Generic;
using System.Threading.Tasks;
using Timerom.App.Repository.Database;
using Timerom.App.Repository.Interface;
using Timerom.App.ValueObjects.Entity;

namespace Timerom.App.Repository
{
    public class CategoryRepository : ICategoryReadOnlyRepository, ICategoryWriteOnlyRepository
    {
        private CategoryDatabase database;

        private async Task Instance()
        {
            if(database == null)
                database = await CategoryDatabase.Instance();
        }

        public async Task<List<Category>> GetAll()
        {
            await Instance();

            return await database.GetAll();
        }

        public async Task Save(Category category)
        {
            await Instance();
            await database.Save(category);
        }

        public async Task Save(IList<Category> categoryList)
        {
            await Instance();
            await database.Save(categoryList);
        }

        public async Task<bool> ExistParentCategoryWithName(string name, long disregardId)
        {
            await Instance();
            return await database.ExistParentCategoryWithName(name, disregardId);
        }

        public async Task<bool> ExistChildrensCategoryWithNameAndParentId(string name, long parentId, long disregardId)
        {
            await Instance();
            return await database.ExistChildrensCategoryWithNameAndParentId(name, disregardId, disregardId);
        }

        public async Task Update(Category category)
        {
            await Instance();
            await database.Update(category);
        }

        public async Task Delete(Category category)
        {
            await Instance();
            await database.Delete(category);
        }

        public async Task<List<Category>> GetChildrensByParentId(long id)
        {
            await Instance();
            return await database.GetChildrensByParentId(id);
        }

        public async Task<Category> GetById(long id)
        {
            await Instance();
            return await database.GetById(id);
        }
    }
}
