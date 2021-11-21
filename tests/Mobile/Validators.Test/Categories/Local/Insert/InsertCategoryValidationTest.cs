using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using Timerom.App.UseCase.Categories.Local.Insert;
using Timerom.Exception;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Builders.Request;
using Xunit;

namespace Validators.Test.Categories.Local.Insert
{
    public class InsertCategoryValidationTest
    {
        [Fact]
        public async Task Validade_Sucess()
        {
            var request = RequestCategory.Instance().Build();

            var categoryDatabase = CategoryReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new InsertCategoryValidation(categoryDatabase);

            var validationResult = await validator.ValidateAsync(request);

            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Validade_Empty_Name()
        {
            var request = RequestCategory.Instance().Build();
            request.Name = "";

            var categoryDatabase = CategoryReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new InsertCategoryValidation(categoryDatabase);

            var validationResult = await validator.ValidateAsync(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.NAME_IS_REQUIRED));
        }

        [Fact]
        public async Task Validade_Empty_Subcategories()
        {
            var request = RequestCategory.Instance().Build();
            request.Childrens.Clear();

            var categoryDatabase = CategoryReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new InsertCategoryValidation(categoryDatabase);

            var validationResult = await validator.ValidateAsync(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.YOU_NEED_ADD_ONE_OR_MORE_SUBCATEGORIES));
        }

        [Fact]
        public async Task Validade_Duplicated_Subcategories()
        {
            var request = RequestCategory.Instance().Build();
            request.Childrens.Add(new Timerom.App.Model.Category
            {
                Name = request.Childrens.First().Name
            });

            var categoryDatabase = CategoryReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new InsertCategoryValidation(categoryDatabase);

            var validationResult = await validator.ValidateAsync(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.THERE_ARE_DUPLICATED_SUBCATEGORIES));
        }

        [Fact]
        public async Task Validade_Duplicated_Categories()
        {
            var request = RequestCategory.Instance().Build();

            var categoryDatabase = CategoryReadOnlyRepositoryBuilder.Instance()
                .ExistParentCategoryWithName(request.Name)
                .Build();

            var validator = new InsertCategoryValidation(categoryDatabase);

            var validationResult = await validator.ValidateAsync(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.CATEGORY_ALREADY_EXIST));
        }
    }
}
