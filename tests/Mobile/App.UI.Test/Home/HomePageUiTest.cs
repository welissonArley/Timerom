using NUnit.Framework;
using System.Linq;
using Xamarin.UITest;

namespace App.UI.Test.Home
{
    [TestFixture(Platform.Android)]
    public class HomePageUiTest : UiTestBase
    {
        IApp app;
        Platform platform;

        public HomePageUiTest(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void NavigateToDashboard()
        {
            app.Tap("OptionNavigateToDashboard");

            var result = app.WaitForElement("DashboardPageDetailContentPage", timeout: Timeout());

            Assert.AreEqual(result.Count(), 1, ErrorMessage("Home", "Dashboard"));
        }

        [Test]
        public void NavigateToStartTask()
        {
            app.Tap("OptionNavigateToStartTask");

            var result = app.WaitForElement("SelectCategoryForTaskContentPage", timeout: Timeout());

            Assert.AreEqual(result.Count(), 1, ErrorMessage("Home", "Select Category for Start Task"));
        }

        [Test]
        public void NavigateToAddTask()
        {
            app.Tap("OptionNavigateToAddTask");

            var result = app.WaitForElement("SelectCategoryForTaskContentPage", timeout: Timeout());

            Assert.AreEqual(result.Count(), 1, ErrorMessage("Home", "Select Category for Add Task"));
        }

        [Test]
        public void NavigateToReports()
        {
            app.Tap("OptionNavigateToReports");

            var result = app.WaitForElement("ActivityAnalyticContentPage", timeout: Timeout());

            Assert.AreEqual(result.Count(), 1, ErrorMessage("Home", "Select Category for Reports"));
        }

        [Test]
        public void NavigateToCategories()
        {
            app.Tap("OptionNavigateToCategories");

            var result = app.WaitForElement("CategoriesContentPage", timeout: Timeout());

            Assert.AreEqual(result.Count(), 1, ErrorMessage("Home", "Select Category for Categories"));
        }
    }
}
