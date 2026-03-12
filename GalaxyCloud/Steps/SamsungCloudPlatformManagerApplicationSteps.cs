// file="SamsungCloudPlatformManagerApplicationSteps.cs" 

using GalaxyCloud.Page;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace GalaxyCloud.Steps
{
    /// <summary>
    /// This class is about the steps of the Samsung Cloud Platform Manager Application Steps feature page
    /// </summary>
    [Binding]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Step methods should not be documented")]
    public class SamsungCloudPlatformManagerApplicationSteps : SamsungCloudPage
    {
        #region Given
        [Given(@"the More information page of the ""(.*)"" app is accessed")]
        public void GivenTheMoreInformationPageOfTheAppIsAccessed(string appName)
        {
            WaitTheSubtextIsLoading(appName);
            ClickAppButton(appName);
            VerifyAppSettingsIsOpened(appName);
            ClicklMoreInformationMenu();
            ClickMoreInformationPopUp();
        }
        #endregion Given
        #region When

        /// <summary>
        /// This step performs the click action on back button
        /// </summary>
        [When(@"return to the previous page")]
        public void WhenReturnToThePreviousPage()
        {
            ClickBackButton();
        }
        #endregion When
        #region Then
        [Then(@"sync now button must be displayed")]
        public void ThenSyncNowButtonMustBeDisplayed()
        {
            Assert.IsTrue(VerifySyncNowButtonIsDisplayed());
        }

        [Then(@"the Dashboard page should be displayed")]
        public void ThenTheDashboardPageShouldBeDisplayed()
        {
            Assert.IsTrue(VerifyDashboardIsOpened());
        }

        /// <summary>
        /// This step verifies that the app settings is displayed
        /// </summary>
        /// <param name="appName">This parameter is the app anme that app settings page should be displayed</param>
        [Then(@"the ""(.*)"" app settings is displayed")]
        public void ThenTheAppSettingsIsDisplayed(string appName)
        {
            VerifyAppSettingsIsOpened(appName);
        }

        /// <summary>
        /// This step verifies that text is displaye on the sync now subtext
        /// </summary>
        [Then(@"the syncing text is displayed during the sync process")]
        public void ThenTheTextIsDisplayedDuringTheSyncProcess()
        {
            Assert.IsTrue(VerifySyncingText());
        }
        #endregion Then
    }
}