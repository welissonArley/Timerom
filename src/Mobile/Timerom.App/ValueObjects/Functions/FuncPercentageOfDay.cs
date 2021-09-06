using System;

namespace Timerom.App.ValueObjects.Functions
{
    public class FuncPercentageOfDay
    {
        private const int HOUR_MINUTE = 1440;

        public double Execute(DateTime start, DateTime end)
        {
            return Math.Round(100 * (end - start).TotalMinutes / HOUR_MINUTE, 2);
        }
    }
}
