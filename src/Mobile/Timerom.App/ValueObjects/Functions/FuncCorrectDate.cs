using System;

namespace Timerom.App.ValueObjects.Functions
{
    public class FuncCorrectDate
    {
        public (DateTime Starts, DateTime Ends) CorrectDate(DateTime starts, DateTime ends, DateTime actual)
        {
            if (starts.Date == ends.Date)
                return (starts, ends);

            if (ends.Date == actual.Date)
                return (ends.Date, ends);

            return (starts, starts.Date.AddMinutes(-1).AddDays(1));
        }
    }
}
