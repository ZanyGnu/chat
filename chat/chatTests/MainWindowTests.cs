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
    public class MainWindowTests
    {
        [TestMethod()]
        public void MainWindowTest()
        {
            MainWindow mainWindow = new MainWindow();
            Assert.IsTrue(mainWindow != null);
        }
    }
}