// file="General.cs" 

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Threading;
using Castle.Core.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
namespace GalaxyCloud.Helpers
{
    /// <summary>
    /// Provides base methods used to manipulate windows elements
    /// After finding an element some interactions are allowed
    ///
    /// Actions
    ///    .Click() - Click at located element
    ///        Example: FindElementByID("myElement").Click();
    ///
    ///    .Clear() - Clears the element content
    ///        Example: FindElementByID("myTextBoxElement").Clear();
    ///
    ///    .SendKeys() - Simulates typing text into element
    ///        Example: FindElementByID("myTextBoxElement").SendKeys("Typing some text!");
    ///                 FindElementByID("myTextBoxElement").SendKeys(Keys.Enter); Using some key
    ///
    /// Attributes
    ///    .Text - Gets the text of the element
    ///        Example: string elementText = FindElementByID("myElement").Text;
    ///
    ///    .Displayed - Indicates if an element is displayed(true) or not(false)
    ///        Example:
    ///        try
    ///        {
    ///            FindElementByID("myElement").Displayed;
    ///        }
    ///        catch (WebDriverException)
    ///        {
    ///            return false
    ///        }
    ///
    ///    .Enabled - Indicates if an element is Enabled(true) or not (false)
    ///        Example: bool isElementEnabled = FindElementByID("myElement").Enabled;
    ///
    ///    .Selected - Indicates if an element is selected(true) or not (false)
    ///        Example: bool isElementSelected = FindElementByID("myElement").Selected;
    ///
    ///    .GetAttribute - Gets an element specific attribute
    ///        Example: string getElementNameAttribute = FindElementByID("myElement").GetAttribute("Attribute Name");
    /// </summary>
    public class General
    {
#pragma warning disable SA1600 // Elements should be documented
        #region Public Atributes
        public string canvasID = "LiveCanvas";
        public string gridViewCN = "GridView";
        public string gridViewItemCN = "GridViewItem";
        public string listViewCN = "ListView";
        public string listItemCN = "NamedContainerAutomationPeer";
        public string listViewItemCN = "ListViewItem";
        public string commandChangeLanguagePowerShell = "Set-WinUILanguageOverride -Language";
        public string commandEnableNetworksInterfaces = "Enable-NetAdapter -IncludeHidden -Name 'Ethernet*', 'Wi-Fi*', 'Cellular*' -Confirm:$false";
        public string commandDisableNetworksInterfaces = "/c netsh interface set interface \"{0}\" admin=disable";
        private const string regexLastSynced = @"((\/2023)|\,)";

#pragma warning restore SA1600 // Elements should be documented
        #endregion

        #region Finding Methods

        /// <summary>
        /// This method finds the element by the automation ID
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="elementID">This parameters is the automation ID that should be verified</param>
        /// <returns>Returns the element found</returns>
        public WindowsElement FindElementByID(WindowsDriver<WindowsElement> driver, string elementID)
        {
            return driver.FindElementByAccessibilityId(elementID);
        }

        /// <summary>
        /// This method finds the element by name
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="elementName">This parameters is the element name that should be verified</param>
        /// <returns>Returns the element found</returns>
        public WindowsElement FindElementByName(WindowsDriver<WindowsElement> driver, string elementName)
        {
            return driver.FindElementByName(elementName);
        }

        /// <summary>
        /// This method finds the element by the XPath
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="elementXPath">This parameters is the XPath element that should be verified</param>
        /// <returns>Returns the element found</returns>
        public WindowsElement FindElementByXPath(WindowsDriver<WindowsElement> driver, string elementXPath)
        {
            return driver.FindElementByXPath(elementXPath);
        }

        /// <summary>
        /// This method finds the element by the class name
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="elementClassName">This parameters is the class name element that should be verified</param>
        /// <returns>Returns the element found</returns>
        public WindowsElement FindElementByClassName(WindowsDriver<WindowsElement> driver, string elementClassName)
        {
            return driver.FindElementByClassName(elementClassName);
        }

