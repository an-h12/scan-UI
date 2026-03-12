// file="TwoWayCommunicationSteps.cs" 

using System;
using System.Collections.Generic;
using System.Threading;
using GalaxyCloud.Page;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace GalaxyCloud.Steps
{
    /// <summary>
    /// This class is about the steps of the TwoWay Communication Steps feature page
    /// </summary>
    [Binding]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Step methods should not be documented")]
    public class TwoWayCommunicationSteps : SamsungCloudPage
    {
        private readonly SamsungAccountPage account = new SamsungAccountPage();
        private readonly SamsungGalleryPage gallery = new SamsungGalleryPage();
        private readonly SamsungNotesPage notes = new SamsungNotesPage();
        private readonly SamsungPassPage pass = new SamsungPassPage();
        private readonly WindowsSettingsPage settingPage = new WindowsSettingsPage();
        private readonly SamsungCloudAssistantPage sca = new SamsungCloudAssistantPage();
        private readonly SamsungSettingsRuntime runtime = new SamsungSettingsRuntime();

        #region Given
        [Given(@"the Sync Using '(.*)' radio button selection is changed in the Gallery application")]
        public void GivenTheSyncUsingRadioButtonSelectionIsChangedInTheGalleryApplication(string status)
        {
            gallery.ChangeGallerySyncInfo(status);
        }
        #endregion Given

        #region When
        [When(@"the toggle button status is changed on Gallery app to ""([^""]*)""")]
        public void WhenTheToggleButtonStatusIsChangedOnGalleryAppTo(string status)
        {
            gallery.ChangeGalleryToogleStatus(status);
        }

        [When(@"the sync using ""(.*)"" radio button selection is changed on the Samsung Pass application")]
        public void GivenTheSyncUsingRadioButtonSelectionIsChangedOnTheSamsungPassApplication(string status)
        {
            pass.ChangeSyncUsing(status);
        }

        [When(@"the toggle button status is changed on Samsung Pass app to ""(.*)""")]
        public void WhenTheToggleButtonStatusIsChangedOnSamsungPassAppTo(string status)
        {
            pass.ChangePassToggleStatus(status);
        }

        [When(@"the toggle button status is changed on Samsung Notes app to ""([^""]*)""")]
        public void WhenTheToggleButtonStatusIsChangedOnSamsungNotesAppTo(string status)
        {
            notes.ChangeNotesToogleStatus(status);
        }
        #endregion When

        #region Then
        [Then(@"the related toggle button ""(.*)"" status in the Samsung Cloud must be updated to ""(.*)""")]
        public void ThenTheRelatedToggleButtonStatusInTheSamsungCloudMustBeUpdatedTo(string appName, string status)
        {
            Assert.AreEqual(status == "ON" ? "1" : "0", GetCloudToggleState(appName), "The switch button did not changed");
        }

        [Then(@"the status of the related toggle button in the Gallery app should be updated to ""([^""]*)""")]
        public void ThenTheStatusOfTheRelatedToggleButtonInTheGalleryAppShouldBeUpdatedTo(string status)
        {
            gallery.ClickGallerySettingsButton();
            Assert.AreEqual(status == "OFF" ? "0" : "1", gallery.GetGalleryToggleState(), "The switch button did not changed");
        }

        [Then(@"the status of the related toggle button in the Notes app should be updated to ""([^""]*)""")]
        public void ThenTheStatusOfTheRelatedToggleButtonInTheNotesAppShouldBeUpdatedTo(string status)
        {
            notes.ClickNotesSettingsButton();
            Assert.AreEqual(status == "OFF" ? "0" : "1", notes.GetNotesToggleState(), "The switch button did not changed");
        }

        [Then(@"the status of the related toggle button in the ""(.*)"" should be updated to ""(.*)""")]
        public void ThenTheStatusOfTheRelatedToggleButtonInTheShouldBeUpdatedTo(string appName, string status)
        {
            Thread.Sleep(10000);
            Assert.AreEqual(status == "OFF" ? "0" : "1", runtime.GetSettingsRuntimeToggleState(appName), "The switch button did not changed");
        }

        [Then(@"the related radio button ""(.*)"" selection in the Samsung Cloud should be updated")]
        public void ThenTheRelatedRadioButtonSelectionInTheSamsungCloudShouldBeUpdated(string status)
        {
            Assert.IsTrue(VerifyCloudSyncOptions(status));
        }

        [Then(@"the ""(.*)"" information of Last Synced and Sync info are updated in the Samsung Cloud")]
        public void ThenTheInformationOfLastSyncedAndSyncInfoAreUpdatedInTheSamsungCloud(string appName)
        {
            if (appName.Contains("Samsung Gallery"))
            {
                Assert.AreEqual(gallery.GetGallerySyncInfo(), GetGalleryCloudSyncInfo());
            }
            else if (appName.Contains("Samsung Pass"))
            {
                Assert.AreEqual(pass.GetPassSyncInfo(), GetPassCloudSyncInfo());
            }
            else
            {
                Assert.IsTrue(GetLastSyncedText().Contains(GetCurrentDateAndTime()));
            }
        }

        [Then(@"the information of Last Synced ""(.*)"" and Sync info are updated")]
        public void ThenTheInformationOfLastSyncedAndSyncInfoAreUpdated(string app)
        {
            if (app.Contains("Samsung Gallery"))
            {
                GetLastSyncInformationApplications(gallery.GetGalleryStatusRefreshToolTip(), GetLastSyncedText());
            }
            else if (app.Contains("Samsung Pass"))
            {
                GetLastSyncInformationApplications(pass.GetPassStatusLastSync(), GetLastSyncedText());
            }
        }

        [Then(@"the status of the related toggle button in the ""(.*)"" app should be updated to ""(.*)""")]
        public void ThenTheStatusOfTheRelatedToggleButtonInTheAppShouldBeUpdatedTo(string app, string status)
        {
            string currestToogleStatus = string.Empty;
            switch (app)
            {
                case "Samsung Gallery":
                    gallery.ClickGallerySettingsButton();
                    currestToogleStatus = gallery.GetGalleryToggleState();
                    break;

                case "Samsung Notes":
                    notes.ClickNotesSettingsButton();
                    currestToogleStatus = notes.GetNotesToggleState();
                    break;

                case "Bluetooth":
                    Thread.Sleep(3000);
                    currestToogleStatus = runtime.GetSettingsRuntimeToggleState(app);
                    break;

                case "Wi-Fi":
                    Thread.Sleep(3000);
                    currestToogleStatus = runtime.GetSettingsRuntimeToggleState(app);
                    break;

                default:
                    break;
            }

            Assert.AreEqual(status == "OFF" ? "0" : "1", currestToogleStatus, "The switch button did not changed");
        }

        [Then(@"the status of the related toggle button in the Samsung Pass application should be updated to ""(.*)""")]
        public void ThenTheStatusOfTheRelatedToggleButtonInTheSamsungPassApplicationShouldBeUpdatedTo(string toggleStatus)
        {
            pass.ClickSamsungPassSettings();
            // Waiting because the Samsung Pass has a delay using two-way protocol
            Thread.Sleep(6000);
            Assert.AreEqual(toggleStatus == "OFF" ? "0" : "1", pass.GetPassToggleState(), "The switch button did not changed");
        }

        [Then(@"the ""(.*)"" application is updated for the same ""(.*)"" selection")]
        public void ThenTheApplicationIsUpdatedForTheSameSelection(string appName, string status)
        {
            if (appName == "Samsung Gallery")
            {
                Assert.IsTrue(gallery.VerifyGallerySyncOptions(status));
            }
            else if (appName == "Samsung Pass")
            {
                Assert.IsTrue(pass.VerifyPassSyncOptions(status));
            }
        }
        #endregion Then

        #region StepDefinition
        [StepDefinition(@"the ""(.*)"" sync now is runs")]
        public void WhenTheSyncNowIsRuns(string appName)
        {
            if (appName == "Bluetooth" | appName == "Wi-Fi")
            {
                runtime.ChangeToggleStatus(appName, "ON");
                runtime.PeformSync(appName);
            }
            else if (appName == "Samsung Pass")
            {
                pass.SyncNowPass();
            }
        }

        [StepDefinition(@"the ""(.*)"" app is opened")]
        public void WhenTheAppIsOpened(string app)
        {
            var appActions = new Dictionary<string, Action>
    {
        { "Samsung Gallery", () => gallery.VerifyGalleryAppIsOpened() },
        { "Wi-Fi", () => runtime.VerifyAppIsLaunched() },
        { "Windows Settings", () => settingPage.VerifySettingsAppIsOpened() },
        { "Samsung Account", () => account.VerifySamsungAccountsAppIsOpened() },
        { "Samsung Notes", () => notes.VerifyNotesAppIsOpened() },
        { "Samsung Pass", () => pass.VerifySamsungPassAppIsOpened() },
        { "Samsung Cloud", () => { VerifyAppIsLaunched(); } },
        { "Samsung Cloud Assistant", () => sca.VerifyAppIsLaunched() }
    };

            if (appActions.TryGetValue(app, out var action))
            {
                action();
            }
            else
            {
                throw new ArgumentException($"App '{app}' is not recognized.");
            }
        }

        [StepDefinition(@"the sync status is changed to ""(.*)""")]
        public void WhenTurnTheToggleTo(string status)
        {
            ChangeCloudToogleStatusOnAppSettings(status);
        }

        [StepDefinition(@"the toggle button status of ""(.*)"" is changed on Cloud app to ""(.*)""")]
        public void WhenTheToggleButtonStatusOfIsChangedOnCloudAppTo(string appName, string status)
        {
            ChangeCloudToogleStatusOnDashboard(appName, status);
        }
        #endregion StepDefinition
    }
}