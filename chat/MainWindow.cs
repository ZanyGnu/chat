﻿
namespace chat
{
    using CefSharp;
    using CefSharp.WinForms;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
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

            Run(delegate
            {
                var task = browser.EvaluateScriptAsync(
                    "(function() { " +
                        "var body = document.body, html = document.documentElement; " +
                        "return document.title;" +
                    "})();");
                
                if (task == null)
                {
                    return;
                }

                task.ContinueWith(t =>
                    {
                        if (!t.IsFaulted)
                        {
                            var response = t.Result;
                            var EvaluateJavaScriptResult = response.Success ? (response.Result ?? "null") : response.Message;
                            Console.WriteLine("Script result = {0}", EvaluateJavaScriptResult);
                            this.Text = EvaluateJavaScriptResult.ToString();
                            if (this.Text.StartsWith("("))
                            {
                                int messageCount = int.Parse(this.Text.Substring(1, this.Text.IndexOf(")") - 1));
                                if (!this.Focused)
                                {
                                    FlashWindow.Flash(this);
                                }
                                if (FormWindowState.Minimized == this.WindowState)
                                {
                                    if (!notifyIcon1.Visible)
                                    {
                                        //notifyIcon1.ShowBalloonTip(20000, "WinWhatsApp", "Messages pending", ToolTipIcon.Info);
                                    }
                                }
                            }
                        }
                    },
                    TaskScheduler.FromCurrentSynchronizationContext());
            }, 
            TimeSpan.FromSeconds(1));
         
        }

        public static async Task Run(Action action, TimeSpan period, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(period, cancellationToken);
                action();
            }
        }

        public static Task Run(Action action, TimeSpan period)
        {
            return Run(action, period, CancellationToken.None);
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500, "WinWhatsapp", "Whatsapp is minimized to the tray", ToolTipIcon.Info);
                this.Hide();
            }

            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
                this.Show();
                this.BringToFront();
                this.Focus();
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = false;
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
        }
    }
}
