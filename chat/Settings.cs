namespace chat
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.IO;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;
    using System.Xml.Serialization;

    public class Settings
    {
        private static readonly object LockObj = new object();

        private static int SettingsVersion0 = 0;
        private static int CurrentSettingsVersion = SettingsVersion0;

        private volatile static Settings currentSettings;

        public static Settings CurrentSettings
        {
            get
            {
                if (currentSettings == null)
                {
                    lock (LockObj)
                    {
                        if (currentSettings == null)
                        {
                            currentSettings = Settings.Rehydrate();
                        }
                    }
                }

                return currentSettings;
            }
        }

        protected static readonly String AppDataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WinWhatsApp");

        protected static readonly string SettingsFileName = Path.Combine(AppDataFolder, "WinWhatsApp.settings.xml");


        [Browsable(false)]
        public int SettingsVersion { get; set; }

        [Browsable(false)]
        public bool NotificationShown { get; set; }

        public bool EscapeToMinimizeToTray { get; set; }

        public bool MimimizeToTray { get; set; }

        [EditorAttribute(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string BrowserCacheLocation { get; set; }

        public void Save()
        {
            TextWriter writer = new StreamWriter(SettingsFileName);

            XmlSerializer serializer = new XmlSerializer(typeof(Settings));
            serializer.Serialize(writer, this);
            writer.Close();
        }

        public static Settings Rehydrate()
        {
            if (!File.Exists(SettingsFileName))
            {
                if (!Directory.Exists(AppDataFolder))
                {
                    Directory.CreateDirectory(AppDataFolder);
                }

                Settings defaultSettings = new Settings();
                defaultSettings.LoadDefaultValues();
                defaultSettings.Save();

                return defaultSettings;
            }

            FileStream fileReader = File.OpenRead(SettingsFileName);

            XmlSerializer serializer = new XmlSerializer(typeof(Settings));
            Settings settings = (Settings)serializer.Deserialize(fileReader);

            fileReader.Close();

            SetNewSettingsDefaultValues(settings);

            return settings;
        }

        private static void SetNewSettingsDefaultValues(Settings settings)
        {
            
            settings.Save();
        }

        private void LoadDefaultValues()
        {
            this.BrowserCacheLocation = Path.Combine(AppDataFolder, "BrowserCache");
            this.EscapeToMinimizeToTray = true;
            this.MimimizeToTray = true;
            this.NotificationShown = false;
        }
    }
}