        /// <summary>
        /// This method finds the elements by the class names
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="elementClassName">This parameters is the  element class name that should be verified</param>
        /// <returns>Returns the elements found</returns>
        public ReadOnlyCollection<WindowsElement> FindElementsByClassName(WindowsDriver<WindowsElement> driver, string elementClassName)
        {
            return driver.FindElementsByClassName(elementClassName);
        }

        /// <summary>
        /// This method finds the elements by the automation IDs
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="elementID">This parameters is the automation ID that should be verified</param>
        /// <returns>Returns the elements found</returns>
        public ReadOnlyCollection<WindowsElement> FindElementsByID(WindowsDriver<WindowsElement> driver, string elementID)
        {
            return driver.FindElementsById(elementID);
        }

        /// <summary>
        /// This method finds the elements by the element name
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="elementName">This parameters is the element name that should be verified</param>
        /// <returns>Returns the elements found</returns>
        public ReadOnlyCollection<WindowsElement> FindElementsByName(WindowsDriver<WindowsElement> driver, string elementName)
        {
            return driver.FindElementsByName(elementName);
        }
        #endregion
        #region Clicking Methods

        /// <summary>
        /// This method performs double click action on element by ID
        /// </summary>
        /// <param name="elementID">This parameters is the element ID</param>
        public void DoubleClickCloudElementByID(string elementID)
        {
            Actions act = new Actions(Hooks.sessionSamsungCloud);
            WindowsElement element = Hooks.sessionSamsungCloud.FindElementByAccessibilityId(elementID);
            act.DoubleClick(element).Perform();
        }

        #endregion

        /// <summary>
        /// This method verifies that element by automation ID is displayed
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="elementID">This parameters is the element automation ID that should be verified</param>
        /// <returns>Returns true if the element is displayed else false</returns>
        #region Verify Element Is Visible
        public bool IsDisplayedByID(WindowsDriver<WindowsElement> driver, string elementID)
        {
            try
            {
                return driver.FindElementByAccessibilityId(elementID).Displayed;
            }
            catch (WebDriverException)
            {
                return false;
            }
        }

        /// <summary>
        /// This method verifies that element by automation ID is displayed
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="elementID">This parameters is the element automation ID that should be verified</param>
        /// <param name="textExpected">This parameter is the text that is expeted to displayed</param>
        /// <returns>Returns true if the element is displayed else false</returns>
        public bool IsTextDisplayedById(WindowsDriver<WindowsElement> driver, string elementID, string textExpected)
        {
            try
            {
                if (GetTextByID(driver, elementID).Equals(textExpected))
                {
                    return true;
                }

                return false;
            }
            catch (WebDriverException)
            {
                return false;
            }
        }

        /// <summary>
        /// This method verifies that element by automation ID is displayed
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="elementID">This parameters is the element automation ID that should be verified</param>
        /// <param name="textExpected">This parameter is the text that is expeted to displayed</param>
        /// <returns>Returns true if the element is displayed else false</returns>
        public bool IsNotTextDisplayedById(WindowsDriver<WindowsElement> driver, string elementID, string textExpected)
        {
            try
            {
                if (!GetTextByID(driver, elementID).Equals(textExpected))
                {
                    return true;
                }

                return false;
            }
            catch (WebDriverException)
            {
                return false;
            }
        }

        /// <summary>
        /// This method verifies that element by name is displayed
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="elementName">This parameters is the element name that should be verified</param>
        /// <returns>Returns true if the element is displayed else false</returns>
        public bool IsDisplayedByName(WindowsDriver<WindowsElement> driver, string elementName)
        {
            try
            {
                return driver.FindElementByName(elementName).Displayed;
            }
            catch (WebDriverException)
            {
                return false;
            }
        }

