// file="NarratorPage.cs" 

using GalaxyCloud.Helpers;
using System;
using System.Diagnostics;
using System.Threading;

namespace GalaxyCloud.Page
{
    /// <summary>
    /// Page Object cho màn hình Samsung Cloud Welcome.
    /// Kế thừa từ class General và tích hợp NarratorVerifier.
    /// </summary>
public class NarratorPage : General
    {
        /// <summary>
        /// Mở cửa sổ Speech recap bằng tổ hợp phím Insert + Alt + X.
        /// </summary>
        public void OpenSpeechRecapWindow()
        {
            _narratorVerifier.OpenSpeechRecapWindow();
        }

        /// <summary>
        /// Kiểm tra xem cửa sổ Speech recap có tồn tại không.
        /// </summary>
        /// <returns>True nếu cửa sổ Speech recap tồn tại, ngược lại False.</returns>
        public bool IsSpeechRecapWindowExists()
        {
            return _narratorVerifier.IsSpeechRecapWindowExists();
        }

        /// <summary>
        /// Đối tượng hỗ trợ lắng nghe sự kiện Narrator.
        /// </summary>
        private readonly NarratorVerifier _narratorVerifier;

        /// <summary>
        /// Khởi tạo instance của <see cref="NarratorPage"/>.
        /// </summary>
        public NarratorPage()
        {
            // Khởi tạo Helper lắng nghe Narrator
            _narratorVerifier = new NarratorVerifier();
        }

