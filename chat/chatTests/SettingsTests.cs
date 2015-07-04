using Microsoft.VisualStudio.TestTools.UnitTesting;
using chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace chat.Tests
{
    [TestClass()]
    public class SettingsTests
    {
        [TestMethod()]
        public void BasicTest()
        {
            Settings settings = new Settings();
            // check construction
            Assert.IsTrue(settings.CurrentSettings != null);
            // check default values
            Assert.IsTrue(settings.CurrentSettings.MimimizeToTray == true);
        }

        [TestMethod()]
        public void NewSettingsCreationAndSaveTest()
        {
            string settingsFileName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Settings settings = new Settings(settingsFileName);
            // check default values
            Assert.IsTrue(settings.CurrentSettings.MimimizeToTray == true);

            // change value, save and test that its persisted
            settings.CurrentSettings.MimimizeToTray = false;
            settings.Save();

            Settings settingsRefreshed = new Settings(settingsFileName);
            Assert.IsFalse(settingsRefreshed.CurrentSettings.MimimizeToTray);

        }

        [TestMethod()]
        public void SettingsSaveTest()
        {
            // check when directory does not already exist
            string settingsFileName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName(), Path.GetRandomFileName());
            Settings settings = new Settings(settingsFileName);
            // check default values
            Assert.IsTrue(settings.CurrentSettings.MimimizeToTray == true);

            // change value, save and test that its persisted
            settings.CurrentSettings.MimimizeToTray = false;
            settings.Save();

            Settings settingsRefreshed = new Settings(settingsFileName);
            Assert.IsFalse(settingsRefreshed.CurrentSettings.MimimizeToTray);

        }
    }
}