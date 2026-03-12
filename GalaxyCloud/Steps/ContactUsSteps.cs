// file="ContactUsSteps.cs" 

using GalaxyCloud.Page;
using NUnit.Framework;
using System.Threading;
using TechTalk.SpecFlow;

namespace GalaxyCloud.Steps
{
    /// <summary>
    /// This class defines the steps for interacting with the Contact Us feature
    /// on the WelcomeScreen SCA Client Steps page.
    /// </summary>
    [Binding]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Step methods should not be documented")]
    public class ContactUsSteps : SamsungCloudPage
    {
        /// <summary>
        /// Common steps for Contact Us feature
        /// </summary>

        [When(@"the ""Contact us"" button is clicked")]
        public void WhenTheContactUsButtonIsClicked()
        {
            Thread.Sleep(1000);
            OpenConctacUsPopUp();
        }

        [Then(@"the popup should be displayed")]
        public void ThenThePopupShouldBeDisplayed()
        {
            Assert.IsTrue(VerifyThatContactUsPopIsDispplayed());
        }

        /// <summary>
        /// Scenario: Display pop-up on "Contact us" button click
        /// Feature: Contact Us - Feature page
        /// Test Case ID: @NCGP-T2331
        /// </summary>

        /// <summary>
        /// Scenario: Validate if the text displayed in the pop-up matches the specified documentation
        /// Feature: Contact Us - Feature page
        /// Test Case ID: @NCGP-T2333
        /// </summary>

        [Then(@"verify that the pop-up text matches with UI pattern")]
        public void ThenVerifyThatThePop_UpTextMatchesWithUIPattern()
        {
            Assert.IsTrue(VerifyContactUsPopUpText());
        }

        /// <summary>
        /// Scenario: Ensure that the pop-up can be reopened after being closed.
        /// Feature: Contact Us - Feature page
        /// Test Case ID: @NCGP-T2332
        /// </summary>

        [Given(@"the ""Contact us"" button is clicked")]
        public void GivenTheContactUsButtonIsClicked()
        {
            Thread.Sleep(1000);
            OpenConctacUsPopUp();
        }

        [Given(@"the popup should be displayed")]
        public void GivenThePopupShouldBeDisplayed()
        {
            VerifyThatContactUsPopIsDispplayed();
        }

        [Given(@"pop-up is closed")]
        public void GivenPop_UpIsClosed()
        {
            CloseContactUsPopUp();
        }

        [When(@"the ""Contact us"" button is clicked to reopen")]
        public void WhenTheContactUsButtonIsClickedToReopen()
        {
            Thread.Sleep(1000);
            OpenConctacUsPopUp();
        }

        [Then(@"the popup should be displayed after reopening")]
        public void ThenThePopupShouldBeDisplayedAfterReopening()
        {
            Assert.IsTrue(VerifyThatContactUsPopIsDispplayed());
        }
    }
}
