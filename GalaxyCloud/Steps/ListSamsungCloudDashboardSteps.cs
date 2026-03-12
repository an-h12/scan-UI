// file="ListSamsungCloudDashboardSteps.cs" 

using GalaxyCloud.Page;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace GalaxyCloud.Steps
{
    /// <summary>
    /// This class is about the steps of the List Samsung Cloud Dashboard feature page
    /// </summary>
    [Binding]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Step methods should not be documented")]
    public class ListSamsungCloudDashboardSteps : SamsungCloudPage
    {
        private const string linkedAccountMessage = "Tap to set up syncing.";
        private readonly SamsungCloudPage cloud = new SamsungCloudPage();

        [Then(@"Samsung Gallery should display the account link status in the subtext below the app name")]
        public void ThenSamsungGalleryShouldDisplayTheAccountLinkStatusInTheSubtextBelowTheAppName()
        {
            Assert.AreEqual(linkedAccountMessage, VerifySubTextGallery());
        }

        [Then(@"""(.*)"" name should be displayed correctly on the ""(.*)"" application")]
        public void ThenNameShouldBeDisplayedCorrectlyOnTheList(string appTitleName, string appName)
        {
            Assert.AreEqual(appTitleName, GetAppName(appName));
        }

        [Then(@"the ""(.*)"" button is displayed")]
        public void ThenTheButtonIsDisplayed(string buttonName)
        {
            Assert.IsTrue(VerifyButtonIsDisplayed(buttonName));
        }

        [Then(@"the '([^']*)' icon is displayed")]
        public void ThenTheIconIsDisplayed(string iconName)
        {
            Assert.IsTrue(GetAppIcon(iconName));
        }

        [Then(@"the toggle buttons should be displayed on the '(.*)'")]
        public void ThenTheToggleButtonsShouldBeDisplayedOnThe(string appName)
        {
            Assert.IsTrue(cloud.VerifyToggleButtonIsDisplayed(appName));
        }
    }
}