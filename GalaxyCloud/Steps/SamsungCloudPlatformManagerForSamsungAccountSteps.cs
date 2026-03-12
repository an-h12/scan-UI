// file="SamsungCloudPlatformManagerForSamsungAccountSteps.cs" 

using GalaxyCloud.Page;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace GalaxyCloud.Steps
{
    /// <summary>
    /// This class is about the steps of the Samsung Cloud Platform Manager For Samsung Account Steps feature page
    /// </summary>
    [Binding]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Step methods should not be documented")]
    public class SamsungCloudPlatformManagerForSamsungAccountSteps : SamsungCloudPage
    {
        [Then(@"the ""(.*)"" session should be shown")]
        public void ThenTheSessionShouldBeShown(string appName)
        {
            Assert.IsTrue(VerifyElementIsEnabled(appName));
        }
    }
}