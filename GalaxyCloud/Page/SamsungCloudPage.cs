// file="CloudSmokePage.cs" 

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using GalaxyCloud.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.UI;

namespace GalaxyCloud.Page
{
    /// <summary>
    /// This class is about the interactions on Cloud sample
    /// </summary>
    public class SamsungCloudPage : General
    {
        private const string aboutSamsungAccountTitleName = "About Samsung account";
        private const string appName = "Sample.SamsungAccount";
        private const string appSubtitleListID = "ContentScrollViewer";
        private const string backButtonID = "BackButton";
        private const string openSourceButtonID = "appOpenSourceLicenseButton";
        private const string buttonMoreInformationID = "ButtonMoreInformation";
        private const string commandBarID = "NavigateCommadBar";
        private const string SettingsButtonID = "SettingsContentText";
        private const string countryComboboxID = "AccountCountryCodeCombobox";
        private const string dataSubMenuLabelsID = "DataSubMenuLabels";
        private const string galleryName = "Samsung Gallery";
        private const string iconID = "Ellipse";
        private const string lastSyncedID = "DataSubMenuLastSynced";
        private const string mobileDataID = "WiFiOrMobileData";
        private const string moreInformationShadowButtonID = "MoreInformationShadowButton";
        private const string samsungCloudDataListSubtitleID = "DataListSubtitle";
        private const string samsungCloudIsSyncedID = "IsSynced";
        private const string samsungCloudSubTextInfoID = "SubTextInfo";
        private const string samsungCloudTitleName = "Samsung Cloud";
        private const string samsungNotesTitleName = "Samsung Notes";
        private const string samsungSettingsTitleName = "Settings";
        private const string subTextInfoID = "SubTextTextBlock";
        private const string syncInfoID = "DataSyncInformation";
        private const string syncInfoRegex = @"(\d*) (paired Samsung devices)|(\d*) (photo)";
        private const string syncNowID = "SyncingButton";
        private const string textBlockCN = "TextBlock";
        private const string toastWithoutInternetConnectionID = "ToastText";
        private const string toggleAttribute = "Toggle.ToggleState";
        private const string toggleCN = "ToggleSwitch";
        private const string toggleID = "ToggleIsSync";
        private const string wifiOnlyID = "WiFiOnly";
        private const string containerClassName = "NamedContainerAutomationPeer";
        private const string toggleSwitchID = "ToggleIsSync";
        private const string namedContainerAutomationPeerCN = "NamedContainerAutomationPeer";
        private const string errorListingApplicationsTextID = "ErrorListingApplicationsText";
        private const string samsungCloudName = "Samsung Cloud";
        private const string popUpCN = "Popup";
        private const string textExpected = "Loading...";
        private const string toggleOn = "ON";
        private const string moreInformationName = "More information";
        private const string syncingButtonID = "SyncingButton";
        private const string samsungGalleryName = "Samsung Gallery";
        private const string syncText = "Sync now";
        private const string syncingText = "Syncing…";
        private const string name = "Name";
        private const string userAccoountID = "userAccount";
        private const string buttonSignOutID = "appSignOutButton";
        private const string buttonContactUsID = "btnContactUs";
        private const string buttonOpenSourceLicenseID = "appOpenSourceLicenseButton";
        private const string signOutExternalID = "signOut";
        private const string signInWelcomePageID = "appSingInButton";
        private const string signButtonID = "idSignInButton";
        private const string sansungCloudEmailID = "iptLgnPlnID";
        private const string samsungAccountPassID = "iptLgnPlnPD";
        private const string appVersionNumberID = "appVersion";
        private const string appVersionLabelID = "appVersionLabel";
        private const string descriptionEDPOnID = "SecurityDescriptionOn";
        private const string descriptionEDPOffID = "SecurityDescriptionOff";
        private const string statusEDPID = "SecurityStatus";
        private const string descriptionStatusEDPID = "using Enhanced data protection to secure your Samsung Cloud data.";
        private const string pageNumberID = "txbTipsPageNumber";
        private const string tipIconID = "imgTipsIcon";
        private const string tipsTitlePageTxt = "Tips";
        private const string nextButtonTipsID = "btnTipsNext";
        private const string previousButtonTipsID = "btnTipsPrevious";
        private const string pagePattern = @"^\d+\s*/\s*\d+$";
        private const string tipsTitleID = "txtTipsSubjectTitle";
        private const string tipsContentID = "txtTipsSubjectContent";
        private const string page01TitleText = "What is Samsung Cloud?";
        private const string page02TitleText = "Keep your data safe";
        private const string page01ExplanatoryText = "Samsung Cloud saves your data in the cloud and syncs it with all of your Galaxy devices. Your data is safe even if you lose your phone, and it's easy to access your data on a new device.";
        private const string page02ExplanatoryText = "From notes to images and videos, your data is safe in the cloud and synced with all your Galaxy devices so you can always find it.";
        // private const string flipViewItemCN = "FlipViewItem";
        // private const string scrollViewerCN = "TextBlock";
        private const string pageContentOpenSourceID = "OpenSourceLicenseContent";
        //private const string btnContactUsID = "btnContactUs";
        private const string popupTitleName = "A log file may help";
        private const string popupDescriptionID = "contactUsDescription";
        private const string popupHyperLinkID = "contactUsHyperlink";
        private const string cancelButtonID = "OneUIPrimaryActionButton";
        private readonly string emailCloudAccount = Environment.GetEnvironmentVariable("SamsungCloudEmailAutomation");
        private readonly string password = Environment.GetEnvironmentVariable("password");

        /// <summary>
        /// This method checks if the application has been opened, it searches in the page if there is any item with the parameter in appName.
        /// FindElementByName is to locate an element, takes a parameter of string which is a value of NAME attribute and it returns a object to FindElementByName() method.
        /// The appName is a private variable set above, in this case is a name of sample application
        /// </summary>
        public void VerifyAppIsLaunched()
        {

            VerifyErroList();

            // Khởi tạo DefaultWait với kiểu cụ thể là WindowsDriver
            // Điều này giúp biến 'driver' trong vòng lặp bên dưới được hiểu ngay là WindowsDriver
            var wait = new DefaultWait<WindowsDriver<WindowsElement>>(Hooks.sessionSamsungCloud);

            // Cấu hình Wait (Vì DefaultWait là bản gốc, cần set các thuộc tính thủ công)
            wait.Timeout = TimeSpan.FromSeconds(20);
            wait.PollingInterval = TimeSpan.FromMilliseconds(500);
            wait.Message = $"App không load xong tiêu đề '{samsungCloudTitleName}' sau 20s.";

            wait.IgnoreExceptionTypes(
            typeof(NoSuchElementException),
            typeof(StaleElementReferenceException)
            );

            wait.Until(driver =>
            {
            // Ở đây 'driver' đã là WindowsDriver có thể gọi trực tiếp hàm này:
            var element = driver.FindElementByAccessibilityId(commandBarID);

            return element.Displayed && element.Text.Contains(samsungCloudTitleName);
            });

        }

        /// <summary>
        /// This method checks if the application has been opened, it searches the page if there is any item with the parameter in tittle.
        /// FindElementByName is to locate an element, takes a parameter of string which is a value of NAME attribute and it returns a object to FindElementByName() method.
        /// </summary>
        /// <param name="title">This parameter is the title name off application on the Cloud sample</param>
        public void VerifyCloudTitle(string title)
        {
            FindElementByName(Hooks.sessionSamsungCloud, title);
        }

