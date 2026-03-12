// file="Hooks.cs" 

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GalaxyCloud.Page;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Windows;
using TechTalk.SpecFlow;

namespace GalaxyCloud.Helpers
{
    /// <summary>
    /// Performs specific actions before or after a feature step, scenario or the entire feature
    ///     Getting data from a Scenario or Step.
    ///         Get Step name -> TestContext.CurrentContext.Test.Name
    ///         Get Scenario name -> ScenarioStepContext.Current.StepInfo.Text
    /// </summary>
    [SetUpFixture]
    [Binding]
    public class Hooks : Session
    {
#pragma warning disable SA1600 // Elements should be documented
        public static WindowsDriver<WindowsElement> sessionSettings;
        public static WindowsDriver<WindowsElement> sessionSamsungCloud;
        public static WindowsDriver<WindowsElement> sessionGallery;
        public static WindowsDriver<WindowsElement> sessionNotes;
        public static WindowsDriver<WindowsElement> sessionBluetoothSync;
        public static WindowsDriver<WindowsElement> sessionSamsungAccount;
        public static WindowsDriver<WindowsElement> sessionPass;
        public static WindowsDriver<WindowsElement> sessionSettingsRuntime;
        public static Dictionary<string, string> dictAdaptersAndStatus = new Dictionary<string, string>();
#pragma warning restore SA1600 // Elements should be documented

        private const string galleryAppName = "\"Samsung Gallery\" app is opened";
        private const string cloudAppName = "\"Samsung Cloud\" app is opened";
        private const string passAppName = "\"Samsung Pass\" app is opened";
        private const string notesAppName = "\"Samsung Notes\" app is opened";
        private const string accountAppName = "\"Samsung Account\" app is opened";

        private static readonly General general = new General();
        private static readonly SamsungGalleryPage gallery = new SamsungGalleryPage();
        private static readonly SamsungAccountPage account = new SamsungAccountPage();

        /// <summary>
        /// <code>[OneTimeTearDown]</code> Generate <see langword="LivingDoc.html"/> report after entire test execution
        /// </summary>
        [AfterTestRun]
        public static void GenerateTestReport()
        {
            Task.Factory.StartNew(async () =>
            {
                // Generate report with test result and execution time
                string reportName = $"Cloud_Test_Report_{Regex.Replace(DateTime.Now.ToString(), "[^0-9a-zA-Z]+", "_")}.html";
                // Initializing a new process
                Process processLivingDoc = new Process();
                // Process to be started
                processLivingDoc.StartInfo.FileName = "livingdoc";
                // Path where the process will be execeuted
                processLivingDoc.StartInfo.WorkingDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                // Arguments used for livingdoc command + output directory and file name
                processLivingDoc.StartInfo.Arguments = "test-assembly GalaxyCloud.dll -t TestExecution.json -o ..\\..\\..\\Reports\\" + reportName;
                // Starting Execution
                processLivingDoc.Start();
                // Waiting for closing the process
                processLivingDoc.WaitForExit();
                // Delay for executing task
                await Task.Delay(10_000);
            });
        }

        /// <summary>
        /// Initialize the Samsung Cloud sync viewer sample if the tag WithoutSample is not present.
        /// </summary>
        [Before]
        public void Before()
        {
            sessionSamsungCloud = InitializeSamsungCloud();
        }

        /// <summary>
        /// Initialize the Samsung Cloud Assistant application
        /// </summary>
        [BeforeStep("SamsungCloud")]
        public void BeforeScaStep()
        {
            if (ScenarioStepContext.Current.StepInfo.Text.Contains(cloudAppName))
            {
                sessionSamsungCloud = InitializeSamsungCloud();
            }
        }

        /// <summary>
        /// Initialize Samsung Gallery application
        /// </summary>
        [BeforeStep("Gallery")]
        public void BeforeGalleryStep()
        {
            if (ScenarioStepContext.Current.StepInfo.Text.Contains(galleryAppName))
            {
                sessionGallery = InitializeGallery();
            }
        }

        /// <summary>
        ///  Initialize Samsung Gallery application
        /// </summary>
        [BeforeStep("Pass")]
        public void BeforePassStep()
        {
            if (ScenarioStepContext.Current.StepInfo.Text.Contains(passAppName))
            {
                sessionPass = InitializePass();
            }
        }

        /// <summary>
        /// Initialize the Samsung Notes application
        /// </summary>
        [BeforeStep("Notes")]
        public void BeforeNotesStep()
        {
            if (ScenarioStepContext.Current.StepInfo.Text.Contains(notesAppName))
            {
                sessionNotes = InitializeNotes();
            }
        }

