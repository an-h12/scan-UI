// file="SettingsSamsungCloudSteps.cs" 

using GalaxyCloud.Page;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace GalaxyCloud.Steps
{
    /// <summary>
    /// This class is about the steps of the WelcomeScreen sca Client Steps feature page
    /// </summary>
    [Binding]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Step methods should not be documented")]
    public class SettingsSamsungCloudSteps : SamsungCloudPage
    {
        [Then(@"return to the ""([^""]*)"" main screen")]
        public void ThenReturnToTheMainScreen(string samsungCloudTitle)
        {
            ClickBackButton();
            Assert.IsTrue(VerifyTitlePageIsDisplayed(samsungCloudTitle));
        }

        [Then(@"Samsung Cloud version should be displayed in main screen")]
        public void ThenSamsungCloudVersionShouldBeDisplayedInMainScreen()
        {
            Assert.IsTrue(VerifySamsungCloudVersionOnSettings());
        }

        [Then(@"the Open Source button is shown on the settings screen")]
        public void ThenTheOpenSourceButtonIsShownOnTheSettingsScreen()
        {
            Assert.IsTrue(VerifyButtonOpenSourcePageIsDisplayed());
        }

        [Then(@"the user email address should be displayed on the settings screen")]
        public void ThenTheUserEmailAddressShouldBeDisplayedOnTheSettingsScreen()
        {
            Assert.IsTrue(VerifyAcountIsDisplayedOnSettings());
        }
    }
}