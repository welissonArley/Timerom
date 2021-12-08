using System.Collections.Generic;
using System.Threading.Tasks;
using Timerom.App.ValueObjects.Entity;

namespace Timerom.App.Repository.Interface
{
    public interface ICategoryWriteOnlyRepository
    {
        Task Save(Category category);
        Task Save(IList<Category> categoryList);
        Task Update(Category category);
        Task Delete(Category category);
    }
}
