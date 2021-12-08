using FluentAssertions;
using System.Threading.Tasks;
using Timerom.App.UseCase.Categories.Local.Insert;
using Timerom.Exception;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Builders.Request;
using Xunit;

namespace Validators.Test.Categories.Local.Insert
{
    public class InsertSubcategoryValidationTest
    {
        [Fact]
        public async Task Validade_Sucess()
        {
            var request = RequestSubcategory.Instance().Build();

            var categoryDatabase = CategoryReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new InsertSubcategoryValidation(categoryDatabase, 1);

            var validationResult = await validator.ValidateAsync(request);

            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Validade_Empty_Name()
        {
            var request = RequestSubcategory.Instance().Build();
            request.Name = "";

            var categoryDatabase = CategoryReadOnlyRepositoryBuilder.Instance().Build();

            var validator = new InsertSubcategoryValidation(categoryDatabase, 1);

            var validationResult = await validator.ValidateAsync(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.NAME_IS_REQUIRED));
        }

        [Fact]
        public async Task Validade_Duplicated_Subategories()
        {
            var request = RequestSubcategory.Instance().Build();

            var categoryDatabase = CategoryReadOnlyRepositoryBuilder.Instance()
                .ExistChildrensCategoryWithNameAndParentId(request.Name, 1)
                .Build();

            var validator = new InsertSubcategoryValidation(categoryDatabase, 1);

            var validationResult = await validator.ValidateAsync(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.CATEGORY_ALREADY_EXIST));
        }
    }
}
