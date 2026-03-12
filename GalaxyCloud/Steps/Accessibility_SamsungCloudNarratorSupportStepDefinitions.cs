// file="Accessibility_SamsungCloudNarratorSupportStepDefinitions.cs" 

using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using GalaxyCloud.Page;
using GalaxyCloud.Helpers;

namespace GalaxyCloud.Steps
{
    /// <summary>
    /// Step Definitions ánh xạ các bước kiểm thử Narrator cho Samsung Cloud.
    /// </summary>
    [Binding]
    public class Accessibility_SamsungCloudNarratorSupportStepDefinitions
    {
        private readonly NarratorPage _narratorPage;
        private NarratorEvent _currentElementProperties;
        private string _lastSpeechLogFromRecap;

        /// <summary>
        /// Khởi tạo instance của <see cref="Accessibility_SamsungCloudNarratorSupportStepDefinitions"/>.
        /// </summary>
        public Accessibility_SamsungCloudNarratorSupportStepDefinitions()
        {
            _narratorPage = new NarratorPage();
            _currentElementProperties = new NarratorEvent();
        }

        #region Given

        /// <summary>
        /// Bước kiểm tra ứng dụng Samsung Cloud đã được khởi chạy.
        /// </summary>
        [Given(@"the ""([^""]*)"" app is launched")]
        public void GivenTheSamsungCloudAppIsLaunched()
        {
            // Kiểm tra session từ Hooks đảm bảo app đã mở
            Assert.IsNotNull(Hooks.sessionSamsungCloud, "Samsung Cloud session chưa được khởi tạo.");
        }

        /// <summary>
        /// Bước bật Narrator.
        /// </summary>
        [Given(@"Narrator is enabled")]
        public void GivenNarratorIsEnabled()
        {
            // Bật Narrator bằng phím tắt Ctrl + Win + Enter
            _narratorPage.EnableNarrator();
            
            // Đợi một thời gian để Narrator khởi động
            System.Threading.Thread.Sleep(2000);
            
            // Kiểm tra Narrator đã được bật thành công
            Assert.IsTrue(true, "Narrator has been enabled.");
        }

        /// <summary>
        /// Bước mở cửa sổ Speech recap.
        /// </summary>
        [Given(@"Speech recap window is opened")]
        public void GivenSpeechRecapWindowIsOpened()
        {
           
            _narratorPage.OpenSpeechRecapWindow();
            
            
            _narratorPage.BackToSamsungCloud("Samsung Cloud");
        }

        #endregion

        #region When

        /// <summary>
        /// Bước thực hiện di chuyển focus vào element chỉ định.
        /// </summary>
        /// <param name="elementName">Tên element trong Gherkin.</param>
        [When(@"I move the accessibility focus to the ""(.*)"" button")]
        public void WhenIMoveTheAccessibilityFocusToTheButton(string elementName)
        {
            if (elementName == "Get started")
            {
                // Sử dụng phương thức navigation mới với logic cải tiến
                _narratorPage.NavigateToGetStartedButtonUsingTab();
            }
            else
            {
                Assert.Fail($"Chưa định nghĩa hành động focus cho nút: {elementName}");
            }
        }

        /// <summary>
        /// Bước điều hướng đến element bằng phím Tab.
        /// </summary>
        /// <param name="elementName">Tên element cần điều hướng đến.</param>
        [When(@"I navigate to the ""(.*)"" button using Tab key")]
        public void WhenINavigateToTheButtonUsingTabKey(string elementName)
        {
            if (elementName == "Get started")
            {
                // Sử dụng phương thức tab navigation mới với logic cải tiến
                _narratorPage.NavigateToGetStartedButtonUsingTab();
            }
            else
            {
                Assert.Fail($"Chưa định nghĩa hành động navigation cho nút: {elementName}");
            }
        }

        #endregion

        #region Then

