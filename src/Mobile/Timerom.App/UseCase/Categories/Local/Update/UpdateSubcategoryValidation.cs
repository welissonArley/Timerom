using FluentValidation;
using Timerom.App.Model;
using Timerom.App.Repository.Interface;
using Timerom.Exception;

namespace Timerom.App.UseCase.Categories.Local.Update
{
    public class UpdateSubcategoryValidation : AbstractValidator<Category>
    {
        public UpdateSubcategoryValidation(ICategoryReadOnlyRepository database, long parentId)
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage(ResourceTextException.NAME_IS_REQUIRED);
            RuleFor(c => c).MustAsync(async (c, cancellation) =>
            {
                bool exists = await database.ExistChildrensCategoryWithNameAndParentId(name: c.Name, parentId: parentId, disregardId: c.Id);
                return !exists;
            }).WithMessage(ResourceTextException.CATEGORY_ALREADY_EXIST);
        }
    }
}
