// file="SamsungCloudAuthenticationPage.cs" 

using System;
using GalaxyCloud.Helpers;

namespace GalaxyCloud.Page
{
    /// <summary>
    /// This class contains the page objects and methods for Samsung Cloud authentication scenarios
    /// </summary>
    public class SamsungCloudAuthenticationPage : General
    {
        // Element locators based on the mapping table
        private const string welcomeTextID = "appSingInTitle"; // AccessibilityID for welcome text
        private const string getStartedButtonID = "appSingInButton"; // AccessibilityID for Get Started button
        private const string samsungCloudTextID = "NavigateCommadBar"; // AccessibilityID for Samsung Cloud text
        private const string samsungCloudTextName = "Samsung Cloud"; // Name for Samsung Cloud text

        // Samsung Account login screen element locators
        private const string emailFieldID = "iptLgnPlnID"; // AccessibilityID for email field
        private const string getStartedAccountButtonID = "GetStartedButton"; // AccessibilityID for Get started button
        private const string emailButtonID = "idSignInButton";
        private const string passwordFieldID = "iptLgnPlnPD"; // AccessibilityID for password field

        private const string nextButtonName = "Next"; // Name for Next button
        private const string signInButtonID = "signInButton"; // AccessibilityID for Sign in button

        // Credentials - Defined at the top for easy modification
        private readonly string samsungAccountEmail = Environment.GetEnvironmentVariable("SamsungCloudEmailAutomation") ?? "Name";
        private readonly string samsungAccountPassword = Environment.GetEnvironmentVariable("password") ?? "Pass";

        /// <summary>
        /// Verifies that the Samsung Cloud welcome text is displayed
        /// Locator priority: AccessibilityID > Name > ClassName > XPath
        /// </summary>
        /// <returns>True if the text is displayed, false otherwise</returns>
        public bool IsWelcomeTextDisplayed()
        {
            try
            {
                // Using AccessibilityID as first priority
                return IsDisplayedByID(Hooks.sessionSamsungCloud, welcomeTextID);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Verifies that the Get Started button is displayed
        /// Locator priority: AccessibilityID > Name > ClassName > XPath
        /// </summary>
        /// <returns>True if the button is displayed, false otherwise</returns>
        public bool IsGetStartedButtonDisplayed()
        {
            try
            {
                // Using AccessibilityID as first priority
                return IsDisplayedByID(Hooks.sessionSamsungCloud, getStartedButtonID);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Clicks the Get Started button
        /// Locator priority: AccessibilityID > Name > ClassName > XPath
        /// </summary>
        public void ClickGetStartedButton()
        {
            // Using AccessibilityID as first priority
            FindElementByID(Hooks.sessionSamsungCloud, getStartedButtonID).Click();
        }

        /// <summary>
        /// Verifies that the Samsung Account login screen is displayed
        /// This method checks if the Samsung Account session is active and waits for the login screen to be ready
        /// </summary>
        /// <returns>True if the login screen is displayed, false otherwise</returns>
        public bool IsSamsungAccountLoginScreenDisplayed()
        {
            try
            {
                // After clicking Get Started, we should have Samsung Account session
                // Check if the Samsung Account session is initialized
                if (Hooks.sessionSamsungAccount == null || Hooks.sessionSamsungAccount.SessionId == null)
                {
                    return false;
                }

                // Wait for the email field to be displayed, which indicates the login screen is ready
                // Using a 10-second timeout to allow enough time for the app to open
                return WaitIsDisplayedByID(Hooks.sessionSamsungAccount, 10, emailFieldID);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Enters Samsung Account credentials
        /// This method performs the complete login process on the Samsung Account screen
        /// </summary>
        public void EnterSamsungAccountCredentials()
        {
            // Ensure the Samsung Account session is available
            if (Hooks.sessionSamsungAccount == null)
            {
                Session session = new Session();
                Hooks.sessionSamsungAccount = session.InitializeSamsungAccount();
            }

            FindElementByID(Hooks.sessionSamsungAccount, getStartedAccountButtonID).Click(); // Click Get started button
            FindElementByID(Hooks.sessionSamsungAccount, emailButtonID).Click();

            // Enter email
            FillEntry(Hooks.sessionSamsungAccount, emailFieldID, samsungAccountEmail);

            // Click Next button (distinguished by Name since ID is shared with Sign in button)
            FindElementByName(Hooks.sessionSamsungAccount, nextButtonName).Click();

            // Wait a moment for the password field to appear
            System.Threading.Thread.Sleep(2000);

            // Enter password
            FillEntry(Hooks.sessionSamsungAccount, passwordFieldID, samsungAccountPassword);

            // Click Sign in button (distinguished by Name since ID is shared with Next button)
            FindElementByID(Hooks.sessionSamsungAccount, signInButtonID).Click();

            // Wait for login process to complete
            System.Threading.Thread.Sleep(20000);
        }

        /// <summary>
        /// Verifies that the Samsung Cloud text is displayed after login
        /// Locator priority: Name > AccessibilityID > ClassName > XPath
        /// </summary>
        /// <returns>True if the text is displayed, false otherwise</returns>
        public bool IsSamsungCloudTextDisplayed()
        {
            try
            {
                // Using Name as first priority based on mapping table
                return IsDisplayedByName(Hooks.sessionSamsungCloud, samsungCloudTextName);
            }
            catch (Exception)
            {
                try
                {
                    // Fallback to AccessibilityID
                    return IsDisplayedByID(Hooks.sessionSamsungCloud, samsungCloudTextID) &&
                           GetTextByID(Hooks.sessionSamsungCloud, samsungCloudTextID).Contains("Samsung Cloud");
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
