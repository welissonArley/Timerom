using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.Model;
using Timerom.App.Repository.Interface;
using Timerom.App.UseCase.Categories.Interfaces;

namespace Timerom.App.UseCase.Categories.Local.GetById
{
    public class GetByIdCategoryUseCase : IGetByIdCategoryUseCase
    {
        private readonly Lazy<ICategoryReadOnlyRepository> repository;
        private ICategoryReadOnlyRepository _repository => repository.Value;

        public GetByIdCategoryUseCase(Lazy<ICategoryReadOnlyRepository> repository)
        {
            this.repository = repository;
        }

        public async Task<Category> Execute(long id)
        {
            ValueObjects.Entity.Category model = await _repository.GetById(id);

            var response = new Category
            {
                Id = model.Id,
                Name = model.Name,
                Type = model.Type
            };

            if (model.ParentCategoryId.HasValue)
            {
                ValueObjects.Entity.Category parent = await _repository.GetById(model.ParentCategoryId.Value);
                response.Parent = new Category
                {
                    Id = parent.Id,
                    Type = parent.Type,
                    Name = parent.Name
                };
            }
            else
            {
                var childrens = await _repository.GetChildrensByParentId(model.Id);
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
