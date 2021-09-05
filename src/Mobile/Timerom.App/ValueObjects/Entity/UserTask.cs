using SQLite;
using System;

namespace Timerom.App.ValueObjects.Entity
{
    public class UserTask
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        [NotNull]
        public string Title { get; set; }
        public string Description { get; set; }
        [NotNull]
        public DateTime StartsAt { get; set; }
        [NotNull]
        public DateTime EndsAt { get; set; }
        [NotNull]
        public long CategoryId { get; set; }
    }
}
