
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

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                //notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }

            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = false;
                this.Show();
                this.Focus();
                this.BringToFront();
            }
        }
    }
}
