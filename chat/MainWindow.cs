
namespace chat
{
    using CefSharp;
    using CefSharp.WinForms;
    using System;
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
                CachePath = Settings.CurrentSettings.BrowserCacheLocation,
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
                        "window.parent.document.body.style.zoom = .90;" + 
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
                            if (!response.Success)
                            {
                                return;
                            }
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
                if (Settings.CurrentSettings.MimimizeToTray)
                {
                    notifyIcon1.Visible = true;
                    if (!Settings.CurrentSettings.NotificationShown)
                    {
                        notifyIcon1.ShowBalloonTip(500, "WinWhatsapp", "Whatsapp is minimized to the tray. Click here to stop showing this notification", ToolTipIcon.Info);
                        notifyIcon1.BalloonTipClicked += NotifyIcon1_BalloonTipClicked;
                    }

                    this.Hide();
                }
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
                this.Show();
                this.BringToFront();
                this.Focus();
            }
        }

        private void NotifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            Settings.CurrentSettings.NotificationShown = true;
            Settings.CurrentSettings.Save();
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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm();
            DialogResult result = form.ShowDialog();
            
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (Settings.CurrentSettings.EscapeToMinimizeToTray)
            {
                if (keyData == Keys.Escape)
                {
                    this.WindowState = FormWindowState.Minimized;
                    return false;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
