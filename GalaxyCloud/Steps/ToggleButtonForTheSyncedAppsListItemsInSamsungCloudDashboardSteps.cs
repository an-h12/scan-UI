// file="ToggleButtonForTheSyncedAppsListItemsInSamsungCloudDashboardSteps.cs" 

using GalaxyCloud.Page;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace GalaxyCloud.Steps
{
    /// <summary>
    /// This class is about the steps of the Toggle Button For The Synced Apps List Items In Samsung Cloud Dashboard Steps feature page
    /// </summary>
    [Binding]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Step methods should not be documented")]
    public class ToggleButtonForTheSyncedAppsListItemsInSamsungCloudDashboard : SamsungCloudPage
    {
        public const string galleryLinkedSubtext = "Sync with OneDrive";
        private const string toggleSwitchID = "ToggleIsSync";
        #region When
        [When(@"the Samsung Gallery was linked with OneDrive")]
        public void WhenTheSamsungGalleryWasLinkedWithOneDrive()
        {
            Assert.AreEqual(galleryLinkedSubtext, VerifySubTextGalleryLinkedOneDrive());
        }
        #endregion When
        #region Then
        [Then(@"the app toggle button must be displayed")]
        public void ThenTheAppToggleButtonMustBeDisplayed()
        {
            Assert.IsTrue(GetCloudItemListButtonDashboard("Samsung Gallery").FindElementByAccessibilityId(toggleSwitchID).Displayed);
        }
        #endregion Then
    }
}