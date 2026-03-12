// file="NotesAppPage.cs" 

using System.Threading;
using GalaxyCloud.Helpers;

namespace GalaxyCloud.Page
{
    /// <summary>
    ///  This class is about the interactions on Samsung Notes application
    /// </summary>
    public class SamsungNotesPage : General
    {
        private const string notesSettingsButtonName = "Settings";
        private const string titleID = "FilterName";
        private const string notesSyncSwitchID = "CloudSyncSwitch";
        private const string toogleStateAttribute = "Toggle.ToggleState";

        /// <summary>
        /// This method verifies that the Samsung Notes application is opened
        /// </summary>
        public void VerifyNotesAppIsOpened()
        {
            IsDisplayedByID(Hooks.sessionNotes, titleID);
        }

        /// <summary>
        /// This method performs the click action on Samsung Notes settings button
        /// </summary>
        public void ClickNotesSettingsButton()
        {
            FindElementByName(Hooks.sessionNotes, notesSettingsButtonName).Click();
        }

        /// <summary>
        /// This method gets the toggle buttons status on Samsung Notes application
        /// </summary>
        /// <returns>Returns the string with the Notes status</returns>
        public string GetNotesToggleState()
        {
            return GetAttributeByID(Hooks.sessionNotes, notesSyncSwitchID, toogleStateAttribute);
        }

        /// <summary>
        /// This method performs the click action on the Samsung Notes toggle buton
        /// </summary>
        public void ClickNotesToggle()
        {
            FindElementByID(Hooks.sessionNotes, notesSyncSwitchID).Click();
        }

        /// <summary>
        /// This method changes the Samsung Notes toggle button
        /// </summary>
        /// <param name="status">This parameter is that status should be applied</param>
        /// <!--The thread sleep is necessary due to the delay to Samsung Notes two-way protocol work-->
        public void ChangeNotesToogleStatus(string status)
        {
            ClickNotesSettingsButton();

            if (GetNotesToggleState().Contains("1") && status.Contains("OFF"))
            {
                ClickNotesToggle();
            }
            else if (GetNotesToggleState().Contains("0") && status.Contains("ON"))
            {
                ClickNotesToggle();
            }

            Thread.Sleep(3000);
        }
    }
}
