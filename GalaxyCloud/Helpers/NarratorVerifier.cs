// file="Accessibility_SamsungCloudNarratorSupportStepDefinitions.cs"

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.EventHandlers;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using FlaUI.UIA3;

namespace GalaxyCloud.Helpers
{
    /// <summary>
    /// Class lưu trữ thông tin sự kiện Narrator.
    /// </summary>
    public class NarratorEvent
    {
        /// <summary>
        /// Gets or sets tên của element.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets loại control của element.
        /// </summary>
        public string ControlType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets trạng thái của element.
        /// </summary>
        public string State { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets thời gian xảy ra sự kiện.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Trả về chuỗi biểu diễn của sự kiện Narrator.
        /// </summary>
        /// <returns>Chuỗi biểu diễn sự kiện với định dạng [HH:mm:ss] Name | ControlType | State.</returns>
        public override string ToString() => $"[{Timestamp:HH:mm:ss}] {Name} | {ControlType} | {State}";
    }

    /// <summary>
    /// Class hỗ trợ kiểm tra Narrator, Speech Recap và điều hướng Focus.
    /// </summary>
    public class NarratorVerifier : IDisposable
    {
        #region Constants & Fields
        // Constants cho Windows 11 Speech Recap
        private const string RECAP_WINDOW_CLASS = "NarratorSpeechRecapDialog";
        private const string RECAP_TEXTBOX_ID = "SpeechHistoryTextBox";
        private const int MAX_RETRIES = 3;

        // Core Automation
        private readonly UIA3Automation _automation;
        private FocusChangedEventHandlerBase? _focusHandler;
        private readonly object _lockObject = new object();
        
        /// <summary>
        /// Gets danh sách các sự kiện Narrator đã ghi nhận.
        /// </summary>
        public List<NarratorEvent> Events { get; private set; }
        #endregion

        /// <summary>
        /// Khởi tạo NarratorVerifier.
        /// </summary>
        public NarratorVerifier()
        {
            // --- QUAN TRỌNG: Phải khởi tạo Automation object ---
            _automation = new UIA3Automation(); 
            Events = new List<NarratorEvent>();
        }

        #region Public Methods - Window & Focus Control

