using Microsoft.VisualStudio.TestTools.UnitTesting;
using chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}