using Moq;
using System.Collections.Generic;
using System.Linq;
using Timerom.App.Repository.Interface;
using Timerom.App.ValueObjects.Entity;
using Useful.ToTests.Builders.Entity;

namespace Useful.ToTests.Builders.Repositories
{
    public class CategoryReadOnlyRepositoryBuilder
    {
        private static CategoryReadOnlyRepositoryBuilder _instance;
        private readonly Mock<ICategoryReadOnlyRepository> _repository;

        private CategoryReadOnlyRepositoryBuilder()
        {
            if (_repository == null)
                _repository = new Mock<ICategoryReadOnlyRepository>();
        }

        public static CategoryReadOnlyRepositoryBuilder Instance()
        {
            _instance = new CategoryReadOnlyRepositoryBuilder();
            return _instance;
        }

        public CategoryReadOnlyRepositoryBuilder ExistChildrensCategoryWithNameAndParentId(string name, long parentId)
        {
            _repository.Setup(x => x.ExistChildrensCategoryWithNameAndParentId(name, parentId, It.IsAny<long>())).ReturnsAsync(true);
            return this;
        }
        public CategoryReadOnlyRepositoryBuilder ExistParentCategoryWithName(string name)
        {
            _repository.Setup(x => x.ExistParentCategoryWithName(name, It.IsAny<long>())).ReturnsAsync(true);
            return this;
        }
        public CategoryReadOnlyRepositoryBuilder GetById(Category parent, IList<Category> childrens = null)
        {
            _repository.Setup(x => x.GetById(parent.Id)).ReturnsAsync(parent);

            if(childrens != null)
            {
                foreach(var subcategory in childrens)
                    _repository.Setup(x => x.GetById(subcategory.Id)).ReturnsAsync(subcategory);

                _repository.Setup(x => x.GetChildrensByParentId(parent.Id)).ReturnsAsync(childrens.ToList());
            }

            return this;
        }

        public CategoryReadOnlyRepositoryBuilder GetAll()
        {
            var response = new List<Category>();

            (Category parent, IList<Category> childrens) = CategoryEntityBuilder.Instance().Productive();

            response.Add(parent);
            response.AddRange(childrens);

            (parent, childrens) = CategoryEntityBuilder.Instance().Neutral();

            response.Add(parent);
            response.AddRange(childrens);

            (parent, childrens) = CategoryEntityBuilder.Instance().Unproductive();

            response.Add(parent);
            response.AddRange(childrens);

            _repository.Setup(x => x.GetAll()).ReturnsAsync(response);
            return this;
        }

        public ICategoryReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