        /// <summary>
        /// Kích hoạt cửa sổ theo tên (Làm cho cửa sổ nổi lên trên cùng).
        /// Dùng để quay lại App sau khi mở Speech Recap.
        /// </summary>
        /// <param name="windowName">Tên của cửa sổ cần kích hoạt.</param>
        public void ActivateWindow(string windowName)
        {
            try
            {
                var desktop = _automation.GetDesktop();
                // Tìm chính xác hoặc tương đối
                var appWindow = desktop.FindFirstChild(cf => cf.ByName(windowName)) 
                             ?? desktop.FindAllChildren(cf => cf.ByControlType(ControlType.Window))
                                       .FirstOrDefault(w => w.Name.Contains(windowName));

                if (appWindow != null)
                {
                    Console.WriteLine($"[NarratorVerifier] Activating window: '{appWindow.Name}'");
                    appWindow.Focus();
                    Thread.Sleep(500); // Chờ window nổi lên
                }
                else
                {
                    Debug.WriteLine($"[Warning] Cannot find window to activate: {windowName}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error activating window: {ex.Message}");
            }
        }

        /// <summary>
        /// Gửi phím Tab để di chuyển focus đến element tiếp theo.
        /// </summary>
        public void SendTabKey()
        {
            Keyboard.Press(VirtualKeyShort.TAB);
            Keyboard.Release(VirtualKeyShort.TAB);
        }

        /// <summary>
        /// Gửi phím Shift + Tab để di chuyển focus đến element trước đó.
        /// </summary>
        public void SendShiftTabKey()
        {
            Keyboard.Pressing(VirtualKeyShort.SHIFT);
            Keyboard.Press(VirtualKeyShort.TAB);
            Keyboard.Release(VirtualKeyShort.TAB);
            Keyboard.Release(VirtualKeyShort.SHIFT);
        }

        #endregion

        #region Public Methods - Event Listening

        /// <summary>
        /// Bắt đầu lắng nghe các sự kiện focus thay đổi từ Narrator.
        /// </summary>
        public void StartListening()
        {
            // Double check an toàn
            if (_automation == null) throw new InvalidOperationException("Automation object is null.");

            lock (_lockObject) { Events.Clear(); }
            
            if (_focusHandler != null) StopListening();
            
            _focusHandler = _automation.RegisterFocusChangedEvent(OnFocusChanged);
        }

        /// <summary>
        /// Dừng lắng nghe các sự kiện focus thay đổi và giải phóng tài nguyên.
        /// </summary>
        public void StopListening()
        {
            if (_focusHandler == null) return;
            try 
            { 
                _automation.UnregisterFocusChangedEvent(_focusHandler); 
            }
            catch { /* Ignore dispose errors */ }
            finally 
            { 
                _focusHandler = null; 
            }
        }

        private void OnFocusChanged(AutomationElement element)
        {
            if (element == null) return;
            try
            {
                // Snapshot dữ liệu ngay lập tức
                var evt = new NarratorEvent
                {
                    Name = element.Name ?? string.Empty,
                    ControlType = element.ControlType.ToString(),
                    State = GetElementState(element),
                    Timestamp = DateTime.Now
                };
                lock (_lockObject) { Events.Add(evt); }
            }
            catch { }
        }

        /// <summary>
        /// Chờ và tìm sự kiện Narrator phù hợp với điều kiện predicate trong khoảng thời gian timeout.
        /// </summary>
        /// <param name="predicate">Hàm điều kiện để tìm sự kiện phù hợp.</param>
        /// <param name="timeoutMs">Thời gian chờ tối đa tính bằng milliseconds.</param>
        /// <returns>Sự kiện Narrator phù hợp hoặc null nếu không tìm thấy trong thời gian timeout.</returns>
        public NarratorEvent? WaitForEvent(Func<NarratorEvent, bool> predicate, int timeoutMs = 2000)
        {
            var startTime = DateTime.Now;
            while ((DateTime.Now - startTime).TotalMilliseconds < timeoutMs)
            {
                lock (_lockObject)
                {
                    var match = Events.AsEnumerable().Reverse().FirstOrDefault(predicate);
                    if (match != null) return match;
                }
                Thread.Sleep(50);
            }
            return null;
        }

        #endregion

        #region Verification Logic (Verify Methods)

        /// <summary>
        /// Kiểm tra Narrator có đọc đúng văn bản mong đợi hay không.
        /// </summary>
        /// <param name="expectedText">Văn bản mong đợi Narrator sẽ đọc.</param>
        /// <param name="waitTimeMs">Thời gian chờ tối đa để Narrator đọc văn bản (ms).</param>
        /// <returns>Kết quả kiểm tra bao gồm trạng thái thành công và thông báo chi tiết.</returns>
        public (bool Success, string Message) VerifyNarratorReading(string expectedText, int waitTimeMs = 2000)
        {
            if (string.IsNullOrWhiteSpace(expectedText)) return (false, "expectedText is empty");

            try
            {
                StartListening();
                SendTabKey();

                // So sánh linh hoạt: Equals hoặc Contains
                var matchedEvent = WaitForEvent(e =>
                    string.Equals(e.Name, expectedText, StringComparison.OrdinalIgnoreCase) ||
                    e.Name.IndexOf(expectedText, StringComparison.OrdinalIgnoreCase) >= 0,
                    waitTimeMs);

                StopListening();

                if (matchedEvent == null)
                {
                    string actualText = GetLastName();
                    return string.IsNullOrEmpty(actualText)
                        ? (false, $"Timeout {waitTimeMs}ms. Narrator did not announce anything.")
                        : (false, $"Mismatch. Expected: '{expectedText}', Actual: '{actualText}'");
                }

                return (true, $"Pass: Narrator announced '{expectedText}'");
            }
            catch (Exception ex)
            {
                return (false, $"Error verifying: {ex.Message}");
            }
        }

        /// <summary>
        /// Kiểm tra chi tiết Narrator bao gồm tên element, loại control và trạng thái.
        /// </summary>
        /// <param name="expectedName">Tên element mong đợi.</param>
        /// <param name="expectedControlType">Loại control mong đợi.</param>
        /// <param name="expectedState">Trạng thái mong đợi.</param>
        /// <param name="waitTimeMs">Thời gian chờ tối đa (ms).</param>
        /// <returns>Kết quả kiểm tra bao gồm trạng thái thành công và thông báo chi tiết.</returns>
        public (bool Success, string Message) VerifyDetailedNarrator(string expectedName, string expectedControlType, string expectedState, int waitTimeMs = 2000)
        {
            try
            {
                StartListening();
                SendTabKey();

                var matchedEvent = WaitForEvent(e =>
                    string.Equals(e.Name, expectedName, StringComparison.OrdinalIgnoreCase),
                    waitTimeMs);

                StopListening();

                if (matchedEvent == null) return (false, $"Timeout. Element '{expectedName}' not found.");

                if (!string.Equals(matchedEvent.ControlType, expectedControlType, StringComparison.OrdinalIgnoreCase))
                    return (false, $"Wrong ControlType. Expected: {expectedControlType}, Actual: {matchedEvent.ControlType}");

                if (!string.IsNullOrEmpty(expectedState) && matchedEvent.State.IndexOf(expectedState, StringComparison.OrdinalIgnoreCase) < 0)
                    return (false, $"Wrong State. Expected to contain: {expectedState}, Actual: {matchedEvent.State}");

                return (true, "Pass Verify Detailed.");
            }
            catch (Exception ex) { return (false, $"Error: {ex.Message}"); }
        }

        #endregion

        #region Speech Recap & Helpers

        /// <summary>
        /// Mở cửa sổ Speech recap bằng tổ hợp phím Insert + Alt + X với cơ chế retry.
        /// </summary>
        public void OpenSpeechRecapWindow()
        {
            // Nếu đã mở rồi thì thôi
            if (IsSpeechRecapWindowExists()) return;

            for (int i = 0; i < MAX_RETRIES; i++)
            {
                try
                {
                    // Dùng INSERT thay vì CAPSLOCK để ổn định hơn
                    Keyboard.Pressing(VirtualKeyShort.INSERT);
                    Keyboard.Pressing(VirtualKeyShort.ALT);
                    Keyboard.Press(VirtualKeyShort.KEY_X);
                    Keyboard.Release(VirtualKeyShort.ALT);
                    Keyboard.Release(VirtualKeyShort.INSERT);

                    Thread.Sleep(1000); 

                    if (IsSpeechRecapWindowExists()) return;
                }
                catch (Exception ex) { Debug.WriteLine($"Retry {i} failed: {ex.Message}"); }
            }
        }

        /// <summary>
        /// Kiểm tra cửa sổ Speech recap (Win 11) có tồn tại không.
        /// </summary>
        /// <returns>True nếu cửa sổ tồn tại, ngược lại False.</returns>
        public bool IsSpeechRecapWindowExists()
        {
            return FindSpeechRecapWindow() != null;
        }

        /// <summary>
        /// Lấy dòng log cuối cùng từ Speech Recap.
        /// </summary>
        /// <returns>Dòng log cuối cùng hoặc chuỗi rỗng nếu không thể lấy được.</returns>
        public string GetLastSpeechLog()
        {
            var window = FindSpeechRecapWindow();
            if (window == null) return string.Empty;

            var textBox = FindSpeechTextBox(window);
            if (textBox == null) return string.Empty;

            string fullText = GetTextFromTextBox(textBox);
            var lines = fullText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            return lines.Length > 0 ? lines.Last().Trim() : string.Empty;
        }

        // --- Private Helpers ---

        private AutomationElement? FindSpeechRecapWindow()
        {
            try
            {
                // Tìm theo ClassName đặc thù của Win 11
                return _automation.GetDesktop().FindFirstDescendant(cf => cf.ByClassName(RECAP_WINDOW_CLASS));
            }
            catch { return null; }
        }

        private AutomationElement? FindSpeechTextBox(AutomationElement window)
        {
            return window?.FindFirstDescendant(cf => cf.ByAutomationId(RECAP_TEXTBOX_ID));
        }

        private string GetTextFromTextBox(AutomationElement textBox)
        {
            if (textBox.Patterns.Text.TryGetPattern(out var textPattern))
                return textPattern.DocumentRange.GetText(-1)?.Trim() ?? string.Empty;
            
            if (textBox.Patterns.Value.TryGetPattern(out var valuePattern))
                return valuePattern.Value.ValueOrDefault?.Trim() ?? string.Empty;

            return textBox.Name?.Trim() ?? string.Empty;
        }

        /// <summary>
        /// Lấy trạng thái element CHUẨN (Loại bỏ nhiễu).
        /// </summary>
        private string GetElementState(AutomationElement element)
        {
            var states = new List<string>();
            try
            {
                if (!element.IsEnabled) states.Add("Disabled");

                if (element.Patterns.Toggle.TryGetPattern(out var toggle))
                    states.Add(toggle.ToggleState.Value.ToString());

                if (element.Patterns.SelectionItem.TryGetPattern(out var select) && select.IsSelected.Value)
                    states.Add("Selected");

                if (element.Patterns.ExpandCollapse.TryGetPattern(out var expand) && 
                    expand.ExpandCollapseState.Value != ExpandCollapseState.LeafNode)
                    states.Add(expand.ExpandCollapseState.Value.ToString());

                // Legacy Pattern (dùng STATE_SYSTEM_CHECKED chuẩn)
                if (element.Patterns.LegacyIAccessible.TryGetPattern(out var legacy))
                {
                    var state = legacy.State.Value;
                    if ((state & AccessibilityState.STATE_SYSTEM_CHECKED) == AccessibilityState.STATE_SYSTEM_CHECKED)
                    {
                        if (!states.Contains("On") && !states.Contains("Checked")) states.Add("Checked");
                    }
                    if ((state & AccessibilityState.STATE_SYSTEM_PRESSED) == AccessibilityState.STATE_SYSTEM_PRESSED)
                        states.Add("Pressed");
                }
            }
            catch { }
            return string.Join(", ", states);
        }

        /// <summary>
        /// Lấy tên của sự kiện focus cuối cùng được ghi nhận.
        /// </summary>
        /// <returns>Tên element hoặc chuỗi rỗng nếu không có sự kiện.</returns>
        public string GetLastName()
        {
            lock (_lockObject) { return Events.LastOrDefault()?.Name ?? string.Empty; }
        }

        /// <summary>
        /// Lấy loại control (ControlType) của sự kiện focus cuối cùng.
        /// </summary>
        /// <returns>Loại control hoặc chuỗi rỗng nếu không có sự kiện.</returns>
        public string GetLastControlType()
        {
            lock (_lockObject) { return Events.LastOrDefault()?.ControlType ?? string.Empty; }
        }

        /// <summary>
        /// Lấy trạng thái (State) của sự kiện focus cuối cùng.
        /// </summary>
        /// <returns>Trạng thái hoặc chuỗi rỗng nếu không có sự kiện.</returns>
        public string GetLastState()
        {
            lock (_lockObject) { return Events.LastOrDefault()?.State ?? string.Empty; }
        }

        /// <summary>
        /// Thực hiện Tab và lắng nghe sự kiện trong khoảng thời gian nhất định.
        /// </summary>
        /// <param name="waitTimeInMs">Thời gian chờ lắng nghe (ms).</param>
        public void TabAndListen(int waitTimeInMs = 2000)
        {
            try
            {
                StartListening();
                SendTabKey();
                Thread.Sleep(waitTimeInMs);
                StopListening();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in TabAndListen: {ex.Message}");
            }
        }

        /// <summary>
        /// Bật/tắt Narrator bằng cách sử dụng phím tắt Ctrl + Win + Enter.
        /// </summary>
        public void ToggleNarrator()
        {
            try
            {
                Keyboard.Pressing(VirtualKeyShort.CONTROL);
                Keyboard.Pressing(VirtualKeyShort.LWIN);
                Keyboard.Press(VirtualKeyShort.RETURN);
                Keyboard.Release(VirtualKeyShort.RETURN);
                Keyboard.Release(VirtualKeyShort.LWIN);
                Keyboard.Release(VirtualKeyShort.CONTROL);
                
                Thread.Sleep(1000); // Chờ Narrator bật/tắt
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error toggling Narrator: {ex.Message}");
            }
        }

        /// <summary>
        /// So sánh thuộc tính element với speech log từ Speech recap.
        /// </summary>
        /// <param name="elementName">Tên element cần so sánh.</param>
        /// <param name="elementControlType">Loại control của element.</param>
        /// <param name="elementState">Trạng thái của element.</param>
        /// <param name="speechLog">Speech log từ Speech recap.</param>
        /// <returns>True nếu tất cả thuộc tính khớp với speech log, ngược lại False.</returns>
        public bool CompareElementPropertiesWithSpeechLog(string elementName, string elementControlType, string elementState, string speechLog)
        {
            if (string.IsNullOrEmpty(speechLog)) return false;

            bool nameMatch = string.IsNullOrEmpty(elementName) ||  speechLog.IndexOf(elementName, StringComparison.OrdinalIgnoreCase) >= 0;
            
            bool controlTypeMatch = string.IsNullOrEmpty(elementControlType) || speechLog.IndexOf(elementControlType, StringComparison.OrdinalIgnoreCase) >= 0;
            
            bool stateMatch = string.IsNullOrEmpty(elementState) || speechLog.IndexOf(elementState, StringComparison.OrdinalIgnoreCase) >= 0;

            return nameMatch && controlTypeMatch && stateMatch;
        }
        #endregion

        /// <summary>
        /// Giải phóng tài nguyên được sử dụng bởi NarratorVerifier.
        /// </summary>
        public void Dispose()
        {
            StopListening();
            _automation?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
