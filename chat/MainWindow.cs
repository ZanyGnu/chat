using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chat
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            var browser = new ChromiumWebBrowser("https://gmail.com/")
            {
                Dock = DockStyle.Fill
            };

            browserPanel.Controls.Add(browser);

            
        }
    }
}
