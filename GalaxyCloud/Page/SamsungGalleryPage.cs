// file="GalleryAppPage.cs" 

using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading;
using GalaxyCloud.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;

namespace GalaxyCloud.Page
{
    /// <summary>
    /// This class is about the interactions on Samsung Gallery application
    /// </summary>
    public class SamsungGalleryPage : General
    {
        private const string gallerySettingsButtonID = "SettingsButton";
        private const string toogleStateAttribute = "Toggle.ToggleState";
        private const string alertDialogName = "Popup";
        private const string alertYesPopUpButton = "OneUIPrimaryActionButton";
        private const string wifiOnlyGalleryID = "CloudInfoSyncWiFiOnlyRadioButton";
        private const string mobileDataGalleryID = "CloudInfoSyncWiFiMobileRadioButton";
        private const string wifiOnlyText = "Wi-Fi and Ethernet only";
        private const string mobileDataText = "Wi-Fi, Ethernet, and mobile data";
        private const string textBlockCN = "TextBlock";
        private const string itemAlbumName = "Albums";
        private const string itemSyncedWithOneDriveName = "Synced with OneDrive";
        private const string itemCameraName = "Camera";
        private const string checkBoxCN = "CheckBox";
        private const string deleteID = "AlbumDetailDeleteAppBarButton";
        private const string alertDeleteID = "OneUIPrimaryActionButton";
        private const string refreshID = "AlbumsRefreshAppBarButton";
        private const string backButtonID = "NavigationBackButton";
        private const string addOneDriveButtonID = "AlbumsAddItemsToOneDriveAppBarButton";
        private const string duplicateOkButtonID = "DuplicatedFileOKButton";
        private const string popUpTextBoxID = "1148";
        private const string popUpOkButtonID = "1";
        private const string refreshStatusToolTipID = "RefreshStatusToolTip";
        private const string uploadStatusTextID = "UploadStatusText";
        private const string uploadCloseBtnID = "UploadCloseBtn";
        private const string folderNumberOfMediasTextID = "FolderNumberOfMediasText";
        private const string refreshButtonID = "PicturesRefreshAppBarButton";
        private const string signOutScreenXPath = @"/Window/Window[1]/Window[2]/Pane/Pane/Group";
        private const string signInScreenXPath = @"/Window/Window[1]/Window[2]/Pane/Pane/Group/Group/Group";
        private const string sansungAccountEmailID = "iptLgnPlnID";
        private const string samsungAccountPassword = "iptLgnPlnPD";
        private const string signInToUseCloudName = "Sign in to use Cloud";
        private const string alertButtonSignoutID = "AlertButtonSignout";
        private const string signInButtonID = "signInButton";
        private const string extendedCommandBarMoreButtonID = "ExtendedCommandBarMoreButton";
        private const string overflowTextLabelID = "OverflowTextLabel";
        private const string navigationBackButtonID = "NavigationBackButton";
        private const string settingsSyncTitleID = "SettingsSyncTitle";
        private const string partTextID = "PART_Text";
        private const string syncCloudInfoPageToggleButtonID = "SetttingsSyncToggleSwitch";
        private readonly string imageUri = TestContext.Parameters["imagePath"];
        private readonly string defaultEmail = Environment.GetEnvironmentVariable("SamsungAccountEmailAutomationOnedrive");
        private readonly string defaultPassword = Environment.GetEnvironmentVariable("password");
        private readonly string emailNoLinkedOnedrive = Environment.GetEnvironmentVariable("SamsungAccountEmailAutomationNoLinkedOnedrive");
        private readonly string password = Environment.GetEnvironmentVariable("password");

        /// <summary>
        /// This method verifies that the Samsung Gallery is opened
        /// </summary>
        public void VerifyGalleryAppIsOpened()
        {
            WaitIsDisplayedByID(Hooks.sessionGallery, 30, gallerySettingsButtonID);
        }

        /// <summary>
        /// This method performs click action on setting button on Gallery application
        /// </summary>
        public void ClickGallerySettingsButton()
        {
            FindElementByID(Hooks.sessionGallery, gallerySettingsButtonID).Click();
        }

