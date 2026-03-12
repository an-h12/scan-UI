// file="ScpmClient.cs" 
using System;
using System.Threading;
using GalaxyCloud.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;

namespace GalaxyCloud.Page
{
    /// <summary>
    /// This class is about the interactions on SCPM Client application
    /// </summary>
    public class SamsungCloudAssistantPage : General
    {
        /// <summary>
        /// Performance class instance
        /// </summary>
        public Performance performance = new Performance();
        private const string appName = "Samsung Cloud Assistant";
        private const string txbTitleID = "txbTitle";
        private const string noNetworkTxtID = "No network connection";
        private const string scpmText = "Samsung Cloud Assistant";
        private const string appNameID = "appName";
        private const string userAccountID = "userAccount";
        private const string attributeName = "Name";
        private const string appVersionID = "appOneUICodeVersion";
        private const string imageCN = "Image";
        private const string btnActionID = "btnAction";
        private const string appSingInTitleID = "appSingInTitle";
        private const string sansungAccountEmailID = "iptLgnPlnID";
        private const string samsungAccountPassword = "iptLgnPlnPD";
        private const string signInButtonID = "signInButton";
        private const string signInScreenXPath = @"/Window/Window[1]/Window[2]/Pane/Pane/Group/Group/Group";
        private const string appSingInTitleText = "Keep your data safe and synced on all your devices using Samsung Cloud.";
        private const string getStartedID = "PART_Text";
        private const string goToSettingsName = "Go to settings";
        private const string feedbackButtonId = "appStoreSendFedbackButton";
        private readonly string defaultEmail = Environment.GetEnvironmentVariable("SamsungAccountEmailAutomationOnedrive");
        private readonly string defaultPassword = Environment.GetEnvironmentVariable("password");

        /// <summary>
        /// This method checks if the application has been opened, it searches in the page if there is any item with the parameter in appName.
        /// FindElementByName is to locate an element, takes a parameter of string which is a value of NAME attribute and it returns a object to FindElementByName() method.
        /// The appName is a private variable set above, in this case is a name of sample application
        /// </summary>
        public void VerifyAppIsLaunched()
        {
            FindElementByName(Hooks.sessionSamsungCloud, appName);
        }

        /// <summary>
        /// This method verifies if the network screen is displayed
        /// </summary>
        /// <returns>Returns true if the title is the app name else false</returns>
        public bool VerifyNoNetworkScreen()
        {
            return GetTextByID(Hooks.sessionSamsungCloud, txbTitleID) == noNetworkTxtID;
        }

        /// <summary>
        /// This method waits until to the text is displayed
        /// </summary>
        public void VerifyScaMainPageIsOpened()
        {
            WaitAtributeTextIsDisplayedByID(Hooks.sessionSamsungCloud, 5, appNameID, attributeName, scpmText);
        }

        /// <summary>
        /// This method waits until to the text is displayed
        /// </summary>
        /// <returns>Test</returns>
        public bool VerifyScaMainPageIsDisplayed()
        {
            return IsDisplayedByID(Hooks.sessionSamsungCloud, appNameID);
        }

        /// <summary>
        /// This method gets the text of the element ID
        /// </summary>
        /// <returns>This returns is the text on a string</returns>
        public string VerifyUserAccount()
        {
            return GetTextByID(Hooks.sessionSamsungCloud, userAccountID);
        }

        /// <summary>
        /// This method gets the text of the element ID
        /// </summary>
        /// <returns>This returns is the text on a string</returns>
        public string VerifyScaVersion()
        {
            return GetTextByID(Hooks.sessionSamsungCloud, appVersionID);
        }

        /// <summary>
        /// This method verifies that the Samsung Accout Profile Image is displayed by the class name element
        /// </summary>
        /// <returns>This returns boolean, true if is displayed else false</returns>
        public bool VerifySamsungAccountProfileImage()
        {
            return IsDisplayedByCN(Hooks.sessionSamsungCloud, imageCN);
        }

