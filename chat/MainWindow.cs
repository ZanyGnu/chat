
namespace chat
{
    using CefSharp;
    using CefSharp.WinForms;
    using System;
    using System.IO;
    using System.Windows.Forms;

    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            var cefsettings = new CefSettings
            {
                CachePath = Path.Combine(Environment.CurrentDirectory, "BrowserCache"),
            };

            Cef.Initialize(cefsettings);
            
            var browser = new ChromiumWebBrowser("https://web.whatsapp.com/")
            {
                Dock = DockStyle.Fill,
            };

            browserPanel.Controls.Add(browser);

            
        }
    }
}
