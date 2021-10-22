using System;

namespace Timerom.App.ValueObjects.Dto
{
    public class ParetoPrincipleFilter
    {
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
        public long? CategoryId { get; set; }
    }
}
