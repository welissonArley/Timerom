using SQLite;
using Timerom.App.ValueObjects.Enuns;

namespace Timerom.App.ValueObjects.Entity
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        [NotNull]
        public CategoryType Type { get; set; }
        public long? ParentCategoryId { get; set; }
    }
}
