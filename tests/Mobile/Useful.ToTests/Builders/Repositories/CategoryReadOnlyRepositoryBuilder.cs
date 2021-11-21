using Moq;
using Timerom.App.Repository.Interface;

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

        public ICategoryReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
