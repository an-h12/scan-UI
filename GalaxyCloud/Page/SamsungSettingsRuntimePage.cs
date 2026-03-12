// file="SamsungSettingsRuntimePage.cs" 

using System;
using System.Threading;
using GalaxyCloud.Helpers;

namespace GalaxyCloud.Page
{
    /// <summary>
    /// This class is about the interactions on Samsung Bluetooth Sync application
    /// </summary>
    public class SamsungSettingsRuntime : General
    {
        private const string btSyncID = "btsync";
        private const string wifiSyncID = "wifisync";
        private const string btSyncToggleID = "btSyncBtn";
        private const string wifiSyncToggleID = "wifiSyncBtn";
        private const string toogleStateAttribute = "Toggle.ToggleState";
        private const string titleBarID = "TitleBar";
        private const string nameAtribute = "Name";
        private const string appName = "Samsung Settings Runtime";
        private const string wifiSupported = "Supported IsSyncEnableForWIFI";

        /// <summary>
        /// This method checks if the application has been opened, it searches in the page if there is any item with the parameter in appName.
        /// FindElementByName is to locate an element, takes a parameter of string which is a value of NAME attribute and it returns a object to FindElementByName() method.
        /// The appName is a private variable set above, in this case is a name of sample application
        /// </summary>
        public void VerifyAppIsLaunched()
        {
            WaitAtributeTextIsDisplayedByID(Hooks.sessionSettingsRuntime, 5, titleBarID, nameAtribute, appName);
            WaitElementTextIsDisplayedByName(Hooks.sessionSettingsRuntime, 5, wifiSupported);
        }

        /// <summary>
        /// This method performs the sync process on the Samsung Settings Runtime
        /// </summary>
        /// <param name="appName">This is the app name that should be peformed the sync process</param>
        public void PeformSync(string appName)
        {
            if (appName == "Bluetooth")
            {
                FindElementByID(Hooks.sessionSettingsRuntime, btSyncID).Click();
            }
            else if (appName == "Wi-Fi")
            {
                FindElementByID(Hooks.sessionSettingsRuntime, wifiSyncID).Click();
            }
        }

        /// <summary>
        /// This method changes the toggle status of an app on the Samsung Settings Runtime
        /// </summary>
        /// <param name="appName">The app name for which the toggle status should be changed</param>
        /// <param name="status">The status to be applied to the toggle button (ON/OFF)</param>
        public void ChangeToggleStatus(string appName, string status)
        {
            Thread.Sleep(2000);
            string toggleState = GetSettingsRuntimeToggleState(appName);

            if ((toggleState.Contains("1") && status.Equals("OFF") && (appName == "Bluetooth" || appName == "Wi-Fi")) ||
    (toggleState.Contains("0") && status.Equals("ON") && (appName == "Bluetooth" || appName == "Wi-Fi")))
            {
                ClickSettingsRuntime(appName);
            }
        }

        /// <summary>
        /// This method performs a click on the Samsung Settings Runtime toggle button.
        /// </summary>
        /// <param name="appName">The app for which the toggle button should be clicked</param>
        public void ClickSettingsRuntime(string appName)
        {
            string toggleID = appName.Equals("Bluetooth") ? btSyncToggleID :
                              appName.Equals("Wi-Fi") ? wifiSyncToggleID :
                              throw new ArgumentException("Unsupported app name", nameof(appName));

            FindElementByID(Hooks.sessionSettingsRuntime, toggleID).Click();
        }

        /// <summary>
        /// This method gets the toggle button's status on the Samsung Settings Runtime application.
        /// </summary>
        /// <param name="appName">The app for which the toggle button's status should be retrieved</param>
        /// <returns>Returns the status of the app on the Samsung Settings Runtime</returns>
        public string GetSettingsRuntimeToggleState(string appName)
        {
            string toggleID = appName.Equals("Bluetooth") ? btSyncToggleID :
                  appName.Equals("Wi-Fi") ? wifiSyncToggleID :
                  throw new ArgumentException("Unsupported app name", nameof(appName));

            return GetAttributeByID(Hooks.sessionSettingsRuntime, toggleID, toogleStateAttribute);
        }
    }
}