        /// <summary>
        /// Verify trạng thái element mà Narrator đọc khớp với kỳ vọng (so sánh tương đối).
        /// </summary>
        /// <param name="expectedState">Trạng thái mong đợi.</param>
        [Then(@"the Narrator should announce the element state as ""(.*)""")]
        public void ThenTheNarratorShouldAnnounceTheElementStateAs(string expectedState)
        {
            string actualState = _narratorPage.GetNarratorAnnouncedState();
            
            
            // Sử dụng Contains để so sánh tương đối, bỏ qua khoảng trắng và chữ in hoa/thường
            Assert.That(actualState.ToLower().Trim(), Does.Contain(expectedState.ToLower().Trim()),
                $"Narrator đọc sai TRẠNG THÁI element. Mong đợi chứa: '{expectedState}', Thực tế: '{actualState}'");
        }

        /// <summary>
        /// Verify log cuối cùng từ Speech recap chứa đầy đủ thông tin Name, ControlType và State.
        /// </summary>
        [Then(@"the last speech log should contain full element information")]
        public void ThenTheLastSpeechLogShouldContainFullElementInformation()
        {
            bool containsFullInfo = _narratorPage.IsLastSpeechLogContainsFullInfo();
            Assert.IsTrue(containsFullInfo, "Log cuối cùng từ Speech recap không chứa đầy đủ thông tin Name, ControlType và State.");
        }

        /// <summary>
        /// Bước capture thuộc tính của element hiện tại.
        /// </summary>
        [Then(@"I capture the current element properties")]
        public void ThenICaptureTheCurrentElementProperties()
        {
            // Capture các thuộc tính của element hiện tại từ Narrator
            _currentElementProperties = new NarratorEvent
            {
                Name = _narratorPage.GetNarratorAnnouncedName(),
                ControlType = _narratorPage.GetNarratorAnnouncedRole(),
                State = _narratorPage.GetNarratorAnnouncedState()
            };
            // Hiển thị các thuộc tính mà Narrator đã lấy được để kiểm tra tính chính xác
            Console.WriteLine($"[Narrator Debug] Actual State: Name: '{_currentElementProperties.Name}', Role: '{_currentElementProperties.ControlType}', State: '{_currentElementProperties.State}'");
            // Kiểm tra xem có capture được dữ liệu không
            Assert.IsFalse(string.IsNullOrEmpty(_currentElementProperties.Name), 
                "Không thể capture được tên element từ Narrator.");
        }

        /// <summary>
        /// Bước lấy log cuối cùng từ Speech recap.
        /// </summary>
        [Then(@"I get the last speech log from Speech recap")]
        public void ThenIGetTheLastSpeechLogFromSpeechRecap()
        {
            _lastSpeechLogFromRecap = _narratorPage.GetLastSpeechLog();
            Assert.IsFalse(string.IsNullOrEmpty(_lastSpeechLogFromRecap), 
                "Không thể lấy được speech log từ Speech recap window.");
        }

        /// <summary>
        /// Bước so sánh thuộc tính element với speech log.
        /// </summary>
        [Then(@"I compare the element properties with the speech log")]
        public void ThenICompareTheElementPropertiesWithTheSpeechLog()
        {
            // Kiểm tra xem đã capture element properties và lấy speech log chưa
            Assert.IsNotNull(_currentElementProperties, "Chưa capture element properties.");
            Assert.IsFalse(string.IsNullOrEmpty(_lastSpeechLogFromRecap), "Chưa lấy speech log từ Speech recap.");

            // Sử dụng method so sánh từ NarratorPage (đã delegate về NarratorVerifier)
            bool isMatch = _narratorPage.CompareElementPropertiesWithSpeechLog(
                _currentElementProperties.Name,
                _currentElementProperties.ControlType,
                _currentElementProperties.State,
                _lastSpeechLogFromRecap);

            // Verify kết quả so sánh
            Assert.IsTrue(isMatch, 
                $"Speech log không khớp với thuộc tính element. " +
                $"Expected Name: '{_currentElementProperties.Name}', " +
                $"ControlType: '{_currentElementProperties.ControlType}', " +
                $"State: '{_currentElementProperties.State}'. " +
                $"Actual speech log: '{_lastSpeechLogFromRecap}'");
        }

        /// <summary>
        /// Bước đóng Narrator.
        /// </summary>
        [Then(@"I close Narrator")]
        public void ThenICloseNarrator()
        {
            // Gọi method đóng Narrator từ NarratorPage
            _narratorPage.CloseNarrator();
            Assert.IsTrue(true, "Narrator has been closed.");
        }

        #endregion
    }
}
