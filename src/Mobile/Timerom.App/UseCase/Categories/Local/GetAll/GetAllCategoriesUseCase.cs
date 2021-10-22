using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Repository;
using Timerom.App.UseCase.Categories.Interfaces;

namespace Timerom.App.UseCase.Categories.Local.GetAll
{
    public class GetAllCategoriesUseCase : IGetAllCategoriesUseCase
    {
        public async Task<(IList<Model.Category> Productive, IList<Model.Category> Neutral, IList<Model.Category> Unproductive)> Execute()
        {
            CategoryDatabase database = await CategoryDatabase.Instance();
            List<ValueObjects.Entity.Category> response = await database.GetAll();

            var productiveList = response.Where(c => c.Type == ValueObjects.Enuns.CategoryType.Productive);
            var neutralList = response.Where(c => c.Type == ValueObjects.Enuns.CategoryType.Neutral);
            var unproductiveList = response.Where(c => c.Type == ValueObjects.Enuns.CategoryType.Unproductive);

            return (FormatingList(productiveList), FormatingList(neutralList), FormatingList(unproductiveList));
        }

        private List<Model.Category> FormatingList(IEnumerable<ValueObjects.Entity.Category> list)
        {
            return list.Where(c => !c.ParentCategoryId.HasValue).Select(c => new Model.Category
            {
                Id = c.Id,
                Name = c.Name,
                Type = c.Type,
                Childrens = new ObservableCollection<Model.Category>(
                    list.Where(w => w.ParentCategoryId.HasValue && w.ParentCategoryId.Value == c.Id).Select(k => new Model.Category
                    {
                        Id = k.Id,
                        Name = k.Name,
                        Type = k.Type
                    }).OrderBy(w => w.Name))
            }).OrderBy(c => c.Name).ToList();
        }
    }
}
