// file="RadiBbuttonForSyncUsingWiFiOnlyOrWiFiOrMobileDataSteps.cs" 

using GalaxyCloud.Page;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace GalaxyCloud.Steps
{
    /// <summary>
    /// This class is about the steps of the Radio Button For Sync Using WiFi Only Or WiFi Or Mobile Data Steps feature page
    /// </summary>
    [Binding]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Step methods should not be documented")]
    public class RadiBbuttonForSyncUsingWi_FiOnlyOrWi_FiOrMobileDataSteps : SamsungCloudPage
    {
        private readonly string toggleStatusON = "ON";

        [Then(@"the Wi-Fi only or Wi-Fi, Ethernet and mobile data options are enabled")]
        public void ThenTheWiFIOnlyOrOrOptionsAreEnabled()
        {
            ChangeCloudToogleStatusOnAppSettings(toggleStatusON);
            Assert.IsTrue(VerifySyncUsingOptionsIsEnabled());
        }

        [StepDefinition(@"the ""(.*)"" settings screen is accessed")]
        public void WhenTheSettingsScreenIsAccessed(string appName)
        {
            WaitTheSubtextIsLoading(appName);
            ClickAppButton(appName);
            VerifyAppSettingsIsOpened(appName);
        }
    }
}

