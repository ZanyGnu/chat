namespace chat
{
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Windows.Forms.Design;

    public class SettingsModel
    {
        private static int SettingsVersion0 = 0;
        private static int CurrentSettingsVersion = SettingsVersion0;
        
        [Browsable(false)]
        public int SettingsVersion { get; set; }

        [Browsable(false)]
        public bool NotificationShown { get; set; }

        public bool EscapeToMinimizeToTray { get; set; }

        public bool MimimizeToTray { get; set; }

        [EditorAttribute(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string BrowserCacheLocation { get; set; }
    }
}