        /// <summary>
        /// Thực hiện quy trình: Tab đi chỗ khác -> Tab quay lại nút Get Started để bắt log.
        /// </summary>
        public void MoveFocusToGetStartedButton()
        {
            try
            {
                _narratorVerifier.SendTabKey();

                Thread.Sleep(1000);

                _narratorVerifier.TabAndListen(waitTimeInMs: 2000);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during tab navigation: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy tên element (Name) mà Narrator vừa đọc được từ sự kiện focus.
        /// </summary>
        /// <returns>Tên element.</returns>
        public string GetNarratorAnnouncedName()
        {
            return _narratorVerifier.GetLastName();
        }

        /// <summary>
        /// Lấy loại control (ControlType) mà Narrator vừa đọc được.
        /// </summary>
        /// <returns>Loại control (VD: Button, Text...).</returns>
        public string GetNarratorAnnouncedRole()
        {
            return _narratorVerifier.GetLastControlType();
        }

        /// <summary>
        /// Lấy trạng thái element (State) mà Narrator vừa đọc được từ sự kiện focus.
        /// </summary>
        /// <returns>Trạng thái element.</returns>
        public string GetNarratorAnnouncedState()
        {
            return _narratorVerifier.GetLastState();
        }

        /// <summary>
        /// Kiểm tra xem dòng log cuối cùng từ Speech recap có chứa đầy đủ thông tin Name, ControlType và State không.
        /// </summary>
        /// <returns>True nếu log chứa đầy đủ thông tin, ngược lại False.</returns>
        public bool IsLastSpeechLogContainsFullInfo()
        {
            string lastSpeechLog = _narratorVerifier.GetLastSpeechLog();
            string lastName = _narratorVerifier.GetLastName();
            string lastControlType = _narratorVerifier.GetLastControlType();
            string lastState = _narratorVerifier.GetLastState();

            // Kiểm tra log có chứa cả Name, ControlType và State
            return !string.IsNullOrEmpty(lastSpeechLog) &&
                   lastSpeechLog.IndexOf(lastName, StringComparison.OrdinalIgnoreCase) >= 0 &&
                   lastSpeechLog.IndexOf(lastControlType, StringComparison.OrdinalIgnoreCase) >= 0 &&
                   lastSpeechLog.IndexOf(lastState, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// Lấy dòng log cuối cùng từ Speech recap.
        /// </summary>
        /// <returns>Dòng log cuối cùng hoặc chuỗi rỗng nếu không tìm thấy.</returns>
        public string GetLastSpeechLog()
        {
            return _narratorVerifier.GetLastSpeechLog();
        }

        /// <summary>
        /// Quay trở lại cửa sổ ứng dụng Samsung Cloud (Active Window).
        /// Dùng sau khi đã mở các cửa sổ phụ như Speech Recap.
        /// </summary>
        /// <param name="windowName">Tên cửa sổ (Mặc định: Samsung Cloud).</param>
        public void BackToSamsungCloud(string windowName = "Samsung Cloud")
        {
            // Gọi xuống hàm ActivateWindow của Verifier để thực hiện tìm và focus
            _narratorVerifier.ActivateWindow(windowName);
            
            // Chờ ngắn để window thực sự nổi lên trước khi thực hiện các bước tiếp theo
            Thread.Sleep(500);
        }

        /// <summary>
        /// Điều hướng đến nút Get Started bằng phím Tab.
        /// </summary>
        public void NavigateToGetStartedButtonUsingTab()
        {
            try
            {
                // Mở cửa sổ Speech recap
                OpenSpeechRecapWindow();
                Thread.Sleep(2000);
                
                // QUAY LẠI APP (Để chuẩn bị nhấn Tab)
                BackToSamsungCloud("Samsung Cloud");
                Thread.Sleep(1000);
                                                
                // Reset focus bằng cách shift+tab trước
                _narratorVerifier.SendShiftTabKey();
                Thread.Sleep(500);
                                                
                // Bắt đầu lắng nghe sự kiện
                _narratorVerifier.StartListening();
                                                
                // Nhấn Tab để di chuyển đến nút Get Started
                _narratorVerifier.SendTabKey();
                                                
                // Chờ một thời gian để Narrator đọc xong
                Thread.Sleep(2000);
                                                
                // Dừng lắng nghe
                _narratorVerifier.StopListening();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during tab navigation to Get Started button: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Đóng Narrator bằng cách tắt process.
        /// </summary>
        public void CloseNarrator()
        {
            try
            {
                // Tìm và tắt process Narrator
                var narratorProcesses = Process.GetProcessesByName("Narrator");
                foreach (var process in narratorProcesses)
                {
                    process.Kill();
                    process.WaitForExit(5000); // Chờ tối đa 5 giây để process kết thúc
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error closing Narrator: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Bật Narrator bằng cách sử dụng phím tắt Ctrl + Win + Enter.
        /// </summary>
        public void EnableNarrator()
        {
            _narratorVerifier.ToggleNarrator();
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
            return _narratorVerifier.CompareElementPropertiesWithSpeechLog(elementName, elementControlType, elementState, speechLog);
        }

        /// <summary>
        /// Kiểm tra Narrator có đọc đúng thông tin của element đang focus không.
        /// </summary>
        /// <param name="expectedText">Text mong đợi Narrator đọc.</param>
        /// <param name="timeoutMs">Thời gian chờ tối đa (ms).</param>
        /// <returns>True nếu Narrator đọc đúng, ngược lại False.</returns>
        public (bool Success, string Message) VerifyNarratorReading(string expectedText, int timeoutMs = 5000)
        {
            return _narratorVerifier.VerifyNarratorReading(expectedText, timeoutMs);
        }

        /// <summary>
        /// Kiểm tra chi tiết Narrator có đọc đầy đủ Name, Role, State của element đang focus không.
        /// </summary>
        /// <param name="expectedName">Tên element mong đợi.</param>
        /// <param name="expectedRole">Role của element mong đợi.</param>
        /// <param name="expectedState">Trạng thái element mong đợi.</param>
        /// <param name="timeoutMs">Thời gian chờ tối đa (ms).</param>
        /// <returns>True nếu Narrator đọc đầy đủ và đúng, ngược lại False.</returns>
        public (bool Success, string Message) VerifyDetailedNarrator(string expectedName, string expectedRole, string expectedState, int timeoutMs = 5000)
        {
            return _narratorVerifier.VerifyDetailedNarrator(expectedName, expectedRole, expectedState, timeoutMs);
        }
    }
}
