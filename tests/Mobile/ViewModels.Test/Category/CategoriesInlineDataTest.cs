using System.Collections.Generic;
using Useful.ToTests.Builders.Request;

namespace ViewModels.Test.Category
{
    public class CategoriesInlineDataTest : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { RequestCategory.Instance().Productive() };
            yield return new object[] { RequestCategory.Instance().Neutral() };
            yield return new object[] { RequestCategory.Instance().Unproductive() };
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
