// file="EDPSamsungCloudSteps.cs" 

using GalaxyCloud.Page;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace GalaxyCloud.Steps
{
    /// <summary>
    /// This class is about the steps of the EDP Cloud Client Steps feature page
    /// </summary>
    [Binding]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Step methods should not be documented")]
    public class EDPSamsungCloudSteps : SamsungCloudPage
    {
        #region Then

        [Then(@"the encryption status is checked")]
        public void ThenTheEncryptionStatusIsChecked()
        {
            Assert.IsTrue(VerifyStatusEDPIsDisplayed());
        }

        [Then(@"the string in the submenu is displayed depending on the status")]
        public void ThenTheStringInTheSubmenuIsDisplayedDependingOnTheStatus()
        {
            Assert.IsTrue(VerifyDescriptionEDPIsDisplayed());
        }

        [Then(@"the EDP information is available as ""([^""]*)""")]
        public void ThenTheEDPInformationIsAvailableAs(string informationEDP)
        {
            Assert.IsTrue(VerifyInformationEDPIsDisplayed(informationEDP));
        }
        #endregion Then
    }
}
