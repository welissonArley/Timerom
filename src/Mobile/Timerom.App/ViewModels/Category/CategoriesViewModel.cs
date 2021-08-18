using Prism.Mvvm;
using System.Collections.Generic;

namespace Timerom.App.ViewModels.Category
{
    public class CategoriesViewModel : BindableBase
    {
        public IList<Model.Category> ProductiveCategories { get; private set; }

        public CategoriesViewModel()
        {
        }
    }
}
