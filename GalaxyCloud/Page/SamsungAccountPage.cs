// file="SamsungAccountPage.cs" 

using System;
using GalaxyCloud.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;

namespace GalaxyCloud.Page
{
    /// <summary>
    ///  This class is about the interactions on Samsung Account application
    /// </summary>
    public class SamsungAccountPage : General
    {
        public string signOutButtonID = "SignOutItem";
        public string samsungAccountTitleN = "Samsung Account";
        public string popUpWindowsId = "MainWebView";
        private const string sansungAccountEmailID = "iptLgnPlnID";
        private const string samsungAccountPassword = "iptLgnPlnPD";
        private const string getStartedButtonID = "GetStartedButton";
        private const string signInName = "Sign in";
        private const string signOutScreenXPath = @"/Window/Window[2]/Window[2]/Pane/Pane/Pane/Group";
        private const string dataListTitleID = "DataListTitle";
        private readonly string emailNotLinkedOnedrive = Environment.GetEnvironmentVariable("SamsungAccountEmailAutomationNotLinkedOnedrive");
        private readonly string password = Environment.GetEnvironmentVariable("password");
        private readonly string defaultEmail = Environment.GetEnvironmentVariable("SamsungAccountEmailAutomationOnedrive");
        private readonly string emailChineseAccount = Environment.GetEnvironmentVariable("SamsungAccountEmailChinese");

        /// <summary>
        /// This method verifies is the Samsung Account application is oppened
        /// </summary>
        public void VerifySamsungAccountsAppIsOpened()
        {
            WaitElementTextIsDisplayedByName(Hooks.sessionSamsungAccount, 15, "Samsung Account");
        }

        /// <summary>
        /// This method clicks on sync now button
        /// </summary>
        public void ClickSignOUtButton()
        {
            FindElementByID(Hooks.sessionSamsungAccount, signOutButtonID).Click();
            WaitIsDisplayedByID(Hooks.sessionSamsungAccount, 10, "ContentScrollViewer");
        }

        /// <summary>
        /// This method click on siging  button
        /// </summary>
        public void ClickSignInButton()
        {
            WindowsElement element = FindElementByXPath(Hooks.sessionSamsungAccount, signOutScreenXPath);
            element.SendKeys(Keys.Tab);
            element.SendKeys(Keys.Tab);
            element.SendKeys(Keys.Enter);
        }

        /// <summary>
        /// This method inserts the email in the login field
        /// </summary>
        /// <param name="email">This parameter is the email that should be sent</param>
        public void InputLogin(string email)
        {
            FindElementByID(Hooks.sessionSamsungAccount, sansungAccountEmailID).Clear();
            FindElementByID(Hooks.sessionSamsungAccount, sansungAccountEmailID).SendKeys(email);
        }

        /// <summary>
        /// This method inserts the email in the login field
        /// </summary>
        /// <param name="password">This parameter is the password that should be sent</param>
        public void InputPassword(string password)
        {
            FindElementByID(Hooks.sessionSamsungAccount, samsungAccountPassword).Clear();
            FindElementByID(Hooks.sessionSamsungAccount, samsungAccountPassword).SendKeys(password);
        }

        /// <summary>
        /// This method performs click on next button on singin process
        /// </summary>
        public void ClickNextButton()
        {
            WindowsElement element = FindElementByID(Hooks.sessionSamsungAccount, popUpWindowsId);
            element.SendKeys(Keys.Enter);
        }

        /// <summary>
        /// This method performs click on get started button
        /// </summary>
        public void ClickGetStartedButton()
        {
            FindElementByID(Hooks.sessionSamsungAccount, getStartedButtonID).Click();
        }

        /// <summary>
        /// This method perform click on signout button
        /// </summary>
        public void ConfirmSignOutSamsungAccount()
        {
            WindowsElement element = FindElementByID(Hooks.sessionSamsungAccount, popUpWindowsId);
            element.SendKeys(Keys.Tab);
            element.SendKeys(Keys.Enter);
            WaitElementTextIsDisplayedByID(Hooks.sessionSamsungAccount, 10, getStartedButtonID);
        }

        /// <summary>
        /// This method perfoms the logout on Samsung Account
        /// </summary>
        public void LogoutSamsungAccount()
        {
            ClickSignOUtButton();
            ConfirmSignOutSamsungAccount();
        }

        /// <summary>
        /// This method verifies if the Samsung Account is logged in, if not performs login.
        /// </summary>
        /// <param name="email">This parameter is the e-mail that should be sent to the signin fiedl</param>
        /// <param name="password">This parameter is the password that should be sent to the sign-in field</param>
        public void IsUserLogoutSamsungAccount(string email, string password)
        {
            if (IsDisplayedByID(Hooks.sessionSamsungAccount, signOutButtonID))
            {
                LogoutSamsungAccount();
            }

            LoginInSamsungaAccount(email, password);
        }

        /// <summary>
        /// This method performs login on Samsung Account application
        /// </summary>
        /// <param name="email">This parameter is the e-mail that should be sent to the signin fiedl</param>
        /// <param name="password">This parameter is the password that should be sent to the sign-in field</param>
        public void LoginInSamsungaAccount(string email, string password)
        {
            ClickGetStartedButton();
            InputLogin(email);
            ClickNextButton();
            InputPassword(password);
            ClickSignInButton();
        }

        /// <summary>
        /// Method to initialize the Samsung Account application
        /// </summary>
        public void OpenSamsungAccount()
        {
            Hooks.sessionSamsungAccount.LaunchApp();
        }

        /// <summary>
        /// This method perfoms log out on Samsung Account application
        /// </summary>
        public void PerformLogout()
        {
            VerifySamsungAccountsAppIsOpened();
            LogoutSamsungAccount();
        }

        /// <summary>
        /// This method perfoms sign in on Samsung Account with a not linked account with OneDrive
        /// </summary>
        public void PerformSiginWithNotLinkedOneDriveAccount()
        {
            LoginInSamsungaAccount(emailNotLinkedOnedrive, password);
        }

        /// <summary>
        /// This method perfoms sign in on Samsung Account with a not linked account with OneDrive
        /// </summary>
        public void PerformSiginWithChineseAccount()
        {
            LoginInSamsungaAccount(emailChineseAccount, password);
        }

        /// <summary>
        /// This method perfoms sign in on Samsung Account with a linked account with OneDrive
        /// </summary>
        public void PerformSiginWithLinkedOneDriveAccount()
        {
            LoginInSamsungaAccount(defaultEmail, password);
        }

        /// <summary>
        /// This method verifies that the Samsung Account is logged in on default account
        /// </summary>
        /// <returns>Returns true is the buttons is displayed else false.</returns>
        public bool VerifySamsungAccountIsNotLoggedIn()
        {
            Hooks.sessionSamsungAccount.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            return WaitIsDisplayedByNameBoolean(Hooks.sessionSamsungAccount, 2, signInName);
        }

        /// <summary>
        /// This method verifies that the Samsung Cloud menu paga is displayed on the Samsung Account
        /// </summary>
        /// <returns>Returns true if the Samsung Cloud menu page is displayed else false</returns>
        public bool VerifySamsungCloudMenuIsOpened()
        {
            return WaitIsDisplayedByID(Hooks.sessionSamsungAccount, 15, dataListTitleID);
        }
    }
}