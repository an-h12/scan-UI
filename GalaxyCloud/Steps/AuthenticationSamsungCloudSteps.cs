// file="AuthenticationSamsungCloudSteps.cs" 

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
    public class AuthenticationSamsungCloudSteps : SamsungCloudPage
    {
        [Given(@"the initial page is displayed with the ""([^""]*)"" button")]
        public void GivenTheInitialPageIsDisplayedWithTheButton(string getStartedButton)
        {
            Assert.IsTrue(GetStartedButtonIsDisplayedByName(getStartedButton));
        }

        [Then(@"it is possible to sign in of the account")]
        public void ThenItIsPossibleToSignInOfTheAccount()
        {
            Assert.IsTrue(SignInSamsungCloud());
        }
    }
}
