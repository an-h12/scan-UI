// file="SettingsPage.cs"

using GalaxyCloud.Helpers;
using OpenQA.Selenium.Interactions;

namespace GalaxyCloud.Page
{
    /// <summary>
    ///  This class is about the interactions on Windows settings
    /// </summary>
    public class WindowsSettingsPage : General
    {
        private const string settingsAppTitle = "SettingsAppTitle";
        private const string timeAndLanguageID = "SettingsPageGroupTimeRegion";
        private const string regionID = "SettingsPageTimeRegionRegion";
        private const string currentFormatID = "SystemSettings_Region_RegionalFormat_ComboBox";
        private const string currentShortDate = "SystemSettings_Region_ShortDateStatus_ValueTextBlock";

        /// <summary>
        /// This method verify if the Windoows settings page is opened
        /// </summary>
        public void VerifySettingsAppIsOpened()
        {
            FindElementByID(Hooks.sessionSettings, settingsAppTitle);
        }

        /// <summary>
        /// This method changes the format of the region
        /// </summary>
        /// <param name="region">This parameter is the region that should be applied</param>
        public void ChangeFormatCurrentRegion(string region)
        {
            FindElementByID(Hooks.sessionSettings, timeAndLanguageID).Click();
            FindElementByID(Hooks.sessionSettings, regionID).Click();
            FindElementByID(Hooks.sessionSettings, currentFormatID).Click();
            Actions act = new Actions(Hooks.sessionSettings);
            act.SendKeys(region).Perform();
            act.Click().Perform();
        }

        /// <summary>
        /// This method gets the Windows current date
        /// </summary>
        /// <returns>Returns the current date on a string</returns>
        public string GetSettingCurrentDate()
        {
            return FindElementByID(Hooks.sessionSettings, currentShortDate).Text;
        }
    }
}
