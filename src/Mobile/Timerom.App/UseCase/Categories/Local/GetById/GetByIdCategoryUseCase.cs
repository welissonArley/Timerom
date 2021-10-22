using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository;
using Timerom.App.UseCase.Categories.Interfaces;

namespace Timerom.App.UseCase.Categories.Local.GetById
{
    public class GetByIdCategoryUseCase : IGetByIdCategoryUseCase
    {
        public async Task<Category> Execute(long id)
        {
            CategoryDatabase database = await CategoryDatabase.Instance();
            ValueObjects.Entity.Category model = await database.GetById(id);

            var response = new Category
            {
                Id = model.Id,
                Name = model.Name,
                Type = model.Type
            };

            if (model.ParentCategoryId.HasValue)
            {
                ValueObjects.Entity.Category parent = await database.GetById(model.ParentCategoryId.Value);
                response.Parent = new Category
                {
                    Id = parent.Id,
                    Type = parent.Type,
                    Name = parent.Name
                };
            }
            else
            {
                var childrens = await database.GetChildrensByParentId(model.Id);
                response.Childrens = new ObservableCollection<Category>(childrens.Select(c => new Category
                {
                    Id = c.Id,
                    Type = c.Type,
                    Name = c.Name
                }));
            }

            return response;
        }
    }
}
