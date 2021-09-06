using System;

namespace Timerom.App.ValueObjects.Formater
{
    public class TimeSpanToCompleteStringFormater
    {
        public string Format(TimeSpan time)
        {
            var response = "";

            if(time.Days == 1)
                response = $"1 {ResourceText.TITLE_DAY}";
            else if (time.Days > 1)
                response = $"{time.Hours} {ResourceText.TITLE_DAYS}";

            if (time.Hours == 1)
                response = $"{response} 1 {ResourceText.TITLE_HOUR}";
            else if (time.Hours > 1)
                response = $"{response} {time.Hours} {ResourceText.TITLE_HOURS}";

            if (time.Minutes == 1)
                response = $"{response} 1 {ResourceText.TITLE_MINUTE}";
            else if (time.Minutes > 1)
                response = $"{response} {time.Minutes} {ResourceText.TITLE_MINUTES}";

            return string.IsNullOrWhiteSpace(response) ? "-" : response;
        }
    }
}
