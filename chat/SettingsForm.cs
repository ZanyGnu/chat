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

        private void buttonSave_Click(object sender, System.EventArgs e)
        {
            Settings.CurrentSettings.Save();
        }

        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
