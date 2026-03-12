// file="SamsungCloudDashboardSteps.cs" 

using GalaxyCloud.Page;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace GalaxyCloud.Steps
{
    /// <summary>
    /// This class is about the steps of the Samsung Cloud Dashboard feature page
    /// </summary>
    [Binding]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Step methods should not be documented")]
    public class SamsungCloudDashboardSteps : SamsungCloudPage
    {
        [Then(@"the sync using is displayed on compatible devices with a sim card")]
        public void ThenTheSyncUsingIsDisplayedOnCompatibleDevicesWithASimCard()
        {
            Assert.AreEqual(VerifyNetworkInterfaces(), VerifySyncUsingIsDisplayed());
        }
    }
}