        /// <summary>
        /// This method verifies that element by class name is displayed
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="elementCN">This parameters is the element class name that should be verified</param>
        /// <returns>Returns true if the element is displayed else false</returns>
        public bool IsDisplayedByCN(WindowsDriver<WindowsElement> driver, string elementCN)
        {
            try
            {
                return driver.FindElementByClassName(elementCN).Displayed;
            }
            catch (WebDriverException)
            {
                return false;
            }
        }

        /// <summary>
        /// This method verifies that element by automation ID is enabled
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="elementID">This parameters is the element automation ID that should be verified</param>
        /// <returns>Returns true if the element is enabled else false</returns>
        public bool IsEnabledByID(WindowsDriver<WindowsElement> driver, string elementID)
        {
            try
            {
                return driver.FindElementByAccessibilityId(elementID).Enabled;
            }
            catch (WebDriverException)
            {
                return false;
            }
        }

        /// <summary>
        /// This method verifies that element by name is enabled
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="elementName">This parameters is the element name that should be verified</param>
        /// <returns>Returns true if the element is enabled else false</returns>
        public bool IsEnabledByName(WindowsDriver<WindowsElement> driver, string elementName)
        {
            try
            {
                return driver.FindElementByName(elementName).Enabled;
            }
            catch (WebDriverException)
            {
                return false;
            }
        }

        /// <summary>
        /// This method verifies that element by automation ID is selected
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="elementID">This parameters is the element automation ID that should be verified</param>
        /// <returns>Returns true if the element is selected else false</returns>
        public bool IsSelectedByID(WindowsDriver<WindowsElement> driver, string elementID)
        {
            return driver.FindElementByAccessibilityId(elementID).Selected;
        }

        #endregion

        /// <summary>
        /// This method gets an attribute by element automation ID
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="elementID">This parameter is the element automation ID that should be verified</param>
        /// <param name="attribute">This parameter is the attribute that should be got</param>
        /// <returns>Retunrs the attribute in a string</returns>
        #region Getting Attributes
        public string GetAttributeByID(WindowsDriver<WindowsElement> driver, string elementID, string attribute)
        {
            return driver.FindElementByAccessibilityId(elementID).GetAttribute(attribute);
        }

        /// <summary>
        /// This method gets an attribute by class name element
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="elementCN">This parameter is the element class name that should be verified</param>
        /// <param name="attribute">This parameter is the attribute that should be got</param>
        /// <returns>Retunrs the attribute in a string</returns>
        public string GetAttributeByClassName(WindowsDriver<WindowsElement> driver, string elementCN, string attribute)
        {
            return driver.FindElementByClassName(elementCN).GetAttribute(attribute);
        }

        /// <summary>
        /// This method gets the text attribute by element automation ID
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="elementID">This parameter is the element automation ID that should be verified</param>
        /// <returns>Retunrs the attribute in a string</returns>
        public string GetTextByID(WindowsDriver<WindowsElement> driver, string elementID)
        {
            return driver.FindElementByAccessibilityId(elementID).Text;
        }

        /// <summary>
        /// This method gets the text attribute by element name
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="elementName">This parameter is the element name that should be verified</param>
        /// <returns>Retunrs the attribute in a string</returns>
        public string GetTextByName(WindowsDriver<WindowsElement> driver, string elementName)
        {
            return driver.FindElementByName(elementName).Text;
        }

        /// <summary>
        /// This method gets the text attribute by element class name
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="elementCN">This parameter is the element class name that should be verified</param>
        /// <returns>Retunrs the attribute in a string</returns>
        public string GetTextByClassName(WindowsDriver<WindowsElement> driver, string elementCN)
        {
            return driver.FindElementByClassName(elementCN).Text;
        }

        /// <summary>
        /// This method gets the text attribute by element index
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="elementCN">This parameter is the element class name that should be verified</param>
        /// <param name="index">This parameter is the element index that should be got</param>
        /// <returns>Retunrs the attribute in a string</returns>
        public string GetTextByIndex(WindowsDriver<WindowsElement> driver, string elementCN, int index)
        {
            return driver.FindElementsByClassName(elementCN).ElementAt(index).Text;
        }

