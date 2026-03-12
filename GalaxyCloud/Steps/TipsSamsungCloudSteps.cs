// file="TipsSamsungCloudSteps.cs" 

using GalaxyCloud.Page;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace GalaxyCloud.Steps
{
    /// <summary>
    /// This class is about the steps of the Tips feature page
    /// </summary>
    [Binding]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Step methods should not be documented")]
    public class TipsSamsungCloudSteps : SamsungCloudPage
    {
        [Then(@"the previous button is not displayed on the first tip page")]
        public void ThenThePreviousButtonIsNotDisplayedOnTheFirstTipPage()
        {
            Assert.IsTrue(VerifyTipsPreviousButtonIsNotDisplayed());
        }

        [Then(@"the next button is not displayed on the last tip page")]
        public void ThenTheNextButtonIsNotDisplayedOnTheLastTipPage()
        {
            Assert.IsTrue(VerifyTipsNextButtonIsNotDisplayed());
        }

        [Then(@"the current page and total pages are displayed")]
        public void ThenTheCurrentPageAndTotalPagesAreDisplayed()
        {
            Assert.IsTrue(VerifyCurrentAndTotalPagedWhenNavigating());
        }

        [Then(@"the title and explanatory text are according to the UI")]
        public void ThenTheTitleAndExplanatoryTextAreAccordingToTheUI()
        {
            Assert.IsTrue(ValidateTheTipsText());
        }
    }
}
