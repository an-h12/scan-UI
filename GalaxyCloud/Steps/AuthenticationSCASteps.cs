// file="AuthenticationSCASteps.cs" 
using GalaxyCloud.Page;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace GalaxyCloud.Steps
{
    /// <summary>
    /// This class implements steps for AuthenticationSCA feature
    /// </summary>
    [Binding]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Step methods should not be documented")]
    public class AuthenticationSCASteps : SamsungCloudAssistantPage
    {
        private readonly ISpecFlowOutputHelper outputHelper;

        public AuthenticationSCASteps(ISpecFlowOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
        }
        #region When
        [When(@"measure time for Sign In action at Samsung Cloud Assistant (.*) times")]
        public void WhenMeasureTimeForSignInActionAtSamsungCloudAssistantAndSamsungAccountTimes(int times)
        {
            SignInTimePerformance(times);
        }

        [When(@"measure time for Sign Out action at Samsung Cloud Assistant (.*) times")]
        public void WhenMeasureTimeForSignOutActionAtSamsungCloudAssistantTimes(int times)
        {
            SignOutTimePerformance(times);
        }
        #endregion When
        #region Then
        [Then(@"sign in time spent should be less than (.*) seconds")]
        public void ThenSignInTimeSpentShouldBeLessThanSeconds(int expectedSeconds)
        {
            performance.LogPerformanceInfoIntoHTMLReport(outputHelper);
            Assert.IsTrue(performance.GetAverageTime() <= expectedSeconds * 1000, $"Average memory <{performance.GetAverageTime()}> is greater than Expected <{expectedSeconds * 1000}>");
        }

        [Then(@"sign out time spent should be less than (.*) seconds")]
        public void ThenSignOutTimeSpentShouldBeLessThanSeconds(int expectedSeconds)
        {
            performance.LogPerformanceInfoIntoHTMLReport(outputHelper);
            Assert.IsTrue(performance.GetAverageTime() <= expectedSeconds * 1000, $"Average memory <{performance.GetAverageTime()}> is greater than Expected <{expectedSeconds * 1000}>");
        }
        #endregion Then
    }
}