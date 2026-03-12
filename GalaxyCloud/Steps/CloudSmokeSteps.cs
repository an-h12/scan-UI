// file="CloudSmokeSteps.cs" 

using GalaxyCloud.Page;
using TechTalk.SpecFlow;

namespace GalaxyCloud.Steps
{
    /// <summary>
    /// This class is about the steps of the Cloud Smoke feature page
    /// </summary>
    [Binding]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Step methods should not be documented")]
    public class AppBasicsSteps
    {
        private readonly SamsungCloudPage cloud = new SamsungCloudPage();
        #region Given
        [Given(@"Samsung Cloud app list is displayed")]
        public void GivenCloudAppListIsDisplayed()
        {
            cloud.VerifyErroList();
            cloud.WaitAppListIsDisplayed();
        }
        #endregion Given
        #region StepDefintion
        [StepDefinition(@"""([^""]*)"" app is launched")]
        public void GivenAppIsLaunched(string appName)
        {
            cloud.VerifyAppIsLaunched();
            cloud.VerifyCloudTitle(appName);
        }

        [StepDefinition(@"""(.*)"" button is oppened")]
        public void WhenButtonIsClicked(string pageName)
        {
            cloud.VerifyPageIsOpen(pageName);
        }

        [StepDefinition(@"the Back button is clicked")]
        public void ThenTheBackButtonIsClicked()
        {
            cloud.ClickBackButton();
        }

        [StepDefinition(@"the ""(.*)"" radio button selection in the application on the Samsung Cloud is changed")]
        public void ThenTheRadioButtonSelectionInTheApplicationOnTheSamsungCloudIsChanged(string status)
        {
            cloud.ChangeCloudSyncUsingOption(status);
        }

        [StepDefinition(@"the Tips page is accessed")]
        public void ThenTheTipsPageIsAccessed()
        {
            cloud.NavigateToTipsPage();
        }
        #endregion StepDefinition
    }
}