        #endregion
        #region Others Methods

        /// <summary>
        /// This method gets the coordinates by element automation ID
        /// </summary>
        /// <returns>Returns the coordiantes</returns>
        public Point GetCanvasCoordinates()
        {
            Point coordinate = Hooks.sessionSamsungCloud.FindElementByAccessibilityId(canvasID).Coordinates.LocationInViewport;
            return coordinate;
        }

        /// <summary>
        /// This method sends the character to the element by automation ID
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="elementID">This parameter is the element automation ID that the key should be sent</param>
        /// <param name="value">This parameter is the value that should be sent</param>
        public void SendKeys(WindowsDriver<WindowsElement> driver, string elementID, string value)
        {
            driver.FindElementByAccessibilityId(elementID).SendKeys($"{value}");
        }

        /// <summary>
        /// This method fills inputs
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="elementID">This parameter is the element automation ID that the key should be sent</param>
        /// <param name="value">This parameter is the value that should be sent</param>
        public void FillEntry(WindowsDriver<WindowsElement> driver, string elementID, string value)
        {
            WindowsElement element = FindElementByID(driver, elementID);
            element.Click();
            element.Clear();
            element.SendKeys(value);
        }

        /// <summary>
        /// This method gets the current Windows date and time
        /// </summary>
        /// <returns>Returns the date and time in a string</returns>
        public string GetCurrentDateAndTime()
        {
            return DateTime.Now.ToString("M/d, h:mm");
        }

        /// <summary>
        /// This method waits for the given time or until the element by automation ID is enabled
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="time">This parameter is the time thah should be waited</param>
        /// <param name="elementID">This parameter is the element automation ID that the element should be enabled</param>
        public void WaitIsEnabledByID(WindowsDriver<WindowsElement> driver, double time, string elementID)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
            wait.Until(x => IsEnabledByID(driver, elementID));
        }

