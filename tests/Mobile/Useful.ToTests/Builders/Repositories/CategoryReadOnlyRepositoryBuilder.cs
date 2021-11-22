using Moq;
using System.Collections.Generic;
using Timerom.App.Repository.Interface;
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
        public CategoryReadOnlyRepositoryBuilder GetById()
        {
            _repository.Setup(x => x.GetById(It.IsAny<long>())).ReturnsAsync(CategoryEntityBuilder.Instance().Build());
            return this;
        }

        public CategoryReadOnlyRepositoryBuilder GetChildrensByParentId()
        {
            _repository.Setup(x => x.GetChildrensByParentId(It.IsAny<long>()))
                .ReturnsAsync(new List<Timerom.App.ValueObjects.Entity.Category> { CategoryEntityBuilder.Instance().Build() });
            return this;
        }

        public ICategoryReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
