namespace chat
{
    using System.Windows.Forms;
    
    public partial class SettingsForm : Form
    {
        private Settings Settings;

        public SettingsForm(Settings settings)
        {
            this.Settings = settings;
            InitializeComponent();
            this.propertyGridSettings.SelectedObject = Settings.CurrentSettings;
        }

        private void buttonSave_Click(object sender, System.EventArgs e)
        {
            Settings.Save();
            this.Close();
        }

        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
