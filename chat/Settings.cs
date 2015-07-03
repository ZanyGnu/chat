﻿namespace chat
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.IO;
    using System.Windows.Forms.Design;
    using System.Xml.Serialization;

    public class Settings
    {
        private SettingsModel currentSettings;

        public SettingsModel CurrentSettings
        {
            get
            {
                return this.currentSettings;
            }
        }

        private static readonly String DefaultAppDataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WinWhatsApp");
        private static readonly String DefaultSettingsFileName = "WinWhatsApp.settings.xml";

        private string settingsFileName;

        public void Save()
        {
            TextWriter writer = new StreamWriter(this.settingsFileName);

            XmlSerializer serializer = new XmlSerializer(typeof(Settings));
            serializer.Serialize(writer, this.currentSettings);
            writer.Close();
        }

        public Settings():this(Path.Combine(DefaultAppDataFolder, DefaultSettingsFileName))
        {

        }

        public Settings(string settingsFileName)
        {
            this.settingsFileName = settingsFileName;
            this.currentSettings = this.Rehydrate();
        }

        private SettingsModel Rehydrate()
        {
            if (!File.Exists(this.settingsFileName))
            {
                string settingsDirectory = Path.GetDirectoryName(this.settingsFileName);
                if (!Directory.Exists(settingsDirectory))
                {
                    Directory.CreateDirectory(settingsDirectory);
                }

                SettingsModel defaultSettings = new SettingsModel();
                this.LoadDefaultValues(defaultSettings);
                this.Save();

                return defaultSettings;
            }

            FileStream fileReader = File.OpenRead(this.settingsFileName);

            XmlSerializer serializer = new XmlSerializer(typeof(SettingsModel));
            SettingsModel settings = (SettingsModel)serializer.Deserialize(fileReader);

            fileReader.Close();

            SetNewSettingsDefaultValues(settings);

            return settings;
        }

        private void SetNewSettingsDefaultValues(SettingsModel settings)
        {            

        }

        private void LoadDefaultValues(SettingsModel settings)
        {
            settings.BrowserCacheLocation = Path.Combine(DefaultAppDataFolder, "BrowserCache");
            settings.EscapeToMinimizeToTray = true;
            settings.MimimizeToTray = true;
            settings.NotificationShown = false; 
        }
    }
}