        /// <summary>
        /// Closes the Samsung Notes session after test execution
        /// </summary>
        [AfterScenario("Notes")]
        public void AfterNotesScenario()
        {
            Close(sessionNotes);
        }

        /// <summary>
        /// Initialize the Windows Settings
        /// </summary>
        [BeforeStep("Settings")]
        public void BeforeSettingsStep()
        {
            if (ScenarioStepContext.Current.StepInfo.Text.Contains("\"Windows Settings\""))
            {
                sessionSettings = InitializeSettings();
            }
        }

        /// <summary>
        /// Initialize the Samsung Account application
        /// </summary>
        [BeforeStep("SamsungAccount")]
        public void BeforeSamsungAccountStep()
        {
            if (ScenarioStepContext.Current.StepInfo.Text.Contains(accountAppName))
            {
                sessionSamsungAccount = InitializeSamsungAccount();
            }
        }

        /// <summary>
        /// Closes the Windows Settings after test execution
        /// </summary>
        [AfterScenario("Settings")]
        public void AfterSettingsScenario()
        {
            Close(sessionSettings);
        }

        /// <summary>
        /// Closes the Samsung Bluetooth Sync session after test execution
        /// </summary>
        [AfterScenario("Pass")]
        public void AfterPassScenario()
        {
            Close(sessionPass);
        }

        /// <summary>
        /// This method deletes one picture on the Samsung Gallery application
        /// </summary>
        [AfterScenario("DeleteGalleryImage")]
        public void DeleteGalleryImage()
        {
            gallery.DeleteGalleryPicture();
        }

        /// <summary>
        /// This method performs signin with default account on Samsung Gallery
        /// </summary>
        [AfterScenario("DefaultAccount")]
        public void AfterAccountScenario()
        {
            string defaultEmail = Environment.GetEnvironmentVariable("SamsungAccountEmailAutomationOnedrive");
            string defaultPassword = Environment.GetEnvironmentVariable("password");

            sessionSamsungAccount = InitializeSamsungAccount();
            account.IsUserLogoutSamsungAccount(defaultEmail, defaultPassword);
            account.WaitElementTextIsDisplayedByName(Hooks.sessionSamsungAccount, 5, "Profile info");
            Close(sessionSamsungAccount);
        }

        /// <summary>
        /// Closes the Samsung Account application
        /// </summary>
        [AfterStep("SamsungAccount")]
        public void AfterSamsungAccountStep()
        {
            Close(sessionSamsungAccount);
        }

        /// <summary>
        /// Performs login with Samsung Account linked with OneDrive on Samsung Gallery after test execution
        /// </summary>
        [AfterScenario("Samsung")]
        public void AfterSamsungScenario()
        {
            string defaultEmail = Environment.GetEnvironmentVariable("SamsungAccountEmailAutomationOnedrive");
            string defaultPassword = Environment.GetEnvironmentVariable("password");

            sessionSamsungAccount = InitializeSamsungAccount();
            account.IsUserLogoutSamsungAccount(defaultEmail, defaultPassword);
            account.WaitElementTextIsDisplayedByName(Hooks.sessionSamsungAccount, 5, "Profile info");
            Close(sessionSamsungAccount);
        }

        /// <summary>
        /// Changes the Windows language to default after test execution
        /// </summary>
        [AfterScenario("DefaultLanguage")]
        public void DefaultLanguageScenario()
        {
            general.ChangeWindowsLanguage("en-US");
        }

        /// <summary>
        /// Changes the Windows network adpater status to default
        /// </summary>
        [AfterScenario("EnableNetwork")]
        public void AfterChangeInternetAdapterStatus()
        {
            general.EnableDisconnectedInterfaces();
        }

        /// <summary>
        /// Closes the Samsung Cloud Assistant session after test execution
        /// </summary>
        [AfterScenario("SamsungCloud")]
        public void AfterScaScenario()
        {
            Close(sessionSamsungCloud);
        }

        /// <summary>
        /// Closes the Samsung Gallery session after test execution
        /// </summary>
        [AfterScenario("Gallery")]
        public void AfterGalleryScenario()
        {
            Close(sessionGallery);
        }

        /// <summary>
        /// Closes the Samsung Cloud sample after test execution.
        /// </summary>
        /// <param name="scenarioContext">Provides information about the current scenario.</param>
        [AfterScenario]
        public void AfterScenario()
        {
            Close(sessionSamsungCloud);
        }
    }
}