        /// <summary>
        /// This method peforms the click on the email address to access the subpage
        /// </summary>
        public void ClickGalleryEmailAddress()
        {
            FindElementByID(Hooks.sessionGallery, partTextID).Click();
        }

        /// <summary>
        /// This method performs click action on the toggle button
        /// </summary>
        public void ClickGallerySyncToggle()
        {
            FindElementByID(Hooks.sessionGallery, syncCloudInfoPageToggleButtonID).Click();
        }

        /// <summary>
        /// This method gets the Samsung Gallery toggle button status
        /// </summary>
        /// <returns>Returns a string with the toogle button status</returns>
        public string GetGalleryToggleState()
        {
            return GetAttributeByClassName(Hooks.sessionGallery, "ToggleSwitch", toogleStateAttribute);
        }

        /// <summary>
        /// This method changes the toggle button status
        /// </summary>
        /// <param name="status">This parameter is the status that should be applied</param>
        public void ChangeGalleryToogleStatus(string status)
        {
            ClickGallerySettingsButton();

            if (GetGalleryToggleState().Contains("1") && status.Contains("OFF"))
            {
                ClickGallerySyncToggle();
                StopSyncPopUp();
            }
            else if (GetGalleryToggleState().Contains("0") && status.Contains("ON"))
            {
                ClickGallerySyncToggle();
            }
        }

        /// <summary>
        /// This method performs the click action on the sync now button on app settings
        /// </summary>
        public void StopSyncPopUp()
        {
            if (IsDisplayedByName(Hooks.sessionGallery, alertDialogName))
            {
                FindElementByID(Hooks.sessionGallery, alertYesPopUpButton).Click();
            }
        }