        /// <summary>
        /// This method waits for the given time or until the element by automation ID is enabled
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="time">This parameter is the time thah should be waited</param>
        /// <param name="elementID">This parameter is the element automation ID that the element should be displayed</param>
        /// <param name="textExpected">This parameter is the text that should be displayed</param>
        /// <returns>Returns true or false</returns>
        public bool WaitTextIsNotDisplayedByID(WindowsDriver<WindowsElement> driver, double time, string elementID, string textExpected)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
            return wait.Until(_ => IsNotTextDisplayedById(driver, elementID, textExpected));
        }

        /// <summary>
        /// This method waits for the given time or until the element by automation ID is enabled
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="time">This parameter is the time thah should be waited</param>
        /// <param name="elementID">This parameter is the element automation ID that the element should be displayed</param>
        /// <param name="textExpected">This parameter is the text that should be displayed</param>
        /// <returns>Returns true or false</returns>
        public bool WaitTextIsDisplayedByID(WindowsDriver<WindowsElement> driver, double time, string elementID, string textExpected)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
            return wait.Until(_ => IsTextDisplayedById(driver, elementID, textExpected));
        }

        /// <summary>
        /// This method waits for the given time or until the element by automation ID  is displayed
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="time">This parameter is the time thah should be waited</param>
        /// <param name="elementID">This parameter is the element automation ID that the element should be displayed</param>
        /// <returns>Returns true or false</returns>
        public bool WaitIsDisplayedByID(WindowsDriver<WindowsElement> driver, double time, string elementID)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
                return wait.Until(x => IsDisplayedByID(driver, elementID));
            }
            catch (WebDriverException)
            {
                return false;
            }
        }

        /// <summary>
        /// This method waits for the given time or until the element by name is displayed
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="time">This parameter is the time thah should be waited</param>
        /// <param name="elementName">This parameter is the element by name that the element should be displayed</param>
        public void WaitIsDisplayedByName(WindowsDriver<WindowsElement> driver, double time, string elementName)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
            wait.Until(x => IsDisplayedByName(driver, elementName));
        }

        /// <summary>
        /// This method waits for the given time or until the element by name is displayed
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="time">This parameter is the time thah should be waited</param>
        /// <param name="elementName">This parameter is the element by name that the element should be displayed</param>
        /// <returns>This method returns true if the </returns>
        public bool WaitIsDisplayedByNameBoolean(WindowsDriver<WindowsElement> driver, double time, string elementName)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
                return wait.Until(x => IsDisplayedByName(driver, elementName));
            }
            catch (WebDriverException)
            {
                return false;
            }
        }

        /// <summary>
        /// This method waits for the given time or until the element by class name is displayed
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="time">This parameter is the time thah should be waited</param>
        /// <param name="elementCN">This parameter is the element by class name that the element should be displayed</param>
        public void WaitIsDisplayedByCN(WindowsDriver<WindowsElement> driver, double time, string elementCN)
        {
            WebDriverWait wait = new WebDriverWait(Hooks.sessionSamsungCloud, TimeSpan.FromSeconds(time));
            wait.Until(x => IsDisplayedByCN(driver, elementCN));
        }

        /// <summary>
        /// This method waits for the given time or until the element text by automation ID is displayed
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="time">This parameter is the time thah should be waited</param>
        /// <param name="elementID">This parameter is the element automation ID that the element should be displayed</param>
        public void WaitElementTextIsDisplayedByID(WindowsDriver<WindowsElement> driver, double time, string elementID)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
            wait.Until(x => !GetTextByID(driver, elementID).IsNullOrEmpty());
        }

        /// <summary>
        /// This method waits for the given time or until the element text by class name is displayed
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="time">This parameter is the time thah should be waited</param>
        /// <param name="elementCN">This parameter is the element class name that the element should be displayed</param>
        public void WaitElementTextIsDisplayedByCN(WindowsDriver<WindowsElement> driver, double time, string elementCN)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
            wait.Until(x => !GetTextByClassName(driver, elementCN).IsNullOrEmpty());
        }

        /// <summary>
        /// This method waits for the given time or until the element by name is displayed
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="time">This parameter is the time thah should be waited</param>
        /// <param name="elementName">This parameter is the element name that the element should be displayed</param>
        public void WaitElementTextIsDisplayedByName(WindowsDriver<WindowsElement> driver, double time, string elementName)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
            wait.Until(x => !GetTextByName(driver, elementName).IsNullOrEmpty());
        }

        /// <summary>
        /// This method waits for the given time or until the element by index is displayed
        /// </summary>
        /// <param name="driver">This parameter is the session that should be handled</param>
        /// <param name="time">This parameter is the time to wait</param>
        /// <param name="elementCN">This parameter is the name of the element class that the text should be displayed for</param>
        /// <param name=" index">This parameter is the index of the element that the text should be checked/param>
        public void WaitElementTextIsDisplayedByIndex(WindowsDriver<WindowsElement> driver, double time, string elementCN, int index)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
            wait.Until(x => !GetTextByIndex(driver, elementCN, index).IsNullOrEmpty());
        }

        /// <summary>
        /// This method waits for the given time or until the element by name is not displayed
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="time">This parameter is the time thah should be waited</param>
        /// <param name="elementCN">This parameter is the element class name that the element should be displayed</param>
        public void WaitElementIsNotDisplayedByCN(WindowsDriver<WindowsElement> driver, double time, string elementCN)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
            wait.Until(x => IsDisplayedByCN(driver, elementCN) == false);
        }

        /// <summary>
        /// This method waits for the given time or until the text of the element by automation ID is displayed
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="time">This parameter is the time that should be waited</param>
        /// <param name="elementID">This parameter is the element automation ID that should be verified </param>
        /// <param name="atribute">This parameter is the attribute that text should be verified</param>
        /// <param name="text">This parameter is the text that should be verified</param>
        public void WaitAtributeTextIsDisplayedByID(WindowsDriver<WindowsElement> driver, double time, string elementID, string atribute, string text)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
            wait.Until(x => GetAttributeByID(driver, elementID, atribute).Contains(text));
        }

        /// <summary>
        /// This method waits for the given time or until the element by automation ID is enabled
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="time">This parameter is the time thah should be waited</param>
        /// <param name="elementID">This parameter is the element automation ID that the element should be enabled</param>
        /// <returns>Return true if the element is selected else false</returns>
        public bool WaitIsSelectedByID(WindowsDriver<WindowsElement> driver, double time, string elementID)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
                return wait.Until(x => IsSelectedByID(driver, elementID));
            }
            catch (WebDriverException)
            {
                return false;
            }
        }

        /// <summary>
        /// This method gets the int of a string
        /// </summary>
        /// <param name="inputado">This parameter is the string that must be removed from the integer</param>
        /// <returns>Returns the string with the int</returns>
        public string GetIntInString(string inputado)
        {
            return new string(inputado.Where(char.IsDigit).ToArray());
        }

        /// <summary>
        /// This method checks the screen components for one that matches the structure of the regex
        /// </summary>
        /// <param name="syncInfoRegex">This parameter is the regex format</param>
        /// <param name="element">This parameter is the componente type that should be searched</param>
        /// <returns>Returns the component that matched with the regex</returns>
        public string RegexSyncInfo(string syncInfoRegex, string element)
        {
            Regex regexSyncInfo = new Regex(syncInfoRegex,
                 RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var regexFound = regexSyncInfo.Match(element);

            return regexFound.Value;
        }

        /// <summary>
        /// This method gets the last synced information and puts on the correct format
        /// </summary>
        /// <param name="input">This parameter is the string that will be searched with the regular expression</param>
        /// <returns>Returns the string replaced</returns>
        public string RegexLastSynced(string input)
        {
            string pattern = regexLastSynced;
            string substitution = string.Empty;
            Regex regex = new Regex(pattern);
            return regex.Replace(input, substitution);
        }

        /// <summary>
        /// This method sets the time of the timeout of the searching the elements
        /// </summary>
        /// <param name="driver">This parameter is the session that should be manipulated</param>
        /// <param name="timeOut">This parameter sets the time that should be applied to the time out function</param>
        public void SetDefaultCloudTimeOut(WindowsDriver<WindowsElement> driver, double timeOut)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeOut);
        }

        /// <summary>
        /// This method executes commands on the Windows CMD
        /// </summary>
        /// <param name="command">This paramete is the command shoudl be executed</param>
        /// <returns>Returns the outuput of the Windows CMD</returns>
        public string ExecutePSCommand(string command)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Verb = "runas",
                FileName = @"powershell.exe",
                Arguments = command,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            Process process = new Process
            {
                StartInfo = startInfo
            };
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return output;
        }

        /// <summary>
        /// This method changes the Windows system language
        /// </summary>
        /// <param name="language">This parameter is the language that should be applied on the operational system</param>
        public void ChangeWindowsLanguage(string language)
        {
            ExecutePSCommand(commandChangeLanguagePowerShell + language);
        }

        /// <summary>
        /// This method gets the last sync information
        /// </summary>
        /// <param name="appSyncedText">This parameter is the sync info of the Samsung application</param>
        /// <param name="cloudSyncedText">This parameter is the syn info or the Samsung application inside the Samsung Cloud</param>
        /// <returns>Returns true if the texts matchs else false</returns>
        public bool GetLastSyncInformationApplications(string appSyncedText, string cloudSyncedText)
        {
            bool isValid = false;
            string[] splitStrOne = appSyncedText.Split(' ');
            string[] splitStrTwo = cloudSyncedText.Split(' ');

            foreach (var str in splitStrOne)
            {
                if (str == splitStrTwo[5])
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        /// <summary>
        /// This method lists the interfaces name that are type of Ethernet, Wireless or Mobile
        /// </summary>
        /// <returns>Returns the list with interface name</returns>
        public string[] GetInterfaceNames()
        {
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            List<string> names = new List<string>();
            foreach (NetworkInterface iface in interfaces)
            {
                if (iface.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                    iface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                    iface.NetworkInterfaceType == NetworkInterfaceType.Wwanpp ||
                    iface.NetworkInterfaceType == NetworkInterfaceType.Wwanpp2)
                {
                    names.Add(iface.Name);
                }
            }

            return names.ToArray();
        }

        /// <summary>
        /// This method disables the networks interfaces found with the method GetInterfaceNames
        /// </summary>
        public void DisableNetworksInterfaces()
        {
            string[] interfaceNames = GetInterfaceNames();
            foreach (string name in interfaceNames)
            {
                ExecuteCmdCommand(string.Format(commandDisableNetworksInterfaces, name));
            }
        }

        /// <summary>
        /// This method enables all interfaces disconnected that matched with the parameters
        /// </summary>
        public void EnableDisconnectedInterfaces()
        {
            ExecuteCmdCommand(commandEnableNetworksInterfaces);
            WaitForNetworkInterfaces();
        }

        /// <summary>
        /// This method waits until the at least on network interface is active
        /// </summary>
        public void WaitForNetworkInterfaces()
        {
            List<NetworkInterface> interfaces;
            bool anyEnabled;
            AutoResetEvent anyEnabledEvent = new AutoResetEvent(false);
            Timer timer = new Timer((state) =>
            {
                interfaces = NetworkInterface.GetAllNetworkInterfaces()
                    .Where(iface =>
                        iface.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                        iface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                        iface.NetworkInterfaceType == NetworkInterfaceType.Wwanpp ||
                        iface.NetworkInterfaceType == NetworkInterfaceType.Wwanpp2)
                    .ToList();
                anyEnabled = false;
                foreach (NetworkInterface networkInterface in interfaces)
                {
                    if (networkInterface.OperationalStatus == OperationalStatus.Up)
                    {
                        anyEnabled = true;
                        break;
                    }
                }

                if (anyEnabled)
                {
                    anyEnabledEvent.Set();
                }
            }, null,
            TimeSpan.Zero,
            TimeSpan.FromSeconds(1));
            anyEnabledEvent.WaitOne();
            timer.Dispose();
        }

        /// <summary>
        /// This method perform command on CMD
        /// </summary>
        /// <param name="command">This parameter is the command that should be performed</param>
        public void ExecuteCmdCommand(string command)
        {
            Process process = new Process();
            process.StartInfo.FileName = "powershell.exe";
            process.StartInfo.Arguments = command;
            process.StartInfo.Verb = "runas";
            process.Start();
            process.WaitForExit();
        }

        /// <summary>
        /// This method verifies that the input match with the regex pattern
        /// </summary>
        /// <param name="input">This parameter is the input that verified</param>
        /// <param name="pattern">This parameter is the pattern that inut should be matched</param>
        /// <returns>Returns the boolean if the input matchs</returns>
        public bool VerifyRegexMatchs(string input, string pattern)
        {
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// This method verifies if the specified application is open.
        /// </summary>
        /// <param name="applicationName">The name of the application to verify.</param>
        /// <returns>True if the application is open; otherwise, false.</returns>
        public bool VerifyProcessIsRunning(string applicationName)
        {
            Thread.Sleep(3000);
            bool isRunning = Process.GetProcesses().Any(process => process.ProcessName.Equals(applicationName, StringComparison.OrdinalIgnoreCase));

            return isRunning;
        }
        #endregion

    }
}