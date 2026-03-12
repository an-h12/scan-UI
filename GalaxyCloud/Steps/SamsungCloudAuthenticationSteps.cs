// file="SamsungCloudAuthenticationSteps.cs" 

using GalaxyCloud.Page;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace GalaxyCloud.Steps
{
    /// <summary>
    /// This class contains the step definitions for Samsung Cloud authentication scenarios
    /// </summary>
    [Binding]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Step methods should not be documented")]
    public class SamsungCloudAuthenticationSteps : SamsungCloudAuthenticationPage
    {
        #region Step Definitions - Following Gherkin Order

        [Given(@"the ""([^""]*)"" app is launched")]
        public void GivenTheAppIsLaunched(string appName)
        {
            // The app is already launched by the hooks, so we just need to verify it's displayed
            // This step is handled by the BeforeScenario hook in Hooks.cs
        }

        [Then(@"""([^""]*)"" text must be displayed")]
        public void ThenTextMustBeDisplayed(string text)
        {
            if (text == "Keep your data safe and synced on all your devices using Samsung Cloud.")
            {
                Assert.IsTrue(IsWelcomeTextDisplayed(), "Welcome text is not displayed");
            }
            else if (text == "Samsung Cloud")
            {
                Assert.IsTrue(IsSamsungCloudTextDisplayed(), "Samsung Cloud text is not displayed");
            }
        }

        [Then(@"""([^""]*)"" button must be displayed")]
        public void ThenButtonMustBeDisplayed(string buttonName)
        {
            if (buttonName == "Get started")
            {
                Assert.IsTrue(IsGetStartedButtonDisplayed(), "Get Started button is not displayed");
            }
        }

        [When(@"the ""([^""]*)"" button on welcome screen is clicked")]
        public void WhenTheButtonOnWelcomeScreenIsClicked(string buttonName)
        {
            if (buttonName == "Get started")
            {
                ClickGetStartedButton();
            }
        }

        [Then(@"([^""]*) login screen must be displayed")]
        public void ThenLoginScreenMustBeDisplayed(string accountType)
        {
            IsSamsungAccountLoginScreenDisplayed();
            EnterSamsungAccountCredentials();
        }

        // [When(@"([^""]*) credentials are entered")]
        // public void WhenCredentialsAreEntered(string accountType)
        // {
        //     if (accountType == "Samsung Account")
        //     {
        //         EnterSamsungAccountCredentials();
        //     }
        // }

        [Then(@"([^""]*) screen displays ""([^""]*)"" text")]
        public void ThenScreenDisplaysText(string screenName, string text)
        {
            if (screenName == "Samsung Cloud" && text == "Samsung Cloud")
            {
                Assert.IsTrue(IsSamsungCloudTextDisplayed(), "Samsung Cloud text is not displayed");
            }
        }

        #endregion
    }
}
