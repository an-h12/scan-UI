// file="Session.cs" 

using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace GalaxyCloud.Helpers
{
    /// <summary>
    /// This class is about the interactions with the sessions drives
    /// </summary>
    public class Session
    {
        #region Class Constructor and Variables

        private readonly string galleryPackageID = TestContext.Parameters["galleryPackageID"];
        private readonly string notesPackageID = TestContext.Parameters["notesPackageID"];
        private readonly string settingsPackageID = TestContext.Parameters["settingsPackageID"];
        private readonly string samsungAccountPackageID = TestContext.Parameters["samsungAccountPackageID"];
        private readonly string samsungPassPackageID = TestContext.Parameters["samsungPassPackageID"];
        private readonly string samsungCloudPackageID = TestContext.Parameters["samsungCloudPackageID"];
        private readonly string winAppDriverIP = $"http://{TestContext.Parameters["localIP"]}:{TestContext.Parameters["port"]}";

        /// <summary>
        /// The constructor of the Samsung Gallery driver
        /// </summary>
        public WindowsDriver<WindowsElement> DriverGallery { get; set; }

        /// <summary>
        /// The constructor of the Samsung Notes driver
        /// </summary>
        public WindowsDriver<WindowsElement> DriverNotes { get; set; }

        /// <summary>
        /// The constructor of the Samsung Settings driver
        /// </summary>
        public WindowsDriver<WindowsElement> DriverSettings { get; set; }

        /// <summary>
        /// The constructor of the Samsung Account driver
        /// </summary>
        public WindowsDriver<WindowsElement> DriverSamsungAccount { get; set; }

        /// <summary>
        /// The constructor of the Samsung Pass driver
        /// </summary>
        public WindowsDriver<WindowsElement> DriverSamsungPass { get; set; }

        /// <summary>
        /// The constructor of the Samsung Client Assistant
        /// </summary>
        public WindowsDriver<WindowsElement> DriverSamsungCloud { get; set; }

        #endregion

        #region Sample: Initialize With Win App Driver
        /* private const string applicationUrl = "http://127.0.0.1:4723";
         public void Initialize()
         {
             AppiumOptions desiredCapabilites = new AppiumOptions();
             desiredCapabilites.AddAdditionalCapability("app", @$"{cloudPackageID}");
             Driver = new WindowsDriver<WindowsElement>(new Uri($"{applicationUrl}"), desiredCapabilites);
             Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
         }*/
        #endregion
        #region Public Methods

        /// <summary>
        /// This methods initialize the Samsung Gallery session
        /// </summary>
        /// <returns>Returns the session driver</returns>
        public WindowsDriver<WindowsElement> InitializeGallery()
        {
            AppiumOptions desiredCapabilities = new AppiumOptions();
            desiredCapabilities.AddAdditionalCapability("app", galleryPackageID);
            desiredCapabilities.AddAdditionalCapability("platformName", "Windows");
            desiredCapabilities.AddAdditionalCapability("waitForAppLaunch", 60);

            var startTime = DateTime.Now;
            bool appStarted = false;

            while ((DateTime.Now - startTime).TotalSeconds < 30)
            {
                try
                {
                    DriverGallery = new WindowsDriver<WindowsElement>(new Uri(winAppDriverIP), desiredCapabilities);
                    appStarted = true;
                    break;
                }
                catch (WebDriverException ex)
                {
                    Console.WriteLine("Trying to start the application... Error: " + ex.Message);
                    Thread.Sleep(5000);
                }
            }

            if (!appStarted)
            {
                throw new InvalidOperationException("The application was not started correctly.");
            }

            Assert.IsNotNull(DriverGallery);
            DriverGallery.Manage().Window.Maximize();
            DriverGallery.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            return DriverGallery;
        }

        /// <summary>
        /// This methods initialize the Samsung Notes session
        /// </summary>
        /// <returns>Returns the session driver</returns>
        public WindowsDriver<WindowsElement> InitializeNotes()
        {
            AppiumOptions desiredCapabilities = new AppiumOptions();
            desiredCapabilities.AddAdditionalCapability("app", notesPackageID);
            desiredCapabilities.AddAdditionalCapability("platformName", "Windows");
            desiredCapabilities.AddAdditionalCapability("waitForAppLaunch", 15);
            DriverNotes = new WindowsDriver<WindowsElement>(new Uri(winAppDriverIP), desiredCapabilities);

            Assert.IsNotNull(DriverNotes);
            DriverNotes.Manage().Window.Maximize();
            DriverNotes.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            return DriverNotes;
        }

        /// <summary>
        /// This methods initialize the Windows settings session
        /// </summary>
        /// <returns>Returns the session driver</returns>
        public WindowsDriver<WindowsElement> InitializeSettings()
        {
            AppiumOptions desiredCapabilities = new AppiumOptions();
            desiredCapabilities.AddAdditionalCapability("app", settingsPackageID);
            desiredCapabilities.AddAdditionalCapability("platformName", "Windows");
            desiredCapabilities.AddAdditionalCapability("waitForAppLaunch", 15);
            DriverSettings = new WindowsDriver<WindowsElement>(new Uri(winAppDriverIP), desiredCapabilities);

            Assert.IsNotNull(DriverSettings);
            DriverSettings.Manage().Window.Maximize();
            DriverSettings.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            return DriverSettings;
        }

        /// <summary>
        /// This methods initialize the Samsung Account session
        /// </summary>
        /// <returns>Returns the session driver</returns>
        public WindowsDriver<WindowsElement> InitializeSamsungAccount()
        {
            AppiumOptions desiredCapabilities = new AppiumOptions();
            desiredCapabilities.AddAdditionalCapability("app", samsungAccountPackageID);
            desiredCapabilities.AddAdditionalCapability("platformName", "Windows");
            desiredCapabilities.AddAdditionalCapability("waitForAppLaunch", 15);
            DriverSamsungAccount = new WindowsDriver<WindowsElement>(new Uri(winAppDriverIP), desiredCapabilities);

            Assert.IsNotNull(DriverSamsungAccount);
            DriverSamsungAccount.Manage().Window.Maximize();
            DriverSamsungAccount.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            return DriverSamsungAccount;
        }

        /// <summary>
        /// This methods initialize the Samsung Account session
        /// </summary>
        /// <returns>Returns the session driver</returns>
        public WindowsDriver<WindowsElement> InitializePass()
        {
            AppiumOptions desiredCapabilities = new AppiumOptions();
            desiredCapabilities.AddAdditionalCapability("app", samsungPassPackageID);
            desiredCapabilities.AddAdditionalCapability("platformName", "Windows");
            desiredCapabilities.AddAdditionalCapability("waitForAppLaunch", 15);
            DriverSamsungPass = new WindowsDriver<WindowsElement>(new Uri(winAppDriverIP), desiredCapabilities);

            Assert.IsNotNull(DriverSamsungPass);
            DriverSamsungPass.Manage().Window.Maximize();
            DriverSamsungPass.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            return DriverSamsungPass;
        }

        /// <summary>
        /// This methods initialize the SCA Client session
        /// </summary>
        /// <returns>Returns the session driver</returns>
        public WindowsDriver<WindowsElement> InitializeSamsungCloud()
        {
            try
            {
                AppiumOptions desiredCapabilities = new AppiumOptions();
                desiredCapabilities.AddAdditionalCapability("app", samsungCloudPackageID);
                desiredCapabilities.AddAdditionalCapability("platformName", "Windows");
                desiredCapabilities.AddAdditionalCapability("waitForAppLaunch", 15);
                DriverSamsungCloud = new WindowsDriver<WindowsElement>(new Uri(winAppDriverIP), desiredCapabilities);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                AppiumOptions desiredCapabilities = new AppiumOptions();
                desiredCapabilities.AddAdditionalCapability("app", samsungCloudPackageID);
                desiredCapabilities.AddAdditionalCapability("platformName", "Windows");
                DriverSamsungCloud = new WindowsDriver<WindowsElement>(new Uri(winAppDriverIP), desiredCapabilities);
            }

            Assert.IsNotNull(DriverSamsungCloud);
            DriverSamsungCloud.Manage().Window.Maximize();
            DriverSamsungCloud.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            return DriverSamsungCloud;
        }

        /// <summary>
        /// This method closes the session
        /// </summary>
        /// <param name="appDriver">This parameter gets the session drive that should be closed</param>
        public void Close(WindowsDriver<WindowsElement> appDriver)
        {
            appDriver?.Quit();
        }
        #endregion
    }
}