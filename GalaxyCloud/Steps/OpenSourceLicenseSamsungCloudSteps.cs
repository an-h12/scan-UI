// file="OpenSourceLicenseSamsungCloudSteps.cs" 

using GalaxyCloud.Page;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace GalaxyCloud.Steps
{
    /// <summary>
    /// This class is about the steps of the Open Source License feature page
    /// </summary>
    [Binding]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Step methods should not be documented")]
    public class OpenSourceLicenseSamsungCloudSteps : SamsungCloudPage
    {
        [Then(@"the Open Source License screen is shown")]
        public void ThenTheOpenSourceLicenseScreenIsShown()
        {
            Assert.IsTrue(VerifyOpenSourcePageIsDisplayed());
        }

        [Then(@"the user is redirected to the settings screen")]
        public void ThenTheUserIsRedirectedToTheSettingsScreen()
        {
            Assert.IsTrue(VerifySettingsTitle());
        }
    }
}