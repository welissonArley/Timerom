using System.Collections.Generic;
using Timerom.App;

namespace ViewModels.Test.Tasks
{
    public class TimerTaskInlineDataTest : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "" };
            yield return new object[] { "Task Title" };
            yield return new object[] { ResourceText.TITLE_CLICK_HERE_FILL_TASK_TITLE };
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
