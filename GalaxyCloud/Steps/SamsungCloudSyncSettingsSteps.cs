// file="SamsungCloudSyncSettingsSteps.cs" 

using System.Threading;
using GalaxyCloud.Helpers;
using GalaxyCloud.Page;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace GalaxyCloud.Steps
{
    /// <summary>
    /// This class is about the steps of the Samsung Cloud Sync Settings Steps feature page
    /// </summary>
    [Binding]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Step methods should not be documented")]
    public class SamsungCloudSyncSettingsSteps : SamsungCloudPage
    {
        private const string toggleStatusON = "ON";
        private const string dataSyncInformationID = "DataSyncInformation";
        private const string tapToStopSyncText = "Tap here to stop syncing";
        private string lastDateSync = string.Empty;

        /// <summary>
        /// This step verifies that the app settings page is displayed
        /// </summary>
        /// <param name="appName">This parameter is the app name that app settings page shoudl be opened</param>
        #region Given
        [Given(@"get last synchronization date")]
        public void GivenGetLastSynchronizationDate()
        {
            lastDateSync = GetLastSyncedText();
        }
        #endregion Given

        #region When
        [When(@"the ""(.*)"" toggle button status is ""(.*)""")]
        public void WhenTheToggleButtonStatusIs(string appName, string status)
        {
            ChangeCloudToogleStatusOnDashboard(appName, status);
        }

        [When(@"the sync ocurrs")]
        public void WhenSyncNowOcurrs()
        {
            ChangeCloudToogleStatusOnAppSettings(toggleStatusON);
            ClickSyncButton();
            WaitTextIsNotDisplayedByID(Hooks.sessionSamsungCloud, 30, dataSyncInformationID, tapToStopSyncText);
        }

        [When(@"the toggle button status is ""(.*)"" on app settings")]
        public void WhenTheToggleButtonStatusIsOnAppSettings(string toggleStatus)
        {
            ChangeCloudToogleStatusOnAppSettings(toggleStatus);
        }

        [When(@"switch between the Dashboard and the ""([^""]*)"" app settings screen")]
        public void WhenSwitchBetweenTheDashboardAndTheAppSettingsScreen(string appName)
        {
            ClickBackButton();
            Thread.Sleep(2000);
            WaitTheSubtextIsLoading(appName);
            ClickAppButton(appName);
            VerifyAppSettingsIsOpened(appName);
        }

        [When(@"the more information menu is accessed")]
        public void WhenTheMoreInformationMenuIsAccessed()
        {
            ClicklMoreInformationMenu();
            ClickMoreInformationPopUp();
        }

        [When(@"the more option menu is clicked")]
        public void WhenTheMoreOptionMenuIsClicked()
        {
            ClicklMoreInformationMenu();
        }

        [When(@"the user start/stop synchronization")]
        public void WhenStopSyncing()
        {
            DoubleClickSyncNow();
        }

        #endregion When

        #region Then
        [Then(@"should be can opened the ""(.*)"" app settings page")]
        public void ThenShouldBeCanOpenedTheAppSettingsPage(string titleName)
        {
            Assert.True(VerifyAppSettingsIsOpened(titleName));
        }

        [Then(@"sync now button should be disabled")]
        public void ThenSyncNowButtonShouldBeDisabled()
        {
            Assert.AreEqual("0", GetCloudToggleStateAppSettings());
            Assert.IsFalse(VerifySyncNowButtonIsEnabled());
        }

        [Then(@"last Sync info should be displayed")]
        public void ThenLastSyncInfoShouldBeDisplayed()
        {
            WaitLastSyncInfoIsVisible();
            Assert.IsNotEmpty(GetSyncInfoText());
            Assert.IsNotNull(GetSyncInfoText());
        }

        [Then(@"sync info for date and time should be updated")]
        public void ThenSyncInfoForDateAndTimeShouldBeUpdated()
        {
            Assert.IsTrue(CompareSyncedAndActualDateWithMinuteMargin());
        }

        [Then(@"the radio button ""(.*)"" in the app settings screen must remain selected")]
        public void ThenTheRadioButtonInTheAppSettingsScreenMustRemainSelected(string radioName)
        {
            Assert.IsTrue(VerifyCloudSyncOptions(radioName));
        }

        [Then(@"the more information menu icon is enabled")]
        public void ThenTheMoreInformationMenuIconIsEnabled()
        {
            Assert.IsTrue(VerifyMoreInformationMenuIsEnabled());
        }

        [Then(@"the popup button is displayed")]
        public void ThenThePopupIsDisplayed()
        {
            Assert.IsTrue(VerifyMoreInformationPopUpButtonIsEnabled());
        }

        [Then(@"the ""(.*)"" page is displayed")]
        public void ThenThePageIsDisplayed(string titlePage)
        {
            Assert.IsTrue(VerifyMoreInformationIsOpened(titlePage));
        }

        [Then(@"the more information menu icon should not be displayed")]
        public void ThenTheMoreInformationMenuIconShouldNotBeDisplayed()
        {
            Assert.IsFalse(VerifyMoreInformationMenuIconIsDisplayed());
        }

        [Then(@"the synchronization date has not changed")]
        public void ThenTheSyncingTextStatusIsDisplayed()
        {
            Assert.AreEqual(lastDateSync, GetLastSyncedText());
        }
        #endregion Then

        #region StepDefinition
        [StepDefinition(@"the windows internet connection is OFF")]
        public void GivenTheWindowsInternetConnectionIs()
        {
            DisableNetworksInterfaces();
        }

        [StepDefinition(@"the ""(.*)"" app settings is accessed")]
        public void WhenTheAppSettingsIsAccessed(string appName)
        {
            Thread.Sleep(2000);
            WaitTheSubtextIsLoading(appName);
            ClickAppButton(appName);
            VerifyAppSettingsIsOpened(appName);
        }
    }
    #endregion StepDefinition
}