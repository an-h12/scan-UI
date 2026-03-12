//file="DataString.cs" 

namespace GalaxyCloud.Helpers
{
    /// <summary>
    /// This class are string that can be used on methods of the pages
    /// </summary>
    public class DataString : General
    {
#pragma warning disable SA1600 // Elements should be documented
        public static string connectToMicrosoftOneDriver = "Your Samsung account isn't linked to OneDrive. Link it to OneDrive on your phone or tablet.";

        #region PT-BR
        public static string dataListSubtitlePtBr = "Sincronizar";
        public static string subTextInfoPtBr = "Sincronizar com OneDrive";
        public static string isSyncedPtBr = "botão de alternância";
        public static string dataSubMenuLastSyncedPtBr = "Última sincronização:";
        public static string dataSubMenuLabelsPtBr = "Sincronizar agora";
        public static string toggleIsSyncPtBr = "botão de alternância";
        public static string galleryDataSyncInformationPtBr = "foto";
        public static string notesDataSyncInformationPtBr = "Vá para Samsung Notes para sincronizar manualmente.";
        public static string toastWithoutInternetConnectionPtBr = "Conecte-se a uma rede Wi-Fi ou Ethernet para sincronizar este aplicativo.";
        public static string expectedContactUsPopUpTitle = "A log file may help";
        public static string expectedContactUsPopUpDescription = "If you're contacting us to report an error, including the Windows log file may help us solve the issue for you.";
        public static string expectedContactUsPopupHyperlinkText = "Find log file in Windows Explorer";
        #endregion

        #region KO-KP
        public static string dataListSubtitleKoKp = "동기화";
        public static string subTextInfoKoKp = "OneDrive에 동기화";
        public static string isSyncedKoKp = "토글 스위치";
        public static string dataSubMenuLastSyncedKoKp = "마지막 동기화:";
        public static string dataSubMenuLabelsKoKp = "지금 동기화";
        public static string toggleIsSyncKoKp = "토글 스위치";
        public static string galleryDataSyncInformationKoKp = "개의 동기화된 비디오";
        public static string notesDataSyncInformationKoKp = "앱에서 수동으로 동기화하세요.";
        public static string settingsDataSyncInformationKoKp = "등록된 삼성 기기 0개";
        #endregion

        #region EN-US
        public static string toastWithoutInternetConnectionEnUs = "Connect to Wi-Fi or Ethernet to sync this app.";
        public static string labelSyncOccurringEnUs = "Syncing…";
        public static string labelStopSyncEnUs = "Tap here to stop syncing";
#pragma warning restore SA1600 // Elements should be documented
        #endregion
    }
}