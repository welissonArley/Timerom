using System.Collections.Generic;
using Timerom.App.ValueObjects.Enuns;

namespace Timerom.App.Model
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CategoryType Type { get; set; }
        public IList<Category> Childrens { get; set; }
    }
}
