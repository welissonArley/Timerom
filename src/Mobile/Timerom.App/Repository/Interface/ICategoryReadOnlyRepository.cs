using System.Collections.Generic;
using System.Threading.Tasks;
using Timerom.App.ValueObjects.Entity;

namespace Timerom.App.Repository.Interface
{
    public interface ICategoryReadOnlyRepository
    {
        Task<List<Category>> GetAll();
        Task<bool> ExistParentCategoryWithName(string name, long disregardId);
        Task<bool> ExistChildrensCategoryWithNameAndParentId(string name, long parentId, long disregardId);
        Task<List<Category>> GetChildrensByParentId(long id);
        Task<Category> GetById(long id);
    }
}
