// file="NotesAppPage.cs" 

using System;
using System.Threading;
using GalaxyCloud.Helpers;

namespace GalaxyCloud.Page
{
    /// <summary>
    /// This class is about the interactions on Samsung Pass application
    /// </summary>
    public class SamsungPassPage : General
    {
        private const string passwordFieldID = "PasswordField_4";
        private const string optionsButtonID = "MoreButton";
        private const string settingsName = "Settings";
        private const string lastSyncedID = "SyncNowSubText";
        private const string windowsSecurityName = "Windows Security";
        private const string passSyncToggleCN = "ToggleSwitch";
        private const string synNowID = "Sync now";
        private const string syncNowTitleID = "SyncNowTitle";
        private const string syncNowSubTextID = "SyncNowSubText";
        private const string wifiOnlyID = "SettingsSyncUsingWifiOnlyRadioButton";
        private const string mobileDataID = "SettingsSyncUsingWifiOrDataRadioButton";
        private const string wifiOnlyText = "Wi-Fi and Ethernet only";
        private const string mobileDataText = "Wi-Fi, Ethernet, and mobile data";
        private const string titleID = "SettingsPageTitle";
        private readonly string windowsHello = Environment.GetEnvironmentVariable("passWindowsHello");
        private readonly string toogleStateAttribute = "Toggle.ToggleState";

        /// <summary>
        /// This method verifies is the Samsung Account application is oppened
        /// </summary>
        public void VerifySamsungPassAppIsOpened()
        {
            WaitElementTextIsDisplayedByName(Hooks.sessionPass, 10, windowsSecurityName);
            WindowsHelloPassword();
        }

        /// <summary>
        /// This method sends the password to the Windows Hello field
        /// </summary>
        public void WindowsHelloPassword()
        {
            FindElementByID(Hooks.sessionPass, passwordFieldID).SendKeys(windowsHello);
        }

        /// <summary>
        /// This method performs click on settings option on Samsung Pass
        /// </summary>
        public void ClickSamsungPassSettings()
        {
            FindElementByID(Hooks.sessionPass, optionsButtonID).Click();
            FindElementByName(Hooks.sessionPass, settingsName).Click();
            WaitAtributeTextIsDisplayedByID(Hooks.sessionPass, 10, titleID, "Name", "Settings");
        }

        /// <summary>
        /// This method gets the last synced information on the app settings page
        /// </summary>
        /// <returns>Returns the last synced information in a string</returns>
        public string GetPassStatusLastSync()
        {
            ClickSamsungPassSettings();
            return FindElementByID(Hooks.sessionPass, lastSyncedID).Text;
        }

        /// <summary>
        /// This method gets the Samsung Gallery toggle button status
        /// </summary>
        /// <returns>Returns a string with the toogle button status</returns>
        public string GetPassToggleState()
        {
            return GetAttributeByClassName(Hooks.sessionPass, passSyncToggleCN, toogleStateAttribute);
        }

        /// <summary>
        /// This method performs click action on the toggle button
        /// </summary>
        public void ClickPassSyncToggle()
        {
            FindElementByClassName(Hooks.sessionPass, passSyncToggleCN).Click();
            // Waiting because the Samsung Pass has a delay using two-way protocol
            Thread.Sleep(6000);
        }

        /// <summary>
        /// This method changes the toggle button status
        /// </summary>
        /// <param name="status">This parameter is the status that should be applied</param>
        public void ChangePassToggleStatus(string status)
        {
            ClickSamsungPassSettings();
            if (GetPassToggleState().Contains("1") && status.Contains("OFF"))
            {
                ClickPassSyncToggle();
            }
            else if (GetPassToggleState().Contains("0") && status.Contains("ON"))
            {
                ClickPassSyncToggle();
            }
        }

        /// <summary>
        /// This method performs the sync process
        /// </summary>
        public void SyncNowPass()
        {
            ClickSamsungPassSettings();
            FindElementByID(Hooks.sessionPass, syncNowTitleID).Click();
            WaitAtributeTextIsDisplayedByID(Hooks.sessionPass, 20, syncNowTitleID, "Name", synNowID);
        }

        /// <summary>
        /// This method gets the sync information on the Samsung Pass
        /// </summary>
        /// <returns>Returns the string with the sync informations</returns>
        public string GetPassSyncInfo()
        {
            return GetIntInString(RegexLastSynced(GetAttributeByID(Hooks.sessionPass, syncNowSubTextID, "Name")));
        }

        /// <summary>
        /// This method changes the toggle status on Samsung Pass in the dashboard page
        /// </summary>
        /// <param name="status">This parameter is the toggle status that should be applied</param>
        public void ChangeSyncUsing(string status)
        {
            ChangePassToggleStatus("ON");

            if (status.Contains(wifiOnlyText))
            {
                FindElementByID(Hooks.sessionPass, wifiOnlyID).Click();
            }
            else if (status.Contains(mobileDataText))
            {
                FindElementByID(Hooks.sessionPass, mobileDataID).Click();
            }
        }

        /// <summary>
        /// This method verifies the sync using options
        /// </summary>
        /// <param name="status">This parameter that should be verified</param>
        /// <returns>Returns the boolean, true if is selected else false</returns>
        public bool VerifyPassSyncOptions(string status)
        {
            ClickSamsungPassSettings();

            if (status.Contains(wifiOnlyText))
            {
                // Waiting because the Samsung Pass has a delay using two-way protocol
                Thread.Sleep(6000);
                return IsSelectedByID(Hooks.sessionPass, wifiOnlyID);
            }
            else if (status.Contains(mobileDataText))
            {
                // Waiting because the Samsung Pass has a delay using two-way protocol
                Thread.Sleep(6000);
                return IsSelectedByID(Hooks.sessionPass, mobileDataID);
            }
            else
            {
                return false;
            }
        }
    }
}