        /// <summary>
        /// This method verifies the sync using options
        /// </summary>
        /// <param name="status">This parameter that should be verified</param>
        /// <returns>Returns the boolean, true if is selected else false</returns>
        public bool VerifyGallerySyncOptions(string status)
        {
            EntryGallerySyncWithOneDrivePage();

            if (status.Contains(wifiOnlyText))
            {
                return IsSelectedByID(Hooks.sessionGallery, wifiOnlyGalleryID);
            }
            else if (status.Contains(mobileDataText))
            {
                return IsSelectedByID(Hooks.sessionGallery, mobileDataGalleryID);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// This method changes the toggle status on Samsung Gallery in the dashboard page
        /// </summary>
        /// <param name="status">This parameter is the toggle status that should be applied</param>
        public void ChangeGallerySyncInfo(string status)
        {
            EntryGallerySyncWithOneDrivePage();

            if (status.Contains(wifiOnlyText))
            {
                FindElementByID(Hooks.sessionGallery, wifiOnlyGalleryID).Click();
            }
            else if (status.Contains(mobileDataText))
            {
                FindElementByID(Hooks.sessionGallery, mobileDataGalleryID).Click();
            }
        }

        /// <summary>
        /// This method performs click action on Sync OneDrive settings
        /// </summary>
        public void EntryGallerySyncWithOneDrivePage()
        {
            {
                Regex regexEmail = new Regex(@"^[\w\.\+\-]+@[a-zA-Z0-9\-\.\+]+\.[a-zA-Z]{2,6}(\.[a-zA-Z]{2})?$",
                 RegexOptions.Compiled | RegexOptions.IgnoreCase);

                ClickGallerySettingsButton();
                ReadOnlyCollection<WindowsElement> getItem = FindElementsByClassName(Hooks.sessionGallery, textBlockCN);

                foreach (WindowsElement item in getItem)
                {
                    if (regexEmail.IsMatch(item.Text))
                    {
                        item.Click();
                    }
                }
            }
        }

        /// <summary>
        /// This method deletes the pictures on Samsung Gallery previously add
        /// </summary>
        public void DeleteGalleryPicture()
        {
            FindElementByName(Hooks.sessionGallery, itemAlbumName).Click();
            FindElementByName(Hooks.sessionGallery, itemSyncedWithOneDriveName).Click();
            FindElementByName(Hooks.sessionGallery, itemCameraName).Click();
            FindElementByClassName(Hooks.sessionGallery, checkBoxCN).Click();
            FindElementByID(Hooks.sessionGallery, deleteID).Click();
            FindElementByID(Hooks.sessionGallery, alertDeleteID).Click();
            WaitElementIsNotDisplayedByCN(Hooks.sessionGallery, 5, "ProgressBar");
        }

        /// <summary>
        /// This method performs the click action on refresh button on the main page on Samsung Gallery
        /// </summary>
        public void RefreshGallery()
        {
            FindElementByID(Hooks.sessionGallery, refreshID).Click();
            WaitAtributeTextIsDisplayedByID(Hooks.sessionGallery, 60, refreshStatusToolTipID, "Name", "Last");
        }

        /// <summary>
        /// This method gets the sync information in the sync now subtext
        /// </summary>
        /// <returns>Returns the sync info on a string</returns>
        public string GetGallerySyncInfo()
        {
            FindElementByName(Hooks.sessionGallery, itemAlbumName).Click();
            FindElementByName(Hooks.sessionGallery, itemSyncedWithOneDriveName).Click();
            WaitElementTextIsDisplayedByID(Hooks.sessionGallery, 5, folderNumberOfMediasTextID);
            ReadOnlyCollection<WindowsElement> foundElements = FindElementsByClassName(Hooks.sessionGallery, textBlockCN);

            foreach (WindowsElement element in foundElements)
            {
                if (element.Text.Contains("image"))
                {
                    return GetIntInString(element.Text);
                }
            }

            return null;
        }

        /// <summary>
        /// This method performs the click action on back button
        /// </summary>
        public void ClickGalleryBackButton()
        {
            FindElementByID(Hooks.sessionGallery, backButtonID).Click();
        }

        /// <summary>
        /// This method adds one image on Samsung Gallery application
        /// </summary>
        public void AddImageGallery()
        {
            FindElementByName(Hooks.sessionGallery, itemAlbumName).Click();
            FindElementByName(Hooks.sessionGallery, itemSyncedWithOneDriveName).Click();
            FindElementByID(Hooks.sessionGallery, addOneDriveButtonID).Click();
            WindowsElement textBoxPopUP = FindElementByID(Hooks.sessionGallery, popUpTextBoxID);
            textBoxPopUP.Click();
            textBoxPopUP.SendKeys($"{imageUri}");
            FindElementByID(Hooks.sessionGallery, popUpOkButtonID).Click();
            Hooks.sessionGallery.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            if (IsDisplayedByID(Hooks.sessionGallery, duplicateOkButtonID))
            {
                FindElementByID(Hooks.sessionGallery, duplicateOkButtonID).Click();
            }

            WaitAtributeTextIsDisplayedByID(Hooks.sessionGallery, 5, uploadStatusTextID, "Name", "Completed");
            FindElementByID(Hooks.sessionGallery, uploadCloseBtnID).Click();
        }

        /// <summary>
        /// This method gets the status of the refresh button
        /// </summary>
        /// <returns>Returns the string with the status of the sync info</returns>
        public string GetGalleryStatusRefreshToolTip()
        {
            WindowsElement refreshTooltip = FindElementByID(Hooks.sessionGallery, refreshButtonID);
            Actions act = new Actions(Hooks.sessionGallery);
            act.MoveToElement(refreshTooltip).Build().Perform();
            Thread.Sleep(3000);
            return FindElementByID(Hooks.sessionGallery, refreshStatusToolTipID).Text;
        }

        /// <summary>
        /// This method performs the signout on Samsung Gallery
        /// </summary>
        public void PerformSignOut()
        {
            /// This wait is because when the click is performed quickly after entry on the settings page,
            /// the Samsung Gallery shows a error, but not is.
            Thread.Sleep(3000);
            EntryGallerySyncWithOneDrivePage();
            if (IsDisplayedByID(Hooks.sessionGallery, alertButtonSignoutID))
            {
                FindElementByID(Hooks.sessionGallery, alertButtonSignoutID).Click();
            }
            else
            {
                FindElementByID(Hooks.sessionGallery, extendedCommandBarMoreButtonID).Click();
                FindElementByID(Hooks.sessionGallery, overflowTextLabelID).Click();
            }

            Thread.Sleep(5000);
            WindowsElement element = FindElementByXPath(Hooks.sessionGallery, signOutScreenXPath);
            element.SendKeys(Keys.Tab);
            element.SendKeys(Keys.Enter);
            Thread.Sleep(3000);
        }

        /// <summary>
        /// This method performs the signin on the signin form
        /// </summary>
        /// <param name="email">This parameter is the e-mail that should be applied</param>
        /// <param name="password">This parameter is the password that should be applied</param>
        public void PerformSignIn(string email, string password)
        {
            InputLoginUser(email);
            InputLoginPassword(password);
        }

        /// <summary>
        /// This method inputs the password on the signin form
        /// </summary>
        /// <param name="email">This parameter is the password that should be applied</param>
        public void InputLoginUser(string email)
        {
            Thread.Sleep(3000);
            WindowsElement element = FindElementByXPath(Hooks.sessionGallery, signInScreenXPath);
            element.FindElementByAccessibilityId(sansungAccountEmailID).Clear();
            element.FindElementByAccessibilityId(sansungAccountEmailID).SendKeys(email);
            element.FindElementByAccessibilityId(signInButtonID).Click();
        }

        /// <summary>
        /// This method inputs the email
        /// </summary>
        /// <param name="password">This parameter is the e-mail that should be applied</param>
        public void InputLoginPassword(string password)
        {
            Thread.Sleep(3000);
            WindowsElement elementPassword = FindElementByXPath(Hooks.sessionGallery, signInScreenXPath);
            elementPassword.FindElementByAccessibilityId(samsungAccountPassword).SendKeys(password);
            elementPassword.FindElementByAccessibilityId(signInButtonID).Click();
            Thread.Sleep(7000);
        }

        /// <summary>
        /// This method performs signin with a no linked account
        /// </summary>
        public void SignInWithNoLinkedAccount()
        {
            PerformSignOut();
            FindElementByName(Hooks.sessionGallery, signInToUseCloudName).Click();
            PerformSignIn(emailNoLinkedOnedrive, password);
        }

        /// <summary>
        /// This method performs signin with a linked account
        /// </summary>
        public void SignInWithLinkedAccount()
        {
            PerformSignOut();
            if (IsDisplayedByID(Hooks.sessionGallery, gallerySettingsButtonID))
            {
                FindElementByID(Hooks.sessionGallery, gallerySettingsButtonID).Click();
            }

            FindElementByName(Hooks.sessionGallery, signInToUseCloudName).Click();
            PerformSignIn(defaultEmail, defaultPassword);
        }

        /// <summary>
        /// This method performs the login to Samsung Account linked with OneDrive
        /// </summary>
        public void SignInOneDriveLinkedAccount()
        {
            PerformSignOut();
            FindElementByName(Hooks.sessionGallery, gallerySettingsButtonID).Click();
            FindElementByID(Hooks.sessionGallery, signInToUseCloudName).Click();
            PerformSignIn(defaultEmail, defaultPassword);
        }

        /// <summary>
        /// This method returns the login to Samsung Account linked with OneDrive
        /// </summary>
        public void AfterScenarioSignInGallery()
        {
            WaitElementTextIsDisplayedByID(Hooks.sessionGallery, 5, settingsSyncTitleID);
            FindElementByID(Hooks.sessionGallery, navigationBackButtonID).Click();
            SignInOneDriveLinkedAccount();
        }

        /// <summary>
        /// This method performs the set sync on the Samsung Gallery
        /// </summary>
        public void SetUpSync()
        {
            ClickGallerySettingsButton();
            ConfirmSignOutPopUp();
        }

        /// <summary>
        /// This method perform click on the sign out pop up if that is shown
        /// </summary>
        public void ConfirmSignOutPopUp()
        {
            if (IsDisplayedByID(Hooks.sessionGallery, "OneUIPrimaryActionButton"))
            {
                FindElementByID(Hooks.sessionGallery, "OneUIPrimaryActionButton").Click();

                if (IsDisplayedByID(Hooks.sessionGallery, "OneUIPrimaryActionButton"))
                {
                    FindElementByID(Hooks.sessionGallery, "OneUIPrimaryActionButton").Click();
                }
            }
        }

        /// <summary>
        /// Method to initialize the Cloud sample application
        /// </summary>
        public void OpenGalleryApp()
        {
            Hooks.sessionGallery.LaunchApp();
            Thread.Sleep(5000);
        }
    }
}