// file="WelcomeScreenSamsungCloudSteps.cs" 

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
    public class WelcomeScreenSamsungCloudSteps : SamsungCloudPage
    {
        #region Then

        [Then(@"""([^""]*)"" button is displayed on the main screen")]
        public void ThenButtonIsDisplayedOnTheMainScreen(string settings)
        {
            Assert.IsTrue(VerifySettingsButtonIsDisplayed(settings));
        }

        [Then(@"the title ""([^""]*)"" is displayed")]
        public void ThenTheTitleIsDisplayed(string samsungCloudTitle)
        {
            Assert.IsTrue(VerifyTitlePageIsDisplayed(samsungCloudTitle));
        }

        [Then(@"the Tips icon is available on the main page")]
        public void ThenTheTipsIconIsAvailableOnTheMainPage()
        {
            Assert.IsTrue(VerifyTipsIconIsDisplayed());
        }

        #endregion Then
    }
}