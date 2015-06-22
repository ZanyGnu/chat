namespace chat
{
    using System.Windows.Forms;
    
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            this.propertyGridSettings.SelectedObject = Settings.CurrentSettings;
        }
    }
}
