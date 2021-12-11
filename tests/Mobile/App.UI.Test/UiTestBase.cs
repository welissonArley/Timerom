using System;

namespace App.UI.Test
{
    public class UiTestBase
    {
        protected string ErrorMessage(string from, string to)
        {
            return $"Navigate to {to} from {from} didn't happen.";
        }

        protected TimeSpan Timeout()
        {
            return new TimeSpan(hours: 0, minutes: 0, seconds: 5);
        }
    }
}
