using FluentValidation;
using Timerom.App.Model;
using Timerom.App.Repository;
using Timerom.Exception;

namespace Timerom.App.UseCase.Categories.Local.Insert
{
    public class InsertSubcategoryValidation : AbstractValidator<Category>
    {
        public InsertSubcategoryValidation(CategoryDatabase database, long parentId)
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage(ResourceTextException.NAME_IS_REQUIRED);
            RuleFor(c => c.Name).MustAsync(async (c, cancellation) =>
            {
                bool exists = await database.ExistChildrensCategoryWithNameAndParentId(name: c, parentId: parentId, disregardId: 0);
                return !exists;
            }).WithMessage(ResourceTextException.CATEGORY_ALREADY_EXIST);
        }
    }
}
