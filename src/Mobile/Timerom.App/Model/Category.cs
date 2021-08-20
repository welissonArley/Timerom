using System.Collections.ObjectModel;
using Timerom.App.ValueObjects.Enuns;

namespace Timerom.App.Model
{
    public class Category
    {
        public Category()
        {
            Childrens = new ObservableCollection<Category>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public CategoryType Type { get; set; }
        public ObservableCollection<Category> Childrens { get; set; }
    }
}
