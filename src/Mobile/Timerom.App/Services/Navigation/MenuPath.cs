using System;
using Timerom.App.Views.Views.AboutThisProject;
using Timerom.App.Views.Views.Category;
using Timerom.App.Views.Views.Dashboard;
using Timerom.App.Views.Views.Reports.ActivityAnalytic;
using Timerom.App.Views.Views.Reports.ParetoPrinciple;

namespace Timerom.App.Services.Navigation
{
    public class MenuPath : IMenuPath
    {
        public Uri ActivityAnalytic()
        {
            return new Uri($"/DashboardPage/NavigationPage/{nameof(ActivityAnalyticPage)}", UriKind.Absolute);
        }

        public Uri Categories()
        {
            return new Uri($"/DashboardPage/NavigationPage/{nameof(CategoriesPage)}", UriKind.Absolute);
        }

        public Uri Dashboard()
        {
            return new Uri($"/DashboardPage/NavigationPage/{nameof(DashboardPageDetail)}", UriKind.Absolute);
        }

        public Uri IconsAndIllustrations()
        {
            return new Uri($"/DashboardPage/NavigationPage/{nameof(IlustrationsInformationsPage)}", UriKind.Absolute);
        }

        public Uri ParetoPrinciple()
        {
            return new Uri($"/DashboardPage/NavigationPage/{nameof(ChooseDatesParetoPrinciplePage)}", UriKind.Absolute);
        }

        public Uri PrivacyPolicy()
        {
            return new Uri($"/DashboardPage/NavigationPage/{nameof(PrivacyPolicyPage)}", UriKind.Absolute);
        }

        public Uri UseTerms()
        {
            return new Uri($"/DashboardPage/NavigationPage/{nameof(TermsOfUsePage)}", UriKind.Absolute);
        }
    }
}
