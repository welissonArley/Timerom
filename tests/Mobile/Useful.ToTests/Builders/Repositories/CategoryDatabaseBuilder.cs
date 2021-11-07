using Moq;
using Timerom.App.Repository;

namespace Useful.ToTests.Builders.Repositories
{
    public class CategoryDatabaseBuilder
    {
        private static CategoryDatabaseBuilder _instance;
        private readonly Mock<CategoryDatabase> _repository;

        private CategoryDatabaseBuilder()
        {
            if (_repository == null)
                _repository = new Mock<CategoryDatabase>();
        }

        public static CategoryDatabaseBuilder Instance()
        {
            _instance = new CategoryDatabaseBuilder();
            return _instance;
        }

        public CategoryDatabaseBuilder ExistChildrensCategoryWithNameAndParentId(string name, long parentId)
        {
            _repository.Setup(x => x.ExistChildrensCategoryWithNameAndParentId(name, parentId, It.IsAny<long>())).ReturnsAsync(true);
            return this;
        }
        public CategoryDatabaseBuilder ExistParentCategoryWithName(string name)
        {
            _repository.Setup(x => x.ExistParentCategoryWithName(name, It.IsAny<long>())).ReturnsAsync(true);
            return this;
        }

        public CategoryDatabase Build()
        {
            return _repository.Object;
        }
    }
}
