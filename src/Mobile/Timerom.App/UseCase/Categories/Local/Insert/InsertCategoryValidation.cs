using FluentValidation;
using System.Linq;
using Timerom.App.Model;
using Timerom.App.Repository;
using Timerom.Exception;

namespace Timerom.App.UseCase.Categories.Local.Insert
{
    public class InsertCategoryValidation : AbstractValidator<Category>
    {
        public InsertCategoryValidation(CategoryDatabase database)
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage(ResourceTextException.NAME_IS_REQUIRED);
            RuleFor(c => c.Childrens).Must(c => c.Count > 0).WithMessage(ResourceTextException.YOU_NEED_ADD_ONE_OR_MORE_SUBCATEGORIES);
            RuleFor(c => c.Childrens).Must(c => c.Select(k => k.Name).Distinct().Count() == c.Count).WithMessage(ResourceTextException.THERE_ARE_DUPLICATED_SUBCATEGORIES);
            RuleFor(c => c.Name).MustAsync(async (c, cancellation) =>
            {
                bool exists = await database.ExistParentCategoryWithName(name: c, disregardId: 0);
                return !exists;
            }).WithMessage(ResourceTextException.CATEGORY_ALREADY_EXIST);
        }
    }
}
