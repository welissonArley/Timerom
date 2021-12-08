using System.Collections.Generic;
using Timerom.App.ValueObjects.Dto;
using Useful.ToTests.Builders.Entity;

namespace UseCases.Test.Reports.ActivityAnalytic.Local
{
    public class ParetoPrincipleInlineDataTest : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new ParetoPrincipleFilter { CategoryId = CategoryEntityBuilder.Instance().Productive().parent.Id } };
            yield return new object[] { new ParetoPrincipleFilter { CategoryId = CategoryEntityBuilder.Instance().Neutral().parent.Id } };
            yield return new object[] { new ParetoPrincipleFilter { CategoryId = CategoryEntityBuilder.Instance().Unproductive().parent.Id } };
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