        /// <summary>
        /// This method performs the click action on the retry button on the SCPM Client
        /// </summary>
        public void ClickOnRetryButton()
        {
            Thread.Sleep(8000);
            FindElementByID(Hooks.sessionSamsungCloud, btnActionID).Click();
        }

        /// <summary>
        /// This method verifies that the welcome message is displayed on Samsung Cloud Assistant
        /// </summary>
        /// <returns>Returns true if the welcome message is diplayed else false</returns>
        public bool VerifyWelcomeMessageIsDisplayed()
        {
            return GetTextByID(Hooks.sessionSamsungCloud, appSingInTitleID) == appSingInTitleText;
        }

        /// <summary>
        /// This method performs the login on Samsung Cloud Assistant
        /// </summary>
        public void PeformLoginSCA()
        {
            GetStartedClicked();
            PerformSignIn(defaultEmail, defaultPassword);
        }

        /// <summary>
        /// This method inputs the password on the signin form
        /// </summary>
        /// <param name="email">This parameter is the password that should be applied</param>
        public void InputLoginUser(string email)
        {
            WaitIsDisplayedByID(Hooks.sessionSamsungCloud, 10, sansungAccountEmailID);
            WindowsElement element = FindElementByXPath(Hooks.sessionSamsungCloud, signInScreenXPath);
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
            WaitIsDisplayedByID(Hooks.sessionSamsungCloud, 10, samsungAccountPassword);
            WindowsElement elementPassword = FindElementByXPath(Hooks.sessionSamsungCloud, signInScreenXPath);
            elementPassword.FindElementByAccessibilityId(samsungAccountPassword).Clear();
            elementPassword.FindElementByAccessibilityId(samsungAccountPassword).SendKeys(password);
            elementPassword.FindElementByAccessibilityId(signInButtonID).Click();
            WaitIsDisplayedByID(Hooks.sessionSamsungCloud, 10, userAccountID);
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
        /// This method attemps the sigin on Samsung Cloud Assistant
        /// </summary>
        public void AttempLogin()
        {
            VerifyWelcomeMessageIsDisplayed();
            GetStartedClicked();
        }

        /// <summary>
        /// This method performs the click action on the GetStarted button on Samsung Cloud Assitant
        /// </summary>
        public void GetStartedClicked()
        {
            FindElementByID(Hooks.sessionSamsungCloud, getStartedID).Click();
        }

        /// <summary>
        /// This method performs the signin on the signin form measuring time performance
        /// </summary>
        /// <param name="times">Amount of times for the action to be repeated</param>
        public void SignInTimePerformance(int times)
        {
            SamsungAccountPage samsungAccount = new SamsungAccountPage();
            Session session = new Session();
            WindowsDriver<WindowsElement> sessionSamsungAccount;
            sessionSamsungAccount = session.InitializeSamsungAccount();

            for (int i = 0; i < times; i++)
            {
                // Login Samsung Account Window
                GetStartedClicked();
                // Insert Email
                WaitIsDisplayedByID(Hooks.sessionSamsungCloud, 10, sansungAccountEmailID);
                WindowsElement elementEmail = FindElementByXPath(Hooks.sessionSamsungCloud, signInScreenXPath);
                elementEmail.FindElementByAccessibilityId(sansungAccountEmailID).Clear();
                elementEmail.FindElementByAccessibilityId(sansungAccountEmailID).SendKeys(defaultEmail);
                elementEmail.FindElementByAccessibilityId(signInButtonID).Click();
                // Insert Password
                WaitIsDisplayedByID(Hooks.sessionSamsungCloud, 10, samsungAccountPassword);
                WindowsElement elementPassword = FindElementByXPath(Hooks.sessionSamsungCloud, signInScreenXPath);
                elementPassword.FindElementByAccessibilityId(samsungAccountPassword).Clear();
                elementPassword.FindElementByAccessibilityId(samsungAccountPassword).SendKeys(defaultPassword);
                elementPassword.FindElementByAccessibilityId(signInButtonID).Click();
                // Measure time until user account ID is displayed at SCA app
                performance.stopwatch.Start();
                WaitIsDisplayedByID(Hooks.sessionSamsungCloud, 10, userAccountID);
                performance.stopwatch.Stop();
                performance.actionTime.Add(performance.stopwatch.ElapsedMilliseconds);
                performance.stopwatch.Reset();
                // Log out at Samsung Account app
                sessionSamsungAccount.SwitchTo();
                WaitIsDisplayedByName(sessionSamsungAccount, 10, samsungAccount.samsungAccountTitleN);
                WaitIsDisplayedByID(sessionSamsungAccount, 10, samsungAccount.signOutButtonID);
                FindElementByID(sessionSamsungAccount, samsungAccount.signOutButtonID).Click();
                WaitIsDisplayedByID(sessionSamsungAccount, 15, samsungAccount.popUpWindowsId);
                WindowsElement signOutPopup = FindElementByID(sessionSamsungAccount, samsungAccount.popUpWindowsId);
                signOutPopup.SendKeys(Keys.Tab);
                signOutPopup.SendKeys(Keys.Enter);
                // Verify Get Started Button is displayed on Samsung Cloud Assistant app
                WaitIsDisplayedByName(Hooks.sessionSamsungCloud, 15, appSingInTitleText);
            }

            sessionSamsungAccount.Quit();
        }

        /// <summary>
        /// This method performs the signin on the signin form measuring time performance
        /// </summary>
        /// <param name="times">Amount of times for the action to be repeated</param>
        public void SignOutTimePerformance(int times)
        {
            SamsungAccountPage samsungAccount = new SamsungAccountPage();
            Session session = new Session();
            WindowsDriver<WindowsElement> sessionSamsungAccount;
            sessionSamsungAccount = session.InitializeSamsungAccount();

            for (int i = 0; i < times; i++)
            {
                // Login Samsung Account Window
                PeformLoginSCA();
                WaitIsDisplayedByID(Hooks.sessionSamsungCloud, 10, userAccountID);
                // Log out at Samsung Account app
                sessionSamsungAccount.SwitchTo();
                WaitIsDisplayedByName(sessionSamsungAccount, 10, samsungAccount.samsungAccountTitleN);
                WaitIsDisplayedByID(sessionSamsungAccount, 10, samsungAccount.signOutButtonID);
                FindElementByID(sessionSamsungAccount, samsungAccount.signOutButtonID).Click();
                WaitIsDisplayedByID(sessionSamsungAccount, 15, samsungAccount.popUpWindowsId);
                WindowsElement element = FindElementByID(sessionSamsungAccount, samsungAccount.popUpWindowsId);
                element.SendKeys(Keys.Tab);
                element.SendKeys(Keys.Enter);
                // Measure time until Samsung Cloud Assistant App logs out
                performance.stopwatch.Start();
                WaitElementTextIsDisplayedByName(Hooks.sessionSamsungCloud, 15, appSingInTitleText);
                performance.stopwatch.Stop();
                performance.actionTime.Add(performance.stopwatch.ElapsedMilliseconds);
                performance.stopwatch.Reset();
            }

            sessionSamsungAccount.Quit();
        }

        /// <summary>
        /// This method verifies that the buttons is displayed
        /// </summary>
        /// <returns>Return true if the button is displayed else false</returns>
        public bool VerifyScaMainPageGoTosettingsButtonIsDisplayed()
        {
            return IsDisplayedByName(Hooks.sessionSamsungCloud, goToSettingsName);
        }

        /// <summary>
        /// This method verifies that the buttons is displayed
        /// </summary>
        /// <returns>Return true if the button is displayed else false</returns>
        public bool VerifyFeedbackButtonIsDisplayed()
        {
            return IsDisplayedByID(Hooks.sessionSamsungCloud, feedbackButtonId);
        }

        /// <summary>
        /// This method perform click on the Go to settings page button
        /// </summary>
        public void PerformClickOnGoToSettingPageButton()
        {
            FindElementByName(Hooks.sessionSamsungCloud, goToSettingsName).Click();
        }
    }
}