        /// <summary>
        /// This method check if the application set on the parameter appName is displayed in the list of applications
        /// With the FindElementsByClassName method, it searches all items paired with the "containerClassName" parameter,
        /// after that with the foreach it checks if there is any item with the appName defined by the parameter.
        /// </summary>
        /// <param name="appName">This parameter is the name of the application that wants to search in the list</param>
        /// <returns>If the application is displayed on the list, the return is true, but the if the match does not occur return false</returns>
        public bool VerifyAppItemListIsDisplayed(string appName)
        {
            ReadOnlyCollection<WindowsElement> appList = FindElementsByID(Hooks.sessionSamsungCloud, commandBarID);
            foreach (WindowsElement item in appList)
            {
                if (item.Text.Contains(appName))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// This method gets the text of the component set on the parameter in the Cloud application list.
        /// With the FindElementsByClassName method, it searches all items paired with the "containerClassName" parameter,
        /// after that with the foreach it checks if there is any item equal to the string passed on the parameter.
        /// </summary>
        /// <param name="containedAppName">This parameter is the name of the application that wants to search in the list</param>
        /// <returns>If the text matched with the parameter, the return is the text of the parameter set, but the if the match does not occur return null</returns>
        public string GetAppName(string containedAppName)
        {
            ReadOnlyCollection<WindowsElement> appList = FindElementsByClassName(Hooks.sessionSamsungCloud, containerClassName);

            foreach (var appItem in appList)
            {
                if (appItem.Text == containedAppName)
                {
                    var itemToValidate = appItem.FindElementByClassName(textBlockCN).GetAttribute(name);

                    if (itemToValidate != null)
                    {
                        return itemToValidate;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// This method gets the icon of the component in the Cloud application list.
        /// </summary>
        /// <param name="containedAppName">This parameter is the name of the application that wants to search in the list</param>
        /// <returns>If the icon image exists and is enabled, returns true</returns>
        public bool GetAppIcon(string containedAppName)
        {
            ReadOnlyCollection<WindowsElement> appList = FindElementsByClassName(Hooks.sessionSamsungCloud, containerClassName);

            foreach (var appItem in appList)
            {
                if (appItem.Text == containedAppName)
                {
                    var itemToValidate = appItem.FindElementByClassName("Image");

                    if (itemToValidate != null)
                    {
                        return itemToValidate.Enabled;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// This method gets the text in Samsung Gallery app from Cloud sample app list.
        /// </summary>
        /// <returns>This method returns the text in the subtext on a string</returns>
        public string VerifySubTextGallery()
        {
            WaitTheSubtextIsLoading(appName);
            return GetTextByID(Hooks.sessionSamsungCloud, subTextInfoID);
        }

        /// <summary>
        /// This method gets the text in Samsung Gallery app from Cloud sample app list.
        /// </summary>
        /// <returns>This method returns the text in the subtext on a string</returns>
        public string VerifySubTextGalleryLinkedOneDrive()
        {
            WaitTheSubtextIsLoading(samsungGalleryName);
            return GetCloudItemListButtonDashboard(samsungGalleryName).FindElementByAccessibilityId(subTextInfoID).Text;
        }

        /// <summary>
        /// This method verify if the component set by parameter element NAME is ENABLED.
        /// </summary>
        /// <param name="elementName">This parameter is the element name of the method will be verify if is enabled. The parameter should be a element by NAME.</param>
        /// <returns>This method returns true or false, true if the status is enabled and false if not.</returns>
        public bool VerifyElementIsEnabled(string elementName)
        {
            return IsEnabledByName(Hooks.sessionSamsungCloud, elementName);
        }

        /// <summary>
        /// This method verify if the component set by parameter is element ID is ENABLED.
        /// </summary>
        /// <param name="elementID">This parameter is the element name of the method will be verify if is enabled. The parameter should be a element by ID.</param>
        /// <returns>This method returns true or false, true if the status is enabled and false if not.</returns>
        public bool VerifyElementIsEnabledByID(string elementID)
        {
            return IsEnabledByID(Hooks.sessionSamsungCloud, elementID);
        }

        /// <summary>
        /// This method verify if the component set by parameter element NAME is DISPLAYED.
        /// </summary>
        /// <param name="elementName">This parameter is the element name of the method will be verify if is displayed. The parameter should be a element by NAME.</param>
        public void VerifyElementIsDisplayed(string elementName)
        {
            IsDisplayedByName(Hooks.sessionSamsungCloud, elementName);
        }

        /// <summary>
        /// This method waits until main page text is displayed
        /// </summary>
        /// <param name="elementID">This parameter is the element name of the method will be verify if is enabled. The parameter should be a element by ID.</param>
        /// <param name="text">This parameter is the element text of the method will be verify if is enabled. The parameter should be a element by NAME.</param>
        /// <returns>This method returns true or false, true if the element is displayed and false if not.</returns>
        public bool VerifyElementIsDisplayed(string elementID, string text)
        {
            try
            {
                WaitAtributeTextIsDisplayedByID(Hooks.sessionSamsungCloud, 5, elementID, "Name", text);
                return true;
            }
            catch (WebDriverException)
            {
                return false;
            }
        }

        /// <summary>
        /// This method verify if the component set by parameter element ID is DISPLAYED.
        /// </summary>
        /// <param name="elementID">This parameter is the element name of the method will be verify if is enabled. The parameter should be a element by NAME.</param>
        /// <returns>This method returns true or false, true if the status is displayed and false if not.</returns>
        public bool VerifyElementIsDisplayedByID(string elementID)
        {
            return IsDisplayedByID(Hooks.sessionSamsungCloud, elementID);
        }

        /// <summary>
        /// This method checks whether there is an associated account on the Settings page.
        /// </summary>
        /// <returns>This method returns true or false, true if the account is displayed and false if not.</returns>
        public bool VerifyAcountIsDisplayedOnSettings()
        {
            return GetTextByID(Hooks.sessionSamsungCloud, userAccoountID).Length > 0;
        }

        /// <summary>
        /// This method verify if the component set by parameter element ID is DISPLAYED.
        /// </summary>
        /// <param name="text">This parameter is the element name of the method will be verify if is enabled. The parameter should be a element by NAME.</param>
        /// <returns>This method returns true or false, true if the status is displayed and false if not.</returns>
        public bool VerifySettingsButtonIsDisplayed(string text)
        {
            return GetAttributeByID(Hooks.sessionSamsungCloud, SettingsButtonID, "Name").Contains(text);
        }

        /// <summary>
        /// This method verify if the status of EDP DISPLAYED.
        /// </summary>
        /// <returns>This method returns true or false, true if the status is displayed and false if not.</returns>
        public bool VerifyStatusEDPIsDisplayed()
        {
            string textStatus = GetTextByID(Hooks.sessionSamsungCloud, statusEDPID);
            return textStatus.Contains("On") || textStatus.Contains("Off");
        }

        /// <summary>
        /// This method verify if the component set by parameter element ID is DISPLAYED.
        /// </summary>
        /// <returns>This method returns true or false, true if the status is displayed and false if not.</returns>
        public bool VerifyDescriptionEDPIsDisplayed()
        {
            string textStatus = GetTextByID(Hooks.sessionSamsungCloud, statusEDPID);
            bool isOn = textStatus.Contains("On");

            string modeId = isOn ? descriptionEDPOnID : descriptionEDPOffID;
            string informationEDP = isOn ? $"You're {descriptionStatusEDPID}" : $"You're not {descriptionStatusEDPID}";

            return GetAttributeByID(Hooks.sessionSamsungCloud, modeId, "Name").Contains(informationEDP);
        }

        /// <summary>
        /// This method verify if the component set by parameter element ID is DISPLAYED.
        /// </summary>
        /// <param name="text">This parameter is the element name of the method will be verify if is displayed. The parameter should be a element by NAME.</param>
        /// <returns>This method returns true or false, true if the information is displayed and false if not.</returns>
        public bool VerifyInformationEDPIsDisplayed(string text)
        {
            return GetAttributeByID(Hooks.sessionSamsungCloud, dataSubMenuLabelsID, "Name").Contains(text);
        }

        /// <summary>
        /// This method performs click on get started button
        /// </summary>
        public void ClickGetStartedButton()
        {
            WaitIsDisplayedByID(Hooks.sessionSamsungCloud, 10, signInWelcomePageID);
            Hooks.sessionSamsungCloud.FindElementByAccessibilityId(signInWelcomePageID).Click();
        }

        /// <summary>
        /// This method verify if the component set by parameter element Name is DISPLAYED.
        /// </summary>
        /// <param name="text">This parameter is the element name of the method will be verify if is enabled. The parameter should be a element by NAME.</param>
        /// <returns>This method returns true or false, true if the name is displayed and false if not.</returns>
        public bool GetStartedButtonIsDisplayedByName(string text)
        {
            return IsDisplayedByName(Hooks.sessionSamsungCloud, text);
        }

        /// <summary>
        /// This method performs the sign out on Samsung Cloud
        /// </summary>
        /// <returns>This method returns true or false, true if id exists and false if not.</returns>
        public bool SignOutSamsungCloud()
        {
            WaitIsDisplayedByID(Hooks.sessionSamsungCloud, 10, signOutExternalID);
            Hooks.sessionSamsungCloud.FindElementByAccessibilityId(signOutExternalID).Click();
            return WaitIsDisplayedByID(Hooks.sessionSamsungCloud, 10, signInWelcomePageID);
        }

        /// <summary>
        /// This method performs the login on Samsung Cloud
        /// </summary>
        /// <returns>This method returns true or false, true if the title is displayed and false if not.</returns>
        public bool SignInSamsungCloud()
        {
            ClickGetStartedButton();

            SamsungAccountPage samsungAccount = new SamsungAccountPage();
            Session session = new Session();
            WindowsDriver<WindowsElement> sessionSamsungAccount;
            sessionSamsungAccount = session.InitializeSamsungAccount();

            sessionSamsungAccount.FindElementByAccessibilityId(signButtonID).Click();
            FillEntry(sessionSamsungAccount, sansungCloudEmailID, emailCloudAccount);
            WindowsElement element = FindElementByID(sessionSamsungAccount, "MainWebView");

            element.SendKeys(Keys.Enter);
            FillEntry(sessionSamsungAccount, samsungAccountPassID, password);
            element.SendKeys(Keys.Enter);

            return WaitIsDisplayedByID(Hooks.sessionSamsungCloud,10, commandBarID);
        }

        /// <summary>
        /// Checks if a button, identified by its text, is displayed on the screen.
        /// </summary>
        /// <param name="buttonName">
        /// The text of the button to be verified. Accepted values:
        /// "Sign out", "Contact us", and "Open source licenses".
        /// </param>
        /// <returns>
        /// Returns true if the button is displayed and its text matches the expected value; otherwise, false.
        /// </returns>
        public bool VerifyButtonIsDisplayed(string buttonName)
        {
            string buttonID = buttonName switch
            {
                "Sign out" => buttonSignOutID,
                "Contact us" => buttonContactUsID,
                "Open source licenses" => buttonOpenSourceLicenseID,
                _ => throw new ArgumentException("Invalid button text", paramName: buttonName)
            };

            try
            {
                return FindElementByID(Hooks.sessionSamsungCloud, buttonID).Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Verifies if the settings page is open.
        /// </summary>
        /// <param name="pageName">The name of the page to check.</param>
        /// <returns>Returns true if the page is open; otherwise, false.</returns>
        public bool VerifyPageIsOpen(string pageName)
        {
            ClickAppButton(pageName);
            WaitElementTextIsDisplayedByID(Hooks.sessionSamsungCloud, 5, commandBarID);
            return VerifyTitlePageIsDisplayed(pageName);
        }

        /// <summary>
        /// This method verify if the component set by parameter element ID is DISPLAYED.
        /// </summary>
        /// <returns>This method returns true or false, true if the status is displayed and false if not.</returns>
        public bool VerifySamsungCloudVersionOnSettings()
        {
            string concatVersion = @"Version \d+(\.\d+)*";

            string concatVersionCloud = FindElementByID(Hooks.sessionSamsungCloud, appVersionLabelID).Text + " " +
                                        FindElementByID(Hooks.sessionSamsungCloud, appVersionNumberID).Text;

            return VerifyRegexMatchs(concatVersionCloud, concatVersion);
        }

        /// <summary>
        /// This method make the click action on the Samsung Gallery item on the app list on the dashboard page.
        /// </summary>
        public void ClickGalleryButton()
        {
            FindElementByName(Hooks.sessionSamsungCloud, galleryName).Click();
        }

        /// <summary>
        /// This method make the click action on the Samsung Notes item on the app list on the dashboard page.
        /// </summary>
        public void ClickNotesButton()
        {
            FindElementByName(Hooks.sessionSamsungCloud, samsungNotesTitleName).Click();
        }

        /// <summary>
        /// This method make the click action on the Samsung Settings item on the app list on the dashboard page.
        /// </summary>
        public void ClickSettingsButton()
        {
            FindElementByName(Hooks.sessionSamsungCloud, samsungSettingsTitleName).Click();
        }

        /// <summary>
        /// This method checks whether the page title is Settings.
        /// </summary>
        /// <returns>The returns is true or false, is a boolean. Get the page title and check if it's Settings.
        /// <exception cref="InvalidOperationException">Thrown if the page title does not match the expected title.</exception>
        public bool VerifySettingsTitle()
        {
            if (!IsDisplayedByName(Hooks.sessionSamsungCloud, samsungSettingsTitleName))
            {
                throw new InvalidOperationException($"Page title is not find.");
            }

            return true;
        }

        /// <summary>
        /// This method make the click action on the button set by parameter element NAME.
        /// </summary>
        /// <param name="elementName">This parameter defines the name of the element.</param>
        public void ClickAppButton(string elementName)
        {
            FindElementByName(Hooks.sessionSamsungCloud, elementName).Click();
        }

        /// <summary>
        /// This method make the click action on the back button set by parameter element ID.
        /// </summary>
        public void ClickBackButton()
        {
            FindElementByID(Hooks.sessionSamsungCloud, backButtonID).Click();
        }

        /// <summary>
        /// This method verify if the app setting page on the Cloud sample is opened.
        /// </summary>
        /// <param name="titleName">This parameter is the name of the app settings page that needs to check</param>
        /// <returns>The returns is true or false, is a boolean. Gets the text in the command bar and compares it with the appName parameter,
        /// if true it will check if the sync now button is displayed. If yes, returns TRUE otherwise returns FALSE  </returns>
        public bool VerifyAppSettingsIsOpened(string titleName)
        {
            return GetTextByID(Hooks.sessionSamsungCloud, commandBarID).Contains(titleName) && IsDisplayedByID(Hooks.sessionSamsungCloud, syncNowID);
        }

        /// <summary>
        /// This method change the toggle button status on the dashboard page.
        /// Checks if the current state of the button is different from the passed in the parameter, if so, the click is performed, changing the status.
        /// </summary>
        /// <param name="appName">This parameter is the name of the application who the toggle needs to change the status.</param>
        /// <param name="status">This paramerter is the status who the toggle shoudl be stay.</param>
        public void ChangeCloudToogleStatusOnDashboard(string appName, string status)
        {
            string previousState = GetCloudToggleState(appName);
            if ((previousState.Contains("1") && status.Contains("OFF")) || (previousState.Contains("0") && status.Contains("ON")))
            {
                ClickCloudToggleState(appName);
            }
        }

        /// <summary>
        /// This method change the toggle button status on the app settings page.
        /// Checks if the current state of the button is different from the passed in the parameter, if so, the click is performed, changing the status.
        /// </summary>
        /// <param name="status">This paramerter is the status who the toggle shoudl be stay.</param>
        public void ChangeCloudToogleStatusOnAppSettings(string status)
        {
            string previousState = GetCloudToggleStateAppSettings();
            if ((previousState.Contains("1") && status.Contains("OFF")) || (previousState.Contains("0") && status.Contains("ON")))
            {
                ClickCloudToggleStateAppSettings();
                WaitToggleIsEnable();
            }
        }

        /// <summary>
        /// This method waits for the toggle's specified attribute to match the given status.
        /// </summary>
        /// <param name="status">The expected value of the toggle's attribute to wait for.</param>
        public void WaitToggleIsEnable()
        {
            WaitIsEnabledByID(Hooks.sessionSamsungCloud, 15, toggleID);
        }

        /// <summary>
        /// This method finds elements by Automation ID and returns the one that matches the specified condition.
        /// </summary>
        /// <param name="expectedName">The expected name to match the element.</param>
        /// <returns>The element that matches the Automation ID and condition, or null if not found.</returns>
        public WindowsElement GetCloudItemListButtonDashboard(string expectedName)
        {
            ReadOnlyCollection<WindowsElement> elements = Hooks.sessionSamsungCloud.FindElementsByClassName(listItemCN);
            foreach (WindowsElement element in elements)
            {
                if (element.GetAttribute(name).Equals(expectedName))
                {
                    return element;
                }
            }

            return null;
        }

        /// <summary>
        /// This method gets the actual toggle button status of the application on the dashboard page by parameter.
        /// Verify if the parameter appName contains the string with the application name and gets the toggle status.
        /// </summary>
        /// <param name="appName">This parameter is the name of the application that needs to get status</param>
        /// <returns>Returns the string with the toggle button status.</returns>
        public string GetCloudToggleState(string appName)
        {
            WaitTheSubtextIsLoading(appName);
            return GetCloudItemListButtonDashboard(appName).FindElementByAccessibilityId(toggleSwitchID).GetAttribute(toggleAttribute);
        }

        /// <summary>
        /// This method gets the attribute of the toggle button.
        /// </summary>
        /// <returns>Returns the attribute with an string.</returns>
        public string GetCloudToggleStateAppSettings()
        {
            return GetAttributeByID(Hooks.sessionSamsungCloud, toggleID, toggleAttribute);
        }

        /// <summary>
        /// This method performs the click action on the toggle button.
        /// </summary>
        public void ClickCloudToggleStateAppSettings()
        {
            FindElementByID(Hooks.sessionSamsungCloud, toggleID).Click();
        }

        /// <summary>
        /// This method performed the click action on the toogle button on the dashboard page.
        /// </summary>
        /// <param name="appName">This parameter is the application name that the click should be performed.</param>
        public void ClickCloudToggleState(string appName)
        {
            GetCloudItemListButtonDashboard(appName).FindElementByClassName(toggleCN).Click();
        }

        /// <summary>
        /// This method gets the text of the last synced information on the app settings.
        /// </summary>
        /// <returns>Returns the text with an string.</returns>
        public string GetSyncInfoText()
        {
            return GetTextByID(Hooks.sessionSamsungCloud, syncInfoID);
        }

        /// <summary>
        /// This method performs the click action on the sync now button on the app settings and waits five seconds.
        /// </summary>
        public void ClickSyncButton()
        {
            FindElementByID(Hooks.sessionSamsungCloud, syncingButtonID).Click();
        }

        /// <summary>
        /// This method performs the click action on the sync now button on the app settings.
        /// </summary>
        public void ClickSyncNow()
        {
            FindElementByID(Hooks.sessionSamsungCloud, syncInfoID).Click();
        }

        /// <summary>
        /// This method waits five seconds or the app list to be DISPLAYED on the dashboard.
        /// </summary>
        public void WaitAppListIsDisplayed()
        {
            WaitIsDisplayedByID(Hooks.sessionSamsungCloud, 30, appSubtitleListID);
        }

        /// <summary>
        /// This method waits 15 seconds or the app list to be ENABLED on the dashboard.
        /// </summary>
        public void WaitAppListIsEnabled()
        {
            WaitIsEnabledByID(Hooks.sessionSamsungCloud, 15, appSubtitleListID);
        }

        /// <summary>
        /// This method waits five seconds or the last synced information to be DISPLAYED on the app settings.
        /// </summary>
        public void WaitLastSyncInfoIsVisible()
        {
            WaitElementTextIsDisplayedByID(Hooks.sessionSamsungCloud, 5, lastSyncedID);
        }

        /// <summary>
        /// This method verifies if the sync using options on the app settings are ENABLED.
        /// </summary>
        /// <returns>Returns a boolean, true if both are enabled else false.</returns>
        public bool VerifySyncUsingOptionsIsEnabled()
        {
            return IsEnabledByID(Hooks.sessionSamsungCloud, wifiOnlyID) && IsEnabledByID(Hooks.sessionSamsungCloud, mobileDataID);
        }

        /// <summary>
        /// This method verifies if the sync using options on the app settings are ENABLED.
        /// Assert checks if the results are false, if false the returns are true.
        /// </summary>
        public void VerifySyncUsingOptionsIsDisabled()
        {
            Assert.IsFalse(IsEnabledByID(Hooks.sessionSamsungCloud, wifiOnlyID) && IsEnabledByID(Hooks.sessionSamsungCloud, mobileDataID));
        }

        /// <summary>
        ///  Switch between the radio buttons on the application settings screen.
        /// </summary>
        /// <param name="status">The name of the radio button that will be used in the scenario.</param>
        public void ChangeCloudSyncUsingOption(string status)
        {
            bool syncOptionStatus = VerifyCloudSyncOptions(status);

            if (status.Contains("Wi-Fi and Ethernet only"))
            {
                ChangeCloudSync(syncOptionStatus, wifiOnlyID, mobileDataID);
            }
            else if (status.Contains("Wi-Fi, Ethernet, and mobile data"))
            {
                ChangeCloudSync(syncOptionStatus, mobileDataID, wifiOnlyID);
            }
        }

        /// <summary>
        /// This method checkx if the main radio button selector for scenario is already selected,
        /// if yes click the secondary selector and then the main selector, if not just click the main selector.
        /// </summary>
        /// <param name="syncOptionStatus">The current status of the radio.</param>
        /// <param name="mainOption">The main radio button selector to be selected in the scenario.</param>
        /// <param name="secondaryOption">The secondary radio button selector to be selected in the scenario.</param>
        public void ChangeCloudSync(bool syncOptionStatus, string mainOption, string secondaryOption)
        {
            if (syncOptionStatus)
            {
                FindElementByID(Hooks.sessionSamsungCloud, secondaryOption).Click();
            }

            FindElementByID(Hooks.sessionSamsungCloud, mainOption).Click();
        }

        /// <summary>
        /// This method verifies if the icon application is DISPLAYED on the dashboard page.
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <returns>Returns boolean true if the icon by id is displayed, else false.</returns>
        public bool VerifyIconIsDisplayed(string appName)
        {
            return FindElementByName(Hooks.sessionSamsungCloud, appName).FindElementByAccessibilityId(iconID).Displayed;
        }

        /// <summary>
        /// This method verifies if the more information menu is ENABLED on the app settings page.
        /// </summary>
        /// <returns>Returns boolean true if the icon by id is enabled, else false.</returns>
        public bool VerifyMoreInformationMenuIsEnabled()
        {
            return IsEnabledByID(Hooks.sessionSamsungCloud, buttonMoreInformationID);
        }

        /// <summary>
        /// This method gets the radio button selection status.
        /// </summary>
        /// <param name="status">The name of the radio button that will be used in the scenario.</param>
        /// <returns>The radio button selection status.</returns>
        public bool VerifyCloudSyncOptions(string status)
        {
            if (status.Contains("Wi-Fi and Ethernet only"))
            {
                return WaitIsSelectedByID(Hooks.sessionSamsungCloud, 5, wifiOnlyID);
            }
            else if (status.Contains("Wi-Fi, Ethernet, and mobile data"))
            {
                return WaitIsSelectedByID(Hooks.sessionSamsungCloud, 5, mobileDataID);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// This method verifies if the sync now button is ENABLED on the app settings page.
        /// </summary>
        /// <returns>Returns boolean, true or false.</returns>
        public bool VerifySyncNowButtonIsEnabled()
        {
            return IsEnabledByID(Hooks.sessionSamsungCloud, syncNowID);
        }

        /// <summary>
        /// This method verifies if the sync now button is DISPLAYED on the app settings page.
        /// </summary>
        /// <returns>Returns boolean, true or false.</returns>
        public bool VerifySyncNowButtonIsDisplayed()
        {
            return IsDisplayedByID(Hooks.sessionSamsungCloud, syncNowID);
        }

        /// <summary>
        /// This method gets the last synced text on the app settings page.
        /// </summary>
        /// <returns>Returns the string with the text.</returns>
        public string GetLastSyncedText()
        {
            return GetTextByID(Hooks.sessionSamsungCloud, lastSyncedID);
        }

        /// <summary>
        /// This method sets a margin of error when comparing dates.
        /// </summary>
        /// <returns>Returns true if the minutes are equal or within the margin</returns>
        public bool CompareSyncedAndActualDateWithMinuteMargin()
        {
            string format = "M/d, h:mm";

            var t = GetLastSyncedText().Substring(13, 11).TrimStart().TrimEnd();
            DateTime datetimeApp = DateTime.ParseExact(GetLastSyncedText().Substring(13, 11).TrimStart().TrimEnd(), format, CultureInfo.InvariantCulture);
            DateTime datetimeActual = DateTime.ParseExact(GetCurrentDateAndTime(), format, CultureInfo.InvariantCulture);

            int minutesApp = datetimeApp.Minute;
            int minutesActual = datetimeActual.Minute;

            return minutesApp == minutesActual || minutesActual == minutesApp+1;
        }

        /// <summary>
        /// This method gets the sync info information on the app settings page.
        /// </summary>c
        /// <returns>Returns the string with the text.</returns>
        public string GetGalleryCloudSyncInfo()
        {
            WaitElementTextIsDisplayedByID(Hooks.sessionSamsungCloud, 5, syncInfoID);
            return GetIntInString(RegexSyncInfo(syncInfoRegex, GetTextByID(Hooks.sessionSamsungCloud, syncInfoID)));
        }

        /// <summary>
        /// This method gets the sync info information on the app settings page.
        /// </summary>
        /// <returns>Returns the string with the text.</returns>
        public string GetPassCloudSyncInfo()
        {
            WaitElementTextIsDisplayedByID(Hooks.sessionSamsungCloud, 5, syncInfoID);
            return GetIntInString(RegexLastSynced(GetLastSyncedText()));
        }

        /// <summary>
        /// This method gets the sync info information on the app settings page.
        /// </summary>
        /// <returns>Returns the string with the text.</returns>
        public int GetBluetoothCloudSyncInfo()
        {
            WaitTheSubtextSyncing();
            WaitElementTextIsDisplayedByID(Hooks.sessionSamsungCloud, 5, syncInfoID);
            int.TryParse(GetIntInString(RegexSyncInfo(syncInfoRegex, GetTextByID(Hooks.sessionSamsungCloud, syncInfoID))), out int syncInfo);
            return syncInfo;
        }

        /// <summary>
        /// This method verifies if the current region format is applied on the last synced information.
        /// </summary>
        /// <param name="lastSettingDate">This parameter is the date of the computer.</param>
        /// <param name="lastAppSyncedDate">This parameter is the date of the Cloud sample.</param>
        /// <returns>Returns the bollean after the verification if the informations matchs with the paramenters.</returns>
        public bool IsLastDateSettingsAplicationEquals(string lastSettingDate, string lastAppSyncedDate)
        {
            string dayAndMonth = ExtractDate(lastAppSyncedDate);

            return lastSettingDate.EndsWith(dayAndMonth) || lastSettingDate.StartsWith(dayAndMonth);
        }

        /// <summary>
        /// This method verifies if the computer has a mobile network adapter.
        /// </summary>
        /// <returns>Returns true if any kind of adapter is available else false.</returns>
        public bool VerifyNetworkInterfaces()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Wwanpp || adapter.NetworkInterfaceType == NetworkInterfaceType.Wman
                              || adapter.NetworkInterfaceType == NetworkInterfaceType.Wwanpp2)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// This method verifies if the sync using options are displayed on app settings page.
        /// </summary>
        /// <returns>Returns true if the menu contains SYNC USING is displayed else false.</returns>
        public bool VerifySyncUsingIsDisplayed()
        {
            ReadOnlyCollection<WindowsElement> itemsFound = FindElementsByClassName(Hooks.sessionSamsungCloud, textBlockCN);
            foreach (WindowsElement item in itemsFound)
            {
                if (item.Text.Contains("Sync using"))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// This method changes the country accoutn and reload the Cloud sample.
        /// </summary>
        /// <param name="countryInitials">This parameter is the country that should be selected.</param>
        public void ChangeCountryAccount(string countryInitials)
        {
            FindElementByID(Hooks.sessionSamsungCloud, countryComboboxID).Click();
            FindElementByName(Hooks.sessionSamsungCloud, countryInitials).Click();
        }

        /// <summary>
        /// This method perfomes the click action on the More information menu button.
        /// </summary>
        public void ClicklMoreInformationMenu()
        {
            FindElementByID(Hooks.sessionSamsungCloud, buttonMoreInformationID).Click();
        }

        /// <summary>
        /// This method verifies if the more information menu popup is enabled.
        /// </summary>
        /// <returns>Returns the boolean with the method finding the attribute of the component.</returns>
        public bool VerifyMoreInformationPopUpButtonIsEnabled()
        {
            return IsEnabledByID(Hooks.sessionSamsungCloud, moreInformationShadowButtonID);
        }

        /// <summary>
        /// This method performs the click action on the more information popup button.
        /// </summary>
        public void ClickMoreInformationPopUp()
        {
            FindElementByID(Hooks.sessionSamsungCloud, moreInformationShadowButtonID).Click();
        }

        /// <summary>
        /// This method verifies if the more information page is opened.
        /// </summary>
        /// <param name="titlePage">This parameter is the page name title.</param>
        /// <returns>Return the bollean with compairation between the parameter and the component found.</returns>
        public bool VerifyMoreInformationIsOpened(string titlePage)
        {
            return GetTextByName(Hooks.sessionSamsungCloud, moreInformationName) == titlePage;
        }

        /// <summary>
        /// This method verifies if the more information menu icon is DISPLAYED on the app settings page.
        /// </summary>
        /// <returns>Returns the boolean with the method finding the attribute of the component.</returns>
        public bool VerifyMoreInformationMenuIconIsDisplayed()
        {
            SetDefaultCloudTimeOut(Hooks.sessionSamsungCloud, 3);
            return IsDisplayedByID(Hooks.sessionSamsungCloud, buttonMoreInformationID);
        }

        /// <summary>
        /// This method verifies if the dashboard page is opened.
        /// </summary>
        /// <returns>Returns the boolean with the method finding the attribute of the component.</returns>
        public bool VerifyDashboardIsOpened()
        {
            return IsDisplayedByID(Hooks.sessionSamsungCloud, samsungCloudDataListSubtitleID);
        }

        /// <summary>
        /// This method verifies if the not linked with OneDrive popup is displayed.
        /// </summary>
        /// <returns>Returns the boolean with the method finding the attribute of the component.</returns>
        public bool IsPopUpNotLinkedWithOneDriverDisplayed()
        {
            return IsDisplayedByCN(Hooks.sessionSamsungCloud, popUpCN);
        }

        /// <summary>
        /// This method gets the text in the sync now button on the settings page.
        /// </summary>
        /// <returns>Teturns a text in a string.</returns>
        public string VerifySynNowText()
        {
            return GetTextByID(Hooks.sessionSamsungCloud, dataSubMenuLabelsID);
        }

        /// <summary>
        /// This method validate is the text is displayed
        /// </summary>
        /// <param name="syncText">The text that should displayed</param>
        /// <returns>True is the text is displayed</returns>
        public bool VerifySyncingText()
        {
            ChangeCloudToogleStatusOnAppSettings(toggleOn);
            ClickSyncButton();
            Thread.Sleep(500);
            return WaitTextIsDisplayedByID(Hooks.sessionSamsungCloud, 10, syncNowID, syncingText);
        }

        /// <summary>
        /// This method change the Windows system language.
        /// </summary>
        /// <param name="language">This parameter is the language that would will be applied in the operational system.</param>
        public void WindowsLanguageIsChangedTo(string language)
        {
            ChangeWindowsLanguage(language);
        }

        /// <summary>
        /// This method performs reload on the Cloud sample page.
        /// </summary>
        public void ReloadSamsungCloudPage()
        {
            FindElementByName(Hooks.sessionSamsungCloud, aboutSamsungAccountTitleName).Click();
            FindElementByName(Hooks.sessionSamsungCloud, samsungCloudTitleName).Click();
            Hooks.sessionSamsungCloud.Close();
            Hooks hooks = new Hooks();
            Hooks.sessionSamsungCloud = hooks.InitializeSamsungCloud();
        }

        /// <summary>
        /// This method gets all strings on the dashboard page.
        /// </summary>
        /// <returns>Returns dictionary with the strings.</returns>
        public Dictionary<string, string> GetStringsInDashboard()
        {
            WaitIsDisplayedByID(Hooks.sessionSamsungCloud, 10, samsungCloudDataListSubtitleID);

            Dictionary<string, string> dictionary = new Dictionary<string, string>
            {
                { samsungCloudDataListSubtitleID, FindElementByID(Hooks.sessionSamsungCloud, samsungCloudDataListSubtitleID).Text },
                { samsungCloudSubTextInfoID, FindElementByID(Hooks.sessionSamsungCloud, samsungCloudSubTextInfoID).Text },
                { samsungCloudIsSyncedID, FindElementByID(Hooks.sessionSamsungCloud, samsungCloudIsSyncedID).GetAttribute("LocalizedControlType") }
            };

            return dictionary;
        }

        /// <summary>
        /// This method gets all strings on the apps settings page.
        /// </summary>
        /// <returns>Returns dictionary with the strings.</returns>
        public List<Dictionary<string, string>> GetStringsInDashboardInAnotherScreen()
        {
            WaitIsDisplayedByID(Hooks.sessionSamsungCloud, 10, samsungCloudDataListSubtitleID);

            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            List<Dictionary<string, string>> listDictionary = new List<Dictionary<string, string>>();

            ClickGalleryButton();
            WaitElementTextIsDisplayedByID(Hooks.sessionSamsungCloud, 10, lastSyncedID);
            dictionary.Add(lastSyncedID, FindElementByID(Hooks.sessionSamsungCloud, lastSyncedID).Text);
            dictionary.Add(dataSubMenuLabelsID, FindElementByID(Hooks.sessionSamsungCloud, dataSubMenuLabelsID).Text);
            dictionary.Add(toggleID, FindElementByID(Hooks.sessionSamsungCloud, toggleID).GetAttribute("LocalizedControlType"));
            WaitElementTextIsDisplayedByID(Hooks.sessionSamsungCloud, 10, syncInfoID);
            dictionary.Add(syncInfoID, FindElementByID(Hooks.sessionSamsungCloud, syncInfoID).Text);
            listDictionary.Add(dictionary);
            ClickBackButton();
            WaitIsDisplayedByName(Hooks.sessionSamsungCloud, 10, samsungCloudTitleName);
            dictionary = new Dictionary<string, string>();

            ClickNotesButton();
            WaitElementTextIsDisplayedByID(Hooks.sessionSamsungCloud, 10, lastSyncedID);
            dictionary.Add(lastSyncedID, FindElementByID(Hooks.sessionSamsungCloud, lastSyncedID).Text);
            dictionary.Add(dataSubMenuLabelsID, FindElementByID(Hooks.sessionSamsungCloud, dataSubMenuLabelsID).Text);
            dictionary.Add(toggleID, FindElementByID(Hooks.sessionSamsungCloud, toggleID).GetAttribute("LocalizedControlType"));
            WaitElementTextIsDisplayedByID(Hooks.sessionSamsungCloud, 10, syncInfoID);
            dictionary.Add(syncInfoID, FindElementByID(Hooks.sessionSamsungCloud, syncInfoID).Text);
            listDictionary.Add(dictionary);
            ClickBackButton();
            WaitIsDisplayedByName(Hooks.sessionSamsungCloud, 10, samsungCloudTitleName);
            dictionary = new Dictionary<string, string>();

            ClickSettingsButton();
            WaitElementTextIsDisplayedByID(Hooks.sessionSamsungCloud, 10, lastSyncedID);
            dictionary.Add(lastSyncedID, FindElementByID(Hooks.sessionSamsungCloud, lastSyncedID).Text);
            dictionary.Add(dataSubMenuLabelsID, FindElementByID(Hooks.sessionSamsungCloud, dataSubMenuLabelsID).Text);
            dictionary.Add(toggleID, FindElementByID(Hooks.sessionSamsungCloud, toggleID).GetAttribute("LocalizedControlType"));
            WaitElementTextIsDisplayedByID(Hooks.sessionSamsungCloud, 10, syncInfoID);
            dictionary.Add(syncInfoID, FindElementByID(Hooks.sessionSamsungCloud, syncInfoID).Text);
            listDictionary.Add(dictionary);
            ClickBackButton();
            WaitIsDisplayedByName(Hooks.sessionSamsungCloud, 10, samsungCloudTitleName);

            return listDictionary;
        }

        /// <summary>
        /// Method to get the toast when not having an internet connection.
        /// Inside has another method to wait X seconds or the toast ID turning available.
        /// </summary>
        /// <returns>This method returns the text in the toast on a string</returns>
        public string GetToastMessageWillBeDisplayed()
        {
            WaitIsDisplayedByID(Hooks.sessionSamsungCloud, 10, toastWithoutInternetConnectionID);
            return GetTextByID(Hooks.sessionSamsungCloud, toastWithoutInternetConnectionID);
        }

        /// <summary>
        /// Method to click two times on the sync now button on the Cloud sample.
        /// The parameter "syncNowID" is the automation ID for the sync now button.
        /// </summary>
        public void DoubleClickSyncNow()
        {
            DoubleClickCloudElementByID(syncNowID);
        }

        /// <summary>
        /// Method to close the Cloud sample application
        /// </summary>
        public void CloseCloud()
        {
            Hooks.sessionSamsungCloud.CloseApp();
        }

        /// <summary>
        /// Method to initialize the Cloud sample application
        /// </summary>
        public void OpenCloud()
        {
            Hooks.sessionSamsungCloud.LaunchApp();
        }

        /// <summary>
        /// This method verifies that the toggle buttons is displayed
        /// </summary>
        /// <param name="appName">This paramenter is the name of the application</param>
        /// <returns>Returns true or false</returns>
        public bool VerifyToggleButtonIsDisplayed(string appName)
        {
            WaitTheSubtextIsLoading(appName);
            var applications = FindElementsByClassName(Hooks.sessionSamsungCloud, namedContainerAutomationPeerCN);

            foreach (var app in applications)
            {
                if (app.Text == appName)
                {
                    var toggleFound = app.FindElementByAccessibilityId(toggleSwitchID);
                    return toggleFound.Displayed;
                }
            }

            return false;
        }

        /// <summary>
        /// This method verifies if the list is not loaded because of the error
        /// </summary>
        public void VerifyErroList()
        {
            Hooks.sessionSamsungCloud.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            if (WaitIsDisplayedByID(Hooks.sessionSamsungCloud, 10, errorListingApplicationsTextID) == true)
            {
                FindElementByName(Hooks.sessionSamsungCloud, samsungCloudName).Click();
            }
        }

        /// <summary>
        /// This method waits the subtext is not displayed
        /// </summary>
        /// <param name="appName">The name of the application that should be verified</param>
        public void WaitTheSubtextIsLoading(string appName)
        {
            var elementsFound = FindElementsByClassName(Hooks.sessionSamsungCloud, namedContainerAutomationPeerCN);
            foreach (var app in elementsFound)
            {
                if (app.Text.Contains(appName))
                {
                    WaitTextIsNotDisplayedByID(Hooks.sessionSamsungCloud, 10, subTextInfoID, textExpected);
                }
            }
        }

        /// <summary>
        /// This method waits the subtext is not displayed
        /// </summary>
        public void WaitTheSubtextSyncing()
        {
            WaitTextIsDisplayedByID(Hooks.sessionSamsungCloud, 10, dataSubMenuLabelsID, syncText);
        }

        /// <summary>
        /// Navigates to the Tips page by clicking on the Tips icon.
        /// </summary>
        public void NavigateToTipsPage()
        {
            try
            {
                FindElementByID(Hooks.sessionSamsungCloud, tipIconID).Click();
                VerifyTitlePageIsDisplayed(tipsTitlePageTxt);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Verify if tips icon is displayed.
        /// </summary>
        /// <returns>True if tips icon is displayed as expected, otherwise throws an exception.</returns>
        public bool VerifyTipsIconIsDisplayed()
        {
            return IsDisplayedByID(Hooks.sessionSamsungCloud, tipIconID);
        }

        /// <summary>
        /// Verify if next button is not displayed on last Tips page.
        /// </summary>
        /// <returns>False if the button is not displayed, otherwise throws an exception.</returns>
        public bool VerifyTipsNextButtonIsNotDisplayed()
        {
            FindElementByID(Hooks.sessionSamsungCloud, nextButtonTipsID).Click();
            return !IsEnabledByID(Hooks.sessionSamsungCloud, nextButtonTipsID);
        }

        /// <summary>
        /// Verify if previous button is not displayed on first Tips page.
        /// </summary>
        /// <returns>False if the button is not displayed, otherwise throws an exception.</returns>
        public bool VerifyTipsPreviousButtonIsNotDisplayed()
        {
            return !IsEnabledByID(Hooks.sessionSamsungCloud, previousButtonTipsID);
        }

        /// <summary>
        /// Verify if current and total page is displayed on Tips page.
        /// </summary>
        /// <param name="pageInfo">The expected text of the page title to verify.</param>
        /// <returns>True if the page is displayed as expected, otherwise throws an exception.</returns>
        public string[] ExtractPageInfo()
        {
            string pageInfo = GetAttributeByID(Hooks.sessionSamsungCloud, pageNumberID, "Name");

            if (!VerifyRegexMatchs(pageInfo, pagePattern))
            {
                throw new InvalidOperationException($"Invalid format for the page counter. Expected in 'N/M' format, but received: '{pageInfo}'.");
            }

            string[] pageParts = pageInfo.Split('/');
            if (pageParts.Length != 2)
            {
                throw new InvalidOperationException($"Error processing the page counter. Expected 'N/M', but the string was split into {pageParts.Length} parts. Value received: '{pageInfo}'.");
            }

            return pageParts;
        }

        /// <summary>
        /// Verify if current and total page is displayed on Tips page.
        /// </summary>
        /// <returns>True if the page is displayed as expected, otherwise throws an exception.</returns>
        public bool VerifyCurrentAndTotalPagedWhenNavigating()
        {
            string[] firstInfoPage = ExtractPageInfo();

            int firstPageNumber = int.Parse(firstInfoPage[0].Trim());
            int totalPages = int.Parse(firstInfoPage[1].Trim());

            Hooks.sessionSamsungCloud.FindElementByAccessibilityId(nextButtonTipsID).Click();

            string[] secondPageParts = ExtractPageInfo();

            int secondPageNumber = int.Parse(secondPageParts[0].Trim());

            if (secondPageNumber != firstPageNumber + 1 || totalPages != int.Parse(secondPageParts[1].Trim()))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Verifies that the specified page is successfully displayed by comparing the expected and actual page title.
        /// </summary>
        /// <param name="pageName">The expected text of the page title to verify.</param>
        /// <returns>True if the page is displayed as expected, otherwise throws an exception.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the page title does not match the expected title.</exception>
        public bool VerifyTitlePageIsDisplayed(string pageName)
        {
            string actualText = GetAttributeByID(Hooks.sessionSamsungCloud, commandBarID, "Name");
            if (!actualText.Equals(pageName, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"Expected page title '{pageName}', but received '{actualText}'.");
            }

            return true;
        }

        /// <summary>
        /// Verify if the Open Source License screen is shown
        /// </summary>
        /// <returns>True if the page is displayed as expected, otherwise throws an exception.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the content page does not exists.</exception>
        public bool VerifyOpenSourcePageIsDisplayed()
        {
            if (!IsDisplayedByID(Hooks.sessionSamsungCloud, pageContentOpenSourceID))
            {
                throw new InvalidOperationException($"Page content is not find.");
            }

            return true;
        }

        /// <summary>
        /// Verify if the Open Source License button is shown on settings page
        /// </summary>
        /// <returns>True if the button is displayed as expected, otherwise throws an exception.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the nutton text does not match the expected title.</exception>
        public bool VerifyButtonOpenSourcePageIsDisplayed()
        {
            if (!IsDisplayedByID(Hooks.sessionSamsungCloud, openSourceButtonID))
            {
                throw new InvalidOperationException($"Button is not find.");
            }

            return true;
        }

        /// <summary>
        /// Validates the navigation flow by verifying the page number, title, and text across both pages.
        /// </summary>
        /// <returns>True if the page number, title, and text validation are successful, otherwise false.</returns>
        public bool ValidateTheTipsText()
        {
            var expectedData = new[]
            {
        new { PageNumber = "1 / 2", Title = page01TitleText, Text = page01ExplanatoryText },
        new { PageNumber = "2 / 2", Title = page02TitleText, Text = page02ExplanatoryText }
            };

            foreach (var page in expectedData)
            {
                string actualPageNumber = GetTextByID(Hooks.sessionSamsungCloud, pageNumberID);
                if (!actualPageNumber.Equals(page.PageNumber, StringComparison.Ordinal))
                {
                    Console.WriteLine($"Error: Expected page number '{page.PageNumber}' does not match the actual '{actualPageNumber}'.");
                    return false;
                }
                try
                {
                    // Tìm element Title
                    var titleElement = FindElementByID(Hooks.sessionSamsungCloud, tipsTitleID);
                    string actualTitle = titleElement.GetAttribute("Name"); // Hoặc titleElement.GetAttribute("Name")

                    if (!actualTitle.Equals(page.Title, StringComparison.Ordinal))
                    {
                        Console.WriteLine($"Error: Expected title '{page.Title}' does not match actual '{actualTitle}'.");
                        return false;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"Error: Could not find title element with ID '{tipsTitleID}'.");
                    return false;
                }

                try
                {
                    // Tìm element chứa nội dung dài
                    // Lưu ý: Đảm bảo tipsExplanationTextID trỏ đúng vào đoạn text dài, không phải Title
                    var textElement = FindElementByID(Hooks.sessionSamsungCloud, tipsContentID);
                    string actualText = textElement.Text; // Hoặc textElement.GetAttribute("Name")

                    if (!actualText.Equals(page.Text, StringComparison.Ordinal))
                    {
                        Console.WriteLine($"Error: Expected text '{page.Text}' does not match actual '{actualText}'.");
                        return false;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"Error: Could not find explanatory text element with ID '{tipsContentID}'.");
                    return false;
                }
                if (page.PageNumber != "2 / 2")
                {
                    FindElementByID(Hooks.sessionSamsungCloud, nextButtonTipsID).Click();
                    // Nên thêm wait ngắn ở đây để trang 2 kịp load
                    Thread.Sleep(500);
                }
            }

                return true;
        }

        /// <summary>
        /// Opens the Contact Us pop-up by clicking the designated button.
        /// </summary>
        /// <returns>Returns true if the pop-up is successfully opened, otherwise false.</returns>
        public bool OpenConctacUsPopUp()
        {
            IWebElement button = FindElementByName(Hooks.sessionSamsungCloud, "Contact us");
            if (button != null)
            {
                button.Click();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Opens the Contact Us pop-up by clicking the designated button.
        /// </summary>
        /// <returns>Returns true if the pop-up is successfully opened, otherwise false.</returns>
        public bool VerifyThatContactUsPopIsDispplayed()
        {
            return FindElementByClassName(Hooks.sessionSamsungCloud, popUpCN).Displayed;
        }

        /// <summary>
        /// Verifies the text content of the Contact Us pop-up, including title, description, and hyperlink text.
        /// </summary>
        /// <returns>Returns true if all texts match the expected values stored in DataString, otherwise false.</returns>
        public bool VerifyContactUsPopUpText()
        {
            Dictionary<string, IWebElement> elements = new Dictionary<string, IWebElement>
            {
                { "Title", FindElementByName(Hooks.sessionSamsungCloud, popupTitleName) },
                { "Description", FindElementByID(Hooks.sessionSamsungCloud, popupDescriptionID) },
                { "Hyperlink", FindElementByID(Hooks.sessionSamsungCloud, popupHyperLinkID) }
            };

            if (elements.Values.Any(element => element == null))
            {
                return false;
            }

            return elements["Title"].Text == DataString.expectedContactUsPopUpTitle &&
                   elements["Description"].Text == DataString.expectedContactUsPopUpDescription &&
                   elements["Hyperlink"].Text == DataString.expectedContactUsPopupHyperlinkText;
        }

        /// <summary>
        /// Perform the click on the cancel button
        /// </summary>
        public void CloseContactUsPopUp()
        {
            FindElementByID(Hooks.sessionSamsungCloud, cancelButtonID).Click();
        }

        /// <summary>
        /// This method gets only the date on the last synced string.
        /// </summary>
        /// <param name="lastAppSyncedDate">This parameter is the lastsynced information</param>
        /// <returns>Returns the date on the correct format. </returns>
        private string ExtractDate(string lastAppSyncedDate)
        {
            string[] dateSynced = lastAppSyncedDate.Split(' ');
            return dateSynced.GetValue(3).ToString().Replace(",", string.Empty);
        }
    }
}
//             string[] dateSynced = lastAppSyncedDate.Split(' ');
//             return dateSynced.GetValue(3).ToString().Replace(",", string.Empty);
//         }
//